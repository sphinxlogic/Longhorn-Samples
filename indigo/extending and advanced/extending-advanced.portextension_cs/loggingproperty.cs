//
//  LoggingProperty.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.Logging
{
    using System;
    using System.MessageBus;

    /// <summary>
    /// LoggingProperty is a simple MessageProperty that stores the name of the 
    /// log file in case anyone looking at the message wants to correlate them.
    /// </summary>
    public class LoggingProperty : MessageProperty
    {
        string logFile;

        public LoggingProperty(string logFile) : base()
        {
            this.logFile = logFile;
        }

        protected LoggingProperty(LoggingProperty source) : base(source)
        {
            this.logFile = source.logFile;
        }

        public string LogFile 
        {
            get { return this.logFile; }
        }
 
        public override MessageHeader Clone()
        {
            return new LoggingProperty(this);
        }
    }
}
