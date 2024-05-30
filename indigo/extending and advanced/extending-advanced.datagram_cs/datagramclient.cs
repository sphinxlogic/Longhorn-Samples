//
//  DatagramClient.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

using System;
using System.Collections;
using System.Configuration;
using System.MessageBus;

namespace Microsoft.Samples.MessageBus.Quickstarts.Datagram
{
	/// <summary>
	/// The DatagramClient class is a client to the DatagramService.  It reads
	/// strings input by the user and sends them to the DatagramService inside
	/// of a SOAP message.
	/// </summary>
	public class DatagramClient
	{
		/// <summary>
		/// The service environment for the DatagramClient.  Holds references
		/// to our Port and other interesting things.
		/// </summary>
		private ServiceEnvironment environment;

		/// <summary>
		/// The channel used for sending messages to the DatagramService.
		/// </summary>
		private SendChannel sendChannel;

		/// <summary>
		/// The entry-point for the DatagramClient.  Gets the DatagramService's 
		/// Uri from config and then starts the client.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			// Pull the Uri of the service for which we are a client out of config.
			Uri datagramServiceUri = null;
			try
			{
				datagramServiceUri = new Uri(ConfigurationSettings.AppSettings["DatagramServiceUri"]);
			}
			catch (ArgumentNullException)
			{
				Console.WriteLine("Error: DatagramServiceUri does not exist in config.");
				Environment.Exit(-1);
			}
			catch (UriFormatException)
			{
				Console.WriteLine("Error: DatagramServiceUri is not well-defined in config.");
				Environment.Exit(-1);
			}

			// Create a client for that service.
			DatagramClient client = new DatagramClient(datagramServiceUri);

			// Start the main (looping) logic of { read; send; }
			try
			{
				client.Run();
			}
			catch (EndpointNotFoundException)
			{
				Console.WriteLine("Error: No service listening on DatagramServiceUri.");
				Environment.Exit(-1);
			}
			catch (TimeoutException)
			{
				Console.WriteLine("Error: Connection timed out.");
				Environment.Exit(-1);
			}
		}

		/// <summary>
		/// DatagramClient constructor.
		/// </summary>
		/// <param name="serviceUri">Uri of the DatagramService for which
		/// this class is a client.</param>
		public DatagramClient(Uri serviceUri)
		{
			// Load the default service environment ("main").
			this.environment = ServiceEnvironment.Load();

			// Get a reference to our ServiceEnvironment's port.
			Port port = (Port) environment[typeof(Port)];

			// Create a SendChannel to the DatagramService.
			this.sendChannel = port.CreateSendChannel(serviceUri);
		}

		/// <summary>
		/// Core logic of the DatagramClient.  First, it instantiates the necessary
		/// messaging infrastructure.  It then loops, reading data from the Console
		/// and forwarding that to the DatagramService.
		/// </summary>
		public void Run()
		{
			// Open the environment (allowing us to send / receive messages).
			this.environment.Open();

			// Loop, reading user from the input and sending it to the DatagramService.
			Console.WriteLine("Enter message, or press <ENTER> to exit:");
			string text = Console.ReadLine();
			while (text.Length != 0)
			{
				// Create a new message w/ the correct SOAP action and user-input.
				Uri datagramAction = new Uri("http://schemas.microsoft.com/MB/2003/06/Samples/CoreMessaging/Datagram");
				Message message = new Message(datagramAction, text);

				// Send the message.  The SendChannel takes care of all necessary 
				// addressing information.
				sendChannel.Send(message);

				// Wait for more user-input.
				text = Console.ReadLine();
			}

			// Close our service environment, since we're done.
			this.environment.Close();
		}
	}
}