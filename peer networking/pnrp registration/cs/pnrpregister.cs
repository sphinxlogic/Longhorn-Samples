//=====================================================================
// File:      PnrpRegister.cs
//
// Summary:   Application to register information with PNRP
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
using System.Windows.Forms;

namespace PnrpRegister
{
    /// <summary>
    /// Sample application to register PeerNames
    /// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        private PnrpEndPointRegistration registration;

        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.GroupBox groupBoxPeerName;
        private System.Windows.Forms.TextBox textBoxPeerName;
        private System.Windows.Forms.CheckBox checkBoxSecure;

        private System.Windows.Forms.GroupBox groupBoxEndPoints;
        private System.Windows.Forms.CheckBox checkBoxAddress1;
        private System.Windows.Forms.TextBox textBoxAddress1;
        private System.Windows.Forms.CheckBox checkBoxAddress2;
        private System.Windows.Forms.TextBox textBoxAddress2;
        private System.Windows.Forms.TextBox textBoxAddress3;
        private System.Windows.Forms.CheckBox checkBoxAddress3;
        private System.Windows.Forms.TextBox textBoxAddress4;
        private System.Windows.Forms.CheckBox checkBoxAddress4;

        private System.Windows.Forms.GroupBox groupBoxProperties;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelCloud;
        private System.Windows.Forms.ComboBox dropListCloud;
        private System.Windows.Forms.Label labelIdentity;
        private System.Windows.Forms.ComboBox dropListIdentity;
        private System.Windows.Forms.Label labelClassifier;
        private System.Windows.Forms.TextBox textBoxClassifier;

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // Create the object that will be registered.
            registration = new PnrpEndPointRegistration();

