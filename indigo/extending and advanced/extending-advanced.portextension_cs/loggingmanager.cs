//
//  LoggingManager.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.Logging
{
    using System;
    using System.MessageBus;
    using System.MessageBus.Transports;
    using System.Collections;
    using System.IO;
    using System.Globalization;

    /// <summary>
    /// This enumeration determines which messages we log.
    /// </summary>
    public enum LoggingMode 
    {
        Send = 0x1,                     // send pipeline
        Receive = 0x2,                  // receive pipeline
        SendReceive = Send | Receive,   // both send and receive pipelines
    }

    /// <summary>
    /// LoggingManager is used to be able to log messages that are sent to and received from a Port.
    /// 
    /// For outgoing messages, it hooks in before the Transmit stage and uses the Port's formatters 
    /// to serialize the message to a file.
    /// 
    /// For incoming messages it uses a custom formatter to catch the incoming stream and pipe a
    /// copy of the contents to a file.
    /// 
    /// So to add logging functionality to your Port you would do the following:
    /// LoggingManager loggingManager = new LoggingManager(port);
    /// 
    /// This actually hooks into the Port using a custom PortExtension that delegates
    /// the bulk of its work to the LoggingManager.
    /// </summary>
    public class LoggingManager
    {
        string logFormat;
        LoggingMode loggingMode;
        MessageFormatterCollection originalFormatters = new MessageFormatterCollection();
        Port port;
        LoggingPortExtension loggingPortExtension;

        /// <summary>
        /// Constructor used by the configuration system, internal only
        /// </summary>
        internal LoggingManager() : this(null) 
        {
        }

        /// <summary>
        /// Most commonly used ctor - defaults values for loggingMode and logFormat
        /// </summary>
        /// <param name="port"></param>
        public LoggingManager(Port port) : this(port, LoggingMode.SendReceive)
        {
        }

        public LoggingManager(Port port, LoggingMode loggingMode) : this(port, loggingMode, ".\\message{0}.xml")
        {
        }

        public LoggingManager(Port port, LoggingMode loggingMode, string logFormat)
        {
            if (logFormat == null)
                throw new ArgumentNullException("logFormat");

            this.loggingPortExtension = new LoggingPortExtension(this);
            this.Port = port;
            this.logFormat = logFormat;
            this.loggingMode = loggingMode;
        }

        bool LogSendPipeline
        {
            get
            {
                return ((this.loggingMode & LoggingMode.Send) != 0);
            }
        }

        bool LogReceivePipeline
        {
            get 
            {
                return ((this.loggingMode & LoggingMode.Receive) != 0);
            }
        }

        /// <summary>
        /// Port that this LoggingManager is installed on.  
        /// </summary>
        public Port Port 
        { 
            get 
            { 
                return this.port; 
            } 
            set 
            {
                if (this.port != null) 
                {
                    // remove our extension from the pipeline
                    this.port.Extensions.Remove(loggingPortExtension);
                }
                
                this.port = value;

                if (this.port != null) 
                {
                    // add in extension into the pipeline
                    this.port.Extensions.Add(loggingPortExtension);
                }
            } 
        }

        /// <summary>
        /// Called from LoggingPortExtension.OnOpening() during the Port.Open() process.
        /// We take a snapshot of the formatters on the Port that we're going to use for logging.
        /// This is called before the Port has been opened.
        /// </summary>
        void OnOpening()
        {
            if (port.Formatters.Count == 0)
                throw new InvalidOperationException("LoggingManager only works on Ports with Formatters.");

            // go through our list of formatters
            for (int i = 0; i < port.Formatters.Count; i++) 
            {
                // store off the formatters in originalFormatters
                originalFormatters.Add(port.Formatters[i]);

                // and replace them with a wrapped formatter if we need
                // to log on the receive pipeline
                if (LogReceivePipeline) 
                    port.Formatters[i] = new LoggingFormatter(port.Formatters[i], logFormat);
            }
        }

        /// <summary>
        /// Called from LoggingPortExtension.OnClosed() during the Port.Close() process.
        /// We clear out our cached formatters. This is called after the Port has been closed.
        /// </summary>
        void OnClosed()
        {
            // restore the original list of formatters if necesary
            if (LogReceivePipeline) 
            {
                for (int i = 0; i < port.Formatters.Count; i++) 
                {
                    port.Formatters[i] = originalFormatters[i];
                }
            }

            originalFormatters.Clear();
        }

        /// <summary>
        /// Uses the Port's formatters to write a copy of the message to a file.
        /// </summary>
        /// <param name="message">The message to be processed</param>
        /// <returns>whether we handled the message or not</returns>
        bool ProcessMessage(Message message)
        {
            // only serialize to a log file if our LoggingMode says we should be logging on the send pipeline
            if (LogSendPipeline) 
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                if (message.Encoding == null)
                    throw new ArgumentException("message");

                MessageFormatter formatter = originalFormatters[message.Encoding.ContentType];

                if (formatter == null)
                    throw new InvalidOperationException("No formatter exists in the Port for content type" + message.Encoding.ContentType + ".");

                // create a MessageWriter
                MessageWriter writer = formatter.CreateWriter();

                // ..and a file to write to
                FileStream logFile = LoggingStream.CreateFile(logFormat);

                // ..then serialize the message
                writer.WriteMessage(logFile, message);
                logFile.Close();

                // add a MessageProperty with the logFile in case anyone want to capture
                // this information
                message.Headers.Add(new LoggingProperty(logFile.Name));
            }

            // we didn't consume the message, let it on to the next handler in the pipeline
            return true;
        }

        /// <summary>
        /// This extension will insert itself before the Transmit stage on send.
        /// It uses the port's formatters to write out the contents of the message
        /// to a file.
        /// </summary>
        class LoggingPortExtension : PortExtension
        {
            // store the stages as statics to save allocations            
            static Stage aliasedStage;
            static Stage loggingStage;

            LoggingManagerHandler loggingManagerHandler;
            LoggingManager loggingManager;

            // StageAlias map used for send pipeline.
            StageAlias [] sendAliases;
            IMessageHandler [] sendHandlers;

            // Our array of stages that are used by this port extension. The array
            // contains just a single Stage entry (our logging stage).
            Stage [] stageArray;

            static LoggingPortExtension()
            {
                //  Create the two stages we use. The stage names are arbitrary
                //  so create a GUID and assign uuid: URLs 
                string urlBase = string.Concat("uuid:", Guid.NewGuid().ToString(), ";id=");
                
                int nextId = 0;
                
                loggingStage = new Stage(new Uri(urlBase + nextId.ToString(CultureInfo.InvariantCulture)));

                nextId++;
                aliasedStage = new Stage(new Uri(urlBase + nextId.ToString(CultureInfo.InvariantCulture)));
            }

            internal LoggingPortExtension(LoggingManager loggingManager)
            {
                this.loggingManager = loggingManager;
                this.loggingManagerHandler = new LoggingManagerHandler(loggingManager);

                //  This array is the set of stages we intend to use for our handlers. We
                //  have a single handler so we have a single stage in the array.
                this.stageArray = new Stage[] {loggingStage};

                //  This array defines the order of our new stages in the pipeline. The
                //  loggingStage is first and the aliased stage ("transmit") is second.
                Stage[] aliasedStagesArray = new Stage[] {loggingStage, aliasedStage};

                // set up the stage alias that replaces Transmit with the combo (loggingState, transmit) stage
                this.sendAliases = new StageAlias[] {new StageAlias (PortSendStages.Transmit, aliasedStage, aliasedStagesArray)}; 
               
                // and setup our handler
                this.sendHandlers = new IMessageHandler[] { loggingManagerHandler };
            }

            public override IMessageHandler[] CreateSendHandlers()
            {
                return sendHandlers;
            }

            public override StageAlias[] GetSendAliases()
            {
                return sendAliases;
            }

            public override Stage[] GetSendStages()
            {
                return stageArray;
            }
        
            public override void OnOpening()
            {
                loggingManager.OnOpening();
            }

            public override void OnClosed()
            {
                loggingManager.OnClosed();
            }

            /// <summary>
            /// simple handler that delegates message processing to the LoggingManager
            /// </summary>
            class LoggingManagerHandler : SyncMessageHandler
            {
                LoggingManager loggingManager;

                public LoggingManagerHandler(LoggingManager loggingManager) : base()
                {
                    this.loggingManager = loggingManager;
                }

                public override bool ProcessMessage(Message message)
                {
                    return loggingManager.ProcessMessage(message);
                }
            }
        }

    }
}
