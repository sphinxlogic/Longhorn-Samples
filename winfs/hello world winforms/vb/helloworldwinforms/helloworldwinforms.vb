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
Imports System.Storage
Imports System.Storage.Contacts
Imports System.Storage.Core
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
<DefaultInstanceProperty("GetInstance")> Public Class HelloWorldUsingWinForms
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private itemContext As itemContext

    Private newContactCount As Integer = 0

    Private WithEvents buttonsPanel As System.Windows.Forms.Panel

    Private WithEvents listBoxPanel As System.Windows.Forms.Panel

    Private WithEvents contactListBox As System.Windows.Forms.ListBox

    Private WithEvents removeContactsButton As System.Windows.Forms.Button

    Private WithEvents renameContactsButton As System.Windows.Forms.Button

    Private WithEvents addContactsButton As System.Windows.Forms.Button


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.buttonsPanel = New System.Windows.Forms.Panel
        Me.addContactsButton = New System.Windows.Forms.Button
        Me.renameContactsButton = New System.Windows.Forms.Button
        Me.removeContactsButton = New System.Windows.Forms.Button
        Me.listBoxPanel = New System.Windows.Forms.Panel
        Me.contactListBox = New System.Windows.Forms.ListBox
        Me.buttonsPanel.SuspendLayout()
        Me.listBoxPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'buttonsPanel
        '
        Me.buttonsPanel.Controls.Add(Me.addContactsButton)
        Me.buttonsPanel.Controls.Add(Me.renameContactsButton)
        Me.buttonsPanel.Controls.Add(Me.removeContactsButton)
        Me.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.buttonsPanel.Location = New System.Drawing.Point(0, 234)
        Me.buttonsPanel.Name = "buttonsPanel"
        Me.buttonsPanel.Size = New System.Drawing.Size(349, 37)
        Me.buttonsPanel.TabIndex = 0
        '
        'addContactsButton
        '
        Me.addContactsButton.Location = New System.Drawing.Point(3, 8)
        Me.addContactsButton.Name = "addContactsButton"
        Me.addContactsButton.Size = New System.Drawing.Size(104, 25)
        Me.addContactsButton.TabIndex = 2
        Me.addContactsButton.Text = "Add Contact"
        '
        'renameContactsButton
        '
        Me.renameContactsButton.Location = New System.Drawing.Point(122, 8)
        Me.renameContactsButton.Name = "renameContactsButton"
        Me.renameContactsButton.Size = New System.Drawing.Size(104, 25)
        Me.renameContactsButton.TabIndex = 1
        Me.renameContactsButton.Text = "Rename Contact"
        '
        'removeContactsButton
        '
        Me.removeContactsButton.Location = New System.Drawing.Point(241, 8)
        Me.removeContactsButton.Name = "removeContactsButton"
        Me.removeContactsButton.Size = New System.Drawing.Size(104, 25)
        Me.removeContactsButton.TabIndex = 0
        Me.removeContactsButton.Text = "Remove Contact"
        '
        'listBoxPanel
        '
        Me.listBoxPanel.Controls.Add(Me.contactListBox)
        Me.listBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listBoxPanel.Location = New System.Drawing.Point(0, 0)
        Me.listBoxPanel.Name = "listBoxPanel"
        Me.listBoxPanel.Size = New System.Drawing.Size(349, 234)
        Me.listBoxPanel.TabIndex = 1
        '
        'contactListBox
        '
        Me.contactListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.contactListBox.Location = New System.Drawing.Point(0, 0)
        Me.contactListBox.Name = "contactListBox"
        Me.contactListBox.Size = New System.Drawing.Size(349, 225)
        Me.contactListBox.TabIndex = 0
        '
        'HelloWorldUsingWinForms
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(349, 271)
        Me.Controls.Add(Me.listBoxPanel)
        Me.Controls.Add(Me.buttonsPanel)
        Me.Name = "HelloWorldUsingWinForms"
        Me.Text = "Hello World Using WinForms"
        Me.buttonsPanel.ResumeLayout(False)
        Me.listBoxPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend Shared ReadOnly Property GetInstance() As HelloWorldUsingWinForms
        Get
            If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                SyncLock m_SyncObject
                    If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                        m_DefaultInstance = New HelloWorldUsingWinForms
                    End If
                End SyncLock
            End If
            Return m_DefaultInstance
        End Get
    End Property

    Private Shared m_DefaultInstance As HelloWorldUsingWinForms
    Private Shared m_SyncObject As New Object
