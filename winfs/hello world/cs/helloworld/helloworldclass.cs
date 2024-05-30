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
using System.Storage;
using System.Storage.Contacts;
using System.Storage.Core;
using System.Runtime.InteropServices;

[assembly:ComVisible(false)]
[assembly:CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	class HelloWorldClass
	{
		private static ItemContext itemContext=null;
		[STAThread]
		static void Main(string[] args)
		{
			using (itemContext = ItemContext.Open())
			{
				Console.WriteLine("Contacts BEFORE addition:");
				ListContacts();
				Console.WriteLine("Press enter to continue!");
				Console.ReadLine();

				AddContact("Shane DeSeranno");
				Console.WriteLine("");
				Console.WriteLine("Contacts AFTER addition:");
				ListContacts();
				Console.WriteLine("Press enter to continue!");
				Console.ReadLine();

				RenameContact("Shane DeSeranno", "Dylan Miller");
				Console.WriteLine("");
				Console.WriteLine("Contacts AFTER modification:");
				ListContacts();
				Console.WriteLine("Press enter to continue!");
				Console.ReadLine();

				DeleteContact("Dylan Miller");
				Console.WriteLine("Contacts AFTER deletion:");
				ListContacts();
			}
		}

		private static void DeleteContact(string displayName)
		{
			WellKnownFolder personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);

			ItemSearcher personSearcher = Person.GetSearcher(itemContext);
			personSearcher.Filters.Add("DisplayName = @DisplayName");
			personSearcher.Parameters.Add("DisplayName", displayName);

			Person deletedPerson = personSearcher.FindOne((SortOption[])null) as Person;
			if(deletedPerson != null)
				personalContactsFolder.OutgoingRelationships.RemoveTarget(deletedPerson);
			else
				Console.WriteLine("Error: There were problems finding the requested contact: {0}", displayName);

			itemContext.Update();
		}

		private static void RenameContact(string fromName, string toName)
		{
			WellKnownFolder personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);
			ItemSearcher personSearcher = Person.GetSearcher(itemContext);

			personSearcher.Filters.Add("DisplayName = @DisplayName");
			personSearcher.Parameters.Add("DisplayName", fromName);

			Person modifiedPerson = personSearcher.FindOne((SortOption[])null) as Person;
			if(modifiedPerson != null)
				modifiedPerson.DisplayName = toName;
			else
				Console.WriteLine("Error: There were problems finding the requested contact: {0}", fromName);
  
			itemContext.Update();
		}

		private static void AddContact(string name)
		{
			Person newPerson = new Person();

			newPerson.DisplayName = name;

			SmtpEmailAddress email = new SmtpEmailAddress("somecontact@litware.com");

			//email.Keywords.Add(new Keyword(StandardKeywords.Primary));
			email.AccessPointType = new Keyword("email");
			email.AccessPoint = email.Address;
			newPerson.EAddresses.Add(email);

			TelephoneNumber phone = new TelephoneNumber("1", "425", "555-1234", "");

			//phone.Keywords.Add(new Keyword(StandardKeywords.Home));
			//phone.AccessPointType = new Keyword(StandardKeywords.Home);
			phone.AccessPoint = phone.Number.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			newPerson.EAddresses.Add(phone);

			WellKnownFolder personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);
			if(personalContactsFolder == null)
				personalContactsFolder = UserDataFolder.CreateMyPersonalContactsFolder(itemContext);

			personalContactsFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.ItemId.ToString());
			itemContext.Update();
		}

		private static void ListContacts()
		{
			System.Console.WriteLine("Displaying contacts...");
			FindResult fr = itemContext.FindAll(typeof(Person));

			bool foundContacts = false;
			foreach (Person p in fr)
			{
				foundContacts = true;
				Console.WriteLine("  {0}", p.DisplayName);
			}
			if(foundContacts==false)
				Console.WriteLine("  No contacts found.");
		}
	}
}
