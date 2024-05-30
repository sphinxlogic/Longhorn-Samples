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

            // Start the ServiceEnvironment. 
            se.Open();

            Console.WriteLine("Listening for requests. Press Enter to exit...");
            Console.ReadLine();

            // Close the environment.
            se.Close();
        }
    }
}