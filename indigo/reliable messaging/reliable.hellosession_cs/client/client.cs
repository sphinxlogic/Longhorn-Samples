using System;
using System.MessageBus;
using System.MessageBus.Services;
using System.Threading;
// add the namespace of the service channel
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

            ServiceEnvironment se = ServiceEnvironment.Load();
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException("The ServiceManager is not available in the service se for some reason.");
            }

            se.Open();

            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");

            // Use the ServiceManager to create a channel to the 
            // Hello service. Because of the definition of IHelloChannel,
            // the "Indigo" infrastructure knows that it must create a
            // session between this client instance and the service instance
            // that is created to respond to this client's requests. 
            // Unlike DatagramPortTypeAttribute services, each subsequent 
            // request from this client is handled by the same service object.
            // When the client or the service is done with the conversation,
            // they can call IDialogPortTypeChannel.DoneSending().
            IHelloChannel channel;
            channel = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), uri);

            try 
            {
                Console.WriteLine(channel.Salutation(name));
                Console.WriteLine(channel.Parting("Thanks for the good time."));
                Console.WriteLine(channel.Parting("No, really, I must go."));
                Console.WriteLine("Press enter to stop this client...");
                Console.ReadLine();
                channel.DoneSending();
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