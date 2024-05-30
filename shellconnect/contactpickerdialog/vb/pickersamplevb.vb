'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------
Imports System
Imports System.Storage.Contacts
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Controls

'Mark the assembly as being CLS compliant
<assembly: CLSCompliant(true)>

'Mark the assembly as not visible through COM interop
<assembly: System.Runtime.InteropServices.ComVisible(false)>

Namespace Microsoft.Samples.Communications

    Friend Class PickerSample
        Inherits Window

        Dim picker As ContactPickerDialog
        Dim textBox As TextBox

        Friend Sub New()
            'Sets text and size of this window
            Me.Text = "Contact Picker Sample"
            Me.Width = New Length(600)
            Me.Height = New Length(300)
            Me.WindowAutoLocation = WindowAutoLocation.CenterScreen

            'Creates a dockPanel sized to fill this window
            Dim dockPanel As DockPanel = New DockPanel
            dockPanel.Width = New Length(100.0F, UnitType.Percent)
            dockPanel.Height = New Length(100.0F, UnitType.Percent)
            dockPanel.Background = New SolidColorBrush(Colors.Gray)

            'Creates a showPicker button and adds it to the dockPanel
            Dim showPicker As Button = New Button
            showPicker.Content = "Show Picker"
            showPicker.AddHandler(Button.ClickEventID, New ClickEventHandler(AddressOf OnShowPickerClicked))
            dockPanel.SetDock(showPicker, Dock.Top)
            dockPanel.Children.Add(showPicker)

            'Creates a textBox to display results from our Contact Picker
            textBox = New TextBox
            textBox.IsReadOnly = True
            textBox.ScrollerVisibilityX = ScrollerVisibility.Auto
            textBox.ScrollerVisibilityY = ScrollerVisibility.Auto
            dockPanel.SetDock(textBox, Dock.Fill)
            dockPanel.Children.Add(textBox)

            'Adds the dockPanel
            Me.Content = dockPanel

            'Creates our Contact Picker
            CreateContactPicker()
        End Sub

        Private Sub CreateContactPicker()
            'Creates a new Contact Picker that returns email addresses
            picker = New ContactPickerDialog(ContactPropertyType.EmailAddress)

            'Sets the title of the Contact Picker Dialog
            picker.Title = "Contact Picker Sample"

            'Sets the default Contact Picker view to Personal Contacts
            picker.DefaultFolder = ContactPickerDialog.PersonalContacts

            'Sets the label of the Picker OK button
            picker.OkButtonLabel = "Return contacts"

            'Allows to select and return several contacts
            picker.MultiSelect = True

            'Also configures the picker to return telephone numbers
            Dim phoneNumber As ContactPropertyRequest = New ContactPropertyRequest(ContactPropertyType.TelephoneNumber)
            picker.PropertyRequests.Add(phoneNumber)
        End Sub

        Private Sub OnShowPickerClicked(ByVal sender As Object, ByVal args As ClickEventArgs)
            'Clears our textBox for displaying results
            textBox.Clear()

            'Launches the Picker using the our window as parent
            If (picker.ShowDialog(Me) = DialogResult.OK) Then

                'Prints to screen some information contained inside the returned contacts
                textBox.AppendText("You selected " & picker.SelectedProperties.Count & " properties:" & Chr(13))
                For Each item As SelectedContactProperty In picker.SelectedProperties

                    'Prints displayName, and corresponding property type and property contents
                    textBox.AppendText("DisplayName-> " & item.Contact.DisplayName.ToString(System.Globalization.CultureInfo.InvariantCulture) & ", ")
                    textBox.AppendText([Enum].GetName(GetType(ContactPropertyType), item.PropertyType) & "-> " & item.Property & Chr(13))

                Next

            Else
                'If Cancel was pressed...
                textBox.AppendText("No selected contacts!" & Chr(13))
            End If

        End Sub

    End Class

    Public Class MyApp
        Inherits Application

        Public Shared Sub Main()
            Dim sampleApp As MyApp = New MyApp
            sampleApp.Run()
        End Sub

        Protected Overrides Sub OnStartingUp(ByVal e As StartingUpCancelEventArgs)
            MyBase.OnStartingUp(e)

            Dim sampleWindow As PickerSample = New PickerSample
            sampleWindow.Show()
        End Sub

    End Class

End Namespace
