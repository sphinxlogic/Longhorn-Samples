using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.Threading;

using www_tempuri_org.quickstarts;

namespace Microsoft.Samples.MessageBus.Quickstarts
{

    // HelloCaller implements the callback interface that
    // the Web service calls. 
    public class HelloCaller : IHelloClient
    {
        public static void Main(string[] args) 
        {
            string name = "Client";
            if (args.Length > 0)
                name = args[0];

            HelloCaller client = new HelloCaller(name);
        }

        // This WaitHandle prevents the console 
        // from beign closed before the conversation
        // ends.
        private System.Threading.ManualResetEvent wait = null;

        // Uri of the service for the first call.
        private Uri serviceUri = null;

        // Tracks how many notifications object has received.
        private int notificationCount = 0;
    
        // Client name that is sent.
        private string name;
    
        // serviceEnvironment
        private ServiceEnvironment se = null;

        // Reference to the service instance this client
        // communicates with.
        private IHelloChannel service = null;

        public HelloCaller(string name)
        {
        
            // Load the default service environment, called "main".
            this.se = ServiceEnvironment.Load();
            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }

            // start this client service
            this.se.Open();
        
            this.serviceUri = new Uri("soap.tcp://localhost:46000/HelloService/");
            this.service = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), this.serviceUri, this);
            this.name = name;

            this.wait = new System.Threading.ManualResetEvent(false);
            this.service.Done += new EventHandler(this.OnDone);

            try 
            {
                this.Run();
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            finally
            {
                this.Close();
            }
        }

        private void OnDone(object sender, EventArgs e) 
        {
            this.wait.Set(); 
        }

        public void Close()
        {
            this.se.Close();
        }

        public void Run()
        {
            Console.WriteLine("Sending handshake...");
            service.Handshake(name);
            // holds the application open
            this.wait.WaitOne();
        }
    
        // by default, WSDLGen.exe hands off a reference to the 
        // dialog partner as the first parameter to the method.
        // Because this class creates the original channel and 
        // stores the reference the first parameter is ignored.
        public void Notify(IHelloChannel sender, string notification)
        {
            notificationCount++;
            Console.WriteLine("Notification event. {0}", notification);
            if (notificationCount == 12)
                this.SendDeparture();
        }
    
        // sends the departure message
        public void SendDeparture()
        {
            Console.WriteLine("To finish the conversation, press enter...");
            Console.ReadLine();
            Console.WriteLine("Sending the departure message....");
            service.Departure("Thanks for the good time.");
        }
    
        // method invokes TriggerNotifications twice
        public void ReturnHandshake(IHelloChannel sender, string reply)
        {
            Console.WriteLine(reply);
            Console.WriteLine("Requesting 8 \"ClientEvent 1\" events...");
            sender.TriggerNotifications("ClientEvent 1", 8);
            Console.WriteLine("Requesting 4 \"Different Event 6\" events...");
            sender.TriggerNotifications("Different Event 6", 4);
            return;
        }
        
        public void ReturnDeparture(IHelloChannel sender, string reply)
        {
            if (notificationCount != 12)
                Console.WriteLine("Client departure confirmed before all notifications have arrived.");
            Console.WriteLine("Service says: {0}", reply);
            service.DoneSending();
            Console.WriteLine("Press enter to stop this client...");
            return;
        }
    }
}
