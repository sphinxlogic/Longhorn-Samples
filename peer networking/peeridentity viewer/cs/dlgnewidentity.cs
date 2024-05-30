//=====================================================================
// File:      DlgNewIdentity.cs
//
// Summary:   Dialog to create a new PeerIdentity.
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
    /// Dialog to create a new PeerIdentity.
    /// </summary>
    public class DlgNewIdentity : System.Windows.Forms.Form
    {
        private PeerIdentity identityValue = null;

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelClassifier;
        private System.Windows.Forms.TextBox textBoxClassifier;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor to create the dialog.
        /// </summary>
        public DlgNewIdentity()
        {
            InitializeComponent();

            // Default the name to the user's login name.
            textBoxName.Text = System.Environment.UserName;
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
            this.textBoxClassifier = new System.Windows.Forms.TextBox();
            this.labelClassifier = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(16, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(48, 16);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";

            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(72, 16);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(204, 19);
            this.textBoxName.TabIndex = 1;

            // 
            // textBoxClassifier
            // 
            this.textBoxClassifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClassifier.Location = new System.Drawing.Point(72, 56);
            this.textBoxClassifier.Name = "textBoxClassifier";
            this.textBoxClassifier.Size = new System.Drawing.Size(204, 19);
            this.textBoxClassifier.TabIndex = 3;

            // 
            // labelClassifier
            // 
            this.labelClassifier.Location = new System.Drawing.Point(16, 56);
            this.labelClassifier.Name = "labelClassifier";
            this.labelClassifier.Size = new System.Drawing.Size(56, 16);
            this.labelClassifier.TabIndex = 2;
            this.labelClassifier.Text = "Classifier:";

            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(71, 96);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(159, 96);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // DlgNewIdentity
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(292, 133);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxClassifier);
            this.Controls.Add(this.labelClassifier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgNewIdentity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Identity";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion


        /// <summary>
        /// The PeerIdentity that was imported.
        /// </summary>
        /// <value></value>
        public PeerIdentity Identity
        {
            get
            {
                return identityValue;
            }
        }

        /// <summary>
        /// Validate the data in the dialog and create the Identity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            if (CreateIdentity())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Try to create the identity.
        /// </summary>
        /// <returns></returns>
        private bool CreateIdentity()
        {
            // Validate the classifier
            textBoxClassifier.Text = textBoxClassifier.Text.Trim();
            if (textBoxClassifier.TextLength >= 150)
            {
                MessageBox.Show(this, "The classifier must be less than 150 characters",
                    "Invalid Classifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate the friendly name
            textBoxName.Text = textBoxName.Text.Trim();
            if (textBoxName.TextLength >= 256)
            {
                MessageBox.Show(this, "Identity friendly must be less than 256 characters",
                    "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                identityValue = new PeerIdentity(textBoxName.Text, textBoxClassifier.Text);
                return true;
            }
            catch (Exception e)
            {
                Utilities.DisplayException(e, this);
                return false;
            }
        }

        /// <summary>
        /// Close the dialog without creating a new identity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
