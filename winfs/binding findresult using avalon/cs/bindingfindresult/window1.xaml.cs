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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Storage;
using System.Storage.Core;

namespace BindingFindResult
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
		private System.Storage.ItemContext ic = System.Storage.ItemContext.Open();

		// To use Loaded event put Loaded="WindowLoaded" attribute in root element of .xaml file.
        private void WindowLoaded(object sender, EventArgs e) 
		{
			System.Storage.FindResult result = ic.FindAll(typeof(System.Storage.Core.Contact));
			ContactsListBox.ItemsSource = result;
		}

        // Sample event handler:  
        // private void ButtonClick(object sender, ClickEventArgs e) {}
		public class RelationshipCounts : IDataTransformer
		{
			public object Transform(object o, System.Windows.DependencyProperty dp, System.Globalization.CultureInfo culture)
			{
				System.Storage.Contacts.Employment.EmployeeRelationshipCollection employees = o as System.Storage.Contacts.Employment.EmployeeRelationshipCollection;
				if (employees != null)
				{
					return (employees.GetCountFromStore().ToString());
				}

				System.Storage.Contacts.GroupMembership.MembersRelationshipCollection members = o as System.Storage.Contacts.GroupMembership.MembersRelationshipCollection;
				if( members != null )
				{
					return(members.GetCountFromStore().ToString());
				}

				return ("0");
			}

			public object InverseTransform(object o, System.Reflection.PropertyInfo info, System.Globalization.CultureInfo culture)
			{
				return (null);
			}
		}
    }
}