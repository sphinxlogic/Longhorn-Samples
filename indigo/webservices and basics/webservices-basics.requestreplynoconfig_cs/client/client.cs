using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Policy;
using System.MessageBus.Security;
using System.MessageBus.Services;
// the namespace of the imported service interface
using www_tempuri_org.quickstarts;

namespace Microsoft.Samples.MessageBus.Quickstarts
{
    public class Client 
    {
        public static void Main(string[] args) 
        {
            string name = "Client";
            if (args.Length > 0)
                name = args[0];

            // Load the default service environment without any settings since there is no config file
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException(
                    "The ServiceManager is not available in the service se for some reason."
                    );
            }

            // Start the service environment programmatically.
            Port port = se[typeof(Port)] as Port;
            port.IdentityRole = new Uri("soap.tcp://localhost:46001/HelloService/");

            // Allow PolicyManager to accept unsigned policy messages because 
            // client does not have X509 certificates.
            // CAUTION: Security disabled for demonstration purposes.
            PolicyManager policyManager = se[typeof(PolicyManager)] as PolicyManager;
            policyManager.AreUntrustedPolicyAttachmentsAccepted = true;
            policyManager.IsPolicyReturned = true;

            // Turn off access control. 
            // CAUTION: Security disabled for demonstration purposes.
            SecurityManager security = (SecurityManager)se[typeof(SecurityManager)];
            security.DefaultReceiverScope.AccessControl.AccessRequirementChoices.Add(new AccessRequirementChoice());
            security.IsEnabledForReceive = false;

            // start the service.
            se.Open();

            // Create a proxy channel that points to the service to call.
            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");
            IHelloChannel channel = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), uri);

            try 
            {
                Console.WriteLine(channel.Greeting(name));
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