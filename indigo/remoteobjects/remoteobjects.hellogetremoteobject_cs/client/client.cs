using System;
using System.MessageBus;
using System.MessageBus.Remoting;

namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
{
    public class Client
    {
        public static void Main(string[] args)
        {
            string name = "Test Client"; 
            if (args.Length  != 0 && !(args[0].Equals(String.Empty)))
                name = args[0];
    
            // The remoting configuration starts with the 
            // System.MessageBus.ServiceEnvironment class
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Acquire the RemotingManager to do remote work.
            RemotingManager remManager = se[typeof(RemotingManager)] as RemotingManager;
            if (remManager == null)
            {
                Console.WriteLine("The configuration is incorrect.");
                return;
            }

            // Start the ServiceEnvironment. 
            se.Open();

            Uri serverPortUri = 
                new Uri("soap.tcp://localhost:46460/RemotableObject");

            // obtain a proxy to a currently running object. 
            // The first parameter to GetObject represents the 
            // server's Port Uri. The second parameter is the 
            // name of the object you want to connect to.
            RemotableObject remotedInstance = 
                remManager.GetObject(
                serverPortUri, 
                new Uri("urn:CustomInstance")
                ) 
                as RemotableObject;

            if (remotedInstance != null)
            {
                Console.WriteLine(remotedInstance.Hello(name));
            }
            else
            {
                Console.WriteLine("The remote object is not available.");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
            // close the service 
            se.Close();
        }
    }
}