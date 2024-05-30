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

            RemotingManager remManager = (RemotingManager)se[typeof(RemotingManager)];
            IRemoteInterface remObject = new RemotableObject();
            PublishedServerObject pso = new PublishedServerObject(remObject, new Uri("urn:CustomInterface"));
            remManager.PublishedServerObjects.Add(pso);

            // Start the ServiceEnvironment. 
            se.Open();

            Console.WriteLine("Listening for requests. Press Enter to exit...");
            Console.ReadLine();

            // Close the environment.
            se.Close();
        }
    }
}