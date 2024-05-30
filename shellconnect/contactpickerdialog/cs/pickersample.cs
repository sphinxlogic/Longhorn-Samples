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
using System.Windows.Media;
using System.Windows.Controls;

//Mark the assembly as being CLS compliant
[assembly: CLSCompliant(true)]

//Mark the assembly as not visible through COM interop
[assembly: System.Runtime.InteropServices.ComVisible(false)]

namespace Microsoft.Samples.Communications
{
    internal class PickerSample : Window
    {
        ContactPickerDialog picker;
        TextBox textBox;

        internal PickerSample()
        {
            //Sets text and size of this window
            this.Text = "Contact Picker Sample";
            this.Width = new Length(600);
            this.Height = new Length(300);
            this.WindowAutoLocation = WindowAutoLocation.CenterScreen;

            //Creates a dockPanel sized to fill this window
            DockPanel dockPanel = new DockPanel();
            dockPanel.Width = new Length(100f, UnitType.Percent);
            dockPanel.Height = new Length(100f, UnitType.Percent);
            dockPanel.Background = new SolidColorBrush(Colors.Gray);
            
            //Creates a showPicker button and adds it to the dockPanel
            Button showPicker = new Button();
            showPicker.Content = "Show Picker";
            showPicker.AddHandler(Button.ClickEventID, new ClickEventHandler(OnShowPickerClicked));
            DockPanel.SetDock(showPicker, Dock.Top);
            dockPanel.Children.Add(showPicker);

            //Creates a textBox to display results from our Contact Picker
            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.ScrollerVisibilityX = ScrollerVisibility.Auto;
            textBox.ScrollerVisibilityY = ScrollerVisibility.Auto;
            DockPanel.SetDock(textBox, Dock.Fill);
            dockPanel.Children.Add(textBox);
            
            //Adds the dockPanel
            this.Content = dockPanel;

            //Creates our Contact Picker
            CreateContactPicker();
        }
 
        private void CreateContactPicker(){   
            
            //Creates a new Contact Picker that returns email addresses
            picker = new ContactPickerDialog(ContactPropertyType.SecurityID);

            //Sets the title of the Contact Picker Dialog
            picker.Title = "Contact Picker Sample";

            //Sets the default Contact Picker view to Personal Contacts
            picker.DefaultFolder = ContactPickerDialog.PersonalContacts;

            //Sets the label of the Picker OK button
            picker.OkButtonLabel = "Return contacts";

            //Allows to select and return several contacts
            picker.MultiSelect = true;

            //Also configures the picker to return telephone numbers
            ContactPropertyRequest phoneNumber = new ContactPropertyRequest(ContactPropertyType.TelephoneNumber);
            picker.PropertyRequests.Add(phoneNumber);
        }

        //When the showPicker button is clicked, open Contact Picker
        private void OnShowPickerClicked (object sender, ClickEventArgs args)
        {
            //Clears our textBox for displaying results
            textBox.Clear();

            //Launches the Picker using the our window as parent
            if (picker.ShowDialog(this) == DialogResult.OK)
            {
                //Prints to the textBox some information contained inside the returned contacts
                textBox.AppendText("You selected " + picker.SelectedProperties.Count + " properties:\n");
                foreach (SelectedContactProperty item in picker.SelectedProperties)
                {
                    textBox.AppendText("-------------------------------\n");
                    if (item.Contact is System.Storage.Contacts.Person)
                    {
                        textBox.AppendText("The following contact is a Person:\n");
                    }
                    else if (item.Contact is System.Storage.Contacts.Group)
                    {
                        textBox.AppendText("The following contact is a Group:\n");
                    }
                    else if (item.Contact is System.Storage.Contacts.Organization)
                    {
                        textBox.AppendText("The following contact is an Organization:\n");
                    }

                    //Prints displayName, and corresponding property type and property contents
                    textBox.AppendText("DisplayName-> " + item.Contact.DisplayName + ", ");
                    textBox.AppendText(Enum.GetName(typeof(ContactPropertyType), item.PropertyType) + "-> " + item.Property + "\n");
                }
            }
            else
            {
                //If Cancel was pressed...
                textBox.AppendText("No selected contacts!\n");
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

            PickerSample sampleWindow = new PickerSample();
            sampleWindow.Show();
        }
    }
}
