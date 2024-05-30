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
using System.Data;

namespace Microsoft.Samples.NaturalLanguage
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class App : System.Windows.Forms.Form
	{
		#region private variables
		private System.Windows.Forms.MainMenu standardMainMenu;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem helpMenuItem;
		private System.Windows.Forms.MenuItem contentsMenuItem;
		private System.Windows.Forms.MenuItem indexMenuItem;
		private System.Windows.Forms.MenuItem separatorMenuItem5;
		private System.Windows.Forms.MenuItem aboutMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.RichTextBox textSearch;
		private System.Windows.Forms.LinkLabel labelDidYouMean;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.WebBrowser webBrowser2;
		private System.Windows.Forms.WebBrowser webBrowser3;
		private System.ComponentModel.IContainer components;

		bool bshowDYM;
		SearchTuner searchTuner;
		MenuItem itemHighLighted;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public App()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.standardMainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.contentsMenuItem = new System.Windows.Forms.MenuItem();
			this.indexMenuItem = new System.Windows.Forms.MenuItem();
			this.separatorMenuItem5 = new System.Windows.Forms.MenuItem();
			this.aboutMenuItem = new System.Windows.Forms.MenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnGo = new System.Windows.Forms.Button();
			this.textSearch = new System.Windows.Forms.RichTextBox();
			this.labelDidYouMean = new System.Windows.Forms.LinkLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.webBrowser2 = new System.Windows.Forms.WebBrowser();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.webBrowser3 = new System.Windows.Forms.WebBrowser();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();

// 
// standardMainMenu
// 
			this.standardMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.fileMenuItem, this.helpMenuItem
			});
			this.standardMainMenu.Name = "standardMainMenu";

// 
// fileMenuItem
// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.exitMenuItem
			});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.Text = "&File";

// 
// exitMenuItem
// 
			this.exitMenuItem.Index = 0;
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Text = "E&xit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);

// 
// helpMenuItem
// 
			this.helpMenuItem.Index = 1;
			this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.contentsMenuItem, this.indexMenuItem, this.separatorMenuItem5, this.aboutMenuItem
			});
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.Text = "&Help";

// 
// contentsMenuItem
// 
			this.contentsMenuItem.Index = 0;
			this.contentsMenuItem.Name = "contentsMenuItem";
			this.contentsMenuItem.Text = "&Contents";

// 
// indexMenuItem
// 
			this.indexMenuItem.Index = 1;
			this.indexMenuItem.Name = "indexMenuItem";
			this.indexMenuItem.Text = "&Index";

// 
// separatorMenuItem5
// 
			this.separatorMenuItem5.Index = 2;
			this.separatorMenuItem5.Name = "separatorMenuItem5";
			this.separatorMenuItem5.Text = "-";

// 
// aboutMenuItem
// 
			this.aboutMenuItem.Index = 3;
			this.aboutMenuItem.Name = "aboutMenuItem";
			this.aboutMenuItem.Text = "&About...";

// 
// imageList1
// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;

// 
// splitContainer1
// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer1.Size = new System.Drawing.Size(809, 605);
			this.splitContainer1.SplitterDistance = 58;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer1.Text = "splitContainer1";

// 
// splitterPanel1
// 
			this.splitContainer1.Panel1.Controls.Add(this.btnGo);
			this.splitContainer1.Panel1.Controls.Add(this.textSearch);

// 
// splitterPanel2
// 
			this.splitContainer1.Panel2.Controls.Add(this.labelDidYouMean);
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);

// 
// btnGo
// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnGo.Location = new System.Drawing.Point(731, 14);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(48, 32);
			this.btnGo.TabIndex = 1;
			this.btnGo.Text = "Go";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);

// 
// textSearch
// 
			this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.textSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textSearch.Location = new System.Drawing.Point(23, 14);
			this.textSearch.Multiline = false;
			this.textSearch.Name = "textSearch";
			this.textSearch.Size = new System.Drawing.Size(682, 32);
			this.textSearch.TabIndex = 0;
			this.textSearch.Text = "";
			this.textSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSearch_KeyDown);

// 
// labelDidYouMean
// 
			this.labelDidYouMean.BackColor = System.Drawing.SystemColors.MenuBar;
			this.labelDidYouMean.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDidYouMean.Links.Add(new System.Windows.Forms.LinkLabel.Link(0, 0));
			this.labelDidYouMean.Location = new System.Drawing.Point(160, -1);
			this.labelDidYouMean.Name = "labelDidYouMean";
			this.labelDidYouMean.Size = new System.Drawing.Size(619, 22);
			this.labelDidYouMean.TabIndex = 1;
			this.labelDidYouMean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelDidYouMean.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelDidYouMean_LinkClicked);

// 
// tabControl1
// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowToolTips = true;
			this.tabControl1.Size = new System.Drawing.Size(809, 541);
			this.tabControl1.TabIndex = 2;

