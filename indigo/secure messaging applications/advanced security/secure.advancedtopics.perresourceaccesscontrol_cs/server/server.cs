using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Security;
using System.Xml;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class Server 
    {
        const string ActionFilterFormat = "/env:Envelope/env:Header/wsa:Action[wsa:Action='{0}']";
        const string AddressingNamespace = "http://schemas.xmlsoap.org/ws/2002/12/addressing";
        const string EnvelopeNamespace12 = "http://www.w3.org/2001/12/soap-envelope";
        static readonly XmlQualifiedName MailboxOwnerRole = 
            new XmlQualifiedName("MailboxOwner", Constants.SchemaNamespace);
    
        static XPathFilter CreateActionFilter(string action)
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());
            namespaces.AddNamespace("env", EnvelopeNamespace12);
            namespaces.AddNamespace("wsa", AddressingNamespace);
            XPathFilter actionFilter = new XPathFilter(string.Format(ActionFilterFormat, action), namespaces);
            return actionFilter;
        }

        static XPathFilter CreateMailboxSelector() 
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());
            namespaces.AddNamespace("env", EnvelopeNamespace12);
            namespaces.AddNamespace("ns", MailboxHeader.ElementQName.Namespace);
            string xpath = string.Format("/env:Envelope/env:Header/ns:{0}/text()", MailboxHeader.ElementQName.Name);
            XPathFilter selector = new XPathFilter(xpath, namespaces);
            return selector;
        }

        static SecurityReceiverScope CreateSecurityScope(string name, Uri action) 
        {
            SecurityReceiverScope scope = new SecurityReceiverScope(name, CreateActionFilter(action.AbsoluteUri));
            AccessRequirementChoice choice = new AccessRequirementChoice();
            choice.AccessRequirements.Add(new RoleAccessRequirement(MailboxOwnerRole));
            scope.AccessControl.AccessRequirementChoices.Add(choice);
            scope.ResourceSelector = CreateMailboxSelector();
            scope.SignedMessageParts.Add(SecurityManager.StandardSignedMessageParts);
            scope.ReplySignedMessageParts.Add(SecurityManager.StandardSignedMessageParts);
            return scope;
        }

        public static void Main(string[] args) 
        {
            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the port & security manager from the default environment.
            Port port = se[typeof(Port)] as Port;
            SecurityManager securityManager = se[typeof(SecurityManager)] as SecurityManager;

            // Create a security receiver scope for the send-mail action.
            SecurityReceiverScope sendMailScope = CreateSecurityScope("SendMailScope", Constants.SendMailAction);
            securityManager.Scopes.Add(sendMailScope);

            // Create a security receiver scope for the read-mail action.
            SecurityReceiverScope readMailScope = CreateSecurityScope("ReadMailScope", Constants.ReadMailAction);
            securityManager.Scopes.Add(readMailScope);

            // Register the custom mailbox header so that it gets parsed on receive.
            port.HeaderTypes.Add(MailboxHeader.HeaderType);

            // Set up the main message handler.
            port.ReceiveChannel.Handler = new MailboxServiceReceiveHandler(port);

            // Now that security is completely configured, open the service environment.
            se.Open();

            Console.WriteLine("Hit <enter> to stop service...");
            Console.ReadLine();

            se.Close();
        }
    }

    class MailboxServiceReceiveHandler : SyncMessageHandler 
    {
        Port port;

        public MailboxServiceReceiveHandler(Port port) 
        {
            this.port = port;
        }

        void DoReadMail(Message message, MailboxHeader mailboxHeader) 
        {
            // This is where you would put your application logic for looking the mail messages in the given mailbox.  This 
            // implementation just sends an empty response message.
            Console.WriteLine("Looking up mail for mailbox '{0}'.", mailboxHeader.Mailbox);
            port.SendChannel.Send(message.CreateReply());
        }

        void DoSendMail(MailboxHeader mailboxHeader, SendMailMessageContent sendMailContent) 
        {
            // This is where you would put your appliation logic for delivering the new piece of mail.  This 
            // implementation just dumps it to the console.
            Console.WriteLine("Delivering new piece of mail:");
            Console.WriteLine("\tFrom: {0}", mailboxHeader.Mailbox);
            Console.WriteLine("\tTo: {0}", sendMailContent.Recipient);
            Console.WriteLine("\tSubject: {0}", sendMailContent.Subject);
            Console.WriteLine("\tBody: {0}", sendMailContent.Body);
        }

        public override bool ProcessMessage(Message message)
        {
            if (message.Action.Equals(Constants.SendMailAction)) 
            {
                MailboxHeader mailboxHeader = (MailboxHeader) message.Headers.Find(typeof(MailboxHeader));
                if (mailboxHeader == null) 
                {
                    throw new InvalidOperationException("Mailbox header required for sendMail action.");
                }
                SendMailMessageContent sendMailContent = 
                    (SendMailMessageContent) message.Content.GetObject(typeof(SendMailMessageContent));
                if (sendMailContent == null) 
                {
                    throw new InvalidOperationException("Invalid message body for sendMail action.");
                }
                DoSendMail(mailboxHeader, sendMailContent);
            }
            else if (message.Action.Equals(Constants.ReadMailAction)) 
            {
                MailboxHeader mailboxHeader = (MailboxHeader) message.Headers.Find(typeof(MailboxHeader));
                if (mailboxHeader == null) 
                {
                    throw new InvalidOperationException("Mailbox header required for readMail action.");
                }
                DoReadMail(message, mailboxHeader);
            }
            else 
            {
                Console.WriteLine("Received message with unknown action '{0}'.", message.Action.AbsoluteUri);
            }
            return true;
        }
    }
}
