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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	/// <summary>
	/// Summary description for PopulateContactsForm.
	/// </summary>
	public class PopulateContactsForm : System.Windows.Forms.Form
	{
		private ItemContext itemContext=null;
		private WellKnownFolder personalContactsFolder=null;
		private readonly string SAMPLEDATAFOLDERNAME="WinFS Beta1 Sample Data Folder";
		private Folder sampleDataFolder=null;

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem importMenuItem;
		private System.Windows.Forms.MenuItem cleanMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.CheckedListBox statusListBox;
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		public PopulateContactsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.importMenuItem = new System.Windows.Forms.MenuItem();
			this.cleanMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.statusListBox = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();

			// 
			// mainMenu1
			// 
			this.mainMenu1.IsImageMarginPresent = true;
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.fileMenuItem
			});
			this.mainMenu1.Name = "mainMenu1";

			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.importMenuItem, this.cleanMenuItem, this.menuItem1, this.exitMenuItem
			});
			this.fileMenuItem.Name = "menuItem1";
			this.fileMenuItem.Text = "&File";

			// 
			// importMenuItem
			// 
			this.importMenuItem.Index = 0;
			this.importMenuItem.Name = "menuItem1";
			this.importMenuItem.Text = "&Import...";
			this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);

			// 
			// cleanMenuItem
			// 
			this.cleanMenuItem.Index = 1;
			this.cleanMenuItem.Name = "menuItem1";
			this.cleanMenuItem.Text = "&Clean";
			this.cleanMenuItem.Click += new System.EventHandler(this.cleanMenuItem_Click);

			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Name = "menuItem1";
			this.menuItem1.Text = "-";

			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 3;
			this.exitMenuItem.Name = "menuItem2";
			this.exitMenuItem.Text = "E&xit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);

			// 
			// statusListBox
			// 
			this.statusListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusListBox.Enabled = false;
			this.statusListBox.Location = new System.Drawing.Point(0, 0);
			this.statusListBox.Name = "statusListBox";
			this.statusListBox.Size = new System.Drawing.Size(292, 259);
			this.statusListBox.TabIndex = 0;

			// 
			// PopulateContactsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.Controls.Add(this.statusListBox);
			this.Menu = this.mainMenu1;
			this.Name = "PopulateContactsForm";
			this.Text = "Sample Import Contacts";
			this.Load += new System.EventHandler(this.PopulateContactsForm_Load);
			this.ResumeLayout(false);
		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new PopulateContactsForm());
		}

		private int AddStatus(string Message)
		{
			int statusIndex=statusListBox.Items.Add(Message, false);
			Application.DoEvents();
			return statusIndex;
		}
		private void StatusComplete(int statusIndex)
		{
			statusListBox.Items[statusIndex] = statusListBox.Items[statusIndex] + " Done!";
			statusListBox.SetItemChecked(statusIndex, true);
			Application.DoEvents();
		}

		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
		private void PopulateContactsForm_Load(object sender, System.EventArgs e)
		{
			int statusIndex=AddStatus("Opening ItemContext...");
			itemContext = ItemContext.Open();
			personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);
			StatusComplete(statusIndex);
		}
		private void importMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "Contact Data (*.xml)|*.xml";
			openDialog.ShowDialog(this);

			if (openDialog.FileName.Length > 0)
			{
				//Import this XML file
				SampleDataNamespace.SampleData newData = SampleDataNamespace.SampleData.Load(openDialog.FileName);

				//SampleDataNamespace.SampleData newData = SampleDataNamespace.SampleData.Load(@"D:\SamplesVBL\PopulateContacts\CS\PopulateContacts\contacts.xml");
				InsertData(newData);
			}
		}

		//This method processes all of the arrays of data and inserts
		//them into "WinFS".
		private void InsertData(SampleDataNamespace.SampleData newData)
		{
			CleanData();

			int statusIndex = AddStatus("Importing data...");

			ItemSearcher folderSearcher = Folder.GetSearcher(itemContext);

			folderSearcher.Filters.Add("DisplayName = @displayName");
			folderSearcher.Parameters.Add("displayName", SAMPLEDATAFOLDERNAME);
			sampleDataFolder = folderSearcher.FindOne((SortOption[])null) as Folder;
			if (sampleDataFolder == null)
			{
				//Create the folder since it does not exist
				UserDataFolder udf = UserDataFolder.FindMyUserDataFolder(itemContext);
				if(udf == null)
					udf = UserDataFolder.CreateMyUserDataFolder(itemContext);

				sampleDataFolder = new Folder();
				sampleDataFolder.DisplayName = SAMPLEDATAFOLDERNAME;
				udf.OutFolderMemberRelationships.AddItem(sampleDataFolder, SAMPLEDATAFOLDERNAME);
				int statusIndexFolder=AddStatus("  Creating new sample data folder...");
				itemContext.Update();
				StatusComplete(statusIndexFolder);
			}

			//Create a new contact for each contact in the XML.
			//Also add them to the sample data folder.
			foreach (SampleDataNamespace.Contact newContact in newData.Contacts)
			{
				Person newPerson = AddContact(newContact);
				sampleDataFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
			}

			int statusIndexPerson = AddStatus("  Saving new persons...");
			itemContext.Update();
			StatusComplete(statusIndexPerson);

			//Create a new organization for each organization in the XML.
			foreach (SampleDataNamespace.Organization newOrganization in newData.Organizations)
			{
				Organization newWinFSOrganization = AddOrganization(newOrganization);

				sampleDataFolder.OutFolderMemberRelationships.AddItem(newWinFSOrganization, newWinFSOrganization.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

				System.Collections.ArrayList Employees = new System.Collections.ArrayList();

				//Create a new group for each group in the organization
				foreach (SampleDataNamespace.Group newGroup in newOrganization.Groups)
				{
					Group newWinFSGroup = AddGroup(newGroup);

					sampleDataFolder.OutFolderMemberRelationships.AddItem(newWinFSGroup, newWinFSGroup.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

					//Find necessary members and add them to the group and organization
					foreach (SampleDataNamespace.Member newMember in newGroup.Members)
					{
						ItemSearcher fmSearcher=FolderMember.GetSearcherGivenFolder(sampleDataFolder);
						fmSearcher.Filters.Add("Name = @displayName");
						fmSearcher.Parameters.Add("displayName", newMember.DisplayName);
						FolderMember folderMember = fmSearcher.FindOne((SortOption[])null) as FolderMember;
						Person groupMember = folderMember.Target as Person;

						newWinFSGroup.OutGroupMembershipRelationships.AddMembers(groupMember, groupMember.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

						//Only add the employee to the organization IF they aren't already added.
						if (Employees.Contains(groupMember.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture)) == false)
						{
							newWinFSOrganization.OutContactRoleRelationships.AddContactTarget(groupMember, groupMember.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
							Employees.Add(groupMember.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
						}
					}
				}
			}

			itemContext.Update();
			StatusComplete(statusIndex);
		}

		private Address CreatePostalAddress(string street, string city, string state, string postalCode)
		{
			Address postalAddress = new Address();

			postalAddress.AddressLine = street;
			postalAddress.PrimaryCity = city;
			postalAddress.AdministrativeDivision = state;
			postalAddress.PostalCode = postalCode;
			return postalAddress;
		}

		private Person AddContact(SampleDataNamespace.Contact contactToAdd)
		{
			Person newPerson = new Person();

			newPerson.DisplayName = contactToAdd.DisplayName;
			FullName name = new FullName();
			name.GivenName = contactToAdd.FirstName;
			name.MiddleName = contactToAdd.MiddleName;
			name.Surname = contactToAdd.LastName;
			name.Nickname = contactToAdd.NickName;
			name.Suffix = contactToAdd.Suffix;
			name.Title = contactToAdd.Title;
			newPerson.PersonalNames.Add(name);

			InstantMessagingAddress newIM = new InstantMessagingAddress();

			newIM.Address = contactToAdd.IMAddress;
			newIM.ProviderUri = "MSN";
			//newPerson.PersonalInstantMessagingAddresses.Add(newIM)

			Location newLocation=new Location();
			newLocation.LocationElements.Add(CreatePostalAddress(contactToAdd.PostalAddress.Street,
				contactToAdd.PostalAddress.City,
				contactToAdd.PostalAddress.State,
				contactToAdd.PostalAddress.PostalCode));

			newPerson.OutContactLocationsRelationships.AddLocations(newLocation, newPerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

//			foreach (SampleDataNamespace.EmailAddress email in contactToAdd.EmailAddresses)
//			{
//				SmtpEmailAddress newEmail = new SmtpEmailAddress(email.Address);
//
//				newEmail.Categories.Add(GetCategory(email.Category));
//				newPerson.PersonalEmailAddresses.Add(newEmail);
//			}
			foreach (SampleDataNamespace.PhoneNumber phone in contactToAdd.PhoneNumbers)
			{
				TelephoneNumber newPhone = new TelephoneNumber("1", phone.AreaCode, phone.Number, phone.Extension);
				newPhone.Keywords.Add(GetKeyword(phone.Category));
				newPerson.EAddresses.Add(newPhone);
			}

			return newPerson;
		}

		public static Organization AddOrganization(SampleDataNamespace.Organization organizationToAdd)
		{
			Organization newOrganization = new Organization();
			newOrganization.DisplayName = organizationToAdd.Name;

			return newOrganization;
		}

		public static Group AddGroup(SampleDataNamespace.Group groupToAdd)
		{
			Group newGroup = new Group();
			newGroup.DisplayName = groupToAdd.Name;

			return newGroup;
		}

		private Keyword GetKeyword(string keyword)
		{
			switch (keyword.ToLower(System.Globalization.CultureInfo.CurrentUICulture))
			{
				case "work":
					return new Keyword(StandardKeywords.Work);
				case "primary" :
					return new Keyword(StandardKeywords.Primary);
				case "home" :
					return new Keyword(StandardKeywords.Home);
			}

			return new Keyword(keyword);
		}
		private void cleanMenuItem_Click(object sender, System.EventArgs e)
		{
			CleanData();
		}

		private void CleanData()
		{
			int statusIndex = AddStatus("Cleaning old sample data...");
			ItemSearcher folderSearcher = Folder.GetSearcher(itemContext);

			folderSearcher.Filters.Add("DisplayName = @displayName");
			folderSearcher.Parameters.Add("displayName", SAMPLEDATAFOLDERNAME);
			sampleDataFolder = folderSearcher.FindOne((SortOption[])null) as Folder;
			if (sampleDataFolder != null)
			{
				UserDataFolder.FindMyUserDataFolder(itemContext).OutFolderMemberRelationships.RemoveItem(sampleDataFolder);
				itemContext.Update();
			}
			StatusComplete(statusIndex);
		}

	}
}
