//
//  CustomTransportSample.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.Hosting
{
	using System;
    using System.Globalization;
    using System.IO;
	using System.MessageBus;
    using System.MessageBus.Transports;
    using System.Xml;
	

    /// <summary>
    /// Client class used for the hosting sample. The sample is a very simple
    /// service implemented using Indigo. The client is sending to the service 
    /// a string, the service is replying back to the client and the client is 
    /// finally priting the outcome to the screen.
    /// </summary>
    public class Client
	{
        // the port that the client will use to communicate with the service
        Port port;

        // a request reply manager that will be used to receive the server's reply
        RequestReplyManager requestReplyManager;

		/// <summary>
		/// Constructor.
		/// </summary>
        public Client() 
		{
			this.port = new Port();
            this.requestReplyManager = new RequestReplyManager(port);
            try
            {
                this.port.Open();
            }
            catch (PortIOException)
            {
                Console.WriteLine("Error: Could not open the port");
                Environment.Exit(-1);
            }
		}

        /// <summary>
        /// This method implements all the logic.
        /// </summary>
        /// <param name="name">The string to pass to the server</param>
        /// <param name="serverUri">The server's Uri</param>
        void Run(string name, Uri serverUri) 
        {
            // validate the arguments
            if (name == null)
                throw new ArgumentNullException("name");

            if (serverUri == null)
                throw new ArgumentNullException("serverUri");

            // Create a message that has in its content the string that was passed by the user
            Message message = new Message(new Uri("http://www.tempuri.org/quickstarts/hosting/client"), name);
            
            // Create a channel to send the request and receive the reply using the 
            // RequestReply manager object
            SendRequestChannel channel = requestReplyManager.CreateSendRequestChannel(serverUri);
				
            // Notify the user, send the request syncronously, receive the reply. notice that
            // we do not need to close the message that we are sending because the channel
            // is going to do that automatically for us.
            Console.WriteLine("Sending request-reply. Client says {0}", name);
            Message reply = channel.SendRequest(message);

            // Open the content of the reply and read the string that the 
            // client has sent, notify the user. Notice that we should close the 
            // reply message becuase we extracted its content. In fact when we are 
            // closing the message we are in fact closing the underlying stream that was
            // used to read the content
            string replyMessage = null;
            try
            {
                replyMessage = (string)reply.Content.GetObject(typeof(string));
            }
            catch(InvalidCastException)
            {
                Console.WriteLine("Error: The server did not reply back as expected");

                // check to see if something bad has happened and try to 
                // figure out what it was

                if (reply.Content.IsException)
                {
                    // get the exception content and print out all the info that you can
                    ExceptionContent content = (ExceptionContent)reply.Content;
                    if (content.IsException && !content.IsEmpty)
                    {
                        Exception exception = (Exception) content.GetObject(typeof(Exception));
                        throw new ApplicationException("Server threw an exception: ", exception);
                    }
                }
            }
            finally
            {
                reply.Close();
            }

            Console.WriteLine("Request-reply completed. Server said {0}", replyMessage);
        }
        
        /// <summary>
        /// Closes the port before terminating the application
        /// </summary>
        void Close()
        {
            if ((port != null) && (port.IsOpen))
            {
                port.Close();
                port = null;
            }
        }

        /// <summary>
        /// Prints out the help and outputs error messages if applicable
        /// </summary>
        /// <param name="err"></param>
        static void PrintHelp(string err)
        {
            if (err != null)
            {
                Console.WriteLine("Error:  {0}", err);
                Console.WriteLine();
            }

            Console.WriteLine("Usage: ");
            Console.WriteLine();
            Console.WriteLine("\tHostingClient.exe -server <serverUrl> <name>");
            Console.WriteLine();
            Console.WriteLine("Example:");  
            Console.WriteLine();
            Console.WriteLine("\tHostingClient.exe -server http://localhost/mb/hello.msgx MicrosoftUser");
            Console.WriteLine();
        }

        public static void Main(string[] args)
		{
			Client client = new Client();
            Uri serverUri = null;

            int argIndex = 0;
            while (argIndex < args.Length && (args[argIndex][0] == '-' || args[argIndex][0] == '/')) 
            {
                string modifier = args[argIndex].Substring(1);

                switch (modifier) 
                {
                    default:
                        PrintHelp("unrecognized modifier: " + modifier);
                        break;

                    case "server":
                        argIndex++;
                        serverUri = new Uri(args[argIndex]);
                        break;

                    case "?":
                        PrintHelp(null);
                        break;
                }

                argIndex++;
            }

            if (argIndex >= args.Length)
            {
            }
            else 
            {
                client.Run(args[argIndex], serverUri);
            }

            client.Close();
		}
	}
}