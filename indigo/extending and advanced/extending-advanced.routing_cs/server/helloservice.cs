using System;
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    [DatagramPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        [ServiceMethod]
        public string Greeting(string name)
        {	
            Console.WriteLine("Called by a client with the name {0}", name);
            return String.Format("Hello, {0}!", name);
        }
    }
}