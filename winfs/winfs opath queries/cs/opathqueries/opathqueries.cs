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

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	/// <summary>
	/// Summary description for OPathQueries.
	/// </summary>
	class OPathQueries
	{
		private static ItemContext itemContext=null;
		private static readonly string SAMPLEDATAFOLDERNAME = "WinFS Beta1 Sample Data Folder";
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				using (itemContext = ItemContext.Open())
				{
					if (ValidateSampleDataExists() == false)
					{
						Console.WriteLine("This sample requires the sample data to be populated using the 'PopulateContacts' sample also shipped with the SDK.");
						Console.WriteLine("Please run this tool before executing this sample.");
						return;
					}

					ItemSearcher personSearcher = Person.GetSearcher(itemContext);
					ItemSearcher groupSearcher = Group.GetSearcher(itemContext);
					FindResult result = personSearcher.FindAll();

					Console.WriteLine("Displaying ALL People:");
					Console.WriteLine("*********************************");
					DisplayItems(result);
					result = groupSearcher.FindAll();
					Console.WriteLine("Displaying ALL Groups:");
					Console.WriteLine("*********************************");
					DisplayItems(result);

					//First demonstrate using the Source
					//To do this we check the source's display name and see if it's the Document Control group
					string pattern = "Source(System.Storage.Contacts.GroupMembership).Group.DisplayName = 'Document Control'";

					personSearcher.Filters.Clear();
					personSearcher.Filters.Add(pattern);
					result = personSearcher.FindAll();
					Console.WriteLine("All contacts in the 'Document Control' group");
					Console.WriteLine("OPath:\n{0}", pattern);
					Console.WriteLine("*********************************");
					DisplayItems(result);

					//Now we get all of the groups that have a Target with the displayname starting with Sean.
					pattern = "Target(System.Storage.Contacts.GroupMembership).Members.DisplayName like 'Sean %'";
					groupSearcher.Filters.Clear();
					groupSearcher.Filters.Add(pattern);
					result = groupSearcher.FindAll();
					Console.WriteLine("All groups that have a 'Sean' in them");
					Console.WriteLine("OPath:\n{0}", pattern);
					Console.WriteLine("*********************************");
					DisplayItems(result);

					//Now we get ALL of the people in ANY groups that have a Target with the displayname starting with Sean.
					pattern = "Source(System.Storage.Contacts.GroupMembership).Group.Target(System.Storage.Contacts.GroupMembership).Members.DisplayName like 'Sean %'";
					personSearcher.Filters.Clear();
					personSearcher.Filters.Add(pattern);
					result = personSearcher.FindAll();
					Console.WriteLine("All contacts that are in groups that have a 'Sean' in them");
					Console.WriteLine("OPath:\n{0}", pattern);
					Console.WriteLine("*********************************");
					DisplayItems(result);

					//Now we get ONLY names that start with M in ANY groups that have a Target with the displayname starting with Sean.
					pattern = "DisplayName like 'M%' and " + pattern;
					personSearcher.Filters.Clear();
					personSearcher.Filters.Add(pattern);
					result = personSearcher.FindAll();
					Console.WriteLine("All contacts whose name starts with 'M'");
					Console.WriteLine("that are in groups that have a 'Sean' in them");
					Console.WriteLine("OPath:\n{0}", pattern);
					Console.WriteLine("*********************************");
					DisplayItems(result);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The application generated an unexpected error: {0}", ex.Message);
			}
		}

		static void DisplayItems(FindResult result)
		{
			foreach (Item i in result)
			{
				Console.WriteLine(i.DisplayName);
			}

			Console.WriteLine("");
			Console.Write("Press ENTER to continue.");
			Console.ReadLine();
		}

		private static bool ValidateSampleDataExists()
		{
			ItemSearcher folderSearcher = Folder.GetSearcher(itemContext);

			folderSearcher.Filters.Add("DisplayName = @displayName");
			folderSearcher.Parameters.Add("displayName", SAMPLEDATAFOLDERNAME);

			Folder sampleDataFolder = folderSearcher.FindOne((SortOption[])null) as Folder;

			if (sampleDataFolder != null)
				return true;
			else
				return false;
		}
	}
}