#End Region

    Private Sub HelloWorldUsingWinForms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            itemContext = itemContext.Open()
            ListContacts()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub ShowError(ByVal ex As Exception)
        addContactsButton.Enabled = False
        removeContactsButton.Enabled = False
        renameContactsButton.Enabled = False
        MessageBox.Show(Me, "The application generated an unexpected error: " + ex.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub ListContacts()
        Try
            renameContactsButton.Enabled = False
            removeContactsButton.Enabled = False

            Dim result As FindResult = itemContext.FindAll(GetType(Person))

            contactListBox.Items.Clear()
            Dim person As Person
            For Each person In result
                contactListBox.Items.Add(person.DisplayName)
            Next
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub AddContact(ByVal name As String)
        Dim newPerson As Person = New Person

        newPerson.DisplayName = System.Nullable(Of String).FromObject(name)

        Dim email As SmtpEmailAddress = New SmtpEmailAddress("somecontact@litware.com")

        email.Keywords.Add(New Keyword(StandardKeywords.Primary))
        email.AccessPointType = New Keyword("email")
        email.AccessPoint = email.Address
        newPerson.EAddresses.Add(email)

        Dim phone As TelephoneNumber = New TelephoneNumber("1", "425", "555-1234", "")

        phone.Keywords.Add(New Keyword(StandardKeywords.Home))
        phone.AccessPointType = New Keyword(StandardKeywords.Home)
        phone.AccessPoint = phone.Number.ToString(System.Globalization.CultureInfo.CurrentUICulture)
        newPerson.EAddresses.Add(phone)

        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext)

        personalContactsFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.ItemId.ToString())
        itemContext.Update()
    End Sub

    Private Sub DeleteContact(ByVal displayName As String)
        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.CreateMyPersonalContactsFolder(itemContext)

        Dim personSearcher As ItemSearcher = Person.GetSearcher(itemContext)
        personSearcher.Filters.Add("DisplayName = @DisplayName")
        personSearcher.Parameters.Add("DisplayName", displayName)

        Dim deletedPerson As Person = personSearcher.FindOne(CType(Nothing, SortOption()))
        If deletedPerson Is Nothing Then
            Console.WriteLine("Error: There were problems finding the requested contact: " & displayName)
        Else
            personalContactsFolder.OutgoingRelationships.RemoveTarget(deletedPerson)
        End If

        itemContext.Update()
    End Sub

    Private Sub RenameContact(ByVal fromName As String, ByVal toName As String)
        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.CreateMyPersonalContactsFolder(itemContext)
        Dim personSearcher As ItemSearcher = Person.GetSearcher(itemContext)

        personSearcher.Filters.Add("DisplayName = @DisplayName")
        personSearcher.Parameters.Add("DisplayName", fromName)

        Dim modifiedPerson As Person = personSearcher.FindOne(CType(Nothing, SortOption()))
        If modifiedPerson Is Nothing Then
            Console.WriteLine("Error: There were problems finding the requested contact: " & fromName)
        Else
            modifiedPerson.DisplayName = System.Nullable(Of String).FromObject(toName)
        End If

        itemContext.Update()
    End Sub

    Private Sub addContactsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addContactsButton.Click
        Try
            AddContact("New Contact " & newContactCount)
            newContactCount += 1
            ListContacts()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub renameContactsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renameContactsButton.Click
        Try
            If Not contactListBox.SelectedItem Is Nothing Then
                RenameContact(contactListBox.SelectedItem.ToString(), "Renamed Contact " & newContactCount)
                newContactCount += 1
                ListContacts()
            Else
                renameContactsButton.Enabled = False
            End If
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub removeContactsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removeContactsButton.Click
        Try
            If Not contactListBox.SelectedItem Is Nothing Then
                DeleteContact(contactListBox.SelectedItem.ToString())
                ListContacts()
            Else
                removeContactsButton.Enabled = False
            End If
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub contactListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles contactListBox.SelectedIndexChanged
        If contactListBox.SelectedItem Is Nothing Then
            removeContactsButton.Enabled = False
            renameContactsButton.Enabled = False
        Else
            removeContactsButton.Enabled = True
            renameContactsButton.Enabled = True
        End If
    End Sub
End Class
