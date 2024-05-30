using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControlLibrary
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class MyControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private string _messageText;

		public MyControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			_messageText = "Hello World";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();

// 
// button1
// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 40);
			this.button1.TabIndex = 0;
			this.button1.Text = "WindowsFormsButton";
			this.button1.Click += new System.EventHandler(this.button1_Click);

// 
// MyControl
// 
			this.Controls.Add(this.button1);
			this.Name = "MyControl";
			this.Size = new System.Drawing.Size(200, 40);
			this.ResumeLayout(false);
		}
		#endregion
		public string MessageText
		{
			get { return _messageText; }
			set
			{
				if (_messageText != value)
					_messageText = value;
			}
		}

		public event EventHandler BeforeMessageBox;

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (BeforeMessageBox != null)
				BeforeMessageBox(this, e);

			MessageBox.Show(_messageText);
		}
	}
}
