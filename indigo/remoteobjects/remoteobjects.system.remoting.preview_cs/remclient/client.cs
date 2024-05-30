using System;
using System.Remoting;
using System.MessageBus.Remoting;

namespace Examples.Remoting
{

	class Client
	{
	
		[STAThread]
		static void Main(string[] args)
		{

			//Open the System.Remoting application
			RemotingApplication app = new RemotingApplication();
			app.Open();
			
			//register published type
			RemoteType publishedType = new RemoteType(typeof(HelloWorldServer), new Uri("soap.tcp://localhost:999"));
			app.ProxyTypes.Add(publishedType);
			
			//create and call an instance of the published type
			HelloWorldServer helloWorldServer = (HelloWorldServer) app.CreateProxy(typeof(HelloWorldServer));
			Console.WriteLine(helloWorldServer.HelloWorld());

			//register published instance
			RemoteType publishedInstance = new RemoteType(typeof(IHelloWorldServer), new Uri("soap.tcp://localhost:999/HelloWorldServer"));
			app.ProxyTypes.Add(publishedInstance);
			
			//call published instance
			IHelloWorldServer server = (IHelloWorldServer) app.CreateProxy(typeof(IHelloWorldServer));
			Console.WriteLine(server.HelloWorld());

			Console.ReadLine();
		}
	}
}
