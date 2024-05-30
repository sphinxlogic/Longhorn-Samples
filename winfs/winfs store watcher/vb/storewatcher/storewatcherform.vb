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
<DefaultInstanceProperty("GetInstance")> Public Class StoreWatcherForm
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

    Private context As ItemContext
    Private watcher As StoreWatcher
    Private personalContactsFolder As WellKnownFolder
    Private WithEvents contactsListBox As System.Windows.Forms.ListBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.contactsListBox = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'contactsListBox
        '
        Me.contactsListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.contactsListBox.Location = New System.Drawing.Point(0, 0)
        Me.contactsListBox.Name = "contactsListBox"
        Me.contactsListBox.Size = New System.Drawing.Size(292, 264)
        Me.contactsListBox.TabIndex = 0
        '
        'StoreWatcherForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.Add(Me.contactsListBox)
        Me.Name = "StoreWatcherForm"
        Me.Text = "Store Watcher"
        Me.ResumeLayout(False)

    End Sub

    Friend Shared ReadOnly Property GetInstance() As StoreWatcherForm
        Get
            If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                SyncLock m_SyncObject
                    If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                        m_DefaultInstance = New StoreWatcherForm
                    End If
                End SyncLock
            End If
            Return m_DefaultInstance
        End Get
    End Property

    Private Shared m_DefaultInstance As StoreWatcherForm
    Private Shared m_SyncObject As New Object
#End Region

    Private Sub StoreWatcherForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        context = ItemContext.Open()
        personalContactsFolder = UserDataFolder.FindMyPersonalContactsFolder(context)

        Dim watcherOptions As StoreWatcherOptions = New StoreWatcherOptions
        watcherOptions.Depth = WatcherDepth.ItemAndImmediateDecendents
        watcherOptions.NotifyAdded = True
        watcherOptions.NotifyModified = True
        watcherOptions.NotifyRemoved = True
        watcherOptions.RelationshipTypeToWatch = GetType(FolderMember)

        watcher = New StoreWatcher(context, personalContactsFolder, watcherOptions)
        AddHandler watcher.StoreObjectChanged, AddressOf ItemChangedEvent

        ListContacts()
    End Sub
    Private Delegate Sub ListContactsDelegate()
    Private Sub ItemChangedEvent(ByVal sender As Object, ByVal args As StoreEventArgs)
        Me.Invoke(New ListContactsDelegate(AddressOf ListContacts))
    End Sub
    Private Sub ListContacts()
        contactsListBox.Items.Clear()

        Dim searcher As ItemSearcher = FolderMember.GetItemSearcher(personalContactsFolder, GetType(Person))
        Dim p As Person
        For Each p In searcher.FindAll(CType(Nothing, SortOption()))
            contactsListBox.Items.Add(p.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))
        Next
    End Sub

    Private Sub StoreWatcherForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        RemoveHandler watcher.StoreObjectChanged, AddressOf ItemChangedEvent
    End Sub
End Class
