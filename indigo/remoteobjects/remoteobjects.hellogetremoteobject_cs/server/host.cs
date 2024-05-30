using System; 
using System.MessageBus;
using System.MessageBus.Remoting;

namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
{
    public class Host
    {
        public static void Main()
        {
            // The remoting configuration starts with the 
            // System.MessageBus.ServiceEnvironment class.
            ServiceEnvironment se = ServiceEnvironment.Load();
            RemotingManager remManager = se[typeof(RemotingManager)] as RemotingManager;
            if (remManager == null)
            {
                throw new ApplicationException("The service environment does not have a RemotingManager.");
            }
            // Start the ServiceEnvironment. 
            se.Open();

            // Register an instance of a RemotableType for 
            // remote connection hosted in this service environment.
            RemotableObject remotableInstance = new RemotableObject();
       
            // The client can create a proxy to this remoted instance using 
            // RemotingManager.GetObject.
            PublishedServerObject serverObject = 
                new PublishedServerObject(remotableInstance, new Uri("urn:CustomInstance"));
            remManager.PublishedServerObjects.Add(serverObject);

            Console.WriteLine("Listening for requests. Press Enter to exit...");
            Console.ReadLine();

            // Rescind the publication of the instance.
            remManager.PublishedServerObjects.Remove(serverObject);

            // Close the environment.
            se.Close();
        }
    }
}