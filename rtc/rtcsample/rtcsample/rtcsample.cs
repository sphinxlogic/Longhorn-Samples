using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using System.Collaboration;
using Microsoft.Collaboration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    public class RtcSample : Form
    {
        public RtcSample()
        {
            //
            // Required for Windows Form Designer support
            //
            this.InitializeComponent();

            this.onlineMenu.Click += new EventHandler(this.onlineMenu_Click);
            this.appearOfflineMenu.Click += new EventHandler(this.appearOfflineMenu_Click);

            this.publishPresenceMenu.Click += new EventHandler(this.publishPresenceMenuItem_Click);
            this.recallPresenceMenuItem.Click += new EventHandler(this.recallPresenceMenuItem_Click);
            this.viewPresenceMenu.Click += new EventHandler(this.viewPresenceMenu_Click);
            this.actionsRemoveContactMenu.Click += new System.EventHandler(this.removeContactMenu_Click);
            this.actionsAddContactMenu.Click += new System.EventHandler(this.addContactMenu_Click);
            this.actionsChatMenu.Click += new System.EventHandler(this.chat_Click);

            this.contextMenuViewBuddyInformation.Click += new EventHandler(this.viewPresenceMenu_Click);
            this.contextMenuRemoveBuddy.Click += new EventHandler(this.removeContactMenu_Click);
            this.contextMenuChat.Click += new EventHandler(this.chat_Click);

            this.contextMenu.Popup += new EventHandler(this.popupEventHandler);

            this.Closing += new CancelEventHandler(RtcSample_Closing);

            //the customized rich presence to be published
            this.presence =  new TestPresence();

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            //Create RTC Provider. This is the factory of RTC profile.
            this.profileProvider = new RtcProvider();
            ////
            // End Microsoft.Collaboration Functionality Code
            ////

            this.EnableMenus(false);

            this.Show();
            this.BringToFront();

            //Start sign in process
            this.signInMenu_Click(this, new EventArgs());
        }

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
            this.contactsView = new System.Windows.Forms.TreeView();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.fileSignInMenu = new System.Windows.Forms.MenuItem();
            this.fileSignOutMenu = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.fileMyStatusMenu = new System.Windows.Forms.MenuItem();
            this.onlineMenu = new System.Windows.Forms.MenuItem();
            this.busyMenu = new System.Windows.Forms.MenuItem();
            this.beRightBackMenu = new System.Windows.Forms.MenuItem();
            this.awayMenu = new System.Windows.Forms.MenuItem();
            this.onThePhoneMenu = new System.Windows.Forms.MenuItem();
            this.outToLunchMenu = new System.Windows.Forms.MenuItem();
            this.appearOfflineMenu = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.fileExitMenu = new System.Windows.Forms.MenuItem();
            this.actionsMenu = new System.Windows.Forms.MenuItem();
            this.actionsAddContactMenu = new System.Windows.Forms.MenuItem();
            this.actionsRemoveContactMenu = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.publishPresenceMenu = new System.Windows.Forms.MenuItem();
            this.recallPresenceMenuItem = new System.Windows.Forms.MenuItem();
            this.viewPresenceMenu = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.actionsChatMenu = new System.Windows.Forms.MenuItem();
            this.helpMenu = new System.Windows.Forms.MenuItem();
            this.helpAboutMenu = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanelLabel = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelText = new System.Windows.Forms.StatusBarPanel();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.contextMenuViewBuddyInformation = new System.Windows.Forms.MenuItem();
            this.contextMenuChat = new System.Windows.Forms.MenuItem();
            this.contextMenuRemoveBuddy = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelText)).BeginInit();
            this.SuspendLayout();
            // 
            // contactsView
            // 
            this.contactsView.ContextMenu = this.contextMenu;
            this.contactsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactsView.ImageIndex = -1;
            this.contactsView.Location = new System.Drawing.Point(0, 0);
            this.contactsView.Name = "contactsView";
            this.contactsView.SelectedImageIndex = -1;
            this.contactsView.Size = new System.Drawing.Size(272, 369);
            this.contactsView.Sorted = true;
            this.contactsView.TabIndex = 8;
            this.contactsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contactsView_AfterSelect);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.fileMenu,
                                                                                     this.actionsMenu,
                                                                                     this.helpMenu});
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.fileSignInMenu,
                                                                                     this.fileSignOutMenu,
                                                                                     this.menuItem1,
                                                                                     this.fileMyStatusMenu,
                                                                                     this.menuItem2,
                                                                                     this.fileExitMenu});
            this.fileMenu.Text = "&File";
            // 
            // fileSignInMenu
            // 
            this.fileSignInMenu.Enabled = false;
            this.fileSignInMenu.Index = 0;
            this.fileSignInMenu.Text = "Sign &In...";
            this.fileSignInMenu.Click += new System.EventHandler(this.signInMenu_Click);
            // 
            // fileSignOutMenu
            // 
            this.fileSignOutMenu.Enabled = false;
            this.fileSignOutMenu.Index = 1;
            this.fileSignOutMenu.Text = "Si&gn Out";
            this.fileSignOutMenu.Click += new System.EventHandler(this.signOutMenu_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // fileMyStatusMenu
            // 
            this.fileMyStatusMenu.Index = 3;
            this.fileMyStatusMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                             this.onlineMenu,
                                                                                             this.busyMenu,
                                                                                             this.beRightBackMenu,
                                                                                             this.awayMenu,
                                                                                             this.onThePhoneMenu,
                                                                                             this.outToLunchMenu,
                                                                                             this.appearOfflineMenu});
            this.fileMyStatusMenu.Text = "&My Status";
            // 
            // onlineMenu
            // 
            this.onlineMenu.Index = 0;
            this.onlineMenu.RadioCheck = true;
            this.onlineMenu.Text = "&Online";
            // 
            // busyMenu
            // 
            this.busyMenu.Index = 1;
            this.busyMenu.Text = "&Busy";
            this.busyMenu.Visible = false;
            // 
            // beRightBackMenu
            // 
            this.beRightBackMenu.Index = 2;
            this.beRightBackMenu.Text = "B&e Right Back";
            this.beRightBackMenu.Visible = false;
            // 
            // awayMenu
            // 
            this.awayMenu.Index = 3;
            this.awayMenu.Text = "&Away";
            this.awayMenu.Visible = false;
            // 
            // onThePhoneMenu
            // 
            this.onThePhoneMenu.Index = 4;
            this.onThePhoneMenu.Text = "On The &Phone";
            this.onThePhoneMenu.Visible = false;
            // 
            // outToLunchMenu
            // 
            this.outToLunchMenu.Index = 5;
            this.outToLunchMenu.Text = "Out To &Lunch";
            this.outToLunchMenu.Visible = false;
            // 
            // appearOfflineMenu
            // 
            this.appearOfflineMenu.Index = 6;
            this.appearOfflineMenu.RadioCheck = true;
            this.appearOfflineMenu.Text = "Appear O&ffline";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Text = "-";
            // 
            // fileExitMenu
            // 
            this.fileExitMenu.Index = 5;
            this.fileExitMenu.Text = "E&xit";
            this.fileExitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // actionsMenu
            // 
            this.actionsMenu.Index = 1;
            this.actionsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.actionsAddContactMenu,
                                                                                        this.actionsRemoveContactMenu,
                                                                                        this.menuItem3,
                                                                                        this.publishPresenceMenu,
                                                                                        this.recallPresenceMenuItem,
                                                                                        this.viewPresenceMenu,
                                                                                        this.menuItem4,
                                                                                        this.actionsChatMenu});
            this.actionsMenu.Text = "&Actions";
            // 
            // actionsAddContactMenu
            // 
            this.actionsAddContactMenu.Enabled = false;
            this.actionsAddContactMenu.Index = 0;
            this.actionsAddContactMenu.Text = "&Add contact...";
            // 
            // actionsRemoveContactMenu
            // 
            this.actionsRemoveContactMenu.Enabled = false;
            this.actionsRemoveContactMenu.Index = 1;
            this.actionsRemoveContactMenu.Text = "&Remove contact";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // publishPresenceMenu
            // 
            this.publishPresenceMenu.Index = 3;
            this.publishPresenceMenu.Text = "&Publish my information...";
            // 
            // recallPresenceMenuItem
            // 
            this.recallPresenceMenuItem.Index = 4;
            this.recallPresenceMenuItem.Text = "&Recall my information";
            // 
            // viewPresenceMenu
            // 
            this.viewPresenceMenu.Index = 5;
            this.viewPresenceMenu.Text = "&View buddy information...";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 6;
            this.menuItem4.Text = "-";
            // 
            // actionsChatMenu
            // 
            this.actionsChatMenu.Index = 7;
            this.actionsChatMenu.Text = "&Chat...";
            // 
            // helpMenu
            // 
            this.helpMenu.Index = 2;
            this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.helpAboutMenu});
            this.helpMenu.Text = "&Help";
            // 
            // helpAboutMenu
            // 
            this.helpAboutMenu.Index = 0;
            this.helpAboutMenu.Text = "&About...";
            this.helpAboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 347);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                         this.statusBarPanelLabel,
                                                                                         this.statusBarPanelText});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(272, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 13;
            // 
            // statusBarPanelLabel
            // 
            this.statusBarPanelLabel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanelLabel.Text = "No profile.";
            this.statusBarPanelLabel.Width = 206;
            // 
            // statusBarPanelText
            // 
            this.statusBarPanelText.Text = "Not logged on";
            this.statusBarPanelText.Width = 80;
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.contextMenuRemoveBuddy,
                                                                                        this.contextMenuViewBuddyInformation,
                                                                                        this.contextMenuChat});
            // 
            // contextMenuViewBuddyInformation
            // 
            this.contextMenuViewBuddyInformation.Index = 1;
            this.contextMenuViewBuddyInformation.Text = "View buddy information";
            // 
            // contextMenuChat
            // 
            this.contextMenuChat.Index = 2;
            this.contextMenuChat.Text = "Chat";
            // 
            // contextMenuRemoveBuddy
            // 
            this.contextMenuRemoveBuddy.Index = 0;
            this.contextMenuRemoveBuddy.Text = "Remove buddy";
            // 
            // RtcSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(272, 369);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.contactsView);
            this.Menu = this.mainMenu;
            this.Name = "RtcSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTC Sample ";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelText)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        [STAThread]
        static void Main() 
        {
            Application.Run(new RtcSample());
        }

        private void signInMenu_Click(object sender, EventArgs e)
        {
            //Get profile configuration from the user and crete RtcProfile
            ConfigurationDialog cd = new ConfigurationDialog();
            DialogResult        result = cd.ShowDialog(this);

            if(result == DialogResult.OK)
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                CollabConfiguration configuration = new CollabConfiguration();
                
                //Configure User information
                configuration.realTimeAddress = cd.signInName.Text;
                configuration.networkCredential = 
                    new NetworkCredential(
                    cd.userName.Text, 
                    cd.password.Text, 
                    null);
                
                //Configure real time server information
                configuration.profileConfiguration = new RtcProfileConfiguration();
                
                if (cd.serverName.Text != null && cd.serverName.Text.Length !=0)
                {
                    configuration.profileConfiguration.Servers.Add( 
                        new RtcRealTimeServer(
                        cd.serverName.Text,
                        RtcRealTimeServer.RoleLogInServer));
                }

                //Configure transport and authentication protocol
                String transport = (String) cd.transportComboBox.SelectedItem;
                configuration.profileConfiguration.Transport = 
                    (Microsoft.Collaboration.TransportType) Enum.Parse(typeof(Microsoft.Collaboration.TransportType), transport, true);

                String auth = (String) cd.authenticationComboBox.SelectedItem;
                configuration.profileConfiguration.AuthenticationProtocols = 
                    (Microsoft.Collaboration.AuthenticationProtocols) Enum.Parse(typeof(Microsoft.Collaboration.AuthenticationProtocols), auth, true);

                configuration.autoAcceptChat = cd.autoAcceptChat.Checked;

                this.CreateProfile(configuration);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            //Logon from the profile
            if (this.myProfile != null)
            {
                //Listen to LogOnStateChanged Event
                this.myProfile.LogOnStateChanged += new LogOnStateChangedEventHandler(this.LogOnState_LogOnStateChanged);
                this.myProfile.LogOn();
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void signOutMenu_Click(object sender, EventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            if (this.myProfile != null)
            {
                this.myProfile.LogOff();
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void onlineMenu_Click(object sender, EventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ProfilePresence profilePresence = this.myProfile.Presence;
            profilePresence.LocalPresence.PresenceState = PresenceState.Online;
            UpdateMyStatusMenu();

            //Update local presence information
            profilePresence.BeginUpdateLocalPresence(
                new AsyncCallback(this.UpdateLocalPresenceCallback), 
                profilePresence);
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void appearOfflineMenu_Click(object sender, EventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ProfilePresence profilePresence = this.myProfile.Presence;
            profilePresence.LocalPresence.PresenceState = PresenceState.Offline;
            UpdateMyStatusMenu();

            //Update local presence information
            profilePresence.BeginUpdateLocalPresence(
                new AsyncCallback(this.UpdateLocalPresenceCallback), 
                profilePresence);
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addContactMenu_Click(object sender, EventArgs e)
        {
            AddContactDialog    acd = new AddContactDialog(this.myProfile, null);
            DialogResult        result = acd.ShowDialog(this);

            if(result == DialogResult.OK)
            {
                try
                {
                    ////
                    // Begin Microsoft.Collaboration Functionality Code
                    ////
                    ContactCollection   contacts;
                    Contact             contact;

                    contacts = this.myProfile.PersistedData.ContactsSubscription.Contacts;

                    //Create new contact
                    contact = contacts.CreateContact(
                        acd.realTimeAddress.Text, 
                        acd.realTimeAddress.Text,
                        true);

                    //Add new contact
                    IAsyncResult ar = contacts.BeginAdd(
                        contact, 
                        new AsyncCallback(this.ContactAddCallback), 
                        contacts);
                    ////
                    // End Microsoft.Collaboration Functionality Code
                    ////
                }
                catch(Exception ex)
                {
                    MessageBox.Show(
                        "Error adding contact.\n" + ex.ToString(), 
                        "Error");
                }
            }
        }

        private void removeContactMenu_Click(object sender, EventArgs e)
        {
            Contact             contact;

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ///
            //Find the contact to be removed
            contact = this.myProfile.PersistedData.ContactsSubscription.Contacts.Find(
                this.contactsView.SelectedNode.Text);

            if(contact != null)
            {
                //Remove the contact
                this.myProfile.PersistedData.ContactsSubscription.Contacts.BeginRemove(
                    contact, 
                    new AsyncCallback(this.ContactRemoveCallback), 
                    this.myProfile.PersistedData.ContactsSubscription.Contacts);
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void publishPresenceMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationPresenceDialog apd = new ApplicationPresenceDialog(this.presence, true);
            DialogResult        result = apd.ShowDialog(this);

            if(result == DialogResult.OK)
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                ///
                //Remove the rich presence published earlier
                ProfilePresence profilePresence = this.myProfile.Presence;
                try
                {
                    profilePresence.LocalPresence.Remove(presence);
                }
                catch
                {
                }

                //Create new rich presence
                this.presence.Home = apd.homeTextBox.Text;
                this.presence.Email = apd.emailTextBox.Text;
                this.presence.Information = apd.informationRichTextBox.Text;
                this.presence.TestString = apd.applicationString.Text;

                try
                {
                    this.presence.TestValue = Convert.ToInt32(
                        apd.applicationValue.Text, 
                        10);
                }
                catch
                {
                    presence.TestValue = 0;
                }

                //Publish the new presence
                profilePresence.LocalPresence.Add(presence);
                profilePresence.BeginUpdateLocalPresence(
                    new AsyncCallback(this.UpdateLocalPresenceCallback), 
                    profilePresence);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
        }

        private void recallPresenceMenuItem_Click(object sender, EventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ProfilePresence profilePresence = this.myProfile.Presence;

            //Remove the rich presence published
            try
            {
                profilePresence.LocalPresence.Remove(presence);
            }
            catch
            {
            }

            profilePresence.BeginUpdateLocalPresence(
                new AsyncCallback(this.UpdateLocalPresenceCallback), 
                profilePresence);
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void viewPresenceMenu_Click(object sender, EventArgs e)
        {
            TestPresence[]  presence = null;

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            if (this.contactsView.SelectedNode is PresenceSubscriptionNode)
            {
                //show rich presence of the subscriber
                PresenceSubscription presenceSubscription;

                presenceSubscription = ((PresenceSubscriptionNode)
                    this.contactsView.SelectedNode).PresenceSubscription;
                if(presenceSubscription != null)
                {
                    presence = (TestPresence [])
                        presenceSubscription.Presence.GetApplicationPresence(
                        TestPresence.TestPresencePublisher, 
                        typeof(TestPresence));
                }
            }
            else
            {
                //show rich presence of the end point
                EndpointPresence endpointPresence;

                endpointPresence = (EndpointPresence)
                    (this.contactsView.SelectedNode.Tag);
                if(endpointPresence != null)
                {
                    presence = (TestPresence [])
                        endpointPresence.GetApplicationPresence(
                        TestPresence.TestPresencePublisher, 
                        typeof(TestPresence));
                }
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
            
            if(presence != null && presence.Length > 0)
            {
                // Show first presence
                ApplicationPresenceDialog apd;

                apd = new ApplicationPresenceDialog(presence[0], false);
                apd.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(
                    this, 
                    this.contactsView.SelectedNode.Text
                    + " doesn't publish application presence.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void chat_Click(object sender, EventArgs e)
        {
            if (myProfile != null)
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                // Create the signaling session from myAccount.
                SignalingSession signalingSession = 
                    myProfile.Signaling.CreateSession(
                    "InstantMessaging", 
                    Guid.NewGuid().ToString(), 
                    null);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////

                ChatForm        chatForm = null;
                String rta = null;

                if(contactsView.SelectedNode != null)
                {
                    PresenceSubscriptionNode node = this.contactsView.SelectedNode as PresenceSubscriptionNode;

                    if (node != null)
                    {
                        rta = node.PresenceSubscription.RealTimeAddress;
                    }
                }

                if (rta != null)
                {
                    chatForm = new ChatForm(myProfile, signalingSession, rta);
                }
                else
                {
                    chatForm = new ChatForm(myProfile, signalingSession);
                }

                if (chatForm != null)
                {
                    chatForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Please sign in first!");
            }
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this, 
                "RTC sample V2.\n"
                + "\n"
                + "Copyright (c) 2003 Microsoft Corp.",
                "About RTCSample",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void RtcSample_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            if (this.myProfile != null)
            {
                //Destroy the profile and release resource
                this.profileProvider.RemoveRealTimeProfile(this.myProfile);
                this.myProfile = null;
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void contactsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.actionsRemoveContactMenu.Enabled = false;
            this.viewPresenceMenu.Enabled = false;

            if (e.Node is PresenceSubscriptionNode)
            {
                this.actionsRemoveContactMenu.Enabled = true;
            }

            if (e.Node != null)
            {
                this.viewPresenceMenu.Enabled = true;
            }
        }

        private void popupEventHandler(Object sender, EventArgs e)
        {
            this.contextMenu.MenuItems.Clear();

            if (this.contextMenu.SourceControl == this.contactsView)
            {
                if (this.contactsView.SelectedNode is PresenceSubscriptionNode)
                {
                    this.contextMenu.MenuItems.Add(this.contextMenuRemoveBuddy);
                    this.contextMenu.MenuItems.Add(this.contextMenuViewBuddyInformation);
                    this.contextMenu.MenuItems.Add(this.contextMenuChat);
                }
            }
        }


        #region Private helpers
        private void AddAccessRule(String userUri, EntityType entityType, AccessRole entityRole)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            AccessRule      rule;
            AccessRuleCollection rules;

            //Create Access Rule to allow everyone to subscribe own presence
            rules = this.myProfile.PersistedData.AccessRulesSubscription.AccessRules;
            rule = rules.CreateAccessRule(
                userUri, 
                entityType, 
                EntityAccessMode.Unknown, 
                entityRole);

            try
            {
                //Add the Access Rule
                rules.BeginAdd(
                    rule, 
                    new AsyncCallback(this.AddAccessRuleCallback), 
                    rules);
            }
            catch
            {
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void CreateProfile(CollabConfiguration     configuration)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            this.Cursor = Cursors.WaitCursor;

            if (this.myProfile != null)
            {
                //Destroy the previous profile and release resource
                this.profileProvider.RemoveRealTimeProfile(this.myProfile);
                this.myProfile = null;
            }

            try
            {
                // Create profile
                this.myProfile = (RtcProfile) profileProvider.CreateRealTimeProfile(
                    configuration.realTimeAddress, 
                    configuration.networkCredential, 
                    configuration.profileConfiguration);

                // Add event handlers to the profile
                this.myProfile.Presence.LocalPresenceChanged += new EventHandler(Presence_LocalPresenceChanged);

                this.myProfile.Presence.PresenceSubscriptions.ItemAdded += 
                    new CollectionEventHandler(this.PresenceSubscriptions_CollectionAdded);
                this.myProfile.Presence.PresenceSubscriptions.ItemRemoved += 
                    new CollectionEventHandler(this.PresenceSubscriptions_CollectionRemoved);
                this.myProfile.Presence.PresenceSubscriptions.Cleared += 
                    new EventHandler(this.PresenceSubscriptions_CollectionCleared);

                this.myProfile.Signaling.SessionReceived += 
                    new IncomingSessionEventHandler(this.OnSessionReceived);

                // Save configuration info
                this.profileConfiguration = configuration;
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error creating profile.\n" + ex.ToString(), 
                    "Error");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void EnableMenus(bool signedIn)
        {
            // Update File menu
            this.fileSignInMenu.Enabled = !signedIn;
            this.fileSignOutMenu.Enabled = signedIn;
            this.fileMyStatusMenu.Enabled = signedIn;
            this.onlineMenu.Enabled = signedIn;
            this.appearOfflineMenu.Enabled = signedIn;

            // Update Actions menu
            this.actionsAddContactMenu.Enabled = signedIn;
            this.actionsChatMenu.Enabled = signedIn;
            this.publishPresenceMenu.Enabled = signedIn;

            this.actionsRemoveContactMenu.Enabled = false;
            this.viewPresenceMenu.Enabled = false;

            this.publishPresenceMenu.Enabled = signedIn;
            this.recallPresenceMenuItem.Enabled = signedIn;
        }

        private void UpdateMyStatusMenu()
        {
            ProfilePresence profilePresence = this.myProfile.Presence;

            if (profilePresence.LocalPresence.PresenceState != PresenceState.Offline)
            {
                this.onlineMenu.Checked = true;
                this.appearOfflineMenu.Checked = false;
            }
            else
            {
                this.onlineMenu.Checked = false;
                this.appearOfflineMenu.Checked = true;
            }
        }
        #endregion

        #region Provider API event handlers
        private void LogOnState_LogOnStateChanged(object sender, LogOnStateChangedEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            RtcProfile         profile = (RtcProfile) sender;
            bool                    signedIn;

            if(e.IsErrorEvent)
            {
                //Error occured. Destroy the profile
                MessageBox.Show(
                    "Log on error occurred.\n" + e.GetException().ToString(), 
                    "Logon error");
                if(this.myProfile != null) 
                {
                    if (this.myProfile.LogOnState == LogOnState.LoggedOn)
                    {
                        this.myProfile.LogOff();
                    }

                    this.profileProvider.RemoveRealTimeProfile(this.myProfile);
                    this.myProfile = null;
                }
                this.statusBarPanelText.Text = StatusText[(int) profile.LogOnState];
                return;
            }
            
            signedIn = (profile.LogOnState == LogOnState.LoggedOn);
            EnableMenus(signedIn);

            this.statusBarPanelText.Text = StatusText[(int) profile.LogOnState];
            if(signedIn)
            {
                this.statusBarPanelLabel.Text = this.profileConfiguration.realTimeAddress;
                UpdateMyStatusMenu();
                AddAccessRule(null, EntityType.Wildcard, AccessRole.Allowed);
            }
            else
            {
                this.statusBarPanelLabel.Text = "";
            }

            if (profile.LogOnState == LogOnState.NotLoggedOn)
            {
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void OnSessionReceived(object sender, IncomingSessionEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            bool                accept      = true;
            bool                showConsent = true;
            StringBuilder       message = new StringBuilder();
            SignalingSession    signalingSession = e.Session;

            // Show Dialog to accept or decline the incoming session
            ConsentForm         consentForm = new ConsentForm();
            
            message.Append("You have received an invitation for a ");

            if (signalingSession.MediaType == "InstantMessaging")
            {
                message.Append("chat session from ");
                if (this.profileConfiguration.autoAcceptChat)
                {
                    showConsent = false;
                }
            }
            else
            {
                message.Append("media session from ");
            }

            message.Append(signalingSession.Inviter.RealTimeAddress);
            
            if (signalingSession.Participants.Count > 2)
            {
                message.Append("The others in the session are:\n");
                foreach(SignalingParticipant p in signalingSession.Participants)
                {
                    if( (p != signalingSession.LocalParticipant) && 
                        (p != signalingSession.Inviter))
                    {
                        message.Append(p.RealTimeAddress + "\n");
                    }
                }
            }

            consentForm.Message = message.ToString();
            consentForm.BringToFront();

            if (showConsent)
            {
                DialogResult result = consentForm.ShowDialog();
                accept = (result == DialogResult.OK);
            }

            if (accept)
            {
                try
                {
                    //Accept the incoming session
                    IAsyncResult asyncResult = signalingSession.BeginAccept(
                        new AsyncCallback(this.AcceptCallback),
                        signalingSession);

                    ChatForm chatForm = new ChatForm(myProfile, signalingSession);
                    chatForm.Show();
                    chatForm.BringToFront();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Accept failed. " + ex.Message);
                }

            }
            else
            {
                //Decline the incoming session
                signalingSession.Decline();
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void Presence_LocalPresenceChanged(object sender, EventArgs e)
        {
            UpdateMyStatusMenu();
        }

        private void PresenceSubscriptions_CollectionAdded(object sender, CollectionEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            PresenceSubscription        presenceSubscription;

            presenceSubscription = e.Target as PresenceSubscription;

            Debug.Assert(presenceSubscription != null);
            this.contactsView.Nodes.Add(new PresenceSubscriptionNode(presenceSubscription));
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void PresenceSubscriptions_CollectionRemoved(object sender, CollectionEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            PresenceSubscription        presenceSubscription;

            presenceSubscription = e.Target as PresenceSubscription;

            Debug.Assert(presenceSubscription != null);
            foreach(PresenceSubscriptionNode contactNode in this.contactsView.Nodes)
            {
                if(contactNode.PresenceSubscription == presenceSubscription)
                {
                    contactNode.Remove();
                    break;
                }
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void PresenceSubscriptions_CollectionCleared(object sender, EventArgs e)
        {
            this.contactsView.Nodes.Clear();
        }

        #endregion

        #region Provider API Callback functions
        private void AddAccessRuleCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            try
            {
                AccessRuleCollection rules = (AccessRuleCollection) result.AsyncState;
                rules.EndAdd(result);
            }
            catch
            {
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void AcceptCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            try
            {
                SignalingSession session = (SignalingSession) result.AsyncState;
                session.EndAccept(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error adding access rule.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void ContactAddCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ContactCollection   contacts;
        
            try
            {
                contacts = (ContactCollection) result.AsyncState;
                contacts.EndAdd(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error adding contact.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }
        
        private void ContactRemoveCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ContactCollection   contacts;
        
            try
            {
                contacts = (ContactCollection) result.AsyncState;
                contacts.EndRemove(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error Removing contact.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void UpdateLocalPresenceCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            ProfilePresence     profilePresence;
        
            try
            {
                profilePresence = (ProfilePresence) result.AsyncState;
                profilePresence.EndUpdateLocalPresence(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error updating local presence.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }
        
        #endregion

        #region Private fields
        private RtcProvider                     profileProvider;
        private RtcProfile                      myProfile;
        private CollabConfiguration             profileConfiguration;

        private System.ComponentModel.IContainer components;
        private TreeView contactsView;
        private MainMenu mainMenu;
        private MenuItem fileMenu;
        private MenuItem actionsMenu;
        private MenuItem fileSignInMenu;
        private MenuItem fileSignOutMenu;
        private MenuItem fileExitMenu;
        private MenuItem helpMenu;
        private MenuItem helpAboutMenu;
        private StatusBar statusBar;
        private StatusBarPanel statusBarPanelLabel;
        private StatusBarPanel statusBarPanelText;
        private MenuItem actionsAddContactMenu;
        private MenuItem actionsRemoveContactMenu;
        private MenuItem fileMyStatusMenu;
        private MenuItem onlineMenu;
        private MenuItem busyMenu;
        private MenuItem beRightBackMenu;
        private MenuItem awayMenu;
        private MenuItem onThePhoneMenu;
        private MenuItem outToLunchMenu;
        private MenuItem appearOfflineMenu;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem actionsChatMenu;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem publishPresenceMenu;
        private System.Windows.Forms.MenuItem viewPresenceMenu;
        private System.Windows.Forms.MenuItem recallPresenceMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private TestPresence    presence;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem contextMenuViewBuddyInformation;
        private System.Windows.Forms.MenuItem contextMenuChat;
        private System.Windows.Forms.MenuItem contextMenuRemoveBuddy;

        private static readonly string [] StatusText = new string []
            {
                "Not Logged On",
                "Logging On",
                "Logged On",
                "Rejected",
                "Logging Off",
                "Error"
            };
        #endregion
    }

    internal class CollabConfiguration
    {               
        public string                  realTimeAddress;
        public NetworkCredential       networkCredential;
        public RtcProfileConfiguration profileConfiguration;
        public bool                    autoAcceptChat;
    }
}
