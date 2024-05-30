//
//  CustomPortExtensionSample.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.Logging
{
    using System;
    using System.MessageBus;

    public class CustomPortExtensionSample
    {
        public static void Main(string[] args)
        {
            // this is the uri that we'll listen on for our Port with the
            // custom PortExtension.  We'll then use it to construct a SendChannel
            // in order to send a message to ourself.
            Uri portUri = new Uri("soap.tcp://localhost:9999/test");
            
            // create a Port
            Port port = new Port(portUri);

            // add logging support to it; log only sent messages, as the received message
            // in this case would appear the same.
            LoggingManager loggingManager = new LoggingManager(port, LoggingMode.Send);

            // add a handler to our receive pipeline
            port.ReceiveChannel.Handler = new SimpleHandler();

            // open the Port for message sending/receiving
            port.Open();

            // create a send channel to send the message to ourself
            SendChannel sendChannel = port.CreateSendChannel(port.IdentityRole);

            // send a message that will get logged to a file
            Message message = new Message(new Uri("http://www.tempuri.org/quickstarts/coremessaging/test"), "Testing...1...2...3");
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
