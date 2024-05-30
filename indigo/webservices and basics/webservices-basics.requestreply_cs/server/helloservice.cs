using System;
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    //DatagramPortType attribute indicates a sessionless service
    [DatagramPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        // ServiceMethod attribute exposes methods for access on the service
        [ServiceMethod]
        public string Greeting(string name)
        {	
            Console.WriteLine("Called by a client with the name {0}", name);
            return String.Format("Hello, {0}!", name);
        }

    }
}