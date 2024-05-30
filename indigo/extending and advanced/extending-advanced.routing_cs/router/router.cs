using System;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.MessageBus;
using System.MessageBus.Policy;
using System.MessageBus.Routing;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    class SampleRouter 
    {
        public static void Main(string[] args)
        {   
            // Load the default service environment, called "main".
            ServiceEnvironment environment = ServiceEnvironment.Load();

            // Open the service environment
            environment.Open();

            Console.WriteLine("Press enter to stop the services...");
            Console.ReadLine();
            environment.Close();

        }
    }
}


