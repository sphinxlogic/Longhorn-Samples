//
//  EchoClient.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

using System;
using System.Collections;
using System.Configuration;
using System.MessageBus;
using System.Xml;

namespace Microsoft.Samples.MessageBus.Quickstarts.Echo
{
	/// <summary>
	/// The EchoClient class is a client to the EchoService.  It reads
	/// strings input by the user and sends them to the EchoService inside
	/// of a SOAP message.  The EchoService then replies to the client, echoing
	/// the content sent to the service.  The EchoClient then writes that
	/// response to the Console.
	/// 
	/// This sample builds upon the Datagram sample 
	/// (Microsoft.MessageBus.Samples.Datagram).
	/// </summary>
	public class EchoClient
	{
		/// <summary>
		/// The service environment for the DatagramService.  Holds references
		/// to our Port and other interesting things.
		/// </summary>
		private ServiceEnvironment environment;

		/// <summary>
		/// The channel used for sending messages to the EchoService.
		/// </summary>
		private SendRequestChannel sendRequestChannel;

		/// <summary>
		/// A dispatcher that holds mappings from message actions to
		/// message handlers.  This is used to "route" Messages to 
		/// MessageHandlers based upon the Message's Action. 
		/// </summary>
		private MessageDispatcher dispatcher;

		/// <summary>
		/// The entry-point for the EchoClient.  Gets the service's Uri
		/// from config and then starts the client.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			// Pull the service for which we are a client out of config.
			Uri echoServiceUri = null;
			try
			{
				echoServiceUri = new Uri(ConfigurationSettings.AppSettings["EchoServiceUri"]);
			}
			catch (ArgumentNullException)
			{
				Console.WriteLine("Error: EchoServiceUri does not exist in config.");
				Environment.Exit(-1);
			}
			catch (UriFormatException)
			{
				Console.WriteLine("Error: EchoServiceUri is not well-defined in config.");
				Environment.Exit(-1);
			}

			// Create a client for that service.
			EchoClient client = new EchoClient(echoServiceUri);

			// Start the main (looping) logic of { read; send request; receive reply; write; }
			try
			{
				client.Run();
			}
			catch (EndpointNotFoundException)
			{
				Console.WriteLine("Error: Could not find the Echo Service.");
				Environment.Exit(-1);
			}
			catch (TimeoutException)
			{
				Console.WriteLine("Error: No response received within allotted time.");
				Environment.Exit(-1);
			}
		}

		/// <summary>
		/// Instantiates an instance of the EchoClient.  This loads our service 
		/// environment from config, configures our Port, and populates our dispatcher.
		/// </summary>
		/// <param name="serviceUri">Uri of the EchoService for which
		/// this class is a client.</param>
		public EchoClient(Uri serviceUri)
		{
			// Load our service environment ("EchoClient").
			this.environment = ServiceEnvironment.Load("EchoClient");

			// Get a reference to our ServiceEnvironment's port
			Port port = (Port) environment[typeof(Port)];

			// Get a reference to our ServiceEnvironment's RequestReplyManager.
			RequestReplyManager requestReplyManager = (RequestReplyManager) environment[typeof(RequestReplyManager)];

			// Create a SendRequestChannel to the EchoService.
			this.sendRequestChannel = requestReplyManager.CreateSendRequestChannel(serviceUri);

			// Create a MessageDispatcher.  The MessageDispatcher will be registered with the
			// ReceiveChannel and will handle dispatching Messages to IMessageHandlers based
			// upon Message.Action.  We need to do three things:
			//     + Create a MessageDispatcher
			//     + Create a Filter for each type of Message we're interested in.
			//     + Hook-up a MessageHandler for each Filter.
			
			// Create and hook-up our MessageDispatcher
			this.dispatcher = new MessageDispatcher();
			port.ReceiveChannel.Handler = this.dispatcher;

			// Construct the Filtering code necessary to switch based upon Message.Action.
	        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());
			namespaceManager.AddNamespace("env", "http://www.w3.org/2001/12/soap-envelope");
			namespaceManager.AddNamespace("wsa", "http://schemas.xmlsoap.org/ws/2002/12/addressing");

