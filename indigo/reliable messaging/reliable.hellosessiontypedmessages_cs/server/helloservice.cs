using System;
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    [DialogPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        public Hello()
        {
            Console.WriteLine("Hello service instance created; hash code {0}", this.GetHashCode().ToString());        
        }

        [ServiceMethodAttribute]
        public HelloResponse Greeting(HelloSalutation senderGreeting)
        {    
            Console.WriteLine("Called by a client with the name {0}", senderGreeting.Name);
            HelloResponse response = new HelloResponse(senderGreeting.Name);
            response.Response = String.Format("Hello, {0}! This is service instance {1}", 
                senderGreeting.Name,
                this.GetHashCode().ToString()
                );
            return response;
        }

        ~Hello()
        {
            Console.WriteLine("Hello service instance {0} is being recycled.", this.GetHashCode());
        }
    }

    // The following is a custom type created to encapsulate salutation submitted.  The MessageAttribute and TypedMessage inheritance are required.
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloSalutation : TypedMessage
    {
    
        private string internalSalutation;
    
        // required
        public HelloSalutation() : base() {    }

        public HelloSalutation(string name)
        {
            this.internalSalutation = name;
        }
    
        // required. Can be a series of MessagePartAttribute attributes as well.
        [MessageBodyAttribute]
        public string Name
        {
            get { return this.internalSalutation; }
            set { this.internalSalutation = value; }
        }
    }

    // The following is a custom type created to encapsulate the reponse.  The MessageAttribute and TypedMessage inheritance are required.
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloResponse : TypedMessage
    {
        private string internalResponse;

        // required
        public HelloResponse() : base() {}
    
        public HelloResponse(string response)
        {
            this.internalResponse = response;
        }
    
        // required. Can be a series of MessagePartAttribute attributes as well.
        [MessageBodyAttribute]
        public string Response
        {
            get { return this.internalResponse; }
            set { this.internalResponse= value; }
        }
    }
}