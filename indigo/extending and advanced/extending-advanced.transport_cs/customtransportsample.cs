//
//  CustomTransportSample.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.CustomTransport
{
    using System;
    using System.MessageBus;
    using System.MessageBus.Transports;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;

    /// <summary>
    /// CustomTcpTransport illustrates how you can use subclassing to achieve
    /// basic extensibility for how messages are sent over TCP.  The
    /// protected virtual members off of TcpTransport allow you to get a hold
    /// of the underlying socket and tweak whichever parameters you want. It also
    /// allows you to intercept the conversion from socket->Stream so that you 
    /// can wrap the "network stream" with another stream that does counting of bytes
    /// or encryption, etc.
    /// </summary>
    class CustomTcpTransport : TcpTransport 
    {
        public CustomTcpTransport() : base() 
        {
        }

        /// <summary>
        /// We override this function in order to wrap the network stream
        /// with our own stream that counts bytes of data sent/received
        /// </summary>
        protected override Stream CreateNetworkStream(Socket socket) 
        {
            // first let the base class create a network stream that
            // represents the underlying socket
            Stream baseStream = base.CreateNetworkStream(socket);

            // next, wrap this stream in our custom stream
            Stream wrappedStream = new CountingStream(baseStream);

            // and return the wrapped stream
            return wrappedStream;
        }

        /// <summary>
        /// Override this function to tweak options on sockets that
        /// are accepted off of our listening socket.
        /// </summary>
        /// <param name="socket">Socket that was created through a call to socket.Accept()</param>
        protected override void OnAcceptedSocket(Socket socket) 
        {
            base.OnAcceptedSocket(socket);

            // turn off receive buffering for Accepted sockets
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 0);
        }

        /// <summary>
        /// Override this function to tweak options on sockets that
        /// are created for outgoing connections.  After this function
        /// completes we will call socket.Connect() on this socket.
        /// </summary>
        protected override void OnCreateConnectSocket(Socket socket) 
        {
            base.OnCreateConnectSocket(socket);

            // turn off Nagling for outgoing sockets
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
        }

        /// <summary>
        /// Override this function to tweak options on sockets that
        /// are created for listening.  After this function completes 
        /// we will call socket.Bind() and socket.Listen() on this socket.
        /// </summary>
        protected override void OnCreateListenSocket(Socket socket) 
        {
            base.OnCreateListenSocket(socket);

            // turn on exclusive address use for our listening sockets
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, true);
        }
    }

    /// <summary>
    /// wraps an underlying stream and counts how many bytes are read/written.
    /// Almost all the methods simply delegate to the underlying stream.
    /// We write out our totals on Close().
    /// </summary>
    internal class CountingStream : Stream 
    {
        int totalBytesRead;
        int totalBytesWritten;
        Stream wrappedStream;

        public CountingStream(Stream wrappedStream) 
        {
            this.wrappedStream = wrappedStream;
        }

        public int TotalBytesRead 
        {
            get 
            {
                return totalBytesRead;
            }
        }

        public int TotalBytesWritten 
        {
            get 
            {
                return totalBytesWritten;
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
            return wrappedStream.BeginRead(buffer, offset, count, callback, state);
        }
                
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) 
        {
            totalBytesWritten += count;
            return wrappedStream.BeginWrite(buffer, offset, count, callback, state);
        }
		
        public override void Close() 
        {
            wrappedStream.Close();
            Console.WriteLine("Total bytes read: " + TotalBytesRead);
            Console.WriteLine("Total bytes written: " + TotalBytesWritten);
        }

        public override int EndRead(IAsyncResult result) 
        {
            // record the number of bytes read and add it to our total
            int bytesRead = wrappedStream.EndRead(result);
            totalBytesRead += bytesRead;
            return bytesRead;
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
            totalBytesRead += bytesRead;
            return bytesRead;
        }

        public override int ReadByte()
        {
            int byteRead = wrappedStream.ReadByte();

            if (byteRead != -1)
                totalBytesRead++;

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
            totalBytesWritten += count;
        }

        public override void WriteByte(byte value)
        {
            wrappedStream.WriteByte(value);
            totalBytesWritten++;
        }
    }

    /// <summary>
    /// This sample shows you how to set up a Port to use a custom Transport.
    /// For more details look at the comments in CustomTcpTransport above.
    /// </summary>
    public class CustomTransportSample
    {
        public static void Main(string[] args)
        {
            // this is the uri that we'll listen on for our Port with the
            // custom Transport.  We'll then use it to construct a SendChannel
            // in order to send a message to ourself.
            Uri portUri = new Uri("soap.tcp://localhost:9999/test");

            // create a Port
            Port port = new Port(portUri);

            // create our custom TcpTransport
            CustomTcpTransport customTransport = new CustomTcpTransport();

            // if a transport exists already with our scheme, remove it or
            // we will conflict
            Transport transport = port.Transports[customTransport.Scheme];

            if (transport != null)
                port.Transports.Remove(transport);

            // now we're clear to add in our own transport
            port.Transports.Add(customTransport);

            // add a handler to our receive pipeline
            port.ReceiveChannel.Handler = new SimpleHandler();

            // open the Port for message sending/receiving
            port.Open();

            // create a send channel to send the message to ourself
            SendChannel sendChannel = port.CreateSendChannel(port.IdentityRole);

            // send a message using our custom transport
            Message message = new Message(new Uri("http://www.tempuri.org/quickstarts/coremessaging/test"));
            sendChannel.Send(message);

            // we're done, close up the Port
            port.Close();
        }

        /// <summary>
        /// Trivial MessageHandler that takes the action of the message
        /// and writes it to the console.
        /// </summary>
        class SimpleHandler : SyncMessageHandler 
        {
            public override bool ProcessMessage(Message message) 
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                Console.WriteLine("Received message with action: " + message.Action.ToString());

                return true;
            }
        }
    }
}
