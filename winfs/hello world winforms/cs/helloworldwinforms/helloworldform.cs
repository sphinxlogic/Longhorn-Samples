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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Storage;
using System.Storage.Contacts;
using System.Storage.Core;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	public class HelloWorldForm : System.Windows.Forms.Form
	{
		private ItemContext itemContext=null;
		private int newContactCount=0;
		private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.Panel listPanel;
		private System.Windows.Forms.Button addContactsButton;
		private System.Windows.Forms.Button removeContactsButton;
		private System.Windows.Forms.Button renameContactsButton;
		private System.Windows.Forms.ListBox contactListBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HelloWorldForm()
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
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.removeContactsButton = new System.Windows.Forms.Button();
			this.renameContactsButton = new System.Windows.Forms.Button();
			this.addContactsButton = new System.Windows.Forms.Button();
			this.listPanel = new System.Windows.Forms.Panel();
			this.contactListBox = new System.Windows.Forms.ListBox();
			this.buttonPanel.SuspendLayout();
			this.listPanel.SuspendLayout();
			this.SuspendLayout();

			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.removeContactsButton);
			this.buttonPanel.Controls.Add(this.renameContactsButton);
			this.buttonPanel.Controls.Add(this.addContactsButton);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonPanel.Location = new System.Drawing.Point(0, 233);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(472, 39);
			this.buttonPanel.TabIndex = 1;

			// 
			// removeContactsButton
			// 
			this.removeContactsButton.Location = new System.Drawing.Point(363, 8);
			this.removeContactsButton.Name = "removeContactsButton";
			this.removeContactsButton.Size = new System.Drawing.Size(97, 27);
			this.removeContactsButton.TabIndex = 2;
			this.removeContactsButton.Text = "Remove Contact";
			this.removeContactsButton.Click += new System.EventHandler(this.removeContactsButton_Click);

			// 
			// renameContactsButton
			// 
			this.renameContactsButton.Location = new System.Drawing.Point(188, 8);
			this.renameContactsButton.Name = "renameContactsButton";
			this.renameContactsButton.Size = new System.Drawing.Size(97, 27);
			this.renameContactsButton.TabIndex = 1;
			this.renameContactsButton.Text = "Rename Contact";
			this.renameContactsButton.Click += new System.EventHandler(this.renameContactsButton_Click);

			// 
			// addContactsButton
			// 
			this.addContactsButton.Location = new System.Drawing.Point(13, 8);
			this.addContactsButton.Name = "addContactsButton";
			this.addContactsButton.Size = new System.Drawing.Size(97, 27);
			this.addContactsButton.TabIndex = 0;
			this.addContactsButton.Text = "Add Contact";
			this.addContactsButton.Click += new System.EventHandler(this.addContactsButton_Click);

			// 
			// listPanel
			// 
			this.listPanel.Controls.Add(this.contactListBox);
			this.listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listPanel.Location = new System.Drawing.Point(0, 0);
			this.listPanel.Name = "listPanel";
			this.listPanel.Size = new System.Drawing.Size(472, 233);
			this.listPanel.TabIndex = 2;

			// 
			// contactListBox
			// 
			this.contactListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contactListBox.Location = new System.Drawing.Point(0, 0);
			this.contactListBox.Name = "contactListBox";
			this.contactListBox.Size = new System.Drawing.Size(472, 225);
			this.contactListBox.TabIndex = 1;
			this.contactListBox.SelectedIndexChanged += new System.EventHandler(this.contactListBox_SelectedIndexChanged);

			// 
			// HelloWorldForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 272);
			this.Controls.Add(this.listPanel);
			this.Controls.Add(this.buttonPanel);
			this.Name = "HelloWorldForm";
			this.Text = "Hello World With WinForms";
			this.Load += new System.EventHandler(this.HelloWorldForm_Load);
			this.buttonPanel.ResumeLayout(false);
			this.listPanel.ResumeLayout(false);
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
			Application.Run(new HelloWorldForm());
		}
		private void HelloWorldForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				itemContext = ItemContext.Open();
				ListContacts();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void ShowError(Exception ex)
		{
			addContactsButton.Enabled = false;
			removeContactsButton.Enabled = false;
			renameContactsButton.Enabled = false;
			MessageBoxOptions mbo=0;
			if(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft == true)
				mbo=MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
				
			MessageBox.Show(this, "The application generated an unexpected error: " + ex.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, mbo);
		}

		private void ListContacts()
		{
			try
			{
				renameContactsButton.Enabled = false;
				removeContactsButton.Enabled = false;
				
				FindResult result = itemContext.FindAll(typeof(Person));

				contactListBox.Items.Clear();
				foreach (Person person in result)
				{
					contactListBox.Items.Add(person.DisplayName);
				}
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void AddContact(string name)
		{
			try
			{
				Person newPerson = new Person();
				newPerson.DisplayName = name;

				SmtpEmailAddress email = new SmtpEmailAddress("somecontact@litware.com");
				email.Keywords.Add(new Keyword(StandardKeywords.Primary));
				email.AccessPointType = new Keyword("email");
				email.AccessPoint = email.Address;
				newPerson.EAddresses.Add(email);

				TelephoneNumber phone = new TelephoneNumber("1", "425", "555-1234", "");
				phone.Keywords.Add(new Keyword(StandardKeywords.Home));
				phone.AccessPointType = new Keyword(StandardKeywords.Home);
				phone.AccessPoint = phone.Number.ToString(System.Globalization.CultureInfo.CurrentUICulture);
				newPerson.EAddresses.Add(phone);

				WellKnownFolder personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);
				personalContactsFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.ItemId.ToString());

				itemContext.Update();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		private void DeleteContact(string displayName)
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

		private void RenameContact(string fromName, string toName)
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
		private void addContactsButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				AddContact("New Contact " + newContactCount++);
				ListContacts();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void renameContactsButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (contactListBox.SelectedItem != null)
				{
					RenameContact(contactListBox.SelectedItem.ToString(), "Renamed Contact " + newContactCount++);
					ListContacts();
				}
				else
					renameContactsButton.Enabled = false;
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void removeContactsButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (contactListBox.SelectedItem != null)
				{
					DeleteContact(contactListBox.SelectedItem.ToString());
					ListContacts();
				}
				else
					renameContactsButton.Enabled = false;
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		private void contactListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (contactListBox.SelectedItem != null)
			{
				removeContactsButton.Enabled = true;
				renameContactsButton.Enabled = true;
			}
			else
			{
				removeContactsButton.Enabled = false;
				renameContactsButton.Enabled = false;
			}
		}
	}
}
