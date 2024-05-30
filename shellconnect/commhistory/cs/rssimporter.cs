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
using System.Globalization;
using System.Text;

//
// This file uses the following WinFS classes from the System.Storage namespace
//
using BinaryData = System.Storage.Messages.BinaryData;
using Contact = System.Storage.Core.Contact;
using Folder = System.Storage.Folder;
using ItemContext = System.Storage.ItemContext;
using ItemSearcher = System.Storage.ItemSearcher;
using Message = System.Storage.Messages.Message;
using MessageParticipants = System.Storage.Messages.MessageParticipants;
using Participant = System.Storage.Messages.Participant;
using SmtpEmailAddress = System.Storage.Contacts.SmtpEmailAddress;
using UserDataFolder = System.Storage.Contacts.UserDataFolder;

// RSSImporter downloads the RSS feed as an XmlDocument and
// creates messages which are visible in Communication History.
namespace Microsoft.Samples.Communications
{
    class RssImporter
    {
        // Imports the RSS feed into the Communication History.
        static public void Import(string rssUrl)
        {
            Console.Out.WriteLine("Downloading RSS feed from '{0}'", rssUrl);
            try
            {
                RssFeed rssFeed = RssFeed.Load(rssUrl);

                // Parse the RSS feed. We do not attempt to parse every variation on RSS, since this
                // is not an RSS parsing sample. This works for the MSDN feed and many other RSS 2.0
                // feeds, but not all.  In particular, this example will not store messages that are
                // missing both a date and a title.
                // Create an ItemContext.
                ItemContext itemContext;

                using (itemContext = ItemContext.Open())
                {
                    // Get the folder to store the messages in
                    Folder folder = UserDataFolder.FindMyUserDataFolder(itemContext);

                    // For each <item> element, create the item in WinFS
                    foreach (RssItem rssItem in rssFeed.Channel.Items)
                    {
                        string title = (null != rssItem.Title ? rssItem.Title : "No Title");

                        Console.Out.WriteLine("Creating message: {0}", title);
                        CreateMessageInStore(itemContext, folder, rssItem);
                    }

                    itemContext.Update();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine("{0}An error occured while trying to download the RSS feed.{0}{1}", System.Environment.NewLine, e.Message);
            }
        }

        // Creates the message in the given Folder using the given ItemContext.
        private static void CreateMessageInStore(ItemContext itemContext, Folder folder, RssItem rssItem)
        {
            // Create the new Message item in WinFS
            Message message = new Message();
            String name = message.ItemId.ToString();

            // Set the DisplayName Subject and TimeReceived
            message.DisplayName = (null != rssItem.Title ? rssItem.Title : "No Title");
            message.Subject = (null != rssItem.Title ? rssItem.Title : "No Title");
            if (null != rssItem.PubDate)
            {
                // Parse the RFC1123 date
                message.TimeReceived = rssItem.ParseDate();
            }

            // The following fields are constant as far as this example is concerned
            message.Sensitivity = "";
            message.Priority = 3;
            message.IsRead = false;
            message.IsSigned = false;
            message.IsEncrypted = false;
            if (!MessageExistsInStore(itemContext, rssItem))
            {
                // Add message to folder
                folder.OutFolderMemberRelationships.AddItem(message, name);

                // Add the contents to the Message and to the Folder
                if (null != rssItem.Description)
                {
                    BinaryData binaryData = AddContentsToMessage(message, rssItem);

                    folder.OutFolderMemberRelationships.AddItem(binaryData, name + ".Contents");
                }

                // Add the author if one is specified
                if (null != rssItem.Author)
                {
                    AddAuthorToMessage(itemContext, message, rssItem.Author);
                }
            }
        }

        // Queries the WinFS store to see if the message already exists.
        // Currently only checks the title and the date of messages.
        private static bool MessageExistsInStore(ItemContext itemContext, RssItem rssItem)
        {
            ItemSearcher itemSearcher = Message.GetSearcher(itemContext);
            string query = null;
            bool result = true;

            // Both the title and date are optional on the rss feed.
            if (null != rssItem.Title && null != rssItem.PubDate)
            {
                query = "Subject = @subject && TimeReceived = @timeReceived";
                itemSearcher.Filters.Add(query);

                // Since the subject might contain characters that can confuse
                // the query, the subject and timeReceived are added to the query
                // as parameters of the query.
                itemSearcher.Parameters.Add("subject", rssItem.Title);
                itemSearcher.Parameters.Add("timeReceived", rssItem.ParseDate());

                Message message = itemSearcher.FindOne() as Message;

                result = (null != message);
            }
            else if (null != rssItem.Title)
            {
                query = "Subject = @subject";
                itemSearcher.Filters.Add(query);
                itemSearcher.Parameters.Add("subject", rssItem.Title);

                Message message = itemSearcher.FindOne() as Message;

                result = (null != message);
            }
            else if (null != rssItem.PubDate)
            {
                query = "TimeReceived = @timeReceived";
                itemSearcher.Filters.Add(query);
                itemSearcher.Parameters.Add("timeReceived", rssItem.ParseDate());

                Message message = itemSearcher.FindOne() as Message;

                result = (null != message);
            }
            else
            {
                // Message has neither a title nor a date, just say that the message exists
                // This will cause a message to not be added if it's missing both a date and
                // a title.
                result = true;
            }

            return result;
        }

        // Creates a BinaryData object, associates it with the message, and returns it
        private static BinaryData AddContentsToMessage(Message message, RssItem rssItem)
        {
            // Create the BinaryData object and a byte array to hold
            // the contents.
            BinaryData binaryData = new BinaryData();
            UnicodeEncoding encoder = new UnicodeEncoding();
            byte[] byteArray = encoder.GetBytes(rssItem.Description);

            binaryData.Data = byteArray;

            // associate the contents with the message
            message.OutMessageContentsRelationships.AddContents(binaryData, System.Storage.RelationshipMode.Reference);
            return binaryData;
        }

        // Adds the given address as a participant of the message.  This method
        // searches Contacts to find a matching address.  If a match is found,
        // the message is linked to the contact using a reference.
        private static void AddAuthorToMessage(ItemContext itemContext, Message message, String address)
        {
            Participant participant = new Participant();

            participant.Usage = "From";

            MessageParticipants messageParticipant = new MessageParticipants();

            messageParticipant.Data = participant;

            // Check for an existing contact
            Contact contact = FindContactByAddress(itemContext, address);

            // if we found one, wrap our Participant around it
            if (null != contact)
            {
                messageParticipant.TargetItemId = contact.ItemId;
                participant.Address.Add(FindSmtpEmailAddress(contact, address));
            }
                // otherwise we have a participant for whom there is no contact
                // create a "dangling" Participant for him
            else
            {
                participant.Address.Add(CreateSmtpEmailAddress(address));
            }

            message.OutMessageParticipantsRelationships.Add(messageParticipant);
        }

        // Searches the store for an existing contact.
        private static Contact FindContactByAddress(ItemContext itemContext, String address)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "EAddresses.Cast({0}).AccessPoint = @SmtpAddress", typeof(SmtpEmailAddress).FullName);
            ItemSearcher searcher = Contact.GetSearcher(itemContext);

