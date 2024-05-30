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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Storage;
using System.Storage.Core;
using System.Storage.Contacts;

namespace BindingProjection
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        // To use Loaded event put Loaded="WindowLoaded" attribute in root element of .xaml file.
        private void WindowLoaded(object sender, EventArgs e) 
		{
			ItemContext context = ItemContext.Open();

			Query query = new Query();
			query.Type = typeof(Person);
			query.ProjectionOptions = new ProjectionOption[]
			{
				new ProjectionOption("DisplayName"),
				new ProjectionOption("FirstName", "MAX(PersonalNames.GivenName)"),
				new ProjectionOption("LastName", "MAX(PersonalNames.Surname)"), 
				new ProjectionOption("EmailAddress", "MAX(EAddresses.Cast(System.Storage.Contacts.SmtpEmailAddress)[Keywords[Value='System.Storage.Contacts.Primary']].AccessPoint)")
			};

			System.Storage.FindResult result = context.FindAll(query);
			ResultListBox.ItemsSource = result;
		}

        // Sample event handler:  
        // private void ButtonClick(object sender, ClickEventArgs e) {}
    }
}