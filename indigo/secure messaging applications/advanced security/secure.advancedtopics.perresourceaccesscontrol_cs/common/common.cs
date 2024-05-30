using System;
using System.MessageBus;
using System.Xml;
using System.Xml.Serialization;

[assembly:CLSCompliant(true)]
[assembly:System.Runtime.InteropServices.ComVisible(true)]

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    public sealed class Constants 
    {
        public static readonly Uri ReadMailAction = new Uri("http://tempuri.org/actions/readMail");
        public const string SchemaNamespace = "http://tempuri.org/schemas";
        public static readonly Uri SendMailAction = new Uri("http://tempuri.org/actions/sendMail");
    }

    public class SendMailMessageContent
    {
        string body;
        string recipient;
        string subject;

        public SendMailMessageContent() 
        {
            // empty
        }

        public SendMailMessageContent(string recipient, string subject, string body) 
        {
            this.recipient = recipient;
            this.subject = subject;
            this.body = body;
        }

        public string Body 
        {
            get 
            {
                return this.body;
            }
            set 
            {
                this.body = value;
            }
        }

        public string Recipient 
        {
            get 
            {
                return this.recipient;
            }
            set 
            {
                this.recipient = value;
            }
        }

        public string Subject 
        {
            get 
            {
                return this.subject;
            }
            set 
            {
                this.subject = value;
            }
        }
    }

    [XmlType(Namespace = Constants.SchemaNamespace)]
    public class MailboxHeader : MessageHeader 
    {
        public static readonly XmlQualifiedName ElementQName = new XmlQualifiedName("Mailbox", Constants.SchemaNamespace);
        public static readonly MessageHeaderType HeaderType = new MailboxHeaderType();

        string mailbox;

        public MailboxHeader(string mailbox)
            : base(false, null)
        {
            this.mailbox = mailbox;
        }

        [XmlIgnore]
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        [XmlText]
        public string Mailbox 
        {
            get 
            {
                return this.mailbox;
            }
        }

        [XmlIgnore]
        public override string Name
        {
            get
            {
                return ElementQName.Name;
            }
        }

        [XmlIgnore]
        public override string Namespace
        {
            get
            {
                return ElementQName.Namespace;
            }
        }

        [XmlIgnore]
        public override string Prefix
        { 
            get 
            {
                return null;
            }
        }

        public override MessageHeader Clone()
        {
            return new MailboxHeader(this.mailbox);
        }

        public override void WriteTo(XmlWriter writer)
        {
            writer.WriteStartElement(this.Name, this.Namespace);
            writer.WriteString(this.mailbox);
            writer.WriteEndElement();
        }
    }

    class MailboxHeaderType : MessageHeaderType 
    {
        public override string Name
        {
            get
            {
                return MailboxHeader.ElementQName.Name;
            }
        }

        public override string Namespace
        {
            get
            {
                return MailboxHeader.ElementQName.Namespace;
            }
        }

        public override MessageHeader CreateInstance(XmlReader reader, bool mustUnderstand, Uri role)
        {
            string mailbox = reader.ReadElementString(this.Name, this.Namespace);
            return new MailboxHeader(mailbox);
        }
    }
}
