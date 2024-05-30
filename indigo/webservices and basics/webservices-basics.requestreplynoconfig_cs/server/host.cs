using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Policy;
using System.MessageBus.Security;

using System.MessageBus.Services;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    public class Host
    {
        public static void Main(string[] args)
        {
            // Load and configure the default ServiceEnvironment.
            ServiceEnvironment se = ServiceEnvironment.Load();
            Port port = se[typeof(Port)] as Port;
            port.IdentityRole = new Uri("soap.tcp://localhost:46000/HelloService/");

            // Register the Hello type as activatable.        
            ServiceManager serviceManager = se[typeof(ServiceManager)] as ServiceManager;
            serviceManager.ActivatableServices.Add(typeof(Hello));

            // Enable the PolicyManager to accept unsigned policy messages
            // because this service does not have X509 certificates.
            // For demonstration purposes only.
            PolicyManager policyManager = se[typeof(PolicyManager)] as PolicyManager;
            policyManager.AreUntrustedPolicyAttachmentsAccepted = true;
            policyManager.IsPolicyReturned = true;

            // Disable security for receiving messages. For demonstration purposes only.
            SecurityManager security = (SecurityManager)se[typeof(SecurityManager)];
            security.IsEnabledForReceive = false;

            se.Open();
            Console.WriteLine("Press enter to stop the services...");
            Console.ReadLine();
            se.Close();            
        }
    }
}