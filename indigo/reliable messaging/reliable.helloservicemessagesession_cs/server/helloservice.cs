using System; 
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{

    [DialogPortType(Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {

        public Hello()
        {
            Console.WriteLine("Hello service instance created. Hash code: {0}.", this.GetHashCode().ToString());
        }

        [ServiceMessageAttribute]
        public void Greeting(IHelloCaller caller, string name)
        {
            string tempString = String.Format("Instance {0} received a greeting from \"{1}\"", 
                this.GetHashCode().ToString(),
                name
                );
            Console.WriteLine(tempString);
            tempString = String.Format("Hello, {0}. This is Hello service instance {1}.", 
                name,
                this.GetHashCode().ToString()
                );
            caller.ReturnGreeting(tempString);
            return;								
        }

        [ServiceMessageAttribute]
        public void Departure(IHelloCaller caller, string name, string departureComment)
        {
            string tempString = String.Format("Instance {0} received a departure comment from \"{1}\": {2}", 
                this.GetHashCode().ToString(),
                name,
                departureComment
                );
            Console.WriteLine(tempString);
            tempString = String.Format("Goodbye, {0}. This is Hello service instance {1}.", 
                name,
                this.GetHashCode().ToString()
                );
            caller.ReturnDeparture(tempString);
            return;								
        }

        ~Hello()
        {
            Console.WriteLine("Service instance {0} is being recycled.", this.GetHashCode().ToString());
        }

    }

    // Represents the interface of the client for use by the service instance.
    public interface IHelloCaller : IDialogPortTypeChannel
    {
        // callback from the Greeting
        [ServiceMessageAttribute]
        void ReturnGreeting(string reply);
	
        // callback from the Departure
        [ServiceMessageAttribute]
        void ReturnDeparture(string reply);

    }
}