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
using System.Storage;
using System.Storage.Core;
using System.Storage.Image;

/* Notes about the Thumbnailer:
 * To effectively demonstrate this sample you must
 * copy some images to your WinFS DefaultStore. Once
 * you have done this, you can add "Categories" to the
 * photo to query against. To do this, right click on the
 * photo in explorer, choose properties. Then expand the
 * properties drop down and click on 'Add a category'.
 *
 * This sample will have more features added to it as time
 * such as querying by contacts in the photo, where the
 * photo was take, when the photo was taken, etc.
 */

namespace Microsoft.Samples.WinFS
{
	public class ThumbnailerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel searchPanel;
		private System.Windows.Forms.TextBox filenameText;

		private System.Windows.Forms.Label keywordLabel;

		private System.Windows.Forms.Label filenameLabel;

		private System.Windows.Forms.TextBox keywordText;

		private System.Windows.Forms.Button searchButton;

		private System.Windows.Forms.ListBox searchResults;
		private System.Windows.Forms.Button generateHtmlButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public ThumbnailerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
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
			this.searchPanel = new System.Windows.Forms.Panel();
			this.generateHtmlButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.keywordText = new System.Windows.Forms.TextBox();
			this.filenameText = new System.Windows.Forms.TextBox();
			this.keywordLabel = new System.Windows.Forms.Label();
			this.filenameLabel = new System.Windows.Forms.Label();
			this.searchResults = new System.Windows.Forms.ListBox();
			this.searchPanel.SuspendLayout();
			this.SuspendLayout();

// 
// searchPanel
// 
			this.searchPanel.Controls.Add(this.generateHtmlButton);
			this.searchPanel.Controls.Add(this.searchButton);
			this.searchPanel.Controls.Add(this.keywordText);
			this.searchPanel.Controls.Add(this.filenameText);
			this.searchPanel.Controls.Add(this.keywordLabel);
			this.searchPanel.Controls.Add(this.filenameLabel);
			this.searchPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.searchPanel.Location = new System.Drawing.Point(0, 0);
			this.searchPanel.Name = "searchPanel";
			this.searchPanel.Size = new System.Drawing.Size(168, 443);
			this.searchPanel.TabIndex = 0;

// 
// generateHtmlButton
// 
			this.generateHtmlButton.Location = new System.Drawing.Point(75, 135);
			this.generateHtmlButton.Name = "generateHtmlButton";
			this.generateHtmlButton.Size = new System.Drawing.Size(86, 25);
			this.generateHtmlButton.TabIndex = 9;
			this.generateHtmlButton.Text = "&Generate HTML";
			this.generateHtmlButton.Click += new System.EventHandler(this.generateHtmlButton_Click);

// 
// searchButton
// 
			this.searchButton.Location = new System.Drawing.Point(75, 103);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(86, 25);
			this.searchButton.TabIndex = 8;
			this.searchButton.Text = "&Search";
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);

// 
// keywordText
// 
			this.keywordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.keywordText.Location = new System.Drawing.Point(3, 69);
			this.keywordText.Name = "keywordText";
			this.keywordText.Size = new System.Drawing.Size(158, 20);
			this.keywordText.TabIndex = 7;

// 
// filenameText
// 
			this.filenameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.filenameText.Location = new System.Drawing.Point(4, 28);
			this.filenameText.Name = "filenameText";
			this.filenameText.Size = new System.Drawing.Size(158, 20);
			this.filenameText.TabIndex = 6;

// 
// keywordLabel
// 
			this.keywordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.keywordLabel.Location = new System.Drawing.Point(4, 50);
			this.keywordLabel.Name = "keywordLabel";
			this.keywordLabel.Size = new System.Drawing.Size(157, 12);
			this.keywordLabel.TabIndex = 5;
			this.keywordLabel.Text = "Keyword:";

// 
// filenameLabel
// 
			this.filenameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.filenameLabel.Location = new System.Drawing.Point(4, 8);
			this.filenameLabel.Name = "filenameLabel";
			this.filenameLabel.Size = new System.Drawing.Size(143, 12);
			this.filenameLabel.TabIndex = 4;
			this.filenameLabel.Text = "All or part of the file name:";

// 
// searchResults
// 
			this.searchResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.searchResults.Location = new System.Drawing.Point(168, 0);
			this.searchResults.Name = "searchResults";
			this.searchResults.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.searchResults.Size = new System.Drawing.Size(528, 433);
			this.searchResults.TabIndex = 1;

// 
// ThumbnailerForm
// 
			this.AcceptButton = this.searchButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 443);
			this.Controls.Add(this.searchResults);
			this.Controls.Add(this.searchPanel);
			this.Name = "ThumbnailerForm";
			this.Text = "Thumbnailer";
			this.searchPanel.ResumeLayout(false);
			this.searchPanel.PerformLayout();
			this.ResumeLayout(false);
		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new ThumbnailerForm());
		}

		private void searchButton_Click(object sender, System.EventArgs e)
		{
			searchPanel.Enabled = false;
			searchResults.Enabled = false;
			Cursor = Cursors.WaitCursor;

			using(ItemContext ctx = ItemContext.Open())
			{
				ItemSearcher pictureSearcher = Photo.GetSearcher(ctx);

				if(filenameText.Text.Length > 0)
				{
					pictureSearcher.Filters.Add("InFolderMemberRelationships.Name LIKE @name");
					pictureSearcher.Parameters.Add("name", "%" + filenameText.Text + "%");
				}

				if (keywordText.Text.Length > 0)
				{
					pictureSearcher.Filters.Add("Exists(Extensions.Cast(System.Storage.Core.ItemKeywords).Keywords[Value LIKE @keyword])");
					pictureSearcher.Parameters.Add("keyword", "%" + keywordText.Text + "%");
				}

				FindResult results=pictureSearcher.FindAll();
				ArrayList photos = new ArrayList();

				foreach(Photo pic in results)
				{
					photos.Add(new BindablePhoto(pic));
				}
				searchResults.DataSource=photos;
			}

			searchResults.SelectedItem = null;
			searchResults.Enabled = true;
			searchPanel.Enabled = true;
			Cursor = Cursors.Default;
		}

		private void generateHtmlButton_Click(object sender, System.EventArgs e)
		{
			searchPanel.Enabled = false;
			searchResults.Enabled = false;
			Cursor = Cursors.WaitCursor;

			//This generates HTML and THUMBNAILS for all selected images (all if NONE selected).
			IList items = null;

			if (searchResults.SelectedItems.Count == 0)
				items = searchResults.Items;
			else
				items = searchResults.SelectedItems;

			System.IO.StreamWriter writer = new System.IO.StreamWriter("thumbnail.html",false);

			writer.Write("<html><body><center>");
			using(ItemContext ctx = ItemContext.Open())
			{
				foreach (BindablePhoto photo in items)
				{
					photo.GenerateThumbnail();
					writer.Write(string.Format("<a href='{0}' target='_blank'>", photo.FullName));
					writer.Write(string.Format("<img src='{0}.jpg'/></a> &nbsp;", photo.PictureId));
				}
			}

			writer.Write("</center></body></html>");
			writer.Close();

			System.Diagnostics.Process shell = new System.Diagnostics.Process();
			shell.StartInfo.FileName = "thumbnail.html";
			shell.Start();

			searchResults.Enabled = true;
			searchPanel.Enabled = true;
			Cursor = Cursors.Default;
		}
	}
}
