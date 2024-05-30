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
using System.Storage;
using System.Storage.Core;
using System.Storage.Contacts;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	class AppView
	{
		private static ItemContext itemContext=null;
		[STAThread]
		static void Main(string[] args)
		{
			using (itemContext = ItemContext.Open())
			{
				ViewDefinition viewDef = new ViewDefinition();
				viewDef.Fields.Add("DisplayName");
				viewDef.SortOptions.Add("DisplayName", SortOrder.Descending);

				ApplicationView appView=ApplicationView.CreateView(Person.GetSearcher(itemContext), viewDef);
				ViewRecordReader viewRecord = appView.GetRange(1, 100);
				while (viewRecord.Read())
				{
					string displayName = viewRecord["DisplayName"] as string;
					Console.WriteLine("DisplayName: {0}", displayName);
				}
			}
		}
	}
}
