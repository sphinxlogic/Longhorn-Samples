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
using System.Storage.Synchronization;
using System.Storage.Contacts;
using System.Storage.Core;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	public class VCardSyncForm : System.Windows.Forms.Form
	{
		private ItemContext ctx = null;
		private System.Windows.Forms.StatusBar status;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button createReplicaButton;
		private System.Windows.Forms.TextBox winfsPath;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox remoteDataPath;
		private System.Windows.Forms.GroupBox setupBox;
		private System.Windows.Forms.GroupBox syncBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox VCFContactList;
		private System.Windows.Forms.ListBox WinFSContactList;
		private System.Windows.Forms.Button deleteReplicaButton;
		private System.Windows.Forms.ComboBox syncType;
		private System.Windows.Forms.Button synchronizeButton;
		private System.Windows.Forms.Button refreshListsButton;
		private System.Windows.Forms.Button createWinFSContactButton;
		private System.Windows.Forms.Button viewContacts;

		private System.Windows.Forms.Button createVCardContactButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public VCardSyncForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.status = new System.Windows.Forms.StatusBar();
			this.setupBox = new System.Windows.Forms.GroupBox();
			this.remoteDataPath = new System.Windows.Forms.TextBox();
			this.winfsPath = new System.Windows.Forms.TextBox();
			this.createReplicaButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.syncBox = new System.Windows.Forms.GroupBox();
			this.createVCardContactButton = new System.Windows.Forms.Button();
			this.viewContacts = new System.Windows.Forms.Button();
			this.refreshListsButton = new System.Windows.Forms.Button();
			this.syncType = new System.Windows.Forms.ComboBox();
			this.synchronizeButton = new System.Windows.Forms.Button();
			this.deleteReplicaButton = new System.Windows.Forms.Button();
			this.createWinFSContactButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.VCFContactList = new System.Windows.Forms.ListBox();
			this.WinFSContactList = new System.Windows.Forms.ListBox();
			this.setupBox.SuspendLayout();
			this.syncBox.SuspendLayout();
			this.SuspendLayout();

// 
// status
// 
			this.status.Location = new System.Drawing.Point(0, 424);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(428, 22);
			this.status.TabIndex = 17;

// 
// setupBox
// 
			this.setupBox.Controls.Add(this.remoteDataPath);
			this.setupBox.Controls.Add(this.winfsPath);
			this.setupBox.Controls.Add(this.createReplicaButton);
			this.setupBox.Controls.Add(this.label5);
			this.setupBox.Controls.Add(this.label8);
			this.setupBox.Location = new System.Drawing.Point(18, 18);
			this.setupBox.Name = "setupBox";
			this.setupBox.Size = new System.Drawing.Size(392, 98);
			this.setupBox.TabIndex = 22;
			this.setupBox.TabStop = false;
			this.setupBox.Text = "Setup";

// 
// remoteDataPath
// 
			this.remoteDataPath.Location = new System.Drawing.Point(107, 39);
			this.remoteDataPath.Name = "remoteDataPath";
			this.remoteDataPath.Size = new System.Drawing.Size(277, 20);
			this.remoteDataPath.TabIndex = 22;
			this.remoteDataPath.Text = "c:\\share";

// 
// winfsPath
// 
			this.winfsPath.Location = new System.Drawing.Point(107, 12);
			this.winfsPath.Name = "winfsPath";
			this.winfsPath.Size = new System.Drawing.Size(278, 20);
			this.winfsPath.TabIndex = 13;

// 
// createReplicaButton
// 
			this.createReplicaButton.Location = new System.Drawing.Point(248, 66);
			this.createReplicaButton.Name = "createReplicaButton";
			this.createReplicaButton.Size = new System.Drawing.Size(136, 24);
			this.createReplicaButton.TabIndex = 7;
			this.createReplicaButton.Text = "Create New Replica";
			this.createReplicaButton.Click += new System.EventHandler(this.createReplicaButton_Click);

// 
// label5
// 
			this.label5.Location = new System.Drawing.Point(6, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 14);
			this.label5.TabIndex = 6;
			this.label5.Text = "WinFSPath";

// 
// label8
// 
			this.label8.Location = new System.Drawing.Point(6, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(98, 14);
			this.label8.TabIndex = 23;
			this.label8.Text = "Remote Data Path";

// 
// syncBox
// 
			this.syncBox.Controls.Add(this.createVCardContactButton);
			this.syncBox.Controls.Add(this.viewContacts);
			this.syncBox.Controls.Add(this.refreshListsButton);
			this.syncBox.Controls.Add(this.syncType);
			this.syncBox.Controls.Add(this.synchronizeButton);
			this.syncBox.Controls.Add(this.deleteReplicaButton);
			this.syncBox.Controls.Add(this.createWinFSContactButton);
			this.syncBox.Controls.Add(this.label2);
			this.syncBox.Controls.Add(this.label1);
			this.syncBox.Controls.Add(this.VCFContactList);
			this.syncBox.Controls.Add(this.WinFSContactList);
			this.syncBox.Enabled = false;
			this.syncBox.Location = new System.Drawing.Point(18, 123);
			this.syncBox.Name = "syncBox";
			this.syncBox.Size = new System.Drawing.Size(392, 288);
			this.syncBox.TabIndex = 23;
			this.syncBox.TabStop = false;
			this.syncBox.Text = "Synchronize";

// 
// createVCardContactButton
// 
			this.createVCardContactButton.Location = new System.Drawing.Point(229, 196);
			this.createVCardContactButton.Name = "createVCardContactButton";
			this.createVCardContactButton.Size = new System.Drawing.Size(145, 23);
			this.createVCardContactButton.TabIndex = 33;
			this.createVCardContactButton.Text = "Create VCard Contact";
			this.createVCardContactButton.Click += new System.EventHandler(this.createVCardContactButton_Click);

// 
// viewContacts
// 
			this.viewContacts.Location = new System.Drawing.Point(77, 12);
			this.viewContacts.Name = "viewContacts";
			this.viewContacts.Size = new System.Drawing.Size(81, 23);
			this.viewContacts.TabIndex = 32;
			this.viewContacts.Text = "Show Contacts";
			this.viewContacts.Click += new System.EventHandler(this.viewContacts_Click);

// 
// refreshListsButton
// 
			this.refreshListsButton.Location = new System.Drawing.Point(163, 55);
			this.refreshListsButton.Name = "refreshListsButton";
			this.refreshListsButton.Size = new System.Drawing.Size(60, 23);
			this.refreshListsButton.TabIndex = 31;
			this.refreshListsButton.Text = "Refresh";
			this.refreshListsButton.Click += new System.EventHandler(this.refreshListsButton_Click);

// 
// syncType
// 
			this.syncType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.syncType.Items.AddRange(new object[] {
				"Send and Receive", "WinFS to VCard Only", "VCard to WinFS Only"
			});
			this.syncType.Location = new System.Drawing.Point(107, 229);
			this.syncType.Name = "syncType";
			this.syncType.Size = new System.Drawing.Size(169, 21);
			this.syncType.TabIndex = 30;

// 
// synchronizeButton
// 
			this.synchronizeButton.Location = new System.Drawing.Point(281, 229);
			this.synchronizeButton.Name = "synchronizeButton";
			this.synchronizeButton.Size = new System.Drawing.Size(93, 23);
			this.synchronizeButton.TabIndex = 29;
			this.synchronizeButton.Text = "Synchronize";
			this.synchronizeButton.Click += new System.EventHandler(this.synchronizeButton_Click);

// 
// deleteReplicaButton
// 
			this.deleteReplicaButton.Location = new System.Drawing.Point(282, 259);
			this.deleteReplicaButton.Name = "deleteReplicaButton";
			this.deleteReplicaButton.Size = new System.Drawing.Size(92, 23);
			this.deleteReplicaButton.TabIndex = 28;
			this.deleteReplicaButton.Text = "Delete Replica";
			this.deleteReplicaButton.Click += new System.EventHandler(this.deleteReplicaButton_Click);

// 
// createWinFSContactButton
// 
			this.createWinFSContactButton.Location = new System.Drawing.Point(13, 196);
			this.createWinFSContactButton.Name = "createWinFSContactButton";
			this.createWinFSContactButton.Size = new System.Drawing.Size(145, 23);
			this.createWinFSContactButton.TabIndex = 23;
			this.createWinFSContactButton.Text = "Create WinFS Contact";
			this.createWinFSContactButton.Click += new System.EventHandler(this.createWinFSContactButton_Click);

// 
// label2
// 
			this.label2.Location = new System.Drawing.Point(13, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(178, 16);
			this.label2.TabIndex = 27;
			this.label2.Text = "WinFS:";

// 
// label1
// 
			this.label1.Location = new System.Drawing.Point(228, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 16);
			this.label1.TabIndex = 26;
			this.label1.Text = "VCard:";

// 
// VCFContactList
// 
			this.VCFContactList.Location = new System.Drawing.Point(228, 42);
			this.VCFContactList.Name = "VCFContactList";
			this.VCFContactList.Size = new System.Drawing.Size(145, 147);
			this.VCFContactList.TabIndex = 25;
			this.VCFContactList.DoubleClick += new System.EventHandler(this.VCFContactList_DoubleClick);

// 
// WinFSContactList
// 
			this.WinFSContactList.Location = new System.Drawing.Point(13, 42);
			this.WinFSContactList.Name = "WinFSContactList";
			this.WinFSContactList.Size = new System.Drawing.Size(145, 147);
			this.WinFSContactList.TabIndex = 24;
			this.WinFSContactList.DoubleClick += new System.EventHandler(this.WinFSContactList_DoubleClick);

// 
// VCardSyncForm
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(428, 446);
			this.Controls.Add(this.syncBox);
			this.Controls.Add(this.setupBox);
			this.Controls.Add(this.status);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "VCardSyncForm";
			this.Text = "VCard Syncronization";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.setupBox.ResumeLayout(false);
			this.setupBox.PerformLayout();
			this.syncBox.ResumeLayout(false);
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
			Application.Run(new VCardSyncForm());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			string machineSpecificName = Environment.MachineName;

			using (ItemContext tmpCtx = ItemContext.Open())
			{
				WellKnownFolder personalContacts = UserDataFolder.FindMyPersonalContactsFolder(tmpCtx);

				if (personalContacts == null)
					personalContacts = UserDataFolder.CreateMyPersonalContactsFolder(tmpCtx);

				winfsPath.Text = @"\\" + machineSpecificName + @"\defaultStore"  + personalContacts.ShellPath;
				syncType.SelectedIndex = 0;
			}

			ctx = ItemContext.Open(winfsPath.Text);
			Replica replica = FindReplica(ctx);

			if (replica != null)
			{
				//This replica already exists
				setupBox.Enabled = false;
				LoadContacts();
				syncBox.Enabled = true;
			}
			else
				ctx.Close();	//We don't need this open yet
		}

		private void LoadContacts()
		{
			Folder localFolder=ctx.FindByPath(typeof(Folder), @"\") as Folder;
			ItemSearcher srch = FolderMember.GetItemSearcher(localFolder, typeof(Person));
			FindResult result = srch.FindAll();

			WinFSContactList.Items.Clear();
			foreach (Person p in result)
			{
				WinFSContactList.Items.Add(new BoundPerson(p));
			}

			string[] files = System.IO.Directory.GetFiles(remoteDataPath.Text, "*.vcf");

			VCFContactList.Items.Clear();
			foreach (string s in files)
			{
				VCFContactList.Items.Add(new VCard(s));
			}
		}

		private string TrimWinFSShare(string path, bool getRoot)
		{
			int start = path.IndexOf(@"\", 3);

			start = path.IndexOf(@"\", start + 1);
			if (getRoot)
				return path.Substring(0, start);
			else
				return path.Substring(start + 1);
		}

		private void createReplicaButton_Click(object sender, System.EventArgs e)
		{
			status.Text = "Creating replica...";
			ctx = ItemContext.Open(TrimWinFSShare(winfsPath.Text, true));

			//Disable while replica exists, once replica is deleted, re-enable
			setupBox.Enabled = false;

			//Create the remote location to store the VCards
			if (System.IO.Directory.Exists(remoteDataPath.Text) == false)
				System.IO.Directory.CreateDirectory(remoteDataPath.Text);

			FindFolder(ctx, TrimWinFSShare(winfsPath.Text, false));
			ctx.Close();
			ctx = ItemContext.Open(winfsPath.Text);
			Replica replica = FindReplica(ctx);

			if (replica == null)
			{
				//Create new replica
				Guid replicaId = Guid.NewGuid();
				Folder localFolder = FindFolder(ctx, @"\");

				replica = Replica.CreateReplica(ctx, localFolder.ItemId, replicaId, VCardAdapter.DEFAULTCOMMUNITY);
			}

			LoadContacts();

			syncBox.Enabled = true;
			status.Text = "Replica created!";
		}

		private Replica FindReplica(ItemContext ctx)
		{
			ItemSearcher replicaSearcher = Replica.GetSearcher(ctx);

			replicaSearcher.Filters.Add("CommunityId = @communityId");
			replicaSearcher.Parameters.Add("communityId", VCardAdapter.DEFAULTCOMMUNITY);

			Replica replica = replicaSearcher.FindOne() as Replica;

			return replica;
		}

		private Folder FindFolder(ItemContext ctx, string folderPath)
		{
			//Locate the WinFS Folder to store the contacts and replica
			Folder localFolder = ctx.FindByPath(typeof(Folder), folderPath) as Folder;

			if (localFolder == null)
			{
				localFolder = new Folder();
				localFolder.DisplayName = folderPath;
				Folder.GetRootFolder(ctx).OutFolderMemberRelationships.AddItem(localFolder, folderPath);
				ctx.Update();
			}

			return localFolder;
		}

		private void deleteReplicaButton_Click(object sender, System.EventArgs e)
		{
			Replica replica = FindReplica(ctx);

			status.Text = "Deleting replica...";
			syncBox.Enabled = false;
			setupBox.Enabled = true;

			Folder localFolder = FindFolder(ctx, @"\");
			localFolder.OutFolderMemberRelationships.RemoveItem(replica);
			ctx.Update();
			status.Text = "Replica deleted!";
		}

		private SynchronizationProfile CreateProfile()
		{
			SynchronizationProfile profile = new SynchronizationProfile();

			profile.AdapterType = typeof(VCardAdapter);
			profile.RemoteEndpoint = remoteDataPath.Text;
			switch (syncType.SelectedItem.ToString())
			{
				case "Send and Receive":
					profile.SynchronizationType = SynchronizationType.SendAndReceive;
					break;

				case "WinFS to VCard Only":
					profile.SynchronizationType = SynchronizationType.Send;
					break;

				case "VCard to WinFS Only":
					profile.SynchronizationType = SynchronizationType.Receive;
					break;
			}
			profile.ConflictPolicy = new ConflictResolverConfiguration(ConflictResolverResolutionType.LocalChangeWins);

			LocalEndpoint le = new LocalEndpoint();
			Replica replica = FindReplica(ctx);

			le.ReplicaId = replica.ReplicaId;
			le.WinfsSharePath = winfsPath.Text;
			profile.LocalEndpoint = le;

			return profile;
		}

		private void synchronizeButton_Click(object sender, System.EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			syncBox.Enabled = false;

			status.Text = "Synchronization started...";
	
			SynchronizationEvents se = new SynchronizationEvents();
			se.SynchronizeCompleted += new SynchronizeCompletedEventHandler(se_SynchronizeCompleted);

			SynchronizationRequest request = new SynchronizationRequest();
			request.SynchronizationEvents = se;
			request.Initialize(CreateProfile());
			request.SynchronizeAsync();
		}

		private void SyncCompleted(object sender, SynchronizeCompletedEventArgs e)
		{
			Cursor = Cursors.Default;
			if (e.Error == null)
				status.Text = "Synchronization Complete";
			else
			{
				status.Text = "Synchronization Failed";
				MessageBox.Show(this, e.Error.ToString(), "Failed to synchronize!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			LoadContacts();
			syncBox.Enabled = true;
		}

		private void se_SynchronizeCompleted(object sender, SynchronizeCompletedEventArgs e)
		{
			this.Invoke(new SynchronizeCompletedEventHandler(SyncCompleted), sender, e);
		}

		private void createWinFSContactButton_Click(object sender, System.EventArgs e)
		{
			status.Text = "Creating contact...";

			Person p = new Person();

			p.DisplayName = "WinFS Contact";
			
			EditDialog edit=new EditDialog(p);

			edit.ShowDelete = false;
			if (edit.ShowDialog() == DialogResult.OK)
			{
				Folder localFolder = FindFolder(ctx, @"\");

				localFolder.OutFolderMemberRelationships.AddItem(p, p.ItemId.ToString());
				ctx.Update();

				status.Text = "Contact created!";
				LoadContacts();
			}
			else
			{
				//Cancel pressed
				status.Text = "No contact created.";
			}
		}

		private void refreshListsButton_Click(object sender, System.EventArgs e)
		{
			LoadContacts();
		}

		private void WinFSContactList_DoubleClick(object sender, System.EventArgs e)
		{
			if (WinFSContactList.SelectedItem != null)
			{
				BoundPerson editPerson = WinFSContactList.SelectedItem as BoundPerson;
				EditDialog editDialog = new EditDialog(editPerson.innerPerson);
				DialogResult result = editDialog.ShowDialog();

				switch (result)
				{
					case DialogResult.OK:
						//Save the Person
						ctx.Update();
						LoadContacts();
						break;

					case DialogResult.Yes:
						Folder localFolder = FindFolder(ctx,@"\");
						localFolder.OutFolderMemberRelationships.RemoveItem(editPerson.innerPerson);
						ctx.Update();
						LoadContacts();
						break;
				}
			}
		}

		private void VCFContactList_DoubleClick(object sender, System.EventArgs e)
		{
			if (VCFContactList.SelectedItem != null)
			{
				VCard editCard = VCFContactList.SelectedItem as VCard;
				EditDialog editDialog = new EditDialog(editCard);
				DialogResult result = editDialog.ShowDialog();

				switch (result)
				{
					case DialogResult.OK:
						//Save the VCard
						editCard.Save();
						LoadContacts();
						break;

					case DialogResult.Yes:
						editCard.Delete();
						LoadContacts();
						break;
				}
			}
		}

		private void viewContacts_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process shellView = new System.Diagnostics.Process();
			shellView.StartInfo.FileName=@"library:contact\PersonalContacts";
			shellView.Start();
		}

		private void createVCardContactButton_Click(object sender, System.EventArgs e)
		{
			status.Text = "Creating contact...";

			string tmpFileName=System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName()) + ".vcf";
			VCard newVCard = new VCard(System.IO.Path.Combine(remoteDataPath.Text, tmpFileName));
			newVCard.DisplayName = "VCard Contact";
			newVCard.EMail= "someone@microsoft.com";

			EditDialog edit = new EditDialog(newVCard);

			edit.ShowDelete = false;
			if (edit.ShowDialog() == DialogResult.OK)
			{
				newVCard.Save();
				status.Text = "Contact created!";
				LoadContacts();
			}
			else
			{
				//Cancel pressed
				status.Text = "No contact created.";
			}
		}
	}
	internal class BoundPerson
	{
		public Person innerPerson =null;
		public BoundPerson(Person person)
		{
			innerPerson = person;
		}

		public override string ToString()
		{
			return innerPerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture);
		}

	}
}
