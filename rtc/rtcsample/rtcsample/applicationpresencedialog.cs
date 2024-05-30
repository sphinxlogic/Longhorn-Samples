using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    public class ApplicationPresenceDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox applicationString;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox applicationValue;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox homeTextBox;
        internal System.Windows.Forms.RichTextBox informationRichTextBox;
        internal System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label label5;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ApplicationPresenceDialog(TestPresence presence, bool enabled)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            if(presence != null)
            {
                this.Text = "Detailed presence view";

                this.homeTextBox.Text = presence.Home;
                this.emailTextBox.Text = presence.Email;
                this.informationRichTextBox.Text = presence.Information;
                this.applicationString.Text = presence.TestString;
                this.applicationValue.Text = presence.TestValue.ToString(CultureInfo.InvariantCulture);

                this.homeTextBox.Enabled = enabled;
                this.emailTextBox.Enabled = enabled;
                this.informationRichTextBox.ReadOnly = !enabled;
                this.informationRichTextBox.Enabled = enabled;
                this.applicationString.Enabled = enabled;
                this.applicationValue.Enabled = enabled;
            }
            else
            {
                this.Text = "Set application presence";
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
            this.label1 = new System.Windows.Forms.Label();
            this.applicationString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.applicationValue = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.homeTextBox = new System.Windows.Forms.TextBox();
            this.informationRichTextBox = new System.Windows.Forms.RichTextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Location = new System.Drawing.Point(8, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Custom property";
            // 
            // applicationString
            // 
            this.applicationString.CausesValidation = false;
            this.applicationString.Location = new System.Drawing.Point(120, 152);
            this.applicationString.Name = "applicationString";
            this.applicationString.Size = new System.Drawing.Size(224, 20);
            this.applicationString.TabIndex = 3;
            this.applicationString.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.CausesValidation = false;
            this.label2.Location = new System.Drawing.Point(8, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Custom value";
            // 
            // applicationValue
            // 
            this.applicationValue.CausesValidation = false;
            this.applicationValue.Location = new System.Drawing.Point(120, 184);
            this.applicationValue.Name = "applicationValue";
            this.applicationValue.Size = new System.Drawing.Size(224, 20);
            this.applicationValue.TabIndex = 4;
            this.applicationValue.Text = "";
            this.applicationValue.Validating += new System.ComponentModel.CancelEventHandler(this.applicationValue_Validating);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(176, 224);
            this.okButton.Name = "okButton";
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(264, 224);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.TabIndex = 6;
            this.label3.Text = "Information";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.TabIndex = 8;
            this.label4.Text = "Home";
            // 
            // homeTextBox
            // 
            this.homeTextBox.Location = new System.Drawing.Point(120, 8);
            this.homeTextBox.Name = "homeTextBox";
            this.homeTextBox.Size = new System.Drawing.Size(224, 20);
            this.homeTextBox.TabIndex = 0;
            this.homeTextBox.Text = "";
            // 
            // informationRichTextBox
            // 
            this.informationRichTextBox.Location = new System.Drawing.Point(120, 72);
            this.informationRichTextBox.Name = "informationRichTextBox";
            this.informationRichTextBox.Size = new System.Drawing.Size(224, 64);
            this.informationRichTextBox.TabIndex = 2;
            this.informationRichTextBox.Text = "";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(120, 40);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(224, 20);
            this.emailTextBox.TabIndex = 1;
            this.emailTextBox.Text = "";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 40);
            this.label5.Name = "label5";
            this.label5.TabIndex = 11;
            this.label5.Text = "E-mail";
            // 
            // ApplicationPresenceDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(344, 253);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.informationRichTextBox);
            this.Controls.Add(this.homeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.applicationValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.applicationString);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationPresenceDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
        #endregion

        private void applicationValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for(int index = 0; index < this.applicationValue.Text.Length; index++)
            {
                if(!char.IsDigit(this.applicationValue.Text, index))
                {
                    MessageBox.Show(
                        this, 
                        "The application value must be a number.", 
                        "Error", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);

                    e.Cancel = true;

                    break;
                }
            }
        }
    }
}
