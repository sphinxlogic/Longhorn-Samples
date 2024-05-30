using System;
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    //DatagramPortType attribute indicates a sessionless service named Hello
    [DatagramPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        // ServiceMethod attribute exposes methods for access on the service
        [ServiceMethodAttribute]
        public HelloResponse Greeting(HelloSalutation senderGreeting)
        {	
            Console.WriteLine("Called by a client with the name {0}", senderGreeting.Name);
            HelloResponse response = new HelloResponse(senderGreeting.Name);
            response.Response = String.Format("Hello, {0}!", senderGreeting.Name);
            return response;
        }
    }

    // The HelloSalutation class is a custom class inheriting from the TypedMessage class
    // to allow the developer to create custom classes that can be used for input and output
    // parameters to methods of the service.  This is the default way to send custom types
    // over the Service Framework.
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloSalutation : TypedMessage
    {
	
        private string internalSalutation;
	
        public HelloSalutation() : base() {	}

        public HelloSalutation(string name)
        {
            this.internalSalutation = name;
        }
	
        [MessageBodyAttribute]
        public string Name
        {
            get { return this.internalSalutation; }
            set { this.internalSalutation = value; }
        }
    }

    // The HelloResponse class is a custom class inheriting from the TypedMessage class
    // to allow the developer to create custom classes that can be used for input and output
    // parameters to methods of the service.  This is the default way to send custom types
    // over the Service Framework.
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloResponse : TypedMessage
    {
        private string internalResponse;

        public HelloResponse() : base() {}
	
        public HelloResponse(string response)
        {
            this.internalResponse = response;
        }
	
        [MessageBodyAttribute]
        public string Response
        {
            get { return this.internalResponse; }
            set { this.internalResponse= value; }
        }
    }
}