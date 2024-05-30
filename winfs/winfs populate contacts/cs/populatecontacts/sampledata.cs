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

namespace Microsoft.Samples.WinFS.SampleDataNamespace
{
	public class SampleData
	{
		private Contact[] contactsValue;
		private Household[] householdsValue;
		private Organization[] organizationsValue;

		public Contact[] Contacts { get { return contactsValue; } set { contactsValue = value; } }
		public Household[] Households { get { return householdsValue; } set { householdsValue = value; } }
		public Organization[] Organizations { get { return organizationsValue; } set { organizationsValue = value; } }
		
		public static SampleData Load(string path)
		{
			SampleData newData = new SampleData();
			System.IO.StreamReader reader = null;

			try
			{
				
				XmlSerializer serializer = new XmlSerializer(typeof(SampleData));
				reader = new System.IO.StreamReader(path);

				newData = (SampleData)serializer.Deserialize(reader);
			}
			finally
			{
				if(reader!=null)
					reader.Close();
			}
			return newData;
		}
		public void Save(string path)
		{
			System.IO.StreamWriter writer = null;

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(SampleData));
				writer = new System.IO.StreamWriter(path);

				serializer.Serialize(writer, this);
			}
			finally
			{
				if(writer != null)
					writer.Close();
			}
		}
	}


	#region "Contact XML Serialization"
	public class Contact
	{
		private string titleValue;
		private string firstNameValue;
		private string middleNameValue;
		private string lastNameValue;
		private string suffixValue;
		private string nickNameValue;
		private string displayNameValue;
		private string imAddressValue;
		private Address postalAddressValue = new Address();
		private EmailAddress[] emailAddressesValue;
		private PhoneNumber[] phoneNumbersValue;
		private DocumentTypes documentsValue = new DocumentTypes();
		private BookmarkTypes bookmarksValue = new BookmarkTypes();

		[XmlAttribute()]
		public string Title { get { return titleValue; } set { titleValue = value; } }
		[XmlAttribute()]
		public string FirstName { get { return firstNameValue; } set { firstNameValue = value; } }
		[XmlAttribute()]
		public string MiddleName { get { return middleNameValue; } set { middleNameValue = value; } }
		[XmlAttribute()]
		public string LastName { get { return lastNameValue; } set { lastNameValue = value; } }
		[XmlAttribute()]
		public string Suffix { get { return suffixValue; } set { suffixValue = value; } }
		[XmlAttribute()]
		public string NickName { get { return nickNameValue; } set { nickNameValue = value; } }
		[XmlAttribute()]
		public string DisplayName { get { return displayNameValue; } set { displayNameValue = value; } }
		[XmlAttribute()]
		public string IMAddress { get { return imAddressValue; } set { imAddressValue = value; } }

		public Address PostalAddress { get { return postalAddressValue; } set { postalAddressValue = value; } }
		public EmailAddress[] EmailAddresses { get { return emailAddressesValue; } set { emailAddressesValue = value; } }
		public PhoneNumber[] PhoneNumbers { get { return phoneNumbersValue; } set { phoneNumbersValue = value; } }
		public DocumentTypes Documents { get { return documentsValue; } set { documentsValue = value; } }
		public BookmarkTypes Bookmarks { get { return bookmarksValue; } set { bookmarksValue = value; } }
	}
	public class Address
	{
		private string streetValue;
		private string cityValue;
		private string stateValue;
		private string postalCodeValue;
		private string countryValue;

		[XmlAttribute()]
		public string Street { get { return streetValue; } set { streetValue = value; } }
		[XmlAttribute()]
		public string City { get { return cityValue; } set { cityValue = value; } }
		[XmlAttribute()]
		public string State { get { return stateValue; } set { stateValue = value; } }
		[XmlAttribute()]
		public string PostalCode { get { return postalCodeValue; } set { postalCodeValue = value; } }
		[XmlAttribute()]
		public string Country { get { return countryValue; } set { countryValue = value; } }
	}
	public class EmailAddress
	{
		private string categoryValue;
		private string addressValue;

		[XmlAttribute("Type")]
		public string Category { get { return categoryValue; } set { categoryValue = value; } }
		[XmlAttribute()]
		public string Address { get { return addressValue; } set { addressValue = value; } }
	}
	public class PhoneNumber
	{
		private string categoryValue;
		private string areaCodeValue;
		private string numberValue;
		private string extensionValue;

		[XmlAttribute("Type")]
		public string Category { get { return categoryValue; } set { categoryValue = value; } }
		[XmlAttribute()]
		public string AreaCode { get { return areaCodeValue; } set { areaCodeValue = value; } }
		[XmlAttribute()]
		public string Number { get { return numberValue; } set { numberValue = value; } }
		[XmlAttribute()]
		public string Extension { get { return extensionValue; } set { extensionValue = value; } }
	}
	public class DocumentTypes
	{
		private Document[] sharedValue;
		private Document[] privateValue;

		public Document[] Shared { get { return sharedValue; } set { sharedValue = value; } }
		public Document[] Private { get { return privateValue; } set { privateValue = value; } }
	}
	public class Document
	{
		private string nameValue;
		private string pathLocationValue;

		[XmlAttribute()]
		public string Name { get { return nameValue; } set { nameValue = value; } }
		[XmlAttribute()]
		public string PathLocation { get { return pathLocationValue; } set { pathLocationValue = value; } }
	}
	public class BookmarkTypes
	{
		private Bookmark[] sharedValue;
		private Bookmark[] privateValue;

		public Bookmark[] Shared { get { return sharedValue; } set { sharedValue = value; } }
		public Bookmark[] Private { get { return privateValue; } set { privateValue = value; } }
	}
	public class Bookmark
	{
		private string nameValue;
		private string pathLocationValue;

		[XmlAttribute()]
		public string Name { get { return nameValue; } set { nameValue = value; } }
		[XmlAttribute()]
		public string PathLocation { get { return pathLocationValue; } set { pathLocationValue = value; } }
	}
#endregion

	public class Household
	{
		private string nameValue;
		private Member[] membersValue;

		[XmlAttribute()]
		public string Name { get { return nameValue; } set { nameValue = value; } }
		[XmlElement("Member")]
		public Member[] Members { get { return membersValue; } set { membersValue = value; } }
	}
	public class Member
	{
		private string displayNameValue;

		[XmlAttribute()]
		public string DisplayName { get { return displayNameValue; } set { displayNameValue = value; } }
	}
	public class Organization
	{
		private string nameValue;
		private Group[] groupsValue;

		[XmlAttribute()]
		public string Name { get { return nameValue; } set { nameValue = value; } }
		public Group[] Groups { get { return groupsValue; } set { groupsValue = value; } }
	}
	public class Group
	{
		private string nameValue;
		private Member[] membersValue;
		private Document[] documentsValue;

		[XmlAttribute()]
		public string Name { get { return nameValue; } set { nameValue = value; } }
		[XmlElement("Member")]
		public Member[] Members { get { return membersValue; } set { membersValue = value; } }
		public Document[] Documents { get { return documentsValue; } set { documentsValue = value; } }
	}
}