			// Register our handlers with the MessageDispatcher        
			XPathFilter echoReplyMessageFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='" + EchoReplyMessageHandler.Action + "'", namespaceManager);
			XPathFilter exceptionMessageFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='" + ExceptionMessageHandler.Action + "'", namespaceManager);
			this.dispatcher.Add(echoReplyMessageFilter, new EchoReplyMessageHandler(port));
			this.dispatcher.Add(exceptionMessageFilter, new ExceptionMessageHandler(port));
		}

		/// <summary>
		/// Represents the core logic of the EchoClient:
		///     + Reads data from the console.
		///     + Sends a message to the EchoService with the supplied data.
		///     + Wait for a response to the message.
		///     + Pass the response to ProcessMessage().
		///     + Repeat.
		/// </summary>
		public void Run()
		{
			// Open the environment (allowing us to send / receive messages).
			this.environment.Open();

			// Loop, reading user from the input and sending it to the EchoService.
			Console.WriteLine("Enter message, or press <ENTER> to exit:");
			string text = Console.ReadLine();
			while (text.Length > 0)
			{
				// Create a new message w/ the correct SOAP action and user-input.
				Uri echoRequestAction = new Uri("http://schemas.microsoft.com/MB/2003/06/Samples/CoreMessaging/Echo/Request");
				Message message = new Message(echoRequestAction, text);

				// Send the message.  The SendRequestChannel takes care of all the 
				// necessary addressing information.  We block, waiting for a reply.
				Message reply = this.sendRequestChannel.SendRequest(message);

				// Pass the message to our message dispatcher, which
				// will figure out what type of message it is and whether or not
				// we should act upon it.  At this point, we know that the reply 
				// message was in response to the message we went, but we don't 
				// necessarily know the action.
				bool isOurMessage = this.dispatcher.ProcessMessage(reply);

				// If ProcessMessage() returns true, that means that the method is done
				// with the message and it can be closed.  If it returns false, it means
				// that the called function has taken control of the message, and we
				// should now ignore it.
				if (isOurMessage)
				{
					reply.Close();
				}

				// Wait for more user-input.
				text = Console.ReadLine();
			}

			// Close our service environment, since we're done.
			this.environment.Close();
		}

		/// <summary>
		/// Message handler for EchoReply messages received.  If a message arrives 
		/// w/ the the EchoReply action, this handler will process it.
		/// </summary>
		private class EchoReplyMessageHandler : SyncMessageHandler
		{
			/// <summary>
			/// Reference to the handler's port.
			/// </summary>
			private Port port;

			/// <summary>
			/// Constructs an instance of the app's echo reply message handler.  base(false) 
			/// indicates that ProcessMessage will not block on processing the message.  This
			/// is a performance hint to the message dispatching code.
			/// </summary>
			/// <param name="enviroment">Reference to the handler's port.</param>
			public EchoReplyMessageHandler(Port port) : base(false)
			{
				this.port = port;
			}

			/// <summary>
			/// The SOAP action upon which the EchoClient acts.
			/// </summary>
			public static Uri Action
			{
				get
				{
					return new Uri("http://schemas.microsoft.com/MB/2003/06/Samples/CoreMessaging/Echo/Reply");
				}
			}

			/// <summary>
			/// This is the message handler for EchoReply messages.  Read the body of 
			/// the message and print it out to the console.
			/// </summary>
			/// <param name="message">The message that was received.</param>
			/// <returns>Returns true because we've finished processing the message.</returns>
			public override bool ProcessMessage(Message message)
			{
				// Read the message body as a string.
				string text = (string) message.Content.GetObject(typeof(string));
	            
				// Write it out to the console.
				Console.WriteLine(text);

				// Return "true," indicating that we're finished looking at the message.
				return true;
			}
		}

		/// <summary>
		/// Message handler for exception messages received.  If a message arrives 
		/// w/ the the SOAP fault action, this handler will process it.
		/// </summary>
		private class ExceptionMessageHandler : SyncMessageHandler
		{
			/// <summary>
			/// Reference to the handler's port.
			/// </summary>
			private Port port;

			/// <summary>
			/// Constructs an instance of the app's exception message handler.  base(false) 
			/// indicates that ProcessMessage will not block on processing the message.  This
			/// is a performance hint to the message dispatching code.
			/// </summary>
			/// <param name="enviroment">Reference to the handler's port.</param>
			public ExceptionMessageHandler(Port port) : base(false)
			{
				this.port = port;
			}

			/// <summary>
			/// The SOAP exception action (which this class acts upon).
			/// </summary>
			public static Uri Action
			{
				get
				{
					return new Uri("http://schemas.xmlsoap.org/2003/03/soap-envelope#Fault");
				}
			}

			/// <summary>
			/// This is the message handler for exception messages we receive.  It just
			/// prints the exception to the console.
			/// </summary>
			/// <param name="message">The exception that was received.</param>
			/// <returns>Returns true because we've finished processing the message.</returns>
			public override bool ProcessMessage(Message message)
			{
				// Extract the MessageException from the Content.
				MessageException messageException = (MessageException) message.Content.GetObject(typeof(MessageException));

				// Display the exception information to the user.
				Console.WriteLine("Server returned an error: {0}", messageException.Message);

				// Return "true," indicating that we're finished looking at the message.
				return true;
			}
		}
	}
}