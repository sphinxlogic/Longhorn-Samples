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
            string name = "Client";
            if (args.Length > 0)
                name = args[0];

            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the ServiceManager from the default environment
            ServiceManager manager = se[typeof(ServiceManager)] as ServiceManager;
            if (manager == null)
            {
                throw new ApplicationException(
                    "The ServiceManager is not available in the service se for some reason."
                    );
            }
            // Start the service environment.
            se.Open();

            // Create a proxy channel that points to the service to call.
            Uri uri = new Uri("soap.tcp://localhost:46000/HelloService/");
            IHelloChannel channel = (IHelloChannel)manager.CreateChannel(typeof(IHelloChannel), uri);

            try 
            {
                HelloSalutation request;
                HelloResponse reply;
                TokenCacheProperty tokens;

                // send anonymous request
                Console.WriteLine("Sending request as GallAnonim...");
                request = new HelloSalutation("GallAnonim");
                reply = channel.Greeting(request);
                Console.WriteLine("Server's response: {0}", reply.Response);

                // send request as JohnDoe, the SilverMember
                Console.WriteLine("Sending request as JohnSmith..");
                request = new HelloSalutation("JohnSmith");
                tokens = new TokenCacheProperty(new SimpleTokenCache());
                tokens.TokenCache.AddToken(new UserNamePasswordPair("JohnSmith", "htimSnhoJ"));
                request.Headers.Add(tokens);
                reply = channel.Greeting(request);
                Console.WriteLine("Server's response: {0}", reply.Response);

                // send request as JamesBond, the GoldMember
                Console.WriteLine("Sending request as BlueKelly...");
                request = new HelloSalutation("BlueKelly");
                tokens = new TokenCacheProperty(new SimpleTokenCache());
                tokens.TokenCache.AddToken(new UserNamePasswordPair("BlueKelly", "ylleKeulB"));
                request.Headers.Add(tokens);
                reply = channel.Greeting(request);
                Console.WriteLine("Server's response: {0}", reply.Response);
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
