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
	public class StoreWatcherForm : System.Windows.Forms.Form
	{
		private ItemContext itemContext=null;
		private WellKnownFolder personalContactsFolder=null;
		private System.Windows.Forms.ListBox contactsListBox;
		private StoreWatcher watcher=null;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StoreWatcherForm()
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
			this.contactsListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();

			// 
			// contactsListBox
			// 
			this.contactsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contactsListBox.Location = new System.Drawing.Point(0, 0);
			this.contactsListBox.Name = "contactsListBox";
			this.contactsListBox.Size = new System.Drawing.Size(292, 264);
			this.contactsListBox.TabIndex = 0;

			// 
			// StoreWatcherForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.Controls.Add(this.contactsListBox);
			this.Name = "StoreWatcherForm";
			this.Text = "Store Watcher";
			this.Load += new System.EventHandler(this.StoreWatcherForm_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.StoreWatcherForm_Closing);
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
			Application.Run(new StoreWatcherForm());
		}
		private void StoreWatcherForm_Load(object sender, System.EventArgs e)
		{
			itemContext = ItemContext.Open();
			personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext);

			StoreWatcherOptions watcherOptions = new StoreWatcherOptions();
			watcherOptions.Depth = WatcherDepth.ItemAndImmediateDecendents;
			watcherOptions.NotifyAdded = true;
			watcherOptions.NotifyModified = true;
			watcherOptions.NotifyRemoved = true;
			watcherOptions.RelationshipTypeToWatch = typeof(FolderMember);
			
			watcher = new StoreWatcher(itemContext, personalContactsFolder, watcherOptions);
			watcher.StoreObjectChanged += new StoreEventHandler(ItemChangedEvent);

			ListContacts();
		}
		private delegate void ListContactsDelegate();
		private void ItemChangedEvent(object sender, StoreEventArgs args)
		{
			this.Invoke(new ListContactsDelegate(ListContacts));
		}
		private void ListContacts()
		{
			contactsListBox.Items.Clear();
			
			ItemSearcher searcher = FolderMember.GetItemSearcher(personalContactsFolder, typeof(Person));
			foreach (Person p in searcher.FindAll((SortOption[])null))
			{
				contactsListBox.Items.Add(p.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
			}
		}
		private void StoreWatcherForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			watcher.StoreObjectChanged -= new StoreEventHandler(ItemChangedEvent);
		}
	}
}
