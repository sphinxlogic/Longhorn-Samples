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
Imports System.Windows
Imports System.Windows.Controls


'Mark the assembly as being CLS compliant
<assembly: CLSCompliant(true)>

'Mark the assembly as not visible through COM interop
<assembly: System.Runtime.InteropServices.ComVisible(false)>

Namespace Microsoft.Samples.Communications

    Friend Class CTBSample
        Inherits Window

        Dim contactTB as ContactTextBox 
        Dim textBox as TextBox 

        Friend Sub New()
            'Sets text and size of this window
            Me.Text = "ContactTextBox Sample"
            Me.Width = New Length(600)
            Me.Height = New Length(300)
            Me.WindowAutoLocation = WindowAutoLocation.CenterScreen

            'Creates a dockPanel sized to fill this window
            Dim dockPanel As DockPanel = New DockPanel
            dockPanel.Width = New Length(100.0F, UnitType.Percent)
            dockPanel.Height = New Length(100.0F, UnitType.Percent)

            'Creates a ContactTextBox for email addresses and adds it to the DockPanel
            contactTB = New ContactTextBox
            contactTB.Height = New Length(50)
            contactTB.PropertyType = ContactPropertyType.EmailAddress
            DockPanel.SetDock(contactTB, Dock.Top)
            dockPanel.Children.Add(contactTB)

            'Creates a resolve button and adds it to the DockPanel
            Dim resolve As Button = New Button
            resolve.Content = "Resolve"
            resolve.AddHandler(Button.ClickEventID, New ClickEventHandler(AddressOf OnResolveClicked))
            DockPanel.SetDock(resolve, Dock.Top)
            dockPanel.Children.Add(resolve)
            
            'Creates a contents button and adds it to the DockPanel
            Dim contents As Button = New Button
            contents.Content = "Print contents"
            contents.AddHandler(Button.ClickEventID, New ClickEventHandler(AddressOf OnContentsClicked))
            DockPanel.SetDock(contents, Dock.Top)
            dockPanel.Children.Add(contents)

            'Creates a textBox to display results from our ContactTextBox
            textBox = New TextBox
            textBox.IsReadOnly = true
            textBox.ScrollerVisibilityX = ScrollerVisibility.Auto
            textBox.ScrollerVisibilityY = ScrollerVisibility.Auto
            DockPanel.SetDock(textBox, Dock.Fill)
            dockPanel.Children.Add(textBox)
            
            'Adds the dockPanel
            Me.Content = dockPanel

        End Sub

        'When the resolve is clicked, resolve contacts
        Private Sub OnResolveClicked(ByVal sender As Object, ByVal args As ClickEventArgs)

            contactTB.Resolve()

        End Sub

        'When the contents is clicked, print contents
        Private Sub OnContentsClicked (ByVal sender As Object, ByVal args As ClickEventArgs)

            'Clears our textBox for displaying results
            textBox.Clear()

            textBox.AppendText("---------------" & Chr(13) & "Unresolved contents:" & Chr(13))
            For Each element As String In contactTB.UnresolvedText
                textBox.AppendText(element & Chr(13))
            Next

            textBox.AppendText("---------------" & Chr(13) & "Resolved contents:" & Chr(13))
            For Each element As ResolvedItem In contactTB.ResolvedItems
                textBox.AppendText(element.ResolvedText & Chr(13))
            Next

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

            Dim sampleWindow As CTBSample = New CTBSample
            sampleWindow.Show()
        End Sub

    End Class

End Namespace
