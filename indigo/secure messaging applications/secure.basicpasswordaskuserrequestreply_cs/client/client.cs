using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;
using System.Authorization;
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

            // Retrieve the SecurityManager from the default environment
            SecurityManager secManager = se[typeof(SecurityManager)] as SecurityManager;

            if (secManager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }
            Console.WriteLine("Enter UserName:");

            string UserName = Console.ReadLine();

            Console.WriteLine("Enter Password:");

            string password = Console.ReadLine();

            // Create a new UserName Token and add password and user information.
            UserNameToken t = new UserNameToken(UserName, password , 24); 

            secManager.EndpointSettings.TokenCache.AddToken(t);
        
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }

            // Start the service environment.
            se.Open();

            // Create a proxy channel that points to the service to call.
            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");
            IHelloChannel channel = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), uri);

            try 
            {
                Console.WriteLine(channel.Greeting(UserName));
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            finally
            {
                se.Close();
            }
        }    
    }
}