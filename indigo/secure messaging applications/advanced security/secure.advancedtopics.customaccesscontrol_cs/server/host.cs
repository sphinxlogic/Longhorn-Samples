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
            ServiceEnvironment se = ServiceEnvironment.Load();
            se.Open();
            Console.WriteLine("Press enter to stop the services...");
            Console.ReadLine();
            se.Close();
        }
    }
}
