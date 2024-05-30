using System;
using System.MessageBus;
using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    public class Host
    {
        public static void Main(string[] args)
        {
            ServiceEnvironment environs = ServiceEnvironment.Load();
            environs.Open();
            Console.WriteLine("Press enter to stop the services...");
            Console.ReadLine();
            environs.Close();
        }
    }
}