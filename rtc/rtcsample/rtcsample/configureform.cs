using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    /// <summary>
    /// Summary description for ConfigureForm.
    /// </summary>
    public class ConfigurationDialog : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.TextBox serverName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox signInName;
        internal System.Windows.Forms.TextBox userName;
        internal System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        internal System.Windows.Forms.CheckBox autoAcceptChat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ComboBox transportComboBox;
        internal System.Windows.Forms.ComboBox authenticationComboBox;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ConfigurationDialog()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Create or open a key
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\minirtcsample");

            // Get the data from a specified item in the key.
            this.signInName.Text = (String)rk.GetValue("signInName", "sip uri");
            this.userName.Text = (String)rk.GetValue("userName", "user account");
            this.serverName.Text = (String)rk.GetValue("serverName", "sip server");
            this.transportComboBox.SelectedIndex = 0;
            this.authenticationComboBox.SelectedIndex = 0;

            this.okButton.Click +=new EventHandler(OKButton_Click);
            this.cancelButton.Click +=new EventHandler(CancelButton_Click);
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
            this.serverName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.signInName = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.autoAcceptChat = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.transportComboBox = new System.Windows.Forms.ComboBox();
            this.authenticationComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // serverName
            // 
            this.serverName.Location = new System.Drawing.Point(104, 16);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(280, 20);
            this.serverName.TabIndex = 0;
            this.serverName.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sign in name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "User name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password:";
            // 
            // signInName
            // 
            this.signInName.Location = new System.Drawing.Point(104, 136);
            this.signInName.Name = "signInName";
            this.signInName.Size = new System.Drawing.Size(280, 20);
            this.signInName.TabIndex = 6;
            this.signInName.Text = "";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(104, 176);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(280, 20);
            this.userName.TabIndex = 7;
            this.userName.Text = "";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(104, 216);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(280, 20);
            this.password.TabIndex = 8;
            this.password.Text = "";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(208, 280);
            this.okButton.Name = "okButton";
            this.okButton.TabIndex = 12;
            this.okButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(304, 280);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            // 
            // autoAcceptChat
            // 
            this.autoAcceptChat.Checked = true;
            this.autoAcceptChat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoAcceptChat.Location = new System.Drawing.Point(272, 248);
            this.autoAcceptChat.Name = "autoAcceptChat";
            this.autoAcceptChat.Size = new System.Drawing.Size(112, 24);
            this.autoAcceptChat.TabIndex = 14;
            this.autoAcceptChat.Text = "Auto Accept Chat";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Transport";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Authentication";
            // 
            // transportComboBox
            // 
            this.transportComboBox.Items.AddRange(new object[] {
                                                                   "TCP",
                                                                   "TLS"});
            this.transportComboBox.Location = new System.Drawing.Point(104, 56);
            this.transportComboBox.Name = "transportComboBox";
            this.transportComboBox.Size = new System.Drawing.Size(144, 21);
            this.transportComboBox.TabIndex = 15;
            this.transportComboBox.Text = "comboBox1";
            // 
            // authenticationComboBox
            // 
            this.authenticationComboBox.Items.AddRange(new object[] {
                                                                        "NTLM",
                                                                        "Kerberos"});
            this.authenticationComboBox.Location = new System.Drawing.Point(104, 96);
            this.authenticationComboBox.Name = "authenticationComboBox";
            this.authenticationComboBox.Size = new System.Drawing.Size(144, 21);
            this.authenticationComboBox.TabIndex = 15;
            this.authenticationComboBox.Text = "authenticationComboBox";
            // 
            // ConfigurationDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(386, 311);
            this.Controls.Add(this.transportComboBox);
            this.Controls.Add(this.autoAcceptChat);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.signInName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.authenticationComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigurationDialog_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            // Create or open a key
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\minirtcsample");

            // Get the data from a specified item in the key.
            rk.SetValue("signInName", this.signInName.Text);
            rk.SetValue("userName", this.userName.Text);
            rk.SetValue("serverName", this.serverName.Text);


            this.Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ConfigurationDialog_Load(object sender, System.EventArgs e)
        {
        
        }    
    }
}
