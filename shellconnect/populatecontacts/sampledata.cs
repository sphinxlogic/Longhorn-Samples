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
using System.Xml.Serialization;
using System.Collections;

namespace Microsoft.Samples.Communications
{
    public class SampleData
    {
        public Person[] People = new Person[0];
        public Organization[] Organizations = new Organization[0];
        public Group[] Groups = new Group[0];
        
        public static SampleData Load(string path)
        {
            SampleData NewData = new SampleData();
            XmlSerializer Serializer = new XmlSerializer(typeof(SampleData));
            System.IO.StreamReader Reader = new System.IO.StreamReader(path);

            try
            {
                NewData = (SampleData)Serializer.Deserialize(Reader);
            }
            finally
            {
                Reader.Close();
            }
            return NewData;
        }

        public void Save(string path)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(SampleData));
            System.IO.StreamWriter Writer = new System.IO.StreamWriter(path);

            try
            {
                Serializer.Serialize(Writer, this);
            }
            finally
            {
                Writer.Close();
            }
        }
    }


#region "Contact XML Serialization"
    public class Person
    {
        [XmlAttribute()] public string Title;
        [XmlAttribute()] public string FirstName;
        [XmlAttribute()] public string MiddleName;
        [XmlAttribute()] public string LastName;
        [XmlAttribute()] public string Suffix;
        [XmlAttribute()] public string NickName;
        [XmlAttribute()] public string DisplayName;
        [XmlAttribute()] public string IMAddress;
        [XmlAttribute()] public string UserTile;
        [XmlAttribute()] public string OnlineStatus;

        public PostalAddress[] PostalAddresses = new PostalAddress[0];
        public EmailAddress[] EmailAddresses = new EmailAddress[0];
        public PhoneNumber[] PhoneNumbers = new PhoneNumber[0];
    }

    public class PostalAddress
    {
        [XmlAttribute()] public string Street;
        [XmlAttribute()] public string City;
        [XmlAttribute()] public string State;
        [XmlAttribute()] public string PostalCode;
        [XmlAttribute()] public string CountryRegion;
    }

    public class EmailAddress
    {
        [XmlAttribute("Type")] public string Keyword;
        [XmlAttribute()] public string Address;
    }

    public class PhoneNumber
    {
        [XmlAttribute("Type")] public string Keyword;
        [XmlAttribute()] public string CountryCode;
        [XmlAttribute()] public string AreaCode;
        [XmlAttribute()] public string Number;
        [XmlAttribute()] public string Extension;
    }
    #endregion

    public class Member
    {
        [XmlAttribute()] public string DisplayName;

        public PhoneNumber[] PhoneNumbers = new PhoneNumber[0];
    }

    public class Organization
    {
        [XmlAttribute()] public string Name;
        [XmlElement("Member")] public Member[] Members = new Member[0];

        public PostalAddress[] PostalAddresses = new PostalAddress[0];
        public EmailAddress[] EmailAddresses = new EmailAddress[0];
    }
    
    public class Group
    {
        [XmlAttribute()] public string Name;
        [XmlElement("Member")] public Member[] Members = new Member[0];

        public EmailAddress[] EmailAddresses = new EmailAddress[0];
    }
}
