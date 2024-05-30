namespace System.Windows.Desktop
{
    using System;
    using System.Collections;
    using System.Xml;
    using System.IO;

    public sealed class RSSFeed
    {
        private ArrayList stories;
        public ArrayList Stories
        {
            get
            {
                return stories;
            }
        }
        private string title = "";
        public string Title
        {
            get
            {
                return title;
            }
        }
        private string link = "";
        public string Link
        {
            get
            {
                return link;
            }
        }
        private string description = "";
        public string Description
        {
            get
            {
                return description;
            }
        }

        public RSSFeed(string url)
        {
            this.stories = new ArrayList();
            //XmlTextReader rss = new XmlTextReader(url);
            XmlTextReader rss = new XmlTextReader(new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\" ?><rss version=\"2.0\"><channel><title>MSDN: .NET Framework and CLR</title><link>http://msdn.microsoft.com/netframework/</link><description>The latest information for developers on the Microsoft .NET Framework and Common Language Runtime (CLR).</description><language>en-us</language><ttl>1440</ttl><item><title>Document Centric Applications</title><pubDate>Mon, 29 Sep 2003 07:00:00 GMT</pubDate><description>Chris Sells outlines an implementation of simple document handling for an SDI application, including proper save functionality and opening documents using the command line.</description><link>http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnforms/html/winforms09182003.asp</link></item><item><title>Blogging: Design Your Own Weblog</title><pubDate>Fri, 26 Sep 2003 07:00:00 GMT</pubDate><description>In this article the author builds a full-featured blog application to illustrate the use of the Repeater and DataList controls that render nested data in a master-detail relationship.</description><link>http://msdn.microsoft.com/msdnmag/issues/03/10/Blogging/default.aspx</link></item><item><title>Using Role-Based Security</title><pubDate>Fri, 26 Sep 2003 07:00:00 GMT</pubDate><description>See how WSE 2.0 integrates X.509-based WS-Security authentication with role-based security features in the Microsoft .NET Framework, and how to use WS-Policy in WSE 2.0 to greatly simplify tasks.</description><link>http://msdn.microsoft.com/webservices/building/wse/default.aspx?pull=/library/en-us/dnwssecur/html/wserolebasedsec.asp</link></item><item><title>Secure, Reliable, Web Services</title><pubDate>Wed, 17 Sep 2003 07:00:00 GMT</pubDate><description>This IBM/Microsoft white paper is a concise overview of the key Web services interoperability specifications. Learn how these specifications enable interoperable, SOA-based applications that are secure, reliable, and transacted.</description><link>http://msdn.microsoft.com/webservices/understanding/advancedwebservices/default.aspx?pull=/library/en-us/dnwebsrv/html/wsoverview.asp</link></item><item><title>Implementing Drag and Drop</title><pubDate>Wed, 10 Sep 2003 07:00:00 GMT</pubDate><description>Adding drag and drop to your Windows Forms apps is easy. See how it works, and learn how to build features for dragging and dropping text, pictures, files, and list items.</description><link>http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dv_vstechart/html/vbtchImpDragDrop.asp</link></item><item><title>Using C# Browser Controls</title><pubDate>Tue, 07 Oct 2003 07:00:00 GMT</pubDate><description>Migrate your existing applets to the Microsoft .NET Framework using C# Browser Controls version 1.1. End users who use applications built with the C# Browser Controls should also download the official release.</description><link>http://msdn.microsoft.com/vjsharp/downloads/browsercontrols/</link></item><item><title>Visual Basic .NET Resource Kit CD</title><pubDate>Wed, 01 Oct 2003 07:00:00 GMT</pubDate><description>This kit contains Microsoft .NET controls, worth $899 US, from ComponentOne and other leading .NET component vendors. Technical content includes white papers, videos, and code samples to help you get the most from Visual Basic .NET. Download for free or order a CD for a small fee.</description><link>http://msdn.microsoft.com/vbasic/vbrkit/default.aspx</link></item><item><title>.NET: Generics in the CLR</title><pubDate>Mon, 29 Sep 2003 07:00:00 GMT</pubDate><description>Jason Clark does a series on programming with generics</description><link>http://msdn.microsoft.com/msdnmag/issues/03/10/NET/default.aspx</link></item></channel></rss>"));
            while(rss.Read())
            {
                if(rss.NodeType == XmlNodeType.Element)
                {
                    switch(rss.Name.ToLower())
                    {
                        case "item":
                            this.stories.Add(new RSSStory(rss));
                            break;
                        case "title":
                            this.title = rss.ReadString();
                            break;
                        case "link":
                            this.link = rss.ReadString();
                            break;
                        case "description":
                            this.description = rss.ReadString();
                            break;
                    }
                }
            }
        }
    }
}
