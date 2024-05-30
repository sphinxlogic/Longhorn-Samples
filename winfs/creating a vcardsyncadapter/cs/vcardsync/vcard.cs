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

namespace Microsoft.Samples.WinFS
{
	public class VCard
	{
		private string path = "";
		private bool isDeletedValue = false;
		private bool isChangedValue = false;
		private string displayNameValue = "";
		private string eMailValue = "";
		private Guid userIdValue = new Guid();

		//Will be true if the file is non-existent, but the DB has an entry for it
		public bool IsDeleted { get { return isDeletedValue; } set { isDeletedValue = value; } }
		//Will be true if the files modified time is more recent than the DB modified time
		public bool IsChanged { get { return isChangedValue; } set { isChangedValue = value; } }
		public string DisplayName { get { return displayNameValue; } set { displayNameValue = value; } }
		public string EMail { get { return eMailValue; } set { eMailValue = value; } }
		public Guid UserId { get { return userIdValue; } set{userIdValue=value;} }

		public DateTime LastEdit
		{
			get
			{
				return System.IO.File.GetLastWriteTime(path);
			}
		}

		public VCard(string filePath)
		{
			path = filePath;
			if (System.IO.File.Exists(filePath))
			{
				//Open
				System.IO.StreamReader reader = null;

				try
				{
					reader = new System.IO.StreamReader(filePath);

					string line = reader.ReadLine();

					while (line != null)
					{
						string[] parts = line.Split(':');

						if (parts.Length > 1)
						{
							string param = line.Substring(parts[0].Length + 1);

							switch (parts[0].ToLowerInvariant())
							{
								case "uid":
									UserId = new Guid(param);
									break;
								case "fn":
									DisplayName = param;
									break;
								case "email;perf;internet":
									EMail = param;
									break;
							}
						}

						line = reader.ReadLine();
					}
				}
				finally
				{
					if (reader != null)
						reader.Close();
				}
			}
		}

		public void Rename(string filePath)
		{
			if (path != filePath)
			{
				System.IO.File.Move(path, filePath);
				path = filePath;
			}
		}

		public void Save()
		{
			System.IO.StreamWriter writer = null;
			try
			{
				writer = new System.IO.StreamWriter(path, false);

				writer.WriteLine("BEGIN:VCARD");
				writer.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "UID:{0}", UserId.ToString()));
				writer.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "FN:{0}", DisplayName));
				writer.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "EMAIL;PERF;INTERNET:{0}", EMail));
				writer.WriteLine("VERSION:2.1");
				writer.WriteLine("END:VCARD");
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		public void Delete()
		{
			System.IO.File.Delete(this.path);
			IsDeleted = true;
		}

		public override string ToString()
		{
			return DisplayName;
		}

	}
	public class VCardDB
	{
		private System.Collections.ArrayList list = new System.Collections.ArrayList();
		private string path="";

		public DateTime LastEdit
		{
			get
			{
				return System.IO.File.GetLastWriteTime(path);
			}
		}

		public VCardDB(string filePath)
		{
			path=System.IO.Path.Combine(filePath, "vcf.db");
			if (System.IO.File.Exists(path))
			{
				System.IO.StreamReader reader = null;

				try
				{
					reader = new System.IO.StreamReader(path);

					string line = reader.ReadLine();

					while (line != null)
					{
						Add(line);
						line = reader.ReadLine();
					}
				}
				finally
				{
					if (reader != null)
						reader.Close();
				}
			}
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			System.Collections.ArrayList vcfList = new System.Collections.ArrayList();

			foreach (string vcf in list)
			{
				string vcfPath=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), vcf + ".vcf");

				if (System.IO.File.Exists(vcfPath))
				{
					VCard existing = new VCard(vcfPath);

					if (existing.LastEdit > LastEdit)	//The vcard was changed
						existing.IsChanged = true;

					vcfList.Add(existing);
				}
				else
				{
					VCard deleted = new VCard(vcfPath);

					deleted.UserId = new Guid(vcf);
					deleted.IsDeleted = true;
					deleted.IsChanged = true;
					vcfList.Add(deleted);
				}
			}

			return vcfList.GetEnumerator();
		}

		public VCard GetVCard(Guid userId)
		{
			string vcfPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), userId.ToString() + ".vcf");
			VCard output = null;

			foreach (string vcf in list)
			{
				if (userId.ToString() == vcf)
				{
					if (System.IO.File.Exists(vcfPath))
						output=new VCard(vcfPath);
					else
					{
						output = new VCard(vcfPath);
						output.UserId = new Guid(vcf);
						output.IsDeleted = true;
						output.IsChanged = true;
					}
				}
			}

			if (output == null)
				output = new VCard(vcfPath);

			return output;
		}

		public bool Contains(string userId)
		{
			foreach (string vcf in list)
			{
				if (vcf.ToLowerInvariant() == userId.ToLowerInvariant())
					return true;
			}

			return false;
		}

		public int Add(string name)
		{
			if (!list.Contains(name))
				list.Add(name);

			return list.Count;
		}
		public int Add(VCard vcard)
		{
			return Add(vcard.UserId.ToString());
		}

		public void Remove(string name)
		{
			string toRemove = "";

			foreach (string s in list)
				if (s.ToLowerInvariant() == name.ToLowerInvariant())
					toRemove = s;

			if(toRemove.Length > 0)
				list.Remove(toRemove);
		}

		public void Remove(VCard vcard)
		{
			Remove(vcard.UserId.ToString());
		}

		public void Save()
		{
			System.IO.StreamWriter writer = null;

			if (System.IO.File.Exists(path))
				System.IO.File.Delete(path);

			try
			{
				writer = new System.IO.StreamWriter(path, false);
				foreach (string s in list)
					writer.WriteLine(s);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}

			System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
			if ((fileInfo.Attributes & System.IO.FileAttributes.Hidden) == 0)	//Not hidden
				fileInfo.Attributes ^= System.IO.FileAttributes.Hidden;			//Set hidden
		}
	}
}
