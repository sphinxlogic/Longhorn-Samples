using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collaboration;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    public class AddContactDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        internal System.Windows.Forms.TextBox realTimeAddress;
        private System.Windows.Forms.Button cancelButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public AddContactDialog(RealTimeProfile profile, Contact contact)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.Text = "Add contact";
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
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.realTimeAddress = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Real time address";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(232, 40);
            this.okButton.Name = "okButton";
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            // 
            // realTimeAddress
            // 
            this.realTimeAddress.Location = new System.Drawing.Point(112, 8);
            this.realTimeAddress.Name = "realTimeAddress";
            this.realTimeAddress.Size = new System.Drawing.Size(296, 20);
            this.realTimeAddress.TabIndex = 0;
            this.realTimeAddress.Text = "";
            this.realTimeAddress.Validating += new System.ComponentModel.CancelEventHandler(this.realTimeAddress_Validating);
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(328, 40);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            // 
            // AddContactDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(408, 69);
            this.Controls.Add(this.realTimeAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddContactDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
        #endregion

        private void realTimeAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(this.realTimeAddress.Text.Length == 0)
            {
                MessageBox.Show(
                    this, 
                    "You need to specify a valid real time address.", 
                    "Invalid real time address", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                e.Cancel = true;
            }
            else if(this.realTimeAddress.Text.IndexOf(':') == (-1))
            {
                MessageBox.Show(
                    this, 
                    "You need to specify the real time address type (i.e. \"sip:\").",
                    "Invalid real time address", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                e.Cancel = true;
            }
        }
    }
}
