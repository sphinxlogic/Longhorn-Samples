using System;
using System.MessageBus;
using System.MessageBus.Services;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class Host
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
