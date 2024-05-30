using System;
using System.MessageBus.Remoting;

namespace Examples.Remoting
{
	
	public class HelloWorldServer : MarshalByRefObject, IHelloWorldServer
	{

		public string HelloWorld()
		{
			Console.WriteLine("Server called at {0}", DateTime.Now);

			return "HelloWorld";
			
		}

	}
}



