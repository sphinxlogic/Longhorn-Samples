//
//  LoggingFormatter.cs
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

    /// <summary>
    /// The LoggingFormatter is how we plug into the Port to log incoming messages from
    /// a wire format perspective.  For outgoing messages, we thunk to the formatter 
    /// that we're wrapping
    /// </summary>
    class LoggingFormatter : MessageFormatter
    {
        string logFormat;
        MessageFormatter wrappedFormatter;

        public LoggingFormatter(MessageFormatter wrappedFormatter, string logFormat) : base()
        {
            this.wrappedFormatter = wrappedFormatter;
            this.logFormat = logFormat;
        }

        public override MessageEncoding Encoding 
        {
            get 
            {
                return wrappedFormatter.Encoding; 
            }
        }

        public override MessageHeaderTypeCollection HeaderTypes 
        {
            get
            {
                return wrappedFormatter.HeaderTypes;
            }
        }

        public override MessageReader CreateReader() 
        {
            return new LoggingReader(wrappedFormatter.CreateReader(), logFormat);
        }

        public override MessageWriter CreateWriter() 
        {
            // we don't do anything special on write, just return
            // what the original formatter would have done
            return wrappedFormatter.CreateWriter();
        }

        class LoggingReader : MessageReader 
        {
            string logFormat;
            MessageReader wrappedReader;

            internal LoggingReader(MessageReader wrappedReader, string logFormat) : base()
            {
                this.wrappedReader = wrappedReader;
                this.logFormat = logFormat;
            }

            /// <summary>
            /// This is where we actually plug into the input streams
            /// and wrap them in a LoggingStream which will pipe the contents to
            /// a file of our choosing.
            /// </summary>
            /// <param name="stream">message stream to deserialize</param>
            /// <returns>Deserialized message</returns>
            public override Message ReadMessage(Stream stream) 
            {
                return wrappedReader.ReadMessage(new LoggingStream(stream, logFormat));
            }
        }
    }
}
