using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    // Using the DialogPortTypeAttribute establishes a session between 
    // an instance of this service class and the client who created it.
    // All subsequent communication from or to the client will be 
    // with the same service instance.
    [DialogPortTypeAttribute(Name="Hello", Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        private string clientName;

        public Hello()
        {
            Console.WriteLine(
                "The service instance {0} has been created.", 
                this.GetHashCode()
                );
        }

        ~Hello()
        {
            Console.WriteLine(
                "Service instance number {0} is being recycled.", 
                this.GetHashCode()
                );    
        }

        [ServiceSecurityAttribute(Role = "StandardUserRole", Name = "StandardScope")]
        [ServiceMethodAttribute]
        public string Parting(string departureString)
        {
            Console.WriteLine("{0} is departing.  Departure comment is {1}" ,
                clientName, 
                departureString
                );
            return String.Format(
                "Service instance {0} wishes you goodwill.", 
                this.GetHashCode().ToString()
                );
        }

        [ServiceSecurityAttribute(Role = "StandardUserRole", Name = "StandardScope")]
        [ServiceMethodAttribute]
        public string Salutation(string name)
        {    
            this.clientName = name;
            Console.WriteLine("Called by a client with the name {0}", name);
            return String.Format(
                "Hello, {0}! This is Hello service instance {1}.", 
                name, 
                this.GetHashCode().ToString()
                );
        }
    }
}
