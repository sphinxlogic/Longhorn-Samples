using System;
using System.Authorization;
using System.MessageBus;
using System.MessageBus.Security;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    class Client 
    {
        static readonly Uri ServiceUri = new Uri("soap.tcp://localhost:50321");

        static Message CreateReadMailMessage(string mailbox) 
        {
            Message message = new Message(Constants.ReadMailAction);
            message.Headers.Add(new MailboxHeader(mailbox));
            return message;
        }

        static Message CreateSendMailMessage(string sender, string recipient, string subject, string body) 
        {
            Message message = new Message(Constants.SendMailAction, new SendMailMessageContent(recipient, subject, body));
            message.Headers.Add(new MailboxHeader(sender));
            return message;
        }

        public static void Main(string[] args) 
        {
            string bobsMailbox = "BobSmith_Mailbox";
            string billsMailbox = "BillJones_Mailbox";
            string samsMailbox = "SamWinters_Mailbox";

            // Load the default service environment, called "main".
            ServiceEnvironment se = ServiceEnvironment.Load();

            // Retrieve the port, request-reply manager, and security manager from the default environment.
            Port port = se[typeof(Port)] as Port;
            RequestReplyManager requestManager = se[typeof(RequestReplyManager)] as RequestReplyManager;
            SecurityManager securityManager = se[typeof(SecurityManager)] as SecurityManager;

            // Add Bob Smith's username token to the security token cache.
            securityManager.EndpointSettings.TokenCache.AddToken(new UserNamePasswordPair("BobSmith", "Sm1thyRulz"));

            // Start the service environment.
            se.Open();

            // Create send channels that target the mailbox service.
            SendChannel oneWayChannel = port.CreateSendChannel(ServiceUri);
            SendRequestChannel requestChannel = requestManager.CreateSendRequestChannel(ServiceUri);

            // Create a message for sending a new piece of mail from Bob to Bill.
            Console.WriteLine("Attempting to send mail from Bob to Bill...");
            Message message = CreateSendMailMessage(bobsMailbox, billsMailbox, "Hi!", "How's it going?");
            oneWayChannel.Send(message);

            // Create a message for reading Bob's mail.
            Console.Write("Attempting to read Bob's mail... ");
            message = CreateReadMailMessage(bobsMailbox);
            Message replyMessage = requestChannel.SendRequest(message);
            if (replyMessage.Content.IsException) 
            {
                Console.WriteLine("failed.");
            }
            else 
            {
                Console.WriteLine("succeeded.");
            }

            // Create a message for sending a new piece of mail from Bill to Sam.  This should fail because Bob does not 
            // have Bill's username token in his token cache.
            Console.WriteLine("Attempting to send mail from Bill to Sam...");
            message = CreateSendMailMessage(billsMailbox, samsMailbox, "Pure evil", "I'm trying to spoof you!");
            oneWayChannel.Send(message);

            // Create a message for reading Sam's mail.  This should fail because Bob does not have Sam's username token
            // in his token cache.
            Console.Write("Attempting to read Sam's mail...");
            message = CreateReadMailMessage(samsMailbox);
            replyMessage = requestChannel.SendRequest(message);
            if (replyMessage.Content.IsException) 
            {
                Console.WriteLine("failed.");
            }
            else 
            {
                Console.WriteLine("succeeded.");
            }

            se.Close();
        }
    }
}