// 
// tabPage1
// 
			this.tabPage1.Controls.Add(this.webBrowser1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(801, 536);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "MSN";
			this.tabPage1.Click += new System.EventHandler(this.Tab_Click);

// 
// webBrowser1
// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(3, 3);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(795, 530);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.Tag = "http://search.msn.com/results.asp?RS=CHECKED&FORM=MSNH&v=1&q={0}";
			this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.DocumentCompleted);

// 
// tabPage2
// 
			this.tabPage2.Controls.Add(this.webBrowser2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(801, 536);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "MSDN";
			this.tabPage2.Click += new System.EventHandler(this.Tab_Click);

// 
// webBrowser2
// 
			this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser2.Location = new System.Drawing.Point(3, 3);
			this.webBrowser2.Name = "webBrowser2";
			this.webBrowser2.Size = new System.Drawing.Size(795, 530);
			this.webBrowser2.TabIndex = 1;
			this.webBrowser2.Tag = "http://search.microsoft.com/search/results.aspx?View=msdn&st=a&qu={0}&c=0&s=1";

// 
// tabPage3
// 
			this.tabPage3.Controls.Add(this.webBrowser3);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(801, 515);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Microsoft";
			this.tabPage3.Click += new System.EventHandler(this.Tab_Click);

// 
// webBrowser3
// 
			this.webBrowser3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser3.Location = new System.Drawing.Point(3, 3);
			this.webBrowser3.Name = "webBrowser3";
			this.webBrowser3.Size = new System.Drawing.Size(795, 509);
			this.webBrowser3.TabIndex = 1;
			this.webBrowser3.Tag = "http://search.microsoft.com/search/results.aspx?st=b&qu={0}&view=en-us";

// 
// App
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(809, 605);
			this.Controls.Add(this.splitContainer1);
			this.Menu = this.standardMainMenu;
			this.Name = "App";
			this.Text = "Did You Mean";
			this.Load += new System.EventHandler(this.App_Load);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);
		}
			#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new App());
		}

		private void btnGo_Click(System.Object sender, System.EventArgs e)
		{
			SearchAll();
		}

		private void labelDidYouMean_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.textSearch.Text = (string)e.Link.LinkData;
			labelDidYouMean.Visible = false;
			SearchAll();
		}

		private void App_Load(object sender, System.EventArgs e)
		{
			Search = new SearchTuner();
			labelDidYouMean.Visible = false;
			btnGo.NotifyDefault(true);
		}

		private void DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
		{
			labelDidYouMean.Visible = ShowDidYouMean;
		}

		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void Tab_Click(object sender, System.EventArgs e)
		{
			TabPage button = (TabPage)sender;

			labelDidYouMean.Visible = false;
			if (textSearch.Text != null && textSearch.Text.Length == 0) btnGo.PerformClick();

			// start the search
			string text = this.textSearch.Text;
			string revised = Search.SpellCheck(text);

			if (revised != null && revised != text)
			{
				labelDidYouMean.Text = String.Format(System.Globalization.CultureInfo.CurrentUICulture, "Did you mean \"{0}\"?", revised);
				labelDidYouMean.Links.Clear();
				labelDidYouMean.Links.Add(labelDidYouMean.Text.IndexOf('"') + 1, revised.Length, revised);
				labelDidYouMean.Visible = false;
				ShowDidYouMean = true;
			}
			else
			{
				ShowDidYouMean = false;
			}

			SearchAll();
		}

		public bool ShowDidYouMean
		{
			get
			{
				return bshowDYM;
			}
			set
			{
				bshowDYM = value;
			}
		}

		public SearchTuner Search
		{
			get
			{
				return searchTuner;
			}
			set
			{
				searchTuner = value;
			}
		}

		public MenuItem SearchItemHighlighted
		{
			get
			{
				return itemHighLighted;
			}
			set
			{
				itemHighLighted = value;
			}
		}

		public void SearchAll()
		{
			string text = this.textSearch.Text;
			string revised = Search.SpellCheck(text);

			if (revised != null && revised != text)
			{
				labelDidYouMean.Text = String.Format(System.Globalization.CultureInfo.CurrentUICulture, "Did you mean \"{0}\"?", revised);
				labelDidYouMean.Links.Clear();
				labelDidYouMean.Links.Add(labelDidYouMean.Text.IndexOf('"') + 1, revised.Length, revised);
				labelDidYouMean.Visible = false;
				ShowDidYouMean = true;
			}
			else
			{
				ShowDidYouMean = false;
			}
			
			for (int i = 0; i < this.tabControl1.Controls.Count; i++)
			{
				WebBrowser thisBrowser = (WebBrowser)this.tabControl1.Controls[i].Controls[0];

				Search.LinkFormat =(string)thisBrowser.Tag;
				labelDidYouMean.Visible = false;
				thisBrowser.Url = Search.Link(this.textSearch.Text);
			}
		}

		private void textSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				SearchAll();
			}
		}
	}
}
