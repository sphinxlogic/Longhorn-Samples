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

            // create a new remote object and obtain a proxy to it
            RemotableObject newRemotableObject = 
                remManager.CreateInstance(typeof(RemotableObject), null) as RemotableObject;

            Console.WriteLine(newRemotableObject.Hello(name));
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            // close the service 
            se.Close();
        }
    }
}