using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SDKSample
{
	/// <summary>
	/// Summary description for frmButtonHolder.
	/// </summary>
	public class frmButtonHolder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public frmButtonHolder()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();

// 
// button1
// 
			this.button1.Location = new System.Drawing.Point(173, 87);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 33);
			this.button1.TabIndex = 0;
			this.button1.Text = "Button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);

// 
// frmButtonHolder
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 269);
			this.Controls.Add(this.button1);
			this.Name = "frmButtonHolder";
			this.Text = "frmButtonHolder";
			this.Load += new System.EventHandler(this.frmButtonHolder_Load);
			this.ResumeLayout(false);
		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Button1 invoked.");
		}

		private void frmButtonHolder_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
