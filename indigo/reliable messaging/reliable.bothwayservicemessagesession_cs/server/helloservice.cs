using System; 
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    [DialogPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        private string callerName;
        private IHellocaller caller;
        private int notificationCount = 0;

        public Hello()
        {
            Console.WriteLine("A BothWayHello service instance was created; hash code: {0}", this.GetHashCode().ToString());
        }

        [ServiceMessageAttribute]
        public void Handshake(IHellocaller caller, string name)
        {
            this.callerName = name;
            this.caller = caller;
            Console.WriteLine("{0} at {1} just offerred a handshake.", name, caller.To.ToString());
            caller.ReturnHandshake(String.Format("Hello {0}. I'll call you back in a moment.", name));
            return;
        }

        // Sends "eventCount" number of messages. Although this service may already have
        // received another TriggerNotifications call, the "Indigo" infrastructure will
        // not deliver that information to this method until this method has returned.
        [ServiceMessageAttribute]
        public void TriggerNotifications(string eventName, int eventCount)
        {
            this.notificationCount = eventCount;
            Console.WriteLine("{0} just requested {1} notifications.", this.callerName, this.notificationCount.ToString());
            for(int i = 0; i < this.notificationCount; ++i)
            {
                // Provides some visible delay to see messages being sent.
                System.Threading.Thread.Sleep(2000);
                caller.Notify(String.Format("\"{0}\" event notification number {1}. Service instance {2}", 
                    eventName, 
                    i.ToString(), 
                    this.GetHashCode().ToString())
                    );
            }
            return;
        }

        // Called by a client when it wants to announce it is terminating 
        // the conversation.
        [ServiceMessageAttribute]
        public void Departure(string departureString)
        {
            Console.WriteLine("{0} is departing.", callerName);
            this.caller.ReturnDeparture(String.Format("Hello service instance {0} says goodbye.", this.GetHashCode().ToString())
                );
            return;
        }
        
        ~Hello()
        {
            Console.WriteLine("Service instance {0} is being recycled.", this.GetHashCode().ToString());
        }
    }

    // Represents the interface of the client for use by the service instance.
    // Because the service knows what it needs to make a callback, the 
    // service developer defines what the callback interface looks like.
    // WSDLGen.exe will export a version that can be implemented
    // by a client.
    public interface IHellocaller : IDialogPortTypeChannel
    {
        // callback from the Handshake 
        [ServiceMessageAttribute]
        void ReturnHandshake(string reply);
        
        // callback from the Departure
        [ServiceMessageAttribute]
        void ReturnDeparture(string reply);

        // Returns immediately.
        [ServiceMessageAttribute]
        void Notify(string notification);
    }
}
