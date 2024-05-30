using System;
using System.Remoting;
using System.MessageBus.Remoting;

namespace Examples.Remoting
{
	
	class App
	{
	
		[STAThread]
		static void Main(string[] args)
		{

			RemotingApplication app = new RemotingApplication("soap.tcp://localhost:999");

			//published type example
			app.Services.Add(typeof(HelloWorldServer));
	
			//published instance example
			HelloWorldServer server = new HelloWorldServer();
			app.PublishedInstances.Add("HelloWorldServer", server);

			app.Open();

			Console.WriteLine("Server started at {0}", app.Url);

			Console.ReadLine();

		}
	}

}
