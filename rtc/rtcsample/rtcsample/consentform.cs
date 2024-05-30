using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Samples.Collaboration.RtcSample
{
	/// <summary>
	/// Summary description for ConsentForm.
	/// </summary>
	public class ConsentForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.RichTextBox consentText;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button declineButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConsentForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        public string Message
        {
            get
            {
                return this.consentText.Text;
            }

            set
            {
                this.consentText.Text = value;
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
            this.consentText = new System.Windows.Forms.RichTextBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.declineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // consentText
            // 
            this.consentText.Dock = System.Windows.Forms.DockStyle.Top;
            this.consentText.ForeColor = System.Drawing.Color.Blue;
            this.consentText.Location = new System.Drawing.Point(0, 0);
            this.consentText.Name = "consentText";
            this.consentText.ReadOnly = true;
            this.consentText.Size = new System.Drawing.Size(330, 96);
            this.consentText.TabIndex = 0;
            this.consentText.Text = "";
            // 
            // acceptButton
            // 
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(152, 104);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.TabIndex = 0;
            this.acceptButton.Text = "Accept";
            // 
            // declineButton
            // 
            this.declineButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.declineButton.Location = new System.Drawing.Point(248, 104);
            this.declineButton.Name = "declineButton";
            this.declineButton.TabIndex = 1;
            this.declineButton.Text = "Decline";
            // 
            // ConsentForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.declineButton;
            this.ClientSize = new System.Drawing.Size(330, 135);
            this.Controls.Add(this.declineButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.consentText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsentForm";
            this.Text = "Invitation";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
