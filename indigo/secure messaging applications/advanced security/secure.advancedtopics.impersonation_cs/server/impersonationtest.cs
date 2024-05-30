using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;
using System.Security.Principal;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    [DatagramPortType(Name="ImpersonationTest", Namespace="http://www.tempuri.org/quickstarts")]
    public class ImpersonationTest
    {
        [ServiceMethod]
        [ServiceSecurity(Name="DoTest", Confidentiality=false, Role="mbse:AuthenticatedUser")]
        public ResponseMessage DoTest(StartMessage message)
        {	
            Console.WriteLine("Called by client {0}.", message.Name);
        
            TestImpersonation(message.ImpersonationToken);
        
            ResponseMessage response = new ResponseMessage(String.Format("Impersonation test complete for client {0}", message.Name));
            return response;
        }

        internal void TestImpersonation(IImpersonationToken token)
        {
            if (token == null || !token.CanImpersonate)
            {
                Console.WriteLine("Token cannot perform impersonation.");
                return;
            }
        
            Console.WriteLine("Testing impersonation.");
            Console.WriteLine("Identity is: {0}.", WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("Starting impersonation.");
            token.StartImpersonation();
            Console.WriteLine("Identity is: {0}.", WindowsIdentity.GetCurrent().Name);

            Console.WriteLine("Stopping impersonation.");
            token.StopImpersonation();
            Console.WriteLine("Identity is: {0}.", WindowsIdentity.GetCurrent().Name);
        }
    }

    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class StartMessage : TypedMessage
    {
    
        private string internalName;
    
        public StartMessage() : base() {    }

        public StartMessage(string name)
        {
            this.internalName = name;
        }
    
        public IImpersonationToken ImpersonationToken
        {
            get
            {
                MessageHeader[] props = this.Headers.FindMultiple(typeof(SignatureTokenProperty));
                foreach (SignatureTokenProperty stp in props) 
                {
                    IToken token = stp.Token;
                
                    if (token is DerivedKeyToken)
                    {
                        token = ((DerivedKeyToken)token).InnerToken;
                    }
                
                    if (token is IImpersonationToken)
                    {
                        return (IImpersonationToken)token;
                    }
                }
            
                return null;
            }
        }
    
        [MessageBodyAttribute]
        public string Name
        {
            get { return this.internalName; }
            set { this.internalName = value; }
        }
    
    }

    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class ResponseMessage : TypedMessage
    {
        private string internalResponse;

        public ResponseMessage() : base() {}
    
        public ResponseMessage(string response)
        {
            this.internalResponse = response;
        }
    
        [MessageBodyAttribute]
        public string Response
        {
            get { return this.internalResponse; }
            set { this.internalResponse= value; }
        }
    }
}
