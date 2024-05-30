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
using System.Storage.Contacts;
using System.Storage.Core;
using System.Storage.Image;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	public class ImageLinkerForm : System.Windows.Forms.Form
	{
		private ItemContext itemContext = null;
		private System.Windows.Forms.ListBox picturesListBox;
		private System.Windows.Forms.ListBox personListBox;
		private System.Windows.Forms.ListBox peopleInImageListBox;
		private System.Windows.Forms.Button AddLinkButton;
		private System.Windows.Forms.PictureBox picPreview;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ImageLinkerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			itemContext = ItemContext.Open();
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
			this.picturesListBox = new System.Windows.Forms.ListBox();
			this.personListBox = new System.Windows.Forms.ListBox();
			this.peopleInImageListBox = new System.Windows.Forms.ListBox();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.AddLinkButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();

			// 
			// picturesListBox
			// 
			this.picturesListBox.Location = new System.Drawing.Point(4, 24);
			this.picturesListBox.Name = "picturesListBox";
			this.picturesListBox.Size = new System.Drawing.Size(113, 108);
			this.picturesListBox.TabIndex = 0;
			this.picturesListBox.SelectedIndexChanged += new System.EventHandler(this.picturesListBox_SelectedIndexChanged);

			// 
			// personListBox
			// 
			this.personListBox.Location = new System.Drawing.Point(159, 24);
			this.personListBox.Name = "personListBox";
			this.personListBox.Size = new System.Drawing.Size(123, 108);
			this.personListBox.TabIndex = 1;
			this.personListBox.SelectedIndexChanged += new System.EventHandler(this.personListBox_SelectedIndexChanged);

			// 
			// peopleInImageListBox
			// 
			this.peopleInImageListBox.Location = new System.Drawing.Point(161, 142);
			this.peopleInImageListBox.Name = "peopleInImageListBox";
			this.peopleInImageListBox.Size = new System.Drawing.Size(121, 95);
			this.peopleInImageListBox.TabIndex = 2;
			this.peopleInImageListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.peopleInImageListBox_KeyUp);

			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(4, 142);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(123, 98);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picPreview.TabIndex = 3;
			this.picPreview.TabStop = false;
			this.picPreview.WaitOnLoad = false;

			// 
			// AddLinkButton
			// 
			this.AddLinkButton.Enabled = false;
			this.AddLinkButton.Location = new System.Drawing.Point(122, 72);
			this.AddLinkButton.Name = "AddLinkButton";
			this.AddLinkButton.Size = new System.Drawing.Size(33, 23);
			this.AddLinkButton.TabIndex = 4;
			this.AddLinkButton.Text = "< >";
			this.AddLinkButton.Click += new System.EventHandler(this.AddLinkButton_Click);

			// 
			// ImageLinkerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.Controls.Add(this.AddLinkButton);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.peopleInImageListBox);
			this.Controls.Add(this.personListBox);
			this.Controls.Add(this.picturesListBox);
			this.Name = "ImageLinkerForm";
			this.Text = "Image Linker";
			this.Load += new System.EventHandler(this.ImageLinkerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
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
			Application.Run(new ImageLinkerForm());
		}

		private void LoadPictures()
		{
			picturesListBox.Items.Clear();

			ItemSearcher pictureSearcher = Photo.GetSearcher(itemContext);
			foreach (Photo pic in pictureSearcher.FindAll())
			{
				//System.Storage.Image.ContactsInPicture.
				picturesListBox.Items.Add(new PictureItem(GetPictureName(pic, false), GetPictureName(pic,true), pic.ItemId));
			}
		}

		private void LoadPersons()
		{
			personListBox.Items.Clear();

			ItemSearcher personSearcher = Person.GetSearcher(itemContext);

			foreach (Person person in personSearcher.FindAll())
			{
				personListBox.Items.Add(new PersonItem(person.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture), person.ItemId));
			}
		}
		
		private string GetPictureName(Photo pic, bool fullPath)
		{
			foreach (ItemName itemName in ((Item)pic).GetItemNames())
			{
				if(fullPath)
					return itemName.FullPath;
				else
					return itemName.Name;
			}
			return null;
		}
		private void ImageLinkerForm_Load(object sender, System.EventArgs e)
		{
			LoadPictures();
			LoadPersons();
		}
		private void picturesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EnableAddLinkButton();

			if (picturesListBox.SelectedIndex != -1)
			{
				PictureItem selectedPic = picturesListBox.SelectedItem as PictureItem;
				picPreview.Load(selectedPic.FullPathName);
			}

			LoadRelevantContacts();
		}
		private void LoadRelevantContacts()
		{
			peopleInImageListBox.Items.Clear();

			if (picturesListBox.SelectedIndex != -1)
			{
				PictureItem picItem = picturesListBox.SelectedItem as PictureItem;
				Photo selectedPhoto = itemContext.FindItemById(picItem.Key) as Photo;

				foreach (ContactsInPicture contact in selectedPhoto.OutContactsInPictureRelationships)
				{
					if(contact.Contact!=null)
						peopleInImageListBox.Items.Add(new PersonItem(contact.Contact.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture), contact.Contact.ItemId));
				}
			}
		}
		private void personListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EnableAddLinkButton();
		}
		private void EnableAddLinkButton()
		{
			AddLinkButton.Enabled = (picturesListBox.SelectedIndex != -1 && personListBox.SelectedIndex != -1);
		}
		private void AddLinkButton_Click(object sender, System.EventArgs e)
		{
			if (picturesListBox.SelectedIndex != -1 && personListBox.SelectedIndex != -1)
			{
				PictureItem picItem = picturesListBox.SelectedItem as PictureItem;
				PersonItem personItem = personListBox.SelectedItem as PersonItem;

				bool isLinkExist = false;
				Photo photo = itemContext.FindItemById(picItem.Key) as Photo;
				Person person = itemContext.FindItemById(personItem.Key) as Person;

				foreach (ContactsInPicture contact in photo.OutContactsInPictureRelationships)
				{
					if (contact.Contact.ItemId == person.ItemId)
					{
						isLinkExist = true;
						break;
					}
				}
				if (!isLinkExist)
				{
					photo.OutContactsInPictureRelationships.AddContact(person);
					itemContext.Update();
				}
			}
			LoadRelevantContacts();
		}
		private void peopleInImageListBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == 46) //delete
			{
				if (peopleInImageListBox.SelectedIndex != -1)
				{
					PersonItem personItem = peopleInImageListBox.SelectedItem as PersonItem;
					PictureItem picItem = picturesListBox.SelectedItem as PictureItem;

					Photo photo = itemContext.FindItemById(picItem.Key) as Photo;
					Person person = itemContext.FindItemById(personItem.Key) as Person;

					photo.OutContactsInPictureRelationships.RemoveContact(person);
					itemContext.Update();
				}

				LoadRelevantContacts();
			}
		}
	}

	public class PictureItem
	{
		private string fileNameValue;
		private string fullPathNameValue;
		private Guid keyValue;

		public string FileName { get { return fileNameValue; } }
		public string FullPathName { get { return fullPathNameValue; } }
		public Guid Key { get { return keyValue; } }

		public PictureItem(string fileName, string fullPathName, Guid key)
		{
			fileNameValue = fileName;
			fullPathNameValue = fullPathName;
			keyValue = key;
		}
		public override string ToString()
		{
			return FileName;
		}
	}

	public class PersonItem
	{
		private string displayNameValue;
		private Guid keyValue;

		public string DisplayName { get { return displayNameValue; } }
		public Guid Key { get { return keyValue; } }

		public PersonItem(string displayName, Guid key)
		{
			displayNameValue = displayName;
			keyValue = key;
		}
		public override string ToString()
		{
			return DisplayName;
		}
	}
}
