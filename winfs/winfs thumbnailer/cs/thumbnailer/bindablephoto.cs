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
using System.Collections;
using System.Storage;
using System.Storage.Image;
using System.Drawing;

namespace Microsoft.Samples.WinFS
{
	/// <summary>
	/// Summary description for BindablePhoto.
	/// </summary>
	public class BindablePhoto
	{
		private Guid pictureIdValue;
		private string nameValue="";
		private string fullNameValue = "";

		public Guid PictureId { get{ return pictureIdValue; } }
		public string Name { get { return nameValue; } }
		public string FullName { get { return fullNameValue; } }

		public BindablePhoto(Photo picture)
		{
			pictureIdValue = picture.ItemId;
			nameValue = GetPictureName(picture, false);
			fullNameValue = GetPictureName(picture, true);
		}

		private string GetPictureName(Photo pic, bool fullPath)
		{
			foreach (ItemName itemName in ((Item)pic).GetItemNames())
			{
				if (fullPath)
					return itemName.FullPath;
				else
					return itemName.Name;
			}

			return null;
		}

		public void GenerateThumbnail()
		{
			try
			{
				Image image = Image.FromFile(FullName);
				Image thumbnail = new Bitmap(75, 75, image.PixelFormat);
				Graphics thumbnailGraphics = Graphics.FromImage(thumbnail);

				thumbnailGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				thumbnailGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				thumbnailGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				thumbnailGraphics.DrawImage(image, new Rectangle(0,0, thumbnail.Width,thumbnail.Height));
				thumbnailGraphics.Dispose();

				thumbnail.Save(PictureId + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
				image.Dispose();
				thumbnail.Dispose();
			}
			catch {}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