            // Initialize the lists.
            InitializeCloudList();
            InitializeIdentityList();
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
            this.buttonRegister = new System.Windows.Forms.Button();
            this.groupBoxEndPoints = new System.Windows.Forms.GroupBox();
            this.textBoxAddress1 = new System.Windows.Forms.TextBox();
            this.checkBoxAddress1 = new System.Windows.Forms.CheckBox();
            this.textBoxAddress2 = new System.Windows.Forms.TextBox();
            this.checkBoxAddress2 = new System.Windows.Forms.CheckBox();
            this.textBoxAddress3 = new System.Windows.Forms.TextBox();
            this.checkBoxAddress3 = new System.Windows.Forms.CheckBox();
            this.textBoxAddress4 = new System.Windows.Forms.TextBox();
            this.checkBoxAddress4 = new System.Windows.Forms.CheckBox();
            this.groupBoxPeerName = new System.Windows.Forms.GroupBox();
            this.checkBoxSecure = new System.Windows.Forms.CheckBox();
            this.textBoxPeerName = new System.Windows.Forms.TextBox();
            this.groupBoxProperties = new System.Windows.Forms.GroupBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.labelCloud = new System.Windows.Forms.Label();
            this.dropListCloud = new System.Windows.Forms.ComboBox();
            this.dropListIdentity = new System.Windows.Forms.ComboBox();
            this.labelIdentity = new System.Windows.Forms.Label();
            this.textBoxClassifier = new System.Windows.Forms.TextBox();
            this.labelClassifier = new System.Windows.Forms.Label();
            this.groupBoxEndPoints.SuspendLayout();
            this.groupBoxPeerName.SuspendLayout();
            this.groupBoxProperties.SuspendLayout();
            this.SuspendLayout();

            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRegister.Location = new System.Drawing.Point(124, 376);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.TabIndex = 3;
            this.buttonRegister.Text = "&Register";
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);

            // 
            // groupBoxEndPoints
            // 
            this.groupBoxEndPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEndPoints.Controls.Add(this.textBoxAddress1);
            this.groupBoxEndPoints.Controls.Add(this.checkBoxAddress1);
            this.groupBoxEndPoints.Controls.Add(this.textBoxAddress2);
            this.groupBoxEndPoints.Controls.Add(this.checkBoxAddress2);
            this.groupBoxEndPoints.Controls.Add(this.textBoxAddress3);
            this.groupBoxEndPoints.Controls.Add(this.checkBoxAddress3);
            this.groupBoxEndPoints.Controls.Add(this.textBoxAddress4);
            this.groupBoxEndPoints.Controls.Add(this.checkBoxAddress4);
            this.groupBoxEndPoints.Location = new System.Drawing.Point(8, 248);
            this.groupBoxEndPoints.Name = "groupBoxEndPoints";
            this.groupBoxEndPoints.Size = new System.Drawing.Size(312, 118);
            this.groupBoxEndPoints.TabIndex = 2;
            this.groupBoxEndPoints.TabStop = false;
            this.groupBoxEndPoints.Text = "IPEndPoints";

            // 
            // textBoxAddress1
            // 
            this.textBoxAddress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress1.Location = new System.Drawing.Point(95, 20);
            this.textBoxAddress1.MaxLength = 100;
            this.textBoxAddress1.Name = "textBoxAddress1";
            this.textBoxAddress1.Size = new System.Drawing.Size(203, 19);
            this.textBoxAddress1.TabIndex = 1;
            this.textBoxAddress1.Text = "0.0.0.0";

            // 
            // checkBoxAddress1
            // 
            this.checkBoxAddress1.Checked = true;
            this.checkBoxAddress1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddress1.Location = new System.Drawing.Point(8, 20);
            this.checkBoxAddress1.Name = "checkBoxAddress1";
            this.checkBoxAddress1.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAddress1.TabIndex = 0;
            this.checkBoxAddress1.Text = "Address &1:";
            this.checkBoxAddress1.CheckedChanged += new System.EventHandler(this.checkBoxAddress1_CheckedChanged);

            // 
            // textBoxAddress2
            // 
            this.textBoxAddress2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress2.Enabled = false;
            this.textBoxAddress2.Location = new System.Drawing.Point(95, 42);
            this.textBoxAddress2.MaxLength = 100;
            this.textBoxAddress2.Name = "textBoxAddress2";
            this.textBoxAddress2.Size = new System.Drawing.Size(203, 19);
            this.textBoxAddress2.TabIndex = 3;

            // 
            // checkBoxAddress2
            // 
            this.checkBoxAddress2.Location = new System.Drawing.Point(8, 42);
            this.checkBoxAddress2.Name = "checkBoxAddress2";
            this.checkBoxAddress2.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAddress2.TabIndex = 2;
            this.checkBoxAddress2.Text = "Address &2:";
            this.checkBoxAddress2.CheckedChanged += new System.EventHandler(this.checkBoxAddress2_CheckedChanged);

            // 
            // textBoxAddress3
            // 
            this.textBoxAddress3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress3.Enabled = false;
            this.textBoxAddress3.Location = new System.Drawing.Point(95, 64);
            this.textBoxAddress3.MaxLength = 100;
            this.textBoxAddress3.Name = "textBoxAddress3";
            this.textBoxAddress3.Size = new System.Drawing.Size(203, 19);
            this.textBoxAddress3.TabIndex = 5;

            // 
            // checkBoxAddress3
            // 
            this.checkBoxAddress3.Location = new System.Drawing.Point(8, 64);
            this.checkBoxAddress3.Name = "checkBoxAddress3";
            this.checkBoxAddress3.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAddress3.TabIndex = 4;
            this.checkBoxAddress3.Text = "Address &3:";
            this.checkBoxAddress3.CheckedChanged += new System.EventHandler(this.checkBoxAddress3_CheckedChanged);

            // 
            // textBoxAddress4
            // 
            this.textBoxAddress4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddress4.Enabled = false;
            this.textBoxAddress4.Location = new System.Drawing.Point(95, 86);
            this.textBoxAddress4.MaxLength = 100;
            this.textBoxAddress4.Name = "textBoxAddress4";
            this.textBoxAddress4.Size = new System.Drawing.Size(203, 19);
            this.textBoxAddress4.TabIndex = 7;

            // 
            // checkBoxAddress4
            // 
            this.checkBoxAddress4.Location = new System.Drawing.Point(8, 86);
            this.checkBoxAddress4.Name = "checkBoxAddress4";
            this.checkBoxAddress4.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAddress4.TabIndex = 6;
            this.checkBoxAddress4.Text = "Address &4:";
            this.checkBoxAddress4.CheckedChanged += new System.EventHandler(this.checkBoxAddress4_CheckedChanged);

            // 
            // groupBoxPeerName
            // 
            this.groupBoxPeerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPeerName.Controls.Add(this.checkBoxSecure);
            this.groupBoxPeerName.Controls.Add(this.textBoxPeerName);
            this.groupBoxPeerName.Location = new System.Drawing.Point(8, 8);
            this.groupBoxPeerName.Name = "groupBoxPeerName";
            this.groupBoxPeerName.Size = new System.Drawing.Size(310, 71);
            this.groupBoxPeerName.TabIndex = 0;
            this.groupBoxPeerName.TabStop = false;
            this.groupBoxPeerName.Text = "PeerName";

            // 
            // checkBoxSecure
            // 
            this.checkBoxSecure.Location = new System.Drawing.Point(11, 45);
            this.checkBoxSecure.Name = "checkBoxSecure";
            this.checkBoxSecure.Size = new System.Drawing.Size(68, 16);
            this.checkBoxSecure.TabIndex = 1;
            this.checkBoxSecure.Text = "&Secure";
            this.checkBoxSecure.CheckedChanged += new System.EventHandler(this.checkBoxSecure_CheckedChanged);

            // 
            // textBoxPeerName
            // 
            this.textBoxPeerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPeerName.Location = new System.Drawing.Point(11, 18);
            this.textBoxPeerName.MaxLength = 39;
            this.textBoxPeerName.Name = "textBoxPeerName";
            this.textBoxPeerName.ReadOnly = true;
            this.textBoxPeerName.Size = new System.Drawing.Size(289, 19);
            this.textBoxPeerName.TabIndex = 0;
            this.textBoxPeerName.TabStop = false;

            // 
            // groupBoxProperties
            // 
            this.groupBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProperties.Controls.Add(this.textBoxComment);
            this.groupBoxProperties.Controls.Add(this.labelComment);
            this.groupBoxProperties.Controls.Add(this.labelCloud);
            this.groupBoxProperties.Controls.Add(this.dropListCloud);
            this.groupBoxProperties.Controls.Add(this.dropListIdentity);
            this.groupBoxProperties.Controls.Add(this.labelIdentity);
            this.groupBoxProperties.Controls.Add(this.textBoxClassifier);
            this.groupBoxProperties.Controls.Add(this.labelClassifier);
            this.groupBoxProperties.Location = new System.Drawing.Point(8, 89);
            this.groupBoxProperties.Name = "groupBoxProperties";
            this.groupBoxProperties.Size = new System.Drawing.Size(312, 143);
            this.groupBoxProperties.TabIndex = 1;
            this.groupBoxProperties.TabStop = false;
            this.groupBoxProperties.Text = "Properties";

            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Location = new System.Drawing.Point(81, 80);
            this.textBoxComment.MaxLength = 39;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(219, 19);
            this.textBoxComment.TabIndex = 5;

            // 
            // labelComment
            // 
            this.labelComment.Location = new System.Drawing.Point(8, 83);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(53, 16);
            this.labelComment.TabIndex = 4;
            this.labelComment.Text = "C&omment:";

            // 
            // labelCloud
            // 
            this.labelCloud.Location = new System.Drawing.Point(8, 116);
            this.labelCloud.Name = "labelCloud";
            this.labelCloud.Size = new System.Drawing.Size(53, 16);
            this.labelCloud.TabIndex = 6;
            this.labelCloud.Text = "&Cloud:";

            // 
            // dropListCloud
            // 
            this.dropListCloud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListCloud.Location = new System.Drawing.Point(81, 111);
            this.dropListCloud.Name = "dropListCloud";
            this.dropListCloud.Size = new System.Drawing.Size(219, 21);
            this.dropListCloud.Sorted = true;
            this.dropListCloud.TabIndex = 7;

            // 
            // dropListIdentity
            // 
            this.dropListIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListIdentity.Location = new System.Drawing.Point(81, 17);
            this.dropListIdentity.Name = "dropListIdentity";
            this.dropListIdentity.Size = new System.Drawing.Size(219, 21);
            this.dropListIdentity.Sorted = true;
            this.dropListIdentity.TabIndex = 1;
            this.dropListIdentity.SelectedIndexChanged += new System.EventHandler(this.dropListIdentity_SelectedIndexChanged);

            // 
            // labelIdentity
            // 
            this.labelIdentity.Location = new System.Drawing.Point(8, 22);
            this.labelIdentity.Name = "labelIdentity";
            this.labelIdentity.Size = new System.Drawing.Size(53, 16);
            this.labelIdentity.TabIndex = 0;
            this.labelIdentity.Text = "&Identity:";

            // 
            // textBoxClassifier
            // 
            this.textBoxClassifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClassifier.Location = new System.Drawing.Point(81, 50);
            this.textBoxClassifier.MaxLength = 139;
            this.textBoxClassifier.Name = "textBoxClassifier";
            this.textBoxClassifier.Size = new System.Drawing.Size(219, 19);
            this.textBoxClassifier.TabIndex = 3;
            this.textBoxClassifier.Text = "0";
            this.textBoxClassifier.TextChanged += new System.EventHandler(this.textBoxClassifier_TextChanged);

            // 
            // labelClassifier
            // 
            this.labelClassifier.Location = new System.Drawing.Point(8, 53);
            this.labelClassifier.Name = "labelClassifier";
            this.labelClassifier.Size = new System.Drawing.Size(53, 16);
            this.labelClassifier.TabIndex = 2;
            this.labelClassifier.Text = "C&lassifier:";

            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonRegister;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(323, 409);
            this.Controls.Add(this.groupBoxProperties);
            this.Controls.Add(this.groupBoxPeerName);
            this.Controls.Add(this.groupBoxEndPoints);
            this.Controls.Add(this.buttonRegister);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(999, 436);
            this.MinimumSize = new System.Drawing.Size(331, 436);
            this.Name = "FormMain";
            this.Text = "PNRP Registration";
            this.groupBoxEndPoints.ResumeLayout(false);
            this.groupBoxEndPoints.PerformLayout();
            this.groupBoxPeerName.ResumeLayout(false);
            this.groupBoxPeerName.PerformLayout();
            this.groupBoxProperties.ResumeLayout(false);
            this.groupBoxProperties.PerformLayout();
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

        /// <summary>
        /// Initialize the cloud drop list.
        /// </summary>
        private void InitializeCloudList()
        {
            List<Cloud> clouds = CloudWatcher.GetClouds();

            foreach (Cloud cloud in clouds)
            {
                dropListCloud.Items.Add(cloud);
            }

            // Always select the first one (Global)
            dropListCloud.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize the Identity drop down list.
        /// </summary>
        public void InitializeIdentityList()
        {
            List<PeerIdentity> identities = PeerIdentity.GetIdentities();

            foreach (PeerIdentity identity in identities)
            {
                dropListIdentity.Items.Add(identity);
            }

            // Ensure an item is selected, if possible.
            if (identities.Count > 0)
            {
                dropListIdentity.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Get the PeerName to register.
        /// </summary>
        /// <value></value>
        private PeerName SelectedPeerName
        {
            get
            {
                if (textBoxPeerName.TextLength == 0)
                    return PeerName.default;

                // Try to create a PeerName from the text in the control.
                return new PeerName(textBoxPeerName.Text);               
            }
        }

        /// <summary>
        /// Retrieve the selected identity.
        /// </summary>
        /// <value></value>
        public PeerIdentity SelectedIdentity
        {
            get
            {
                return (PeerIdentity)dropListIdentity.SelectedItem;
            }
        }

        /// <summary>
        /// Retrieve the selected cloud.
        /// </summary>
        /// <value></value>
        public Cloud SelectedCloud
        {
            get
            {
                return (Cloud)dropListCloud.SelectedItem;
            }
        }

        /// <summary>
        /// Retrieve the set of IPEndPoints
        /// </summary>
        /// <value></value>
        public IPEndPoint[] IPEndPoints
        {
            get
            {
                IPEndPoint[] data = new IPEndPoint[4];
                int cItem = 0;

                // Add each IPEndPoint to the temporary array
                foreach (Control control in groupBoxEndPoints.Controls)
                {
                    if (control is TextBox)
                    {
                        if (control.Enabled)
                        {
                            try
                            {
                                IPAddress address = IPAddress.Parse(control.Text);
                                data[cItem] = new IPEndPoint(address, 0);
                                cItem++;
                            }
                            catch (Exception e)
                            {
                                // Display an error message if there was a problem with the IPEndPoint
                                DisplayException(e);
                            }
                        }
                    }
                }

                // Create a new array with the selected IPEndPoints
                IPEndPoint[] ipEndPoints = new IPEndPoint[cItem];
                Array.Copy(data, ipEndPoints, cItem);
                return ipEndPoints;
            }
        }


        /// <summary>
        /// Handle the button press by registering or unregistering the PeerName
        /// and then update the controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegister_Click(object sender, System.EventArgs e)
        {
            if (registration.State == RegistrationState.Registered)
            {
                Unregister();
            }
            else
            {
                Register();
            }

            UpdateControls();
        }

        // For each Address checkbox, enable/disable the assocated text control.

        private void checkBoxAddress1_CheckedChanged(object sender, System.EventArgs e)
        {
            textBoxAddress1.Enabled = checkBoxAddress1.Checked;
        }
        private void checkBoxAddress2_CheckedChanged(object sender, System.EventArgs e)
        {
            textBoxAddress2.Enabled = checkBoxAddress2.Checked;
        }
        private void checkBoxAddress3_CheckedChanged(object sender, System.EventArgs e)
        {
            textBoxAddress3.Enabled = checkBoxAddress3.Checked;
        }
        private void checkBoxAddress4_CheckedChanged(object sender, System.EventArgs e)
        {
            textBoxAddress4.Enabled = checkBoxAddress4.Checked;
        }


        /// <summary>
        /// Handle a change in the classifier by updating the PeerName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxClassifier_TextChanged(object sender, System.EventArgs e)
        {
            UpdatePeerName();
        }

        /// <summary>
        /// Handle a change in the selected identity by updating the PeerName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropListIdentity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdatePeerName();
        }

        /// <summary>
        /// Handle a change in the "Secure" checkbox by updating the PeerName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSecure_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdatePeerName();
        }

        /// <summary>
        /// Try to register with the current information.
        /// </summary>
        private void Register()
        {
            try
            {
                PeerName peerName = SelectedPeerName;
                PnrpEndPoint endPoint = new PnrpEndPoint(peerName, IPEndPoints, textBoxComment.Text);

                registration.PnrpEndPoint = endPoint;
                registration.Cloud = SelectedCloud;
                registration.Identity = SelectedIdentity;

                registration.Register();
            }
            catch (Exception e)
            {
                // Display all exceptions.
                DisplayException(e);
            }
        }

        /// <summary>
        /// Make sure the object is not registered.
        /// </summary>
        private void Unregister()
        {
            if (registration.State == RegistrationState.Registered)
            {
                registration.Unregister();
            }
        }

        /// <summary>
        /// Update the display by enabling or disabling the controls.
        /// </summary>
        private void UpdateControls()
        {
            // Enable the controls if we are not registered.
            bool enable = (registration.State != RegistrationState.Registered);

            checkBoxSecure.Enabled = enable;
            dropListIdentity.Enabled = enable;
            dropListCloud.Enabled = enable;
            checkBoxAddress1.Enabled = enable;
            checkBoxAddress2.Enabled = enable;
            checkBoxAddress3.Enabled = enable;
            checkBoxAddress4.Enabled = enable;
            textBoxAddress1.ReadOnly = !enable;
            textBoxAddress2.ReadOnly = !enable;
            textBoxAddress3.ReadOnly = !enable;
            textBoxAddress4.ReadOnly = !enable;
            textBoxComment.ReadOnly = !enable;
            textBoxClassifier.ReadOnly = !enable;

            // Update the button text
            buttonRegister.Text = enable ? "&Register" : "Un&register";
        }


        /// <summary>
        /// Update the text in the PeerName control by either creating a
        /// secure PeerName using the selected identity or
        /// just adding the "0." prefix to the classifier.
        /// </summary>
        private void UpdatePeerName()
        {
            PeerName peerName;

            try
            {
                if (checkBoxSecure.Checked)
                {
                    // Create a secure PeerName based on the selected Identity
                    peerName = SelectedIdentity.CreatePeerName(textBoxClassifier.Text);
                }
                else
                {
                    // Create an insecure PeerName
                    peerName = new PeerName("0." + textBoxClassifier.Text);
                }
                textBoxPeerName.Text = peerName.ToString();
            }
            catch
            {
                // Handle all errors by blanking out the text.
                textBoxPeerName.Text = string.Empty;
            }
        }

        /// <summary>
        /// Utility routine to display an exception in a MessageBox.
        /// </summary>
        /// <param name="e"></param>
        private void DisplayException(Exception e)
        {
            MessageBox.Show(this, e.Message + "\r\n\r\n" + e.StackTrace,
                "Exception from " + e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
