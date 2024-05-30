//=====================================================================
// File:      DlgImportIdentity.cs
//
// Summary:   Dialog to import a PeerIdentity from a file.
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
using System.ComponentModel;
using System.Net.PeerToPeer;
using System.Windows.Forms;

namespace IdentityViewer
{
    /// <summary>
    /// Dialog to import a PeerIdentity from a file.
    /// </summary>
    public class DlgImportIdentity : System.Windows.Forms.Form
    {
        private ExportedPeerIdentity exportedIdentity;
        private PeerIdentity identityValue;

        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelAuthority;
        private System.Windows.Forms.TextBox textBoxAuthority;
        private System.Windows.Forms.Label labelClassifier;
        private System.Windows.Forms.TextBox textBoxClassifier;
        private System.Windows.Forms.Label labelXml;
        private System.Windows.Forms.TextBox textBoxXml;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBrowse;
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor to create a dialog to import an identity.
        /// </summary>
        public DlgImportIdentity()
        {
            InitializeComponent();
        }

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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.textBoxXml = new System.Windows.Forms.TextBox();
            this.labelXml = new System.Windows.Forms.Label();
            this.textBoxClassifier = new System.Windows.Forms.TextBox();
            this.labelClassifier = new System.Windows.Forms.Label();
            this.textBoxAuthority = new System.Windows.Forms.TextBox();
            this.labelAuthority = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();

            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(97, 218);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(193, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(61, 12);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(219, 19);
            this.textBoxFile.TabIndex = 1;
            this.textBoxFile.Leave += new System.EventHandler(this.textBoxFile_Leave);

            // 
            // labelFile
            // 
            this.labelFile.Location = new System.Drawing.Point(4, 12);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(59, 13);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "&File:";

            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(292, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(60, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "&Browse...";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);

            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(61, 38);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(219, 19);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.UseSystemPasswordChar = true;

            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(4, 38);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(59, 13);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "&Password:";

            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.textBoxXml);
            this.groupBox.Controls.Add(this.labelXml);
            this.groupBox.Controls.Add(this.textBoxClassifier);
            this.groupBox.Controls.Add(this.labelClassifier);
            this.groupBox.Controls.Add(this.textBoxAuthority);
            this.groupBox.Controls.Add(this.labelAuthority);
            this.groupBox.Location = new System.Drawing.Point(8, 67);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(345, 138);
            this.groupBox.TabIndex = 5;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Indentity information";

            // 
            // textBoxXml
            // 
            this.textBoxXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXml.Location = new System.Drawing.Point(75, 69);
            this.textBoxXml.Multiline = true;
            this.textBoxXml.Name = "textBoxXml";
            this.textBoxXml.ReadOnly = true;
            this.textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxXml.Size = new System.Drawing.Size(260, 60);
            this.textBoxXml.TabIndex = 5;
            this.textBoxXml.TabStop = false;

            // 
            // labelXml
            // 
            this.labelXml.Location = new System.Drawing.Point(8, 69);
            this.labelXml.Name = "labelXml";
            this.labelXml.Size = new System.Drawing.Size(59, 13);
            this.labelXml.TabIndex = 4;
            this.labelXml.Text = "XML:";

            // 
            // textBoxClassifier
            // 
            this.textBoxClassifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClassifier.Location = new System.Drawing.Point(75, 43);
            this.textBoxClassifier.Name = "textBoxClassifier";
            this.textBoxClassifier.ReadOnly = true;
            this.textBoxClassifier.Size = new System.Drawing.Size(260, 19);
            this.textBoxClassifier.TabIndex = 3;
            this.textBoxClassifier.TabStop = false;

            // 
            // labelClassifier
            // 
            this.labelClassifier.Location = new System.Drawing.Point(8, 43);
            this.labelClassifier.Name = "labelClassifier";
            this.labelClassifier.Size = new System.Drawing.Size(59, 13);
            this.labelClassifier.TabIndex = 2;
            this.labelClassifier.Text = "Classifier:";

            // 
            // textBoxAuthority
            // 
            this.textBoxAuthority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAuthority.Location = new System.Drawing.Point(75, 18);
            this.textBoxAuthority.Name = "textBoxAuthority";
            this.textBoxAuthority.ReadOnly = true;
            this.textBoxAuthority.Size = new System.Drawing.Size(260, 19);
            this.textBoxAuthority.TabIndex = 1;
            this.textBoxAuthority.TabStop = false;

            // 
            // labelAuthority
            // 
            this.labelAuthority.Location = new System.Drawing.Point(8, 18);
            this.labelAuthority.Name = "labelAuthority";
            this.labelAuthority.Size = new System.Drawing.Size(59, 13);
            this.labelAuthority.TabIndex = 0;
            this.labelAuthority.Text = "Authority:";

            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.idx";
            this.openFileDialog.Filter = "Exported identity files|*.idx|All files|*.*";

            // 
            // DlgImportIdentity
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(360, 252);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(368, 279);
            this.Name = "DlgImportIdentity";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Identity";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion


        /// <summary>
        ///  Try to create a PeerIdentity from the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            identityValue = ImportIdentity(exportedIdentity, textBoxPassword.Text);
            if (identityValue != null)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Try to import the exported identity.
        /// </summary>
        /// </summary>
        /// <param name="exportedData"></param>
        /// <param name="password"></param>
        /// <returns>The imported identity or null if it failed.</returns>
        private PeerIdentity ImportIdentity(ExportedPeerIdentity exportedData, string password)
        {
            PeerIdentity identity = null;

            try
            {
                identity = PeerIdentity.Import(exportedData, password);
            }
            catch (Exception e)
            {
                Utilities.DisplayException(e, this);
            }

            return identity;
        }

        /// <summary>
        /// Close the dialog without importing any information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Select a filename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            if (DialogResult.OK == openFileDialog.ShowDialog(this))
            {
                textBoxFile.Text = openFileDialog.FileName;
                UpdateInformation();
            }
        }

        /// <summary>
        /// Handle a focus chance by updating the import information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFile_Leave(object sender, System.EventArgs e)
        {
            UpdateInformation();
        }

        /// <summary>
        /// Refresh the import information based on the selected file.
        /// </summary>
        private void UpdateInformation()
        {
            bool fValid = false;

            try
            {
                string xml = Utilities.ReadTextFile(textBoxFile.Text);
                if (0 < xml.Length)
                {
                    exportedIdentity = new ExportedPeerIdentity(xml);
                    textBoxAuthority.Text = exportedIdentity.PeerName.Authority;
                    textBoxClassifier.Text = exportedIdentity.PeerName.Classifier;
                    textBoxXml.Text = xml;
                    fValid = true;
                }
            }
            catch
            {
                // Suppress all errors since the user may not have entered
                // the correct filename.  The other text fields will be cleared.
            }

            if (!fValid)
            {
                exportedIdentity = null;
                textBoxAuthority.Text = string.Empty;
                textBoxClassifier.Text = string.Empty;
                textBoxXml.Text = string.Empty;
            }
        }
    }
}
