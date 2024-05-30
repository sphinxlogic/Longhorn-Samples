//=====================================================================
// File:      PnrpPresence.cs
//
// Summary:   Application to register and retrieve information using PNRP.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// This source code is intended only as a supplement to Microsoft
// Development Tools and/or on-line documentation.  See these other
// materials for detailed information regarding Microsoft code samples.
// 
// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.PeerToPeer;
using System.Text;
using System.Windows.Forms;

namespace PnrpPresence
{
    /// <summary>
    /// This application registers a PeerName and status information with PNRP
    /// and resolves for similar information.
    /// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        private Queue peerNameQueue; // simple list of PeerNames to resolve
        
        private PnrpEndPointResolver resolver;   // Resolves PeerNames from the queue

        private PnrpEndPointRegistration regDomain; // 0.PnrpPresence        = <name>
        private PnrpEndPointRegistration regName;   // 0.PnrpPresence.<name> = <authority>
        private PnrpEndPointRegistration regStatus; // <authority>.<name>    = <status>

        private PeerName peerNamePnrpPresence;

        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colAuthority;
        private System.Windows.Forms.ColumnHeader colAddress;

        private System.Windows.Forms.Label labelIdentity;
        private System.Windows.Forms.ComboBox dropListIdentity;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox dropListStatus;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label labelCloud;
        private System.Windows.Forms.ComboBox dropListCloud;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonRegister;

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// This is the constructor for the application form.
        /// It initializes the UI, registers some default information,
        /// and begins the resolution process.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            InitializeIdentityList();
            InitializeCloudList();
            InitializeUserSettings();
            InitializeResolve();
        }

        /// <summary>
        /// Initialize the drop list of identities available for the user. 
        /// </summary>
        public void InitializeIdentityList()
        {
            // Get all of the available identities
            List<PeerIdentity> identities = PeerIdentity.GetIdentities();

            // Add each identity to the list box.
            foreach (PeerIdentity identity in identities)
            {
                dropListIdentity.Items.Add(identity);
            }

            // Select the first item
            if (dropListIdentity.Items.Count > 0)
            {
                dropListIdentity.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Initialize the drop list of available clouds. 
        /// </summary>
        public void InitializeCloudList()
        {
            // Get all of the available clouds
            List<Cloud> clouds = CloudWatcher.GetClouds();

            // Fill the list box
            foreach (Cloud cloud in clouds)
            {
                int iItem = dropListCloud.Items.Add(cloud);

                // Select the global cloud as the default
                if (cloud.Scope == Scope.Global)
                {
                    dropListCloud.SelectedIndex = iItem;
                }
            }
        }

        /// <summary>
        /// Initialize the user's current IPAddress and status.
        /// </summary>
        public void InitializeUserSettings()
        {
            textBoxAddress.Text = LocalAddress.ToString();
            dropListStatus.SelectedIndex = 0;

            peerNamePnrpPresence = new PeerName("0.PnrpPresence");
        }

        /// <summary>
        /// Initialize the resolver object.
        /// </summary>
        public void InitializeResolve()
        {
            peerNameQueue = new Queue();

            resolver = new PnrpEndPointResolver();

            resolver.MaxResults = 1000; // We don't expect more than 1000 results
            resolver.TimeOut = new TimeSpan(0, 1, 0); // Timeout after 1 minute
            resolver.ResolutionCriteria = ResolutionCriteria.All;

            resolver.SynchronizingObject = this; // Callbacks happen on the UI thread
            resolver.PeerNameFound += new PeerNameFoundEventHandler(OnPeerNameFound);
            resolver.ResolutionCompleted += new ResolutionCompletedEventHandler(OnResolutionCompleted);
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
            this.listViewMain = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.colAddress = new System.Windows.Forms.ColumnHeader();
            this.colAuthority = new System.Windows.Forms.ColumnHeader();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.labelCloud = new System.Windows.Forms.Label();
            this.dropListCloud = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.dropListStatus = new System.Windows.Forms.ComboBox();
            this.dropListIdentity = new System.Windows.Forms.ComboBox();
            this.labelIdentity = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();

            // 
            // listViewMain
            // 
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colName, this.colStatus, this.colAddress, this.colAuthority
            });
            this.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMain.FullRowSelect = true;
            this.listViewMain.Location = new System.Drawing.Point(0, 77);
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(573, 157);
            this.listViewMain.TabIndex = 1;
            this.listViewMain.View = System.Windows.Forms.View.Details;

            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 113;

            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 78;

            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 183;

            // 
            // colAuthority
            // 
            this.colAuthority.Text = "Authority";
            this.colAuthority.Width = 289;

            // 
            // panelSettings
            // 
            this.panelSettings.Controls.Add(this.buttonRegister);
            this.panelSettings.Controls.Add(this.labelCloud);
            this.panelSettings.Controls.Add(this.dropListCloud);
            this.panelSettings.Controls.Add(this.labelStatus);
            this.panelSettings.Controls.Add(this.buttonRefresh);
            this.panelSettings.Controls.Add(this.dropListStatus);
            this.panelSettings.Controls.Add(this.dropListIdentity);
            this.panelSettings.Controls.Add(this.labelIdentity);
            this.panelSettings.Controls.Add(this.textBoxAddress);
            this.panelSettings.Controls.Add(this.labelAddress);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(573, 77);
            this.panelSettings.TabIndex = 0;

            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegister.Location = new System.Drawing.Point(482, 9);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(78, 23);
            this.buttonRegister.TabIndex = 8;
            this.buttonRegister.Text = "&Register";
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);

            // 
            // labelCloud
            // 
            this.labelCloud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCloud.Location = new System.Drawing.Point(262, 43);
            this.labelCloud.Name = "labelCloud";
            this.labelCloud.Size = new System.Drawing.Size(40, 16);
            this.labelCloud.TabIndex = 6;
            this.labelCloud.Text = "Cloud:";

            // 
            // dropListCloud
            // 
            this.dropListCloud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListCloud.Location = new System.Drawing.Point(307, 43);
            this.dropListCloud.Name = "dropListCloud";
            this.dropListCloud.Size = new System.Drawing.Size(147, 21);
            this.dropListCloud.Sorted = true;
            this.dropListCloud.TabIndex = 7;

            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.Location = new System.Drawing.Point(262, 9);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(42, 16);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status:";

            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(482, 43);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(78, 23);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "Re&fresh";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

            // 
            // dropListStatus
            // 
            this.dropListStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListStatus.Items.AddRange(new object[] {
                "Active", "Be right back", "Busy", "On the phone", "Out to lunch"
            });
            this.dropListStatus.Location = new System.Drawing.Point(307, 9);
            this.dropListStatus.Name = "dropListStatus";
            this.dropListStatus.Size = new System.Drawing.Size(147, 21);
            this.dropListStatus.Sorted = true;
            this.dropListStatus.TabIndex = 5;

            // 
            // dropListIdentity
            // 
            this.dropListIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListIdentity.Location = new System.Drawing.Point(58, 9);
            this.dropListIdentity.Name = "dropListIdentity";
            this.dropListIdentity.Size = new System.Drawing.Size(173, 21);
            this.dropListIdentity.Sorted = true;
            this.dropListIdentity.TabIndex = 1;

            // 
            // labelIdentity
            // 
            this.labelIdentity.Location = new System.Drawing.Point(5, 9);
            this.labelIdentity.Name = "labelIdentity";
            this.labelIdentity.Size = new System.Drawing.Size(49, 16);
            this.labelIdentity.TabIndex = 0;
            this.labelIdentity.Text = "Identity:";

            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress.Location = new System.Drawing.Point(58, 43);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(173, 19);
            this.textBoxAddress.TabIndex = 3;

            // 
            // labelAddress
            // 
            this.labelAddress.Location = new System.Drawing.Point(5, 43);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(49, 16);
            this.labelAddress.TabIndex = 2;
            this.labelAddress.Text = "Address:";

            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonRefresh;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(573, 234);
            this.Controls.Add(this.listViewMain);
            this.Controls.Add(this.panelSettings);
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "FormMain";
            this.Text = "PNRP Presence";
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
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
            Application.Run(new FormMain());
        }

        /********************************************************************/
        // Events

        /// <summary>
        /// This routine handles one result of a PnrpEndPointResolution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPeerNameFound(object sender, PeerNameFoundEventArgs e)
        {
            ProcessPnrpEndPoint(e.PnrpEndPoint);
        }

        /// <summary>
        /// This routine handles the completion of one PnrpEndPointResolution
        /// by starting the timer to delay before processing the next entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResolutionCompleted(object sender, ResolutionCompletedEventArgs e)
        {
            ProcessNextPeerName();
        }

        /// <summary>
        /// This routine handles the Refresh button click by
        /// initializing the queue and starting the resolve process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, System.EventArgs e)
        {
            InitializeQueue();

            // Disable the button until all of the PeerNames have been resolved.
            buttonRefresh.Enabled = false;
        }

        /// <summary>
        /// This routines handles the Register button click by
        /// either registering or unregistering the information with PNRP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegister_Click(object sender, System.EventArgs e)
        {
            if (IsRegistered)
            {
                Unregister();
                buttonRegister.Text = "&Register";
                EnableControls(true);
            }
            else
            {
                if (Register())
                {
                    buttonRegister.Text = "Un&register";
                    EnableControls(false);
                }
            }
        }

        /// <summary>
        /// Enable (or disable) the controls.
        /// </summary>
        /// <param name="enable">True if the controls should be enabled.</param>
        private void EnableControls(bool enable)
        {
            dropListCloud.Enabled = enable;
            dropListIdentity.Enabled = enable;
            dropListStatus.Enabled = enable;
            textBoxAddress.ReadOnly = !enable;
        }


        /********************************************************************/
        // Utility Routines

        /// <summary>
        /// Create a new ListViewItem and add it to the list.
        /// </summary>
        /// <param name="authority">The authority string</param>
        /// <param name="name">The friendly name (classifier)</param>
        /// <param name="endPoint">The PnrpEndPoint that was registered.</param>
        /// <param name="status">The current status for this entry.</param>
        private void AddEntry(string authority, string name, PnrpEndPoint endPoint, string status)
        {
            ListViewItem lvi = new ListViewItem(name);

            lvi.SubItems.Add(status);
            lvi.SubItems.Add(endPoint.IPEndPoints[0].ToString());
            lvi.SubItems.Add(authority);

            // This entry has been verified
            lvi.Checked = true;
            listViewMain.Items.Add(lvi);
        }

        /// <summary>
        /// Find the entry with the matching authority.
        /// </summary>
        /// <param name="authority">The authority to search for.</param>
        /// <returns>The ListView item (or null if not found.)</returns>
        private ListViewItem FindEntry(string authority)
        {
            foreach (ListViewItem lvi in listViewMain.Items)
            {
                if (authority == lvi.SubItems[colAuthority.Index].Text)
                {
                    return lvi;
                }
            }

            return null;
        }

        /// <summary>
        /// Convert the authority hex string into a base 64-encoded string.
        /// </summary>
        /// <param name="authority">The authority string to convert.</param>
        /// <returns>A encoded string that represents the authority.</returns>
        public string ConvertAuthorityToString(string authority)
        {
            // A secure authority always consists of 40 hex characters.
            if (authority.Length != 40)
                return string.Empty;

            // Convert the authority string to a byte array
            byte[] data = new byte[20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = System.Convert.ToByte(authority.Substring(i*2, 2), 16);
            }

            // Convert the byte array to Base 64 string
            return System.Convert.ToBase64String(data);
        }

        /// <summary>
        /// Convert the base 64-encoded string to a 40 character hex string
        /// </summary>
        /// <param name="s">The encoded string.</param>
        /// <returns>A 40 character hex string.</returns>
        public string ConvertStringToAuthority(string s)
        {
            byte[] data = System.Convert.FromBase64String(s);

            StringBuilder authority = new StringBuilder(40);
            for (int i = 0; i < data.Length; i++)
            {
                authority.AppendFormat("{0:x2}", data[i]);
            }

            return authority.ToString();
        }

        /// <summary>
        /// This retrieves the cloud the user has selected.
        /// </summary>
        /// <value></value>
        public Cloud CurrentCloud
        {
            get
            {
                return (Cloud)dropListCloud.SelectedItem;
            }
        }

        /// <summary>
        /// This retrieves the local IP address for the machine.
        /// </summary>
        /// <value></value>
        public IPAddress LocalAddress
        {
            get
            {
                IPHostEntry hostEntry = Dns.GetHostByName(System.Environment.MachineName);

                // No addressses - return nothing
                if (hostEntry.AddressList.Length == 0)
                    return null;

                // Just return the first address
                return (hostEntry.AddressList[0]);
            }
        }


        /********************************************************************/
        // Resolving Routines

        /// <summary>
        /// Initialize the queue and start the resolve process.
        /// </summary>
        private void InitializeQueue()
        {
            // Initialize the queue with the general "0.PnrpPresence"
            peerNameQueue.Enqueue(peerNamePnrpPresence);

            // Make sure we are resolving in the same cloud we are registered in.
            resolver.Cloud = CurrentCloud;

            // Clear the ListView
            listViewMain.Items.Clear();

            // Start to process the first element in the queue
            ProcessNextPeerName();
        }

        /// <summary>
        /// Try to resolve the next item in the list.
        /// </summary>
        private void ProcessNextPeerName()
        {
            if (peerNameQueue.Count != 0)
            {
                // Resolve the next item in the list
                resolver.PeerName = (PeerName)peerNameQueue.Dequeue();
                resolver.BeginResolve();
            }
            else
            {
                // No more items to resolve - enable the refresh button
                buttonRefresh.Enabled = true;
            }
        }

        /// <summary>
        /// This routine create a PeerName from the authority and classifier
        /// and adds that to the queue of items to resolve.
        /// </summary>
        /// <param name="authority">The authority for the new PeerName.</param>
        /// <param name="classifier">The classifier for the new PeerName.</param>
        private void AddPeerNameToQueue(string authority, string classifier)
        {
            // Create the peer name to resolve for.
            PeerName peerName = new PeerName(authority + "." + classifier);
            peerNameQueue.Enqueue(peerName);
        }
        
        /// <summary>
        /// This routine processes a PnrpEndPoint by either adding a new peer name
        /// to the queue, adding a new entry to the list, or updating an existing entry.
        /// </summary>
        /// <param name="endPoint">The PnrpEndPoint to process.</param>
        private void ProcessPnrpEndPoint(PnrpEndPoint endPoint)
        {
            PeerName peerName = endPoint.PeerName;

            if (peerName.Authority == "0")
            {
                int iDot = peerName.Classifier.IndexOf(".");
                if (iDot < 0)
                {
                    // Found a result for "0.PnrpPresence"
                    // so get authority from the comment in the matching "0.PnrpPresence.<name>"
                    AddPeerNameToQueue("0", peerName.Classifier + "." + endPoint.Comment);
                }
                else
                {
                    // Found a result for "0.PnrpPresence.<name>"
                    // so get the status from the comment in the matching "<authority>.<name>"
                    string name = peerName.Classifier.Substring(iDot + 1);
                    string authority = ConvertStringToAuthority(endPoint.Comment);
                    AddPeerNameToQueue(authority, name);
                }
            }
            else
            {
                // Found a result for "<authority>.<name>"
                // so add it to the display if it doesn't already exist.
                if (null == FindEntry(peerName.Authority))
                {
                    AddEntry(peerName.Authority, peerName.Classifier, endPoint, endPoint.Comment);
                }
            }
        }


        /********************************************************************/
        // Registration Routines


        ///////////////////////////////////////////////////////////////////
        // Register the user's data.
        //
        // PnrpEndPointRegistration   PeerName               Comment
        // ------------------------   -------------          ----------
        //                regDomain   0.PnrpPresence         <name>
        //                regName     0.PnrpPresence.<name>  <authority>
        //                regStatus   <authority>.<name>     <status>
        //
        public bool Register()
        {
            bool registered = false;

            try
            {
                PeerIdentity identity = (PeerIdentity)dropListIdentity.SelectedItem;
                string name = identity.FriendlyName;
                PeerName securePeerName = identity.CreatePeerName(name);
                Cloud cloud = CurrentCloud;
                string status = dropListStatus.Text;

                // Well known insecure PeerName: 0.PnrpPresence
                PnrpEndPoint endPoint = CreatePnrpEndPoint(peerNamePnrpPresence, name);
                regDomain = new PnrpEndPointRegistration(endPoint, identity, cloud);

                // Specific insecure PeerName: 0.PnrpPresence.<name>
                PeerName insecurePeerName = new PeerName(peerNamePnrpPresence.PeerNameString + "." + name);
                endPoint = CreatePnrpEndPoint(insecurePeerName,
                                ConvertAuthorityToString(securePeerName.Authority));
                regName = new PnrpEndPointRegistration(endPoint, identity, cloud);

                // Secure PeerName: <authority>.<name>
                endPoint = CreatePnrpEndPoint(securePeerName, status);
                regStatus = new PnrpEndPointRegistration(endPoint, identity, cloud);

                // Register the data
                regStatus.Register();
                regName.Register();
                regDomain.Register();

                registered = true;
            }
            catch (Exception e)
            {
                // Display the error message.
                MessageBox.Show(this, e.Message + "\r\n\r\n" + e.StackTrace,
                    "Exception from " + e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return registered;
        }

        /// <summary>
        /// Ensure the information is not registered.
        /// </summary>
        public void Unregister()
        {
            regDomain.Unregister();
            regName.Unregister();
            regStatus.Unregister();
        }

        /// <summary>
        /// True if the information is registered
        /// </summary>
        /// <value></value>
        public bool IsRegistered
        {
            get
            {
                return (null != regDomain) && (regDomain.State == RegistrationState.Registered);
            }
        }

        /// <summary>
        /// Create a new PnrpEndPoint that will be registered.
        /// </summary>
        /// <param name="peerName">The PeerName to use in the PnrpEndPoint</param>
        /// <param name="comment">The comment to use in the PnrpEndPoint</param>
        /// <returns>A new PnrpEndPoint</returns>
        public PnrpEndPoint CreatePnrpEndPoint(PeerName peerName, string comment)
        {
            IPAddress address = IPAddress.Parse(textBoxAddress.Text);
            IPEndPoint[] ipEndPoints = new IPEndPoint[1];

            ipEndPoints[0] = new IPEndPoint(address, 0);
            return new PnrpEndPoint(peerName, ipEndPoints, comment);
        }
    }
}