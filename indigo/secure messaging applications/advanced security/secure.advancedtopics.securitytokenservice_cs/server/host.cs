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
            ServiceEnvironment environs = null;
            try
            {
              environs = ServiceEnvironment.Load();
            }
            catch (Exception e)
            {
              Console.WriteLine("Failure loading service environment: {0}", e.Message);
              return;
            } 
            environs.Open();
            Console.WriteLine("Press enter to stop the services...");
            Console.ReadLine();
            environs.Close();
        }
    }
}
