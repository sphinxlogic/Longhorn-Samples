using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.Threading;
// add the namespace of the service channel
using www_tempuri_org.quickstarts;

namespace Microsoft.Samples.MessageBus.Quickstarts
{

    public class Client 
    {
        public static void Main(string[] args) 
        {
            string name = "Client";
            if (args.Length > 0)
                name = args[0];

            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();
            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }

            se.Open();

            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");

            // Use the ServiceManager to create a channel to the 
            // Hello service. Because of the definition of IHelloChannel,
            // the "Indigo" infrastructure knows that it must create a
            // session between this client instance and the service instance
            // that is created to respond to this client's requests. 
            // Unlike DatagramPortTypeAttribute services, each subsequent 
            // request from this client is handled by the same service object.
            // When the client or the service is done with the conversation,
            // they can call IDialogPortTypeChannel.DoneSending().
        
            HelloCaller client = new HelloCaller();
            IHelloChannel channel;
            channel = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), uri, client);
            client.ServiceChannel = channel;
            client.ClientName = name;
		
            try 
            {
                client.Run();
                Console.ReadLine();
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            finally
            {
                se.Close();
            }
        }
    }

    // This is the reverse service instance. When the server calls back, he gets 
    // this object.
    public class HelloCaller : IHelloClient
    {

        // client name
        private string name;
	
        // IHelloChannel storage
        private IHelloChannel service = null;
	
        public IHelloChannel ServiceChannel
        {
            set { this.service = value; }
        }
	
        public string ClientName
        {
            set { this.name = value; } 
        }
	
        public void Run()
        {
            Console.WriteLine("Sending greeting...");
            service.Greeting(name);		
        }
	
        public void SendDeparture(IHelloChannel sender)
        {
            Console.WriteLine("To finish the conversation, press enter...");
            Console.ReadLine();
            Console.WriteLine("Sending the departure message....");
            sender.Departure(this.name, "Thanks for the good time.");
        }
	
        public void ReturnGreeting(IHelloChannel sender, string reply)
        {
            Console.WriteLine(reply);
            Console.WriteLine("Sending a departure message...");
            sender.Departure(this.name, "Goodbye.");
            return;
        }
        
        public void ReturnDeparture(IHelloChannel sender, string reply)
        {
            Console.WriteLine("Service says: {0}", reply);
            service.DoneSending();
            Console.WriteLine("Press enter to stop this client...");
            return;
        }
    }
}