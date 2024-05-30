using System;
using System.MessageBus;
using System.Xml;
using System.Drawing;

namespace Microsoft.Samples.MessageBus.QuickStarts 
{
    public sealed class FilterEngineSample
    {
        public static void Main(string[] args)
        {
            // Create a namespace manager with the namespace prefix definitions
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("env", "http://www.w3.org/2001/12/soap-envelope");
            namespaceManager.AddNamespace("wsa", "http://schemas.xmlsoap.org/ws/2002/12/addressing");
            namespaceManager.AddNamespace("sample", "http://www.tempuri.org/quickstarts/filterengine");
            
            // Create XPath filters that match on the WSAddressing action. 
            XPathFilter greenFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/test' and /env:Envelope/env:Header/sample:ColorHeader='Green'",namespaceManager);
            XPathFilter redFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/test' and /env:Envelope/env:Header/sample:ColorHeader='Red'",namespaceManager);
            XPathFilter indigoFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/test' and /env:Envelope/env:Header/sample:ColorHeader='Indigo'",namespaceManager);

            // Associate an object (a Color in this example) with each filter. 
            // (This may be done in the constructor as well.)
            greenFilter.Tag = Color.Green;
            redFilter.Tag = Color.Red;
            indigoFilter.Tag = Color.Indigo;

            // Add the filters to a FilterTable
            FilterTable filterTable = new FilterTable();
            filterTable.Add(greenFilter);
            filterTable.Add(redFilter);
            filterTable.Add(indigoFilter);

            // Create messages initialized with sample actions
            Message greenMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/test"));
            Message redMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/test"));
            Message indigoMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/test"));

            // Add a custom header to each message
            greenMessage.Headers.Add( new SampleColorHeader("Green"));
            redMessage.Headers.Add( new SampleColorHeader("Red"));
            indigoMessage.Headers.Add( new SampleColorHeader("Indigo"));
            
            // Match each message against the FilterTable
            // Cast the Tag property of the matching filters back to a Color and display results.
            FilterCollection matchingFilters;
            Console.WriteLine("Matching the green message");
            matchingFilters = filterTable.Match(greenMessage);
            foreach (Filter filter in matchingFilters)
            {
                Console.WriteLine("---> Match! Filter.Tag contains: {0}",(Color)filter.Tag);
            }

            Console.WriteLine("Matching the red message");
            matchingFilters = filterTable.Match(redMessage);
            foreach (Filter filter in matchingFilters)
            {
                Console.WriteLine("---> Match! Filter.Tag contains: {0}",(Color)filter.Tag);
            }

            Console.WriteLine("Matching the indigo message");
            matchingFilters = filterTable.Match(indigoMessage);
            foreach (Filter filter in matchingFilters)
            {
                Console.WriteLine("---> Match! Filter.Tag contains: {0}",(Color)filter.Tag);
            }

        }
        
        private FilterEngineSample()
        {
        }
    }


    public class SampleColorHeader : MessageHeader 
    {
        const string SamplePrefix = "sample";
        const string SampleNamespace = "http://www.tempuri.org/quickstarts/filterengine";
        const string headerName = "ColorHeader";

        string color;

        /// <summary>
        /// Contructor that defaults the mustUnderstand and role values
        /// </summary>
        /// <param name="color">Color this header represents</param>
        public SampleColorHeader(string color) : this(false, null, color)
        {
            // all the work is done in the delegated ctor
        }

        /// <summary>
        /// Constructor used for cloning purposes.
        /// </summary>
        /// <param name="source">Source object to create a copy from</param>
        protected SampleColorHeader(SampleColorHeader source) : base(source)
        {
            this.color = source.color;
        }

        /// <summary>
        /// Detailed constructor that takes all tweakable values
        /// </summary>
        /// <param name="mustUnderstand">True if a receiver must understand this header.</param>
        /// <param name="role">Role that this header is associated with.</param>
        /// <param name="color">Color this header represents</param>
        public SampleColorHeader(bool mustUnderstand, Uri role, string color) : base(mustUnderstand, role)
        {
            if (color == null)
                throw new ArgumentNullException("color");

            this.color = color;
        }	

        /// <summary>
        /// The main value of this header (i.e. the Color string)
        /// </summary>
        public string Color 
        {
            get 
            {
                return color; 
            }
        }
        	
        /// <summary>
        /// True since we can be serialized to the wire or a durable store
        /// </summary>
        public override bool CanWrite 
        {
            get 
            {
                return true; 
            }
        }

        /// <summary>
        /// Name of our header (for identification and filtering purposes)
        /// </summary>
        public override string Name 
        { 
            get 
            {
                return headerName; 
            }
        }

        /// <summary>
        /// Namespace of our header (for identification and filtering purposes)
        /// </summary>
        public override string Namespace 
        { 
            get 
            {
                return SampleNamespace; 
            }
        }
	
        /// <summary>
        /// Default Namespace prefix of our header (for identification and filtering purposes)
        /// </summary>
        public override string Prefix
        { 
            get 
            {
                return SamplePrefix;
            }
        }

        /// <summary>
        /// Representation of our header as a string (used for filtering, cannot be null)
        /// </summary>
        public override string StringValue
        {
            get 
            {
                return Color; 	
            }
        }

        /// <summary>
        /// public Clone method. The heavy lifting is done in our protected ctor.
        /// </summary>
        /// <returns>a deep copy of the header</returns>
        public override MessageHeader Clone() 
        {
            return new SampleColorHeader(this);
        }

        /// <summary>
        /// Serialize a representation of this header to the given XmlWriter.
        /// The only data we need available to us besides the standard
        /// attributes (name, ns, mustUnderstand) is our Color value.
        /// </summary>
        /// <param name="writer">XmlWriter to serialize ourself to.</param>
        public override void WriteTo(XmlWriter writer) 
        {
            WriteStartHeader(writer);
            writer.WriteString(Color);
            WriteEndHeader(writer);
        }
    }
}