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
using System.Storage.Contacts;
using System.Storage.Core;

namespace Microsoft.Samples.WinFS
{
	/// <summary>
	/// Summary description for EditDialog.
	/// </summary>
	public class EditDialog : System.Windows.Forms.Form
	{
		private VCard editVCard = null;
		private Person editWinFSPerson = null;
		private System.Windows.Forms.TextBox DisplayNameText;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox EmailText;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public bool ShowDelete { get { return deleteButton.Visible; } set { deleteButton.Visible = value; } }

		private EditDialog()
		{
			InitializeComponent();
		}
		public EditDialog(Person editPerson) : this()
		{
			DisplayNameText.Text = editPerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			if (editPerson.PrimaryEmailAddress != null)
				EmailText.Text = editPerson.PrimaryEmailAddress.Address;
			else
				EmailText.Text = "someone@microsoft.com";

			editWinFSPerson = editPerson;
		}
		public EditDialog(VCard editCard) : this()
		{
			DisplayNameText.Text = editCard.DisplayName;
			EmailText.Text = editCard.EMail;
			editVCard = editCard;
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
			this.DisplayNameText = new System.Windows.Forms.TextBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.deleteButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.EmailText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();

// 
// DisplayNameText
// 
			this.DisplayNameText.Location = new System.Drawing.Point(101, 9);
			this.DisplayNameText.Name = "DisplayNameText";
			this.DisplayNameText.Size = new System.Drawing.Size(159, 20);
			this.DisplayNameText.TabIndex = 0;

// 
// OKButton
// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Location = new System.Drawing.Point(146, 58);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(54, 27);
			this.OKButton.TabIndex = 3;
			this.OKButton.Text = "&OK";
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);

// 
// label1
// 
			this.label1.Location = new System.Drawing.Point(14, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 21);
			this.label1.TabIndex = 2;
			this.label1.Text = "Display Name:";

// 
// deleteButton
// 
			this.deleteButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.deleteButton.Location = new System.Drawing.Point(16, 58);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(54, 27);
			this.deleteButton.TabIndex = 2;
			this.deleteButton.Text = "&Delete";

// 
// cancelButton
// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(207, 57);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(53, 28);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";

// 
// EmailText
// 
			this.EmailText.Location = new System.Drawing.Point(101, 36);
			this.EmailText.Name = "EmailText";
			this.EmailText.Size = new System.Drawing.Size(159, 20);
			this.EmailText.TabIndex = 1;

// 
// label2
// 
			this.label2.Location = new System.Drawing.Point(14, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 21);
			this.label2.TabIndex = 6;
			this.label2.Text = "E-Mail:";

// 
// EditDialog
// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(269, 91);
			this.Controls.Add(this.EmailText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.DisplayNameText);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "EditDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Contact";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		private void OKButton_Click(object sender, System.EventArgs e)
		{
			//If the VCard was used to initialize the form
			if (editVCard != null)
			{
				editVCard.DisplayName = DisplayNameText.Text;
				editVCard.EMail = EmailText.Text;
			}

			//If the WinFS Person was used to initialize the form
			if (editWinFSPerson != null)
			{
				editWinFSPerson.DisplayName = DisplayNameText.Text;
				if (EmailText.Text.Length > 0)
				{
					if (editWinFSPerson.PrimaryEmailAddress == null)
					{
						SmtpEmailAddress email = new SmtpEmailAddress();

						email.Keywords.Add(new Keyword(StandardKeywords.Primary));
						email.AccessPointType = new Keyword("email");
						editWinFSPerson.EAddresses.Add(email);
					}

					editWinFSPerson.PrimaryEmailAddress.Address = EmailText.Text;
					editWinFSPerson.PrimaryEmailAddress.AccessPoint = editWinFSPerson.PrimaryEmailAddress.Address;
				}
			}
		}
	}
}
