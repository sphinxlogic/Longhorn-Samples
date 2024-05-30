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
using System.Storage.Locations;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	/// <summary>
	/// Summary description for SearchByType.
	/// </summary>
	class SearchByType
	{
		private static ItemContext itemContext=null;	
		private static readonly string SAMPLEDATAFOLDERNAME = "WinFS Beta1 Sample Data Folder";
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			using (itemContext = ItemContext.Open())
			{
				if (ValidateSampleDataExists() == false)
				{
					Console.WriteLine("This sample requires the sample data to be populated using the 'PopulateContacts' sample.");
					Console.WriteLine("Please run this tool before executing this sample.");
					return;
				}

				FindContacts("Renton");
			}
		}

		private static void FindContacts(string city)
		{
			Console.WriteLine("================== City: {0} ==================", city);

			ItemSearcher personSearcher = Person.GetSearcher(itemContext);

			personSearcher.Filters.Add("OutContactLocationsRelationships.Locations.LocationElements.Cast(System.Storage.Locations.Address).PrimaryCity = @city");
			personSearcher.Parameters.Add("city", city);

			foreach (Person thePerson in personSearcher.FindAll((SortOption[])null))
			{
				Console.WriteLine("Display Name: {0}", thePerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
			}
		}

		private static bool ValidateSampleDataExists()
		{
			ItemSearcher folderSearcher = Folder.GetSearcher(itemContext);

			folderSearcher.Filters.Add("DisplayName = @displayName");
			folderSearcher.Parameters.Add("displayName", SAMPLEDATAFOLDERNAME);
			Folder sampleDataFolder = folderSearcher.FindOne((SortOption[])null) as Folder;
			if(sampleDataFolder != null)
				return true;
			else
				return false;
		}
	}
}
