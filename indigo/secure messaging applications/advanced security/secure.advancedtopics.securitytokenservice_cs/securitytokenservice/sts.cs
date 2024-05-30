using System;
using System.MessageBus;
using System.MessageBus.Services;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class HelloSecurityTokenService
    {
        public static void Main(string[] args)
        {
            ServiceEnvironment environs = ServiceEnvironment.Load("securityTokenService");
            environs.Open();
            Console.WriteLine("Press enter to stop the security token service...");
            Console.ReadLine();
            environs.Close();
        }
    }
}
