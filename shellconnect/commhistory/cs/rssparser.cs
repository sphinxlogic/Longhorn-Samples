//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

// The following classes represent the RSS XML document.
// XmlSerializer parses the RSS document and creates
// an RssFeed object, creating the channel and items
// contained in the document. See the documentation
// for XmlSerializer for more information.
// RssFeed represents the rss element in an RSS XML document
namespace Microsoft.Samples.Communications
{
    [XmlType(TypeName = "rss")]
    public class RssFeed
    {
        // The RSS feed contains a channel
        [XmlElement(ElementName = "channel")]
        public RssChannel Channel
        {
            get { return this.channel; }
            set { this.channel = value; }
        }

        // Loads the given RSS document and returns an RssFeed
        public static RssFeed Load(String path)
        {
            RssFeed rssFeed = null;
            XmlTextReader reader = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RssFeed));

                reader = new XmlTextReader(path);
                rssFeed = (RssFeed)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rssFeed;
        }

        private RssChannel channel;
    }

    // RssChannel represents the channel element in an RSS XML document
    public class RssChannel
    {
        // The RSS channel consists of several items
        [XmlElement(ElementName = "item", Type = typeof(RssItem))]
        public ArrayList Items
        {
            get { return items; }
        }

        private ArrayList items = new ArrayList();
    }

    // RssItem represents the item element in an RSS XML document
    public class RssItem
    {
        // These fields match the RSS XML document
        [XmlElement("author")]
        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        [XmlElement("description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [XmlElement("link")]
        public string Link
        {
            get { return this.link; }
            set { this.link = value; }
        }

        [XmlElement("pubDate")]
        public string PubDate
        {
            get { return this.pubDate; }
            set { this.pubDate = value; }
        }

        [XmlElement("title")]
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        // Simple method which parses the date string into a DateTime object
        public DateTime ParseDate()
        {
            // Parse the RFC1123 date
            return DateTime.ParseExact(this.pubDate.Trim(), "R", new CultureInfo(""));
        }

        private string author = null;

        private string description = null;

        private string link = null;

        private string pubDate = null;

        private string title = null;
    }
}