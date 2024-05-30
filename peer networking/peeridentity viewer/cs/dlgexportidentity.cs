//=====================================================================
// File:      DlgExportIdentity.cs
//
// Summary:   Dialog to export a PeerIdentity as a file.
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
using System.ComponentModel;
using System.Net.PeerToPeer;
using System.Windows.Forms;

namespace IdentityViewer
{
    /// <summary>
    /// Dialog to export a PeerIdentity to a file.
    /// </summary>
    public class DlgExportIdentity : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label labelIdentity;
        private System.Windows.Forms.ComboBox dropListIdentity;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        ///  Constructor that creates the dialog with the PeerIdentity selected.
        /// </summary>
        /// <param name="identity">The PeerIdentity to select</param>
        public DlgExportIdentity(PeerIdentity identity)
        {
            InitializeComponent();

            Utilities.FillIdentityComboBox(dropListIdentity, identity);
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
            this.labelIdentity = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.dropListIdentity = new System.Windows.Forms.ComboBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // labelIdentity
            // 
            this.labelIdentity.Location = new System.Drawing.Point(4, 19);
            this.labelIdentity.Name = "labelIdentity";
            this.labelIdentity.Size = new System.Drawing.Size(59, 13);
            this.labelIdentity.TabIndex = 0;
            this.labelIdentity.Text = "&Identity:";

            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(60, 103);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(156, 103);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(61, 47);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(196, 19);
            this.textBoxFile.TabIndex = 4;

            // 
            // labelFile
            // 
            this.labelFile.Location = new System.Drawing.Point(4, 47);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(59, 13);
            this.labelFile.TabIndex = 3;
            this.labelFile.Text = "&File:";

            // 
            // dropListIdentity
            // 
            this.dropListIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListIdentity.Location = new System.Drawing.Point(61, 19);
            this.dropListIdentity.Name = "dropListIdentity";
            this.dropListIdentity.Size = new System.Drawing.Size(226, 21);
            this.dropListIdentity.Sorted = true;
            this.dropListIdentity.TabIndex = 1;

            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(265, 47);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(22, 19);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);

            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "idx";
            this.saveFileDialog.Filter = "Exported identity files|*.idx|All files|*.*";
            this.saveFileDialog.Title = "Save Identity Information as...";

            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(61, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(226, 19);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.UseSystemPasswordChar = true;

            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(4, 73);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(59, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "&Password:";

            // 
            // DlgExportIdentity
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(290, 137);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.dropListIdentity);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelIdentity);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(999, 164);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(298, 164);
            this.Name = "DlgExportIdentity";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Identity";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        /// <summary>
        ///   Try to export the selected PeerIdentity to the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            if (ExportIdentity())
            {
                Close();
            }
        }

        /// <summary>
        /// Try to export the identity.
        /// </summary>
        /// <returns>True if the identity could be exported.</returns>
        private bool ExportIdentity()
        {
            bool success = false;

            try
            {
                PeerIdentity identity = (PeerIdentity)dropListIdentity.SelectedItem;
                string exportedData = identity.Export(textBoxPassword.Text);
                Utilities.WriteTextFile(textBoxFile.Text, exportedData);
                success = true;
            }
            catch (Exception e)
            {
                Utilities.DisplayException(e, this);
            }

            return success;
        }

        /// <summary>
        /// Close the dialog without exporting any information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        ///  Select a filename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            if (textBoxFile.TextLength == 0)
            {
                // default the file name to the selected identity.
                saveFileDialog.FileName = dropListIdentity.Text;
            }

            if (DialogResult.OK == saveFileDialog.ShowDialog(this))
            {
                textBoxFile.Text = saveFileDialog.FileName;
            }
        }
    }
}
