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

//Mark the assembly as being CLS compliant
[assembly: CLSCompliant(true)]

//Mark the assembly as not visible through COM interop
[assembly: System.Runtime.InteropServices.ComVisible(false)]

namespace Microsoft.Samples.Communications
{
    internal class CTBSample : Window
    {
        ContactTextBox contactTB;
        TextBox textBox;

        internal CTBSample()
        {
            //Sets text and size of this window
            this.Text = "ContactTextBox Sample";
            this.Width = new Length(600);
            this.Height = new Length(300);
            this.WindowAutoLocation = WindowAutoLocation.CenterScreen;

            //Creates a dockPanel sized to fill this window
            DockPanel dockPanel = new DockPanel();
            dockPanel.Width = new Length(100f, UnitType.Percent);
            dockPanel.Height = new Length(100f, UnitType.Percent);

            //Creates a ContactTextBox for email addresses and adds it to the DockPanel
            contactTB = new ContactTextBox();
            contactTB.Height = new Length(50);
            contactTB.PropertyType = ContactPropertyType.EmailAddress;
            DockPanel.SetDock(contactTB, Dock.Top);
            dockPanel.Children.Add(contactTB);

            //Creates a resolve button and adds it to the DockPanel
            Button resolve = new Button();
            resolve.Content = "Resolve";
            resolve.AddHandler(Button.ClickEventID, new ClickEventHandler(OnResolveClicked));
            DockPanel.SetDock(resolve, Dock.Top);
            dockPanel.Children.Add(resolve);
            
            //Creates a contents button and adds it to the DockPanel
            Button contents = new Button();
            contents.Content = "Print contents";
            contents.AddHandler(Button.ClickEventID, new ClickEventHandler(OnContentsClicked));
            DockPanel.SetDock(contents, Dock.Top);
            dockPanel.Children.Add(contents);

            //Creates a textBox to display results from our ContactTextBox
            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.ScrollerVisibilityX = ScrollerVisibility.Auto;
            textBox.ScrollerVisibilityY = ScrollerVisibility.Auto;
            DockPanel.SetDock(textBox, Dock.Fill);
            dockPanel.Children.Add(textBox);
            
            //Adds the dockPanel
            this.Content = dockPanel;
        }

        //When the resolve is clicked, resolve contacts
        private void OnResolveClicked(object e, ClickEventArgs args)
        {
            contactTB.Resolve();
        }

        //When the contents is clicked, print contents
        private void OnContentsClicked (object sender, ClickEventArgs args)
        {
            //Clears our textBox for displaying results
            textBox.Clear();

            textBox.AppendText("---------------\nUnresolved contents:\n");
            foreach (String element in contactTB.UnresolvedText)
            {
                textBox.AppendText(element + "\n");
            }

            textBox.AppendText("---------------\nResolved contents:\n");
            foreach (ResolvedItem element in contactTB.ResolvedItems)
            {
                textBox.AppendText(element.ResolvedText + "\n");
            }
        }
    }

    public class MyApp : Application
    {
        [STAThread]
        public static void Main()
        {
            MyApp sampleApp = new MyApp();
            sampleApp.Run();
        }

        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            base.OnStartingUp(e);

            CTBSample sampleWindow = new CTBSample();
            sampleWindow.Show();
        }
    }
}
