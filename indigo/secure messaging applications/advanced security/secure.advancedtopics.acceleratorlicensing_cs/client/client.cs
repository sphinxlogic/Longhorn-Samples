using System;
using System.MessageBus;
using System.MessageBus.Services;
// the namespace of the imported service interface
using www_tempuri_org.quickstarts;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class Client
    {
        public static void Main(string[] args)
        {
            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }

            // Start the service environment.
            se.Open();

            // Create a proxy channel that points to the service to call.
            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");
            IHelloChannel channel = (IHelloChannel) manager.CreateChannel(typeof(IHelloChannel), uri);

            // Get requests from console, send them to server, and print server responses
            Console.WriteLine("Type a request and press <enter>, or press just <enter> to stop...");

            while (true)
            {
                Console.Write("Request: ");
                string requestMessage = Console.ReadLine();
                if (requestMessage.Length == 0)
                {
                    break;
                }

                try
                {
                    ClientRequestMessage request = new ClientRequestMessage(requestMessage);
                    ServerResponseMessage response = channel.Greeting(request);
                    Console.WriteLine("Response: " + response.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    break;
                }
            }

            // Close the service environment.
            se.Close();
        }
    }
}
