using System;
using System.Xml;
using System.MessageBus;
using System.MessageBus.Services;
using System.MessageBus.Security;
using System.Authorization;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    [DatagramPortType(Name="Hello",Namespace="http://www.tempuri.org/quickstarts")]
    public class Hello
    {
        [ServiceMethodAttribute]
        [ServiceSecurityAttribute(Confidentiality=false, Role="GoldMember, SilverMember, BronzeMember")]
        public HelloResponse Greeting(HelloSalutation senderGreeting)
        {    
            Console.WriteLine("Called by a client with the name {0}", senderGreeting.Name);

            MessageHeader[] headers = senderGreeting.Headers.FindMultiple(typeof(AuthorizationResultsProperty));
            string membershipLevel = null;        
            foreach (AuthorizationResultsProperty authorizationResults in headers) 
            {
                foreach (RoleClaim role in authorizationResults.AuthorizedClaims) 
                {
                    if ("GoldMember".Equals(role.Role.Name)) 
                    {
                        membershipLevel = "GoldMember";                    
                    }
                    else if ("SilverMember".Equals(role.Role.Name)) 
                    {
                        membershipLevel = "SilverMember";
                    }
                    else if ("BronzeMember".Equals(role.Role.Name)) 
                    {
                        membershipLevel = "BronzeMember";
                    }
                }
                if (membershipLevel != null) 
                {
                    break;
                }
            }

            if (membershipLevel == null) 
            {
                throw new ApplicationException("Unexpected application state");
            }

            Console.WriteLine("  The client has membership level of '{0}'.", membershipLevel);

            HelloResponse response = new HelloResponse(senderGreeting.Name);
            response.Response = String.Format("Hello, {0}! Your membership level is '{1}'.", senderGreeting.Name, membershipLevel);

            return response;
        }
    }

    // required
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloSalutation : TypedMessage
    {
    
        private string internalSalutation;
    
        // required
        public HelloSalutation() : base() {    }

        public HelloSalutation(string name)
        {
            this.internalSalutation = name;
        }
    
        // required. Can be a series of MessagePartAttribute attributes as well.
        [MessageBodyAttribute]
        public string Name
        {
            get { return this.internalSalutation; }
            set { this.internalSalutation = value; }
        }
    }

    // required
    [MessageAttribute(Namespace="http://www.tempuri.org/quickstarts")]
    public class HelloResponse : TypedMessage
    {
        private string internalResponse;

        // required
        public HelloResponse() : base() {}
    
        public HelloResponse(string response)
        {
            this.internalResponse = response;
        }
    
        // required. Can be a series of MessagePartAttribute attributes as well.
        [MessageBodyAttribute]
        public string Response
        {
            get { return this.internalResponse; }
            set { this.internalResponse= value; }
        }
    }
}
