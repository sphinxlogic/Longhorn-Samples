using System;
using System.MessageBus.Remoting;

namespace Examples.Remoting
{
	
	[Remotable]
	public interface IHelloWorldServer 
	{

		string HelloWorld();

	}
}
