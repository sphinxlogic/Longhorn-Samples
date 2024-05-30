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
            
            // Create XPath filters that match on the WSAddressing action. 
            XPathFilter greenFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/green'",namespaceManager);
            XPathFilter redFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/red'",namespaceManager);
            XPathFilter indigoFilter = new XPathFilter("/env:Envelope/env:Header/wsa:Action='http://www.tempuri.org/quickstarts/indigo'",namespaceManager);

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
            Message greenMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/green"));
            Message redMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/red"));
            Message indigoMessage = new Message(new Uri("http://www.tempuri.org/quickstarts/indigo"));
            
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

            // private constructor to prevent this class from being instantiated
	    private FilterEngineSample()
            {
            }

    }
}