            searcher.Filters.Add(query);
            searcher.Parameters.Add("SmtpAddress", address);
            return (searcher.FindOne() as Contact);
        }

        // Finds the SmtpEmailAddress matching the given address for a contact.
        private static SmtpEmailAddress FindSmtpEmailAddress(Contact contact, string address)
        {
            SmtpEmailAddress newAddress = null;

            foreach (SmtpEmailAddress smtpEmailAddress in contact.EAddresses.FilterByType<SmtpEmailAddress>())
            {
                if (smtpEmailAddress.Address == address)
                {
                    newAddress = smtpEmailAddress.DeepCopy() as SmtpEmailAddress;
                    newAddress.DisplayName = contact.DisplayName;
                    break;
                }
            }

            return newAddress;
        }

        // Creates a new SmtpEmailAddress for the given address.
        private static SmtpEmailAddress CreateSmtpEmailAddress(string address)
        {
            SmtpEmailAddress smtpEmailAddress;

            smtpEmailAddress = new System.Storage.Contacts.SmtpEmailAddress();
            smtpEmailAddress.Address = address;
            smtpEmailAddress.DisplayName = address;
            smtpEmailAddress.AccessPoint = address;
            smtpEmailAddress.AccessPointType = new System.Storage.Core.Keyword("SMTP");
            return smtpEmailAddress;
        }
    }
}