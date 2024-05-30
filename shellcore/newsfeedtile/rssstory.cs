namespace System.Windows.Desktop
{
    using System;
    using System.Collections;
    using System.Xml;

    public sealed class RSSStory
    {
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

        private DateTime pubDate = DateTime.MinValue;
        public DateTime PubDate
        {
            get
            {
                return pubDate;
            }
        }

        public RSSStory(XmlReader rss)
        {
            while(rss.Read())
            {
                if(rss.NodeType == XmlNodeType.Element)
                {
                    switch(rss.Name.ToLower())
                    {
                        case "title":
                            this.title = rss.ReadString();
                            break;
                        case "link":
                            this.link = rss.ReadString();
                            break;
                        case "description":
                            this.description = rss.ReadString();
                            break;
                        case "pubDate":
                            try
                            {
                                this.pubDate = DateTime.Parse(rss.ReadString());
                            }
                            // if it doesn't parse, stick with default of DateTime.MinValue,
                            //   which will be used as a sentinel so we don't display it
                            catch(FormatException) {}
                            break;
                    }
                }
                else if(rss.NodeType == XmlNodeType.EndElement && rss.Name.ToLower() == "item")
                {
                    break;
                }
            }
        }
    }
}
