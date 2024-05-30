//=====================================================================
// File:      FormIdentity.cs
//
// Summary:   Form that displays identities.
//
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
using System.Net.PeerToPeer;
using System.Windows.Forms;

namespace IdentityViewer
{
    /// <summary>
    /// This for displays the information associated with an Identity.
    /// </summary>
    public class FormIdentity : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelClassifier;
        private System.Windows.Forms.TextBox textBoxClassifier;
        private System.Windows.Forms.Label labelAuthority;
        private System.Windows.Forms.TextBox textBoxAuthority;
        private System.Windows.Forms.Label labelXml;
        private System.Windows.Forms.TextBox textBoxXml;
        private System.Windows.Forms.Button buttonClose;

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor to create a form to display the information for a PeerIdentity.
        /// </summary>
        /// <param name="identity">The PeerIdentity to display.</param>
        public FormIdentity(PeerIdentity identity)
        {
            InitializeComponent();

            // Fill out the text fields
            this.textBoxName.Text = identity.FriendlyName;
            this.textBoxAuthority.Text = identity.PeerName.Authority;
            this.textBoxClassifier.Text = identity.PeerName.Classifier;

            try
            {
                this.textBoxXml.Text = identity.Key.ToXmlString(true);
            }
            catch (Exception e)
            {
                this.textBoxXml.Text = e.Message;
            }

            // Set the window title to the friendly name of this identity.
            this.Text = identity.FriendlyName;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelClassifier = new System.Windows.Forms.Label();
            this.textBoxClassifier = new System.Windows.Forms.TextBox();
            this.labelAuthority = new System.Windows.Forms.Label();
            this.textBoxAuthority = new System.Windows.Forms.TextBox();
            this.labelXml = new System.Windows.Forms.Label();
            this.textBoxXml = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(7, 12);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(66, 17);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";

            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(83, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(248, 19);
            this.textBoxName.TabIndex = 2;

            // 
            // labelClassifier
            // 
            this.labelClassifier.Location = new System.Drawing.Point(7, 38);
            this.labelClassifier.Name = "labelClassifier";
            this.labelClassifier.Size = new System.Drawing.Size(66, 17);
            this.labelClassifier.TabIndex = 3;
            this.labelClassifier.Text = "Classifier:";

            // 
            // textBoxClassifier
            // 
            this.textBoxClassifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClassifier.Location = new System.Drawing.Point(83, 39);
            this.textBoxClassifier.Name = "textBoxClassifier";
            this.textBoxClassifier.ReadOnly = true;
            this.textBoxClassifier.Size = new System.Drawing.Size(248, 19);
            this.textBoxClassifier.TabIndex = 4;

            // 
            // labelAuthority
            // 
            this.labelAuthority.Location = new System.Drawing.Point(7, 64);
            this.labelAuthority.Name = "labelAuthority";
            this.labelAuthority.Size = new System.Drawing.Size(66, 17);
            this.labelAuthority.TabIndex = 5;
            this.labelAuthority.Text = "Authority:";

            // 
            // textBoxAuthority
            // 
            this.textBoxAuthority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAuthority.Location = new System.Drawing.Point(83, 66);
            this.textBoxAuthority.Name = "textBoxAuthority";
            this.textBoxAuthority.ReadOnly = true;
            this.textBoxAuthority.Size = new System.Drawing.Size(248, 19);
            this.textBoxAuthority.TabIndex = 6;

            // 
            // labelXml
            // 
            this.labelXml.Location = new System.Drawing.Point(7, 90);
            this.labelXml.Name = "labelXml";
            this.labelXml.Size = new System.Drawing.Size(66, 17);
            this.labelXml.TabIndex = 7;
            this.labelXml.Text = "XML:";

            // 
            // textBoxXml
            // 
            this.textBoxXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXml.Location = new System.Drawing.Point(83, 93);
            this.textBoxXml.Multiline = true;
            this.textBoxXml.Name = "textBoxXml";
            this.textBoxXml.ReadOnly = true;
            this.textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxXml.Size = new System.Drawing.Size(248, 66);
            this.textBoxXml.TabIndex = 8;

            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(133, 173);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);

            // 
            // FormIdentity
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(341, 205);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxXml);
            this.Controls.Add(this.labelXml);
            this.Controls.Add(this.textBoxAuthority);
            this.Controls.Add(this.labelAuthority);
            this.Controls.Add(this.textBoxClassifier);
            this.Controls.Add(this.labelClassifier);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.MinimumSize = new System.Drawing.Size(329, 232);
            this.Name = "FormIdentity";
            this.Text = "FormIdentity";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        /// <summary>
        ///  Close the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
