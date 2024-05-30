//
//  LoggingStream.cs
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
    using System.Threading;

    /// <summary>
    /// wraps an underlying stream and copies all data that is read into a log file
    /// </summary>
    public class LoggingStream : Stream 
    {
        Stream logStream;
        Stream wrappedStream;
        string logFormat;

        /// <summary>
        /// Constructor that takes a source stream and a stream to pipe a copy
        /// of the source stream's contents to.
        /// </summary>
        public LoggingStream(Stream wrappedStream, Stream logStream) 
        {
            this.wrappedStream = wrappedStream;
            this.logStream = logStream;
        }

        /// <summary>
        /// Constructor that takes a source stream and a filename format for a 
        /// log file.  LoggingStream will take care of creating a log file
        /// on first write.
        /// </summary>
        public LoggingStream(Stream wrappedStream, string logFormat) 
        {
            this.wrappedStream = wrappedStream;
            this.logFormat = logFormat;
        }

        Stream LogStream 
        {
            get 
            {
                if (logStream == null) 
                {
                    logStream = LoggingStream.CreateFile(logFormat);
                }

                return logStream;
            }
        }

        public override bool CanRead 
        { 
            get
            {
                return wrappedStream.CanRead;
            }
        }

        public override bool CanSeek 
        { 
            get
            {
                return wrappedStream.CanSeek;
            }
        }

        public override bool CanWrite 
        { 
            get
            {
                return wrappedStream.CanWrite;
            }
        }

        public override long Length 
        { 
            get
            {
                return wrappedStream.Length;
            }
        }

        public override long Position 
        { 
            get
            {
                return wrappedStream.Position;
            }
            set
            {
                wrappedStream.Position = value;
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) 
        {
            return new ReadAsyncResult(this, buffer, offset, count, callback, state);
        }
                
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) 
        {
            return wrappedStream.BeginWrite(buffer, offset, count, callback, state);
        }
		
        public override void Close() 
        {
            wrappedStream.Close();

            // we don't want to create a log stream if we haven't used it which is 
            // why we don't use the property accessor here
            if (logStream != null)
                logStream.Close();
        }

        /// <summary>
        /// Create a new file based on the logFormat passed in, and return the associated FileStream
        /// </summary>
        /// <summary>
        /// Creates a log file based on the logFormat passed in.  If the logFormat has an insertion option 
        /// (i.e. {0}), then we'll put timestamp information in the filename so that we can differentiate 
        /// based on time sent/received.
        /// </summary>
        /// <param name="logFormat">Format for log filename</param>
        /// <returns>Newly created file</returns>
        public static FileStream CreateFile(string logFormat)
        {
            DateTime now = DateTime.Now;
            string baseName = string.Format("({0}{1}{2})({3}{4}{5}{6})", now.Year.ToString().PadLeft(4, '_'), now.Month.ToString().PadLeft(2, '_'), now.Day.ToString().PadLeft(2, '_'), now.Hour.ToString().PadLeft(2, '_'), now.Minute.ToString().PadLeft(2, '_'), now.Second.ToString().PadLeft(2, '_'), now.Millisecond.ToString().PadLeft(4, '_'));
            string curName = baseName;
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    FileStream filestream = File.Open(string.Format(logFormat, curName), FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                    return filestream;
                }
                catch (IOException)
                {
                }

                curName = baseName + i.ToString();
            }

            throw new InvalidOperationException("Unable to create log file.");
        }

        public override int EndRead(IAsyncResult result) 
        {
            return ReadAsyncResult.End(result);
        }
                
        public override void EndWrite(IAsyncResult result) 
        {
            wrappedStream.EndWrite(result);
        }
                
        public override void Flush() 
        {
            wrappedStream.Flush();
        }
		
        public override int Read(byte[] buffer, int offset, int count) 
        {
            int bytesRead = wrappedStream.Read(buffer, offset, count);

            if (bytesRead > 0) 
            {
                LogStream.Write(buffer, offset, bytesRead);
            }

            return bytesRead;
        }

        public override int ReadByte()
        {
            int byteRead = wrappedStream.ReadByte();

            if (byteRead != -1)
                LogStream.WriteByte((byte)byteRead);

            return byteRead;
        }

        public override long Seek(long offset, SeekOrigin origin) 
        {
            return wrappedStream.Seek(offset, origin);
        }
		
        public override void SetLength(long value) 
        {
            wrappedStream.SetLength(value);
        }
        
        public override void Write(byte[] buffer, int offset, int count) 
        {
            wrappedStream.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            wrappedStream.WriteByte(value);
        }

        /// <summary>
        /// Async implementation of LoggingStream.Read().
        /// </summary>
        class ReadAsyncResult : IAsyncResult
        {
            AsyncCallback callback;      
            bool completedSynchronously; 
            Exception exception;
            bool isCompleted;
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            object state;

            LoggingStream parent;
            byte[] buffer;
            int offset;
            int bytesRead;
            static AsyncCallback read0 = new AsyncCallback(Read0);

            public ReadAsyncResult(LoggingStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) 
            {
                this.parent = parent;
                this.buffer = buffer;
                this.offset = offset;
                this.callback = callback;
                this.state = state;

                IAsyncResult result = parent.wrappedStream.BeginRead(buffer, offset, count, read0, this);

                if (result.CompletedSynchronously) 
                {
                    Read1(result, true);
                }
            }

            public object AsyncState 
            {
                get { return state; }
            }

            public WaitHandle AsyncWaitHandle 
            {
                get { return manualResetEvent; }
            }

            public bool CompletedSynchronously 
            {
                get { return completedSynchronously; }
            }

            public bool IsCompleted 
            {
                get { return isCompleted; }
            }

            /// <summary>
            /// In addition to notifying the callback, we will capture the exception and store it to be thrown during AsyncResult.End.
            /// </summary>
            void Complete(bool completedSynchronously, Exception exception) 
            {
                this.completedSynchronously = completedSynchronously;
                this.isCompleted = true;
                this.exception = exception;

                manualResetEvent.Set();

                if (callback != null)
                    callback(this);
            }

            static void Read0(IAsyncResult result) 
            {
                if (result.CompletedSynchronously)
                    return;

                ReadAsyncResult thisPtr = (ReadAsyncResult)result.AsyncState;

                try 
                {
                    thisPtr.Read1(result, false);
                }
                catch (Exception e)
                {
                    // we're being called off the threadpool, need to make sure we don't have unhandled exceptions
                    Console.WriteLine("Unhandled Exception:" + e);
                }
            }

            void Read1(IAsyncResult result, bool synchronous) 
            {
                Exception exception = null;

                try 
                {
                    this.bytesRead = parent.wrappedStream.EndRead(result);

                    if (this.bytesRead > 0) 
                    {
                        parent.LogStream.Write(this.buffer, this.offset, this.bytesRead);
                    }
                }
                catch (Exception e) 
                {
                    exception = e;
                }
                finally 
                {
                    Complete(synchronous, exception);
                }
            }

            public static int End(IAsyncResult result)
            {
                ReadAsyncResult thisPtr = result as ReadAsyncResult;
                if (thisPtr == null)
                    throw new ArgumentException("Invalid AsyncResult.", "result");

                if (!thisPtr.isCompleted)
                    thisPtr.AsyncWaitHandle.WaitOne();

                if (thisPtr.exception != null)
                    throw thisPtr.exception;

                return thisPtr.bytesRead;
            }
        }
    }
}
