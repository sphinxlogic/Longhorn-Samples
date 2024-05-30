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
using System.IO;
using System.Storage;
using WinFS = System.Storage.Contacts;
using System.Storage.Core;
using System.Storage.Locations;

//Mark the assembly as being CLS compliant
[assembly: CLSCompliant(true)]

//Mark the assembly as not visible through COM interop
[assembly: System.Runtime.InteropServices.ComVisible(false)]

namespace Microsoft.Samples.Communications
{
    public sealed class PopulateContacts
    {
        private static ItemContext context = null;
        private static bool success = true;

        private PopulateContacts() { }

        public static bool ImportContactsFromXml(String file)
        {
            try
            {
                Console.WriteLine("Initializing store...");
                using (context = ItemContext.Open())
                {
                    Console.WriteLine("Loading and parsing the XML file: {0}", file);

                    //This loads the XML Data using XML Serialization
                    SampleData newData = SampleData.Load(file);
                    InsertData(newData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The application generated an unexpected error: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("The inner exception is: {0}", ex.InnerException.Message);
                }
                success = false;
            }

            Console.WriteLine("\n******************************");
            Console.Write("Script results: ");
            if (success)
            {
                Console.WriteLine("PASS! (no errors were found)");
            }
            else
            {
                Console.WriteLine("FAIL! (there were some errors)");
            }
            Console.WriteLine("******************************");

            return success;
        }

        //This method processes all of the arrays of data and inserts them into "WinFS".
        private static void InsertData(SampleData newData)
        {
            //Find/Create MyContacts folder
            WinFS.WellKnownFolder myContacts = WinFS.UserDataFolder.CreateMyPersonalContactsFolder(context);

            //Create a new contact for each contact in the XML, up until maxPersons
            foreach (Person newPerson in newData.People)
            {
                Console.WriteLine("Creating contact: {0}", newPerson.DisplayName);
                AddPerson(newPerson, myContacts);
            }
            context.Update();

            //Create a new organization for each organization in the XML, up until maxOrgs
            foreach (Organization newOrganization in newData.Organizations)
            {
                Console.WriteLine("Creating organization: {0}", newOrganization.Name);
                AddOrganization(newOrganization, myContacts);
            }
            context.Update();

            //Create a new group for each group in the XML, up until maxGroups
            foreach (Group newGroup in newData.Groups)
            {
                Console.WriteLine("Creating group: {0}", newGroup.Name);
                AddGroup(newGroup, myContacts);
            }
            context.Update();
        }

        //Creates a new WinFS Location
        private static Location CreateLocation(PostalAddress address)
        {
            Address newAddress = new Address();
            newAddress.AddressLine = address.Street;
            newAddress.PrimaryCity = address.City;
            newAddress.PostalCode = address.PostalCode;
            newAddress.CountryRegion = address.CountryRegion;

            Location newLocation = new Location();
            newLocation.LocationElements.Add(newAddress);

            return newLocation;
        }

        //Creates a new WinFS EmailAddress
        private static WinFS.SmtpEmailAddress CreateEmail(EmailAddress email)
        {
            WinFS.SmtpEmailAddress newEmail = new WinFS.SmtpEmailAddress(email.Address);
            newEmail.Keywords.Add(GetGeneralKeyword(email.Keyword));
            return newEmail;
        }

        //Creates a new WinFS TelephoneNumber
        private static WinFS.TelephoneNumber CreatePhone(PhoneNumber phone)
        {
            WinFS.TelephoneNumber newPhone = new WinFS.TelephoneNumber(phone.CountryCode, phone.AreaCode, phone.Number, phone.Extension);
            newPhone.Keywords.Add(GetGeneralKeyword(phone.Keyword));
            return newPhone;
        }

        //Creates a new WinFS IMAddress
        private static WinFS.InstantMessagingAddress CreateIMAddress(String IMAddress)
        {
            WinFS.InstantMessagingAddress newIM = new WinFS.InstantMessagingAddress();
            newIM.Address = IMAddress;
            newIM.AccessPoint = IMAddress;
            newIM.AccessPointType = StandardKeywords.Primary;
            newIM.ProviderUri = "MSN";
            return newIM;
        }

        //Adds Online Status to a Contact
        private static void AddOnlineStatusToContact(Contact contact, Keyword onlineStatus)
        {
            WinFS.Presence presence = new WinFS.Presence();
            presence.OnlineStatus.Add(onlineStatus);
            presence.DeviceConnected = true;
            contact.OutgoingRelationships.Add(presence);
        }

        //Adds an EAddress to a Contact
        private static void AddEAddressToContact(Contact contact, ContactEAddress address)
        {
            contact.EAddresses.Add(address);
        }

        //Adds a Location to a Contact
        private static void AddLocationToContact(Contact contact, Location address)
        {
            contact.OutContactLocationsRelationships.AddLocations(address, Guid.NewGuid().ToString());
        }

        //Creates a new WinFS Person and adds it to the corresponding WinFS Folder
        private static void AddPerson(Person contactToAdd, Folder parentFolder)
        {
            WinFS.Person newPerson = new WinFS.Person();

            //Adds display name and full name
            newPerson.DisplayName = contactToAdd.DisplayName;
            if ((contactToAdd.FirstName != null) || (contactToAdd.MiddleName != null) || (contactToAdd.LastName != null) || (contactToAdd.NickName != null) || (contactToAdd.Suffix != null) || (contactToAdd.Title != null))
            {
                WinFS.FullName name = new WinFS.FullName();
                name.GivenName = contactToAdd.FirstName;
                name.MiddleName = contactToAdd.MiddleName;
                name.Surname = contactToAdd.LastName;
                name.Nickname = contactToAdd.NickName;
                name.Suffix = contactToAdd.Suffix;
                name.Title = contactToAdd.Title;
                newPerson.PersonalNames.Add(name);
            }

            //Adds user tile
            if (contactToAdd.UserTile != null)
            {
                Console.WriteLine("\tAdding User Tile: {0}", contactToAdd.UserTile);
                try
                {
                    FileStream stream = File.Open(contactToAdd.UserTile, FileMode.Open);
                    try
                    {
                        System.Collections.Generic.List<Byte> bytes = new System.Collections.Generic.List<Byte>();
                        int next;
                        while ((next = stream.ReadByte()) != -1)
                        {
                            bytes.Add((Byte)next);
                        }
                        newPerson.UserTile = bytes.ToArray();
                        context.Update();
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("\tWarning: Unable to add User Tile: " + e);
                    success = false;
                }
            }

            //Adds IM Address
            if (contactToAdd.IMAddress != null)
            {
                AddEAddressToContact(newPerson, CreateIMAddress(contactToAdd.IMAddress));
            }

            //Adds Online Status
            if (contactToAdd.OnlineStatus != null)
            {
                AddOnlineStatusToContact(newPerson, GetGeneralKeyword(contactToAdd.OnlineStatus));
            }

            //Adds all locations
            foreach (PostalAddress address in contactToAdd.PostalAddresses)
            {
                AddLocationToContact(newPerson, CreateLocation(address));
            }

            //Adds all email addresses
            foreach (EmailAddress email in contactToAdd.EmailAddresses)
            {
                WinFS.SmtpEmailAddress emailAddress = CreateEmail(email);
                AddEAddressToContact(newPerson, emailAddress);
            }

            //Adds all telephone numbers
            foreach (PhoneNumber phone in contactToAdd.PhoneNumbers)
            {
                AddEAddressToContact(newPerson, CreatePhone(phone));
            }

            //Adds Person to Local Contacts
            parentFolder.OutFolderMemberRelationships.AddItem(newPerson, Guid.NewGuid().ToString());
        }

        //Creates a new WinFS Organization and adds it to the corresponding WinFS Folder
        private static void AddOrganization(Organization organizationToAdd, Folder parentFolder)
        {
            WinFS.Organization newOrganization = new WinFS.Organization();
            newOrganization.DisplayName = organizationToAdd.Name;

            //Adds all locations
            foreach (PostalAddress address in organizationToAdd.PostalAddresses)
            {
                AddLocationToContact(newOrganization, CreateLocation(address));
            }

            //Adds all email addresses
            foreach (EmailAddress email in organizationToAdd.EmailAddresses)
            {
                AddEAddressToContact(newOrganization, CreateEmail(email));
            }

            //Adds Organization to Sample Folder and Local Contacts
            parentFolder.OutFolderMemberRelationships.AddItem(newOrganization, Guid.NewGuid().ToString());

            //Find necessary members and add them to the organization
            foreach (Member newMember in organizationToAdd.Members)
            {
                WinFS.Person orgMember = FindPersonByName(newMember.DisplayName);
                if (orgMember != null)
                {
                    Console.WriteLine("\tAdding org member: {0}", newMember.DisplayName);
                    orgMember.OutEmploymentRelationships.AddEmployer(newOrganization, Guid.NewGuid().ToString());
                    foreach (PhoneNumber phone in newMember.PhoneNumbers)
                    {
                        phone.Keyword = "work";
                        AddEAddressToContact(orgMember, CreatePhone(phone));
                    }
                    context.Update();
                }
                else
                {
                    Console.WriteLine("\tWarning: Unable to find contact inside the WinFS store with a DisplayName='{0}'", newMember.DisplayName);
                }
            }
        }

        //Creates a new WinFS Group and adds it to the corresponding WinFS Folder
        private static void AddGroup(Group groupToAdd, Folder parentFolder)
        {
            WinFS.Group newGroup = new WinFS.Group();
            newGroup.DisplayName = groupToAdd.Name;

            //Adds all email addresses
            foreach (EmailAddress email in groupToAdd.EmailAddresses)
            {
                AddEAddressToContact(newGroup, CreateEmail(email));
            }

            //Adds Group to Sample Folder and Local Contacts
            parentFolder.OutFolderMemberRelationships.AddItem(newGroup, Guid.NewGuid().ToString());

            //Find necessary members and add them to the group
            foreach (Member newMember in groupToAdd.Members)
            {
                WinFS.Person groupMember = FindPersonByName(newMember.DisplayName);
                if (groupMember != null)
                {
                    Console.WriteLine("\tAdding group member: {0}", newMember.DisplayName);
                    newGroup.OutGroupMembershipRelationships.AddMembers(groupMember, Guid.NewGuid().ToString());
                    context.Update();
                }
                else
                {
                    Console.WriteLine("\tUnable to find contact with a DisplayName='{0}'", newMember.DisplayName);
                }
            }
        }

        //Creates a keyword given a specific string
        private static Keyword GetGeneralKeyword(string keyword)
        {
            switch (keyword.ToLower(System.Globalization.CultureInfo.InvariantCulture))
            {
                case "work":
                    return StandardKeywords.Work;

                case "primary" :
                    return StandardKeywords.Primary;

                case "home" :
                    return StandardKeywords.Home;

                case "cell" :
                    return StandardKeywords.Cell;

                case "online":
                    return StandardKeywords.OnlineStatus.Online;

                case "away":
                    return StandardKeywords.OnlineStatus.Away;

                case "busy":
                    return StandardKeywords.OnlineStatus.Busy;
            }
            return new Keyword(keyword);
        }

        //Finds a person given its display name
        private static WinFS.Person FindPersonByName(String name)
        {
            ItemSearcher searcher = WinFS.Person.GetSearcher(context);
            searcher.Filters.Add(new SearchExpression("DisplayName = '" + name + "'"));
            return searcher.FindOne() as WinFS.Person;
        }

        public static void Main(String[] args)
        {
            if (args.Length == 0)
            {
                ImportContactsFromXml("Contacts.xml");
            }
            else
            {
                ImportContactsFromXml(args[0]);
            }
        }
    }
}
