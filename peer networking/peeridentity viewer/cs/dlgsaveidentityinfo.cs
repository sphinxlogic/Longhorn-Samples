//=====================================================================
// File:      DlgSaveIdentityInfo.cs
//
// Summary:   Dialog to save the information for a PeerIdentity to a file.
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
using System.Windows.Forms;
using System.Net.PeerToPeer;

namespace IdentityViewer
{
    /// <summary>
    /// Dialog to save the information associated with a PeerIdentity.
    /// </summary>
    public class DlgSaveIdentityInfo : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label labelIdentity;
        private System.Windows.Forms.ComboBox dropListIdentity;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor to create a dialog with the selected PeerIdentity.
        /// </summary>
        /// <param name="identity">The PeerIdentity to initially select.</param>
        public DlgSaveIdentityInfo(PeerIdentity identity)
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
            this.buttonNew = new System.Windows.Forms.Button();
            this.dropListIdentity = new System.Windows.Forms.ComboBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();

            // 
            // labelIdentity
            // 
            this.labelIdentity.Location = new System.Drawing.Point(11, 19);
            this.labelIdentity.Name = "labelIdentity";
            this.labelIdentity.Size = new System.Drawing.Size(50, 13);
            this.labelIdentity.TabIndex = 0;
            this.labelIdentity.Text = "&Identity:";

            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.Location = new System.Drawing.Point(97, 80);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(193, 80);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(61, 47);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(219, 19);
            this.textBoxFile.TabIndex = 4;

            // 
            // labelFile
            // 
            this.labelFile.Location = new System.Drawing.Point(10, 47);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(50, 13);
            this.labelFile.TabIndex = 3;
            this.labelFile.Text = "&File:";

            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNew.Location = new System.Drawing.Point(292, 19);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(60, 23);
            this.buttonNew.TabIndex = 2;
            this.buttonNew.Text = "&New...";
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);

            // 
            // dropListIdentity
            // 
            this.dropListIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListIdentity.Location = new System.Drawing.Point(61, 19);
            this.dropListIdentity.Name = "dropListIdentity";
            this.dropListIdentity.Size = new System.Drawing.Size(219, 21);
            this.dropListIdentity.Sorted = true;
            this.dropListIdentity.TabIndex = 1;

            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(292, 47);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(60, 23);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "&Browse...";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);

            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "idt";
            this.saveFileDialog.Filter = "Identity Information Files|*.idt|All files|*.*";
            this.saveFileDialog.Title = "Save Identity Information as...";

            // 
            // DlgSaveIdentityInfo
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(360, 114);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.dropListIdentity);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelIdentity);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(999, 141);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 141);
            this.Name = "DlgSaveIdentityInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Identity Information";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion


        /// <summary>
        /// Save the information for a PeerIdentity to a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            if (SaveIdentityInformation())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Try to save the information associated with an identity
        /// </summary>
        /// <returns>True if the information was successfully saved.</returns>
        private bool SaveIdentityInformation()
        {
            bool success = false;
            
            try
            {
                PeerIdentity identity = (PeerIdentity)dropListIdentity.SelectedItem;
                PeerIdentityInfo info = identity.GetInfo();
                string xmlIdentityInfo = info.ToXmlString();

                // Save the XML as a Unicode Text file to be compatibile
                // with the Win32 samples.
                Utilities.WriteTextFile(textBoxFile.Text, xmlIdentityInfo);
                success = true;
            }
            catch (Exception e)
            {
                Utilities.DisplayException(e, this);
            }

            return success;
        }

        /// <summary>
        /// Close the dialog without saving any information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Show the subdialog to create a new PeerIdentity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, System.EventArgs e)
        {
            DlgNewIdentity dlg = new DlgNewIdentity();
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                Utilities.FillIdentityComboBox(dropListIdentity, dlg.Identity);
            }
        }
        
        /// <summary>
        /// Select a filename to use.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            if (textBoxFile.TextLength == 0)
            {
                // default the filename to the selected PeerIdentity.
                saveFileDialog.FileName = dropListIdentity.Text;
            }

            if (DialogResult.OK == saveFileDialog.ShowDialog(this))
            {
                textBoxFile.Text = saveFileDialog.FileName;
            }
        }
    }
}
