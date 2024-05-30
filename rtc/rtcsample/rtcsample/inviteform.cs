using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Samples.Collaboration.RtcSample
{
	/// <summary>
	/// Summary description for InviteForm.
	/// </summary>
	public class InviteForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TextBox uriTextBox;
        private System.Windows.Forms.Label uriLabel;
        private System.Windows.Forms.Button inviteButton;
        private System.Windows.Forms.Button cancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InviteForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        public string InviteeUri
        {
            get
            {
                return this.uriTextBox.Text;
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
            this.uriTextBox = new System.Windows.Forms.TextBox();
            this.uriLabel = new System.Windows.Forms.Label();
            this.inviteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uriTextBox
            // 
            this.uriTextBox.AcceptsReturn = true;
            this.uriTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.uriTextBox.Location = new System.Drawing.Point(24, 8);
            this.uriTextBox.Name = "uriTextBox";
            this.uriTextBox.Size = new System.Drawing.Size(272, 20);
            this.uriTextBox.TabIndex = 0;
            this.uriTextBox.Text = "";
            // 
            //uriLabel
            // 
            this.uriLabel.Location = new System.Drawing.Point(0, 8);
            this.uriLabel.Name = "uriLabel";
            this.uriLabel.Size = new System.Drawing.Size(24, 23);
            this.uriLabel.TabIndex = 1;
            this.uriLabel.Text = "URI";
            // 
            // inviteButton
            // 
            this.inviteButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.inviteButton.Location = new System.Drawing.Point(136, 32);
            this.inviteButton.Name = "inviteButton";
            this.inviteButton.TabIndex = 1;
            this.inviteButton.Text = "Invite";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(216, 32);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            // 
            // InviteForm
            // 
            this.AcceptButton = this.inviteButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(298, 63);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.inviteButton);
            this.Controls.Add(this.uriLabel);
            this.Controls.Add(this.uriTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InviteForm";
            this.Text = "Invite";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
