using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    [DatagramPortType(Name="Hello", Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        [ServiceMethod]
        [ServiceSecurity(Confidentiality=false, Role="MBClient")]
        public ServerResponseMessage Greeting(ClientRequestMessage request)
        {
            Console.WriteLine("Call with request '{0}' signed with {1}", request.Message, request.SigningToken);
            string responseMessage = string.Format("Hello! Server received your request '{0}'",  request.Message);
            return new ServerResponseMessage(responseMessage);
        }
    }


    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class ClientRequestMessage : TypedMessage
    {
        string message;

        public ClientRequestMessage()
        {
        }

        public ClientRequestMessage(string message)
        {
            this.message = message;
        }

        [MessageBodyAttribute]
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public IToken SigningToken
        {
            get
            {
                MessageHeader[] messageProperties = this.Headers.FindMultiple(typeof(SignatureTokenProperty));
                if (messageProperties == null)
                {
                    return null;
                }

                SignatureTokenProperty signingTokensProperty = messageProperties[0] as SignatureTokenProperty;
                IToken signingToken = signingTokensProperty.Token;
                if (signingToken is DerivedKeyToken)
                {
                    signingToken = (signingToken as DerivedKeyToken).InnerToken;
                }
                return signingToken;
            }
        }
    }


    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class ServerResponseMessage : TypedMessage
    {
        string message;

        public ServerResponseMessage()
        {
        }

        public ServerResponseMessage(string message)
        {
            this.message = message;
        }

        [MessageBodyAttribute]
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }
}
