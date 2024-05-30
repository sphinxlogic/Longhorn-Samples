//
//  CustomContentSample.cs
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//

namespace Microsoft.Samples.MessageBus.Quickstarts.CustomContent
{
    using System;
    using System.Collections;
    using System.IO;
    using System.MessageBus;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// ElementListContent provides a way of serializing multiple XmlElements in a single
    /// content body.  This allows the user to access an assortment of different objects
    /// at once rather than having to write a "wrapper object" and use ObjectContent.
    /// 
    /// You can contruct this content either with an array of XmlElements or
    /// with an XmlReader which points to a set of elements.
    /// </summary>
    public class ElementListContent : MessageContent
    {
        XmlElement[] elements;
        XmlReader reader;

        /// <summary>
        /// Contructor that takes a list of elements that
        /// will be serialized/read by this content.
        /// </summary>
        public ElementListContent(XmlElement[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            this.elements = elements;
        }

        /// <summary>
        /// Constructor that takes an XmlReader that is 
        /// pointing to a list of elements that we'll use
        /// for the composition of this content.
        /// </summary>
        public ElementListContent(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            this.reader = reader;
        }

        /// <summary>
        /// Get our list of XmlElements. This has either been passed
        /// to us in the ctor or can be contructed from the XmlReader
        /// that was passed in.
        /// </summary>
        public XmlElement[] Elements
        {
            get
            {
                if (this.elements == null) // construct the list from our reader
                {
                    System.Diagnostics.Debug.Assert(this.reader != null);

                    if (reader.MoveToContent() == XmlNodeType.Element)
                    {
                        XmlDocument document = new XmlDocument(reader.NameTable);
                        ArrayList list = new ArrayList();
                        do
                        {
                            XmlNode node = document.ReadNode(this.reader);
                            System.Diagnostics.Debug.Assert(node is XmlElement);
                            list.Add(node);
                        } while (reader.MoveToContent() == XmlNodeType.Element);

                        this.elements = (XmlElement[])list.ToArray(typeof(XmlElement));
                    }
                    else
                    {
                        this.elements = new XmlElement[0];
                    }
                }

                return this.elements;
            }
        }
        
        /// <summary>
        /// Clone our MessageContent.  This is a "deep clone" of our object
        /// </summary>
        public override MessageContent Clone()
        {
            XmlElement[] original = Elements;
            XmlElement[] clone = new XmlElement[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                clone[i] = (XmlElement)original[i].CloneNode(true);
            }
            return new ElementListContent(clone);
        }

        /// <summary>
        /// Close the content. We simply close our underlying reader here if necessary.
        /// </summary>
        public override void Close()
        {
            if (this.reader != null)
                this.reader.Close();
        }

        /// <summary>
        /// Convert the content into an object of CLR type "type", if possible
        /// </summary>
        /// <param name="type">CLR type to convert to</param>
        /// <returns>deserialized object of given type</returns>
        public override object GetObject(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            XmlElement[] elements = Elements;
            if (elements.Length > 0)
            {
                XmlNodeReader reader = new XmlNodeReader(elements[0]);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }

            return null;
        }

        /// <summary>
        /// serialize our content to the given XmlWriter
        /// </summary>
        /// <param name="writer">writer to serialize to</param>
        public override void WriteTo(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (this.elements == null)
            {
                while (reader.MoveToContent() == XmlNodeType.Element)
                {
                    writer.WriteNode(reader, true);
                }
            }
            else
            {
                for (int i = 0; i < this.elements.Length; i++)
                {
                    this.elements[i].WriteTo(writer);
                }
            }
        }
    }
    
    /// <summary>
    /// This sample shows you how to set up messages using a custom Content.
    /// For more details on writing a custom MessageContent class, look at the 
    /// comments in ElementListContent above.
    /// </summary>
    public class CustomContentSample
    {
        public static void Main(string[] args)
        {
            // this is the uri that we'll listen on. We'll then use it to 
            // construct a SendChannel in order to send a message to ourself.
            Uri portUri = new Uri("soap.tcp://localhost:9999/test");
            
            // create a Port
            Port port = new Port(portUri);

            // add a handler to our receive pipeline
            port.ReceiveChannel.Handler = new SimpleHandler();

            // open the Port for message sending/receiving
            port.Open();

            // create a send channel to send the message to ourself
            SendChannel sendChannel = port.CreateSendChannel(port.IdentityRole);

            // create an XmlDocument representing the data we wish to 
            // encapsulate in our custom content
            XmlDocument doc = new XmlDocument();
            string xmlData = "<outerelement><element1>data1</element1><element2>data2</element2></outerelement>";
            doc.Load(new StringReader(xmlData));
            XmlNode root = doc.FirstChild;

            // and take the data elements from the root Xml Node
            XmlElement[] elements = new XmlElement[root.ChildNodes.Count];
            for (int i = 0; i < root.ChildNodes.Count; i++) 
                elements[i] = (XmlElement)root.ChildNodes[i];

            // create a message using our custom content
            ElementListContent content = new ElementListContent(elements);

            Message message = new Message(new Uri("http://www.tempuri.org/quickstarts/coremessaging/test"), content);
            sendChannel.Send(message);

            // we're done, close up the Port
            port.Close();
        }
 
        /// <summary>
        /// Trivial MessageHandler that takes the contents of the message
        /// and writes it to the console.
        /// </summary>
        class SimpleHandler : SyncMessageHandler 
        {
            public override bool ProcessMessage(Message message) 
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                Console.WriteLine("Received message with contents: ");

                // read the contents one element at a time, printing out the corresponding XML
                XmlReader reader = message.Content.Reader;
                XmlDocument document = new XmlDocument(reader.NameTable);
                while (reader.MoveToContent() == XmlNodeType.Element) 
                {
                    XmlNode node = document.ReadNode(reader);
                    Console.WriteLine(node.OuterXml);
                }

                return true;
            }
        }
    }
}
