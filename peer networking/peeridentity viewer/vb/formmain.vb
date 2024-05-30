'=====================================================================
'  File:      FormMain.vb
'
'  Summary:   Demonstrates how to create, delete, export and import
'             Peer-to-Peer Identities.
'
'---------------------------------------------------------------------
' This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
' Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
' 
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Option Explicit On
Option Strict On


' Add the classes in the following namespaces to our namespace
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Net.PeerToPeer
Imports System.Windows.Forms


<DefaultInstanceProperty("GetInstance")> Public Class FormMain
    Inherits System.Windows.Forms.Form

    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Private WithEvents menuFile As System.Windows.Forms.MenuItem
    Private WithEvents menuImport As System.Windows.Forms.MenuItem
    Private WithEvents menuExport As System.Windows.Forms.MenuItem
    Private WithEvents menuSeparator1 As System.Windows.Forms.MenuItem
    Private WithEvents menuSaveIdentityInfo As System.Windows.Forms.MenuItem
    Private WithEvents menuSeparator2 As System.Windows.Forms.MenuItem
    Private WithEvents menuExit As System.Windows.Forms.MenuItem
    Private WithEvents menuIdentity As System.Windows.Forms.MenuItem
    Private WithEvents menuIdentityCreate As System.Windows.Forms.MenuItem
    Private WithEvents menuIdentityDelete As System.Windows.Forms.MenuItem
    Private WithEvents menuView As System.Windows.Forms.MenuItem
    Private WithEvents menuViewRefresh As System.Windows.Forms.MenuItem
    Private WithEvents menuSeparator3 As System.Windows.Forms.MenuItem
    Private WithEvents menuProperties As System.Windows.Forms.MenuItem
    Private WithEvents menuHelp As System.Windows.Forms.MenuItem
    Private WithEvents menuAbout As System.Windows.Forms.MenuItem

    Private WithEvents popupMenu As System.Windows.Forms.ContextMenu
    Private WithEvents popupProperties As System.Windows.Forms.MenuItem
    Private WithEvents menuSeparator5 As System.Windows.Forms.MenuItem
    Private WithEvents popupCreate As System.Windows.Forms.MenuItem
    Private WithEvents popupDelete As System.Windows.Forms.MenuItem
    Private WithEvents popupExport As System.Windows.Forms.MenuItem
    Private WithEvents menuSeparator6 As System.Windows.Forms.MenuItem
    Private WithEvents popupSaveIndentityInfo As System.Windows.Forms.MenuItem

    Private WithEvents listViewMain As System.Windows.Forms.ListView
    Private colName As System.Windows.Forms.ColumnHeader
    Private colClassifier As System.Windows.Forms.ColumnHeader
    Private colAuthority As System.Windows.Forms.ColumnHeader
    Private imageList As System.Windows.Forms.ImageList

    Private components As System.ComponentModel.IContainer = Nothing


    ' Constructor to create the main form.
    '
    Public Sub New()
        InitializeComponent()
    End Sub 'New

    ' Clean up any resources being used.
    '
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)

    End Sub 'Dispose

#Region "Windows Form Designer generated code"

    ' Required method for Designer support - do not modify
    ' the contents of this method with the code editor.
    '
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FormMain))
        Me.listViewMain = New System.Windows.Forms.ListView
        Me.colName = New System.Windows.Forms.ColumnHeader
        Me.colClassifier = New System.Windows.Forms.ColumnHeader
        Me.colAuthority = New System.Windows.Forms.ColumnHeader
        Me.popupMenu = New System.Windows.Forms.ContextMenu
        Me.popupProperties = New System.Windows.Forms.MenuItem
        Me.menuSeparator5 = New System.Windows.Forms.MenuItem
        Me.popupCreate = New System.Windows.Forms.MenuItem
        Me.popupDelete = New System.Windows.Forms.MenuItem
        Me.menuSeparator6 = New System.Windows.Forms.MenuItem
        Me.popupExport = New System.Windows.Forms.MenuItem
        Me.popupSaveIndentityInfo = New System.Windows.Forms.MenuItem
        Me.imageList = New System.Windows.Forms.ImageList(Me.components)
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuFile = New System.Windows.Forms.MenuItem
        Me.menuImport = New System.Windows.Forms.MenuItem
        Me.menuExport = New System.Windows.Forms.MenuItem
        Me.menuSeparator1 = New System.Windows.Forms.MenuItem
        Me.menuSaveIdentityInfo = New System.Windows.Forms.MenuItem
        Me.menuSeparator2 = New System.Windows.Forms.MenuItem
        Me.menuExit = New System.Windows.Forms.MenuItem
        Me.menuIdentity = New System.Windows.Forms.MenuItem
        Me.menuIdentityCreate = New System.Windows.Forms.MenuItem
        Me.menuIdentityDelete = New System.Windows.Forms.MenuItem
        Me.menuView = New System.Windows.Forms.MenuItem
        Me.menuViewRefresh = New System.Windows.Forms.MenuItem
        Me.menuSeparator3 = New System.Windows.Forms.MenuItem
        Me.menuProperties = New System.Windows.Forms.MenuItem
        Me.menuHelp = New System.Windows.Forms.MenuItem
        Me.menuAbout = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'listViewMain
        '
        Me.listViewMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colClassifier, Me.colAuthority})
        Me.listViewMain.ContextMenu = Me.popupMenu
        Me.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewMain.FullRowSelect = True
        Me.listViewMain.HideSelection = False
        Me.listViewMain.Location = New System.Drawing.Point(0, 0)
        Me.listViewMain.Name = "listViewMain"
        Me.listViewMain.Size = New System.Drawing.Size(474, 217)
        Me.listViewMain.SmallImageList = Me.imageList
        Me.listViewMain.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.listViewMain.TabIndex = 0
        Me.listViewMain.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 100
        '
        'colClassifier
        '
        Me.colClassifier.Text = "Classifier"
        Me.colClassifier.Width = 110
        '
        'colAuthority
        '
        Me.colAuthority.Text = "Authority"
        Me.colAuthority.Width = 250
        '
        'popupMenu
        '
        Me.popupMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.popupProperties, Me.menuSeparator5, Me.popupCreate, Me.popupDelete, Me.menuSeparator6, Me.popupExport, Me.popupSaveIndentityInfo})
        Me.popupMenu.Name = "popupMenu"
        '
        'popupProperties
        '
        Me.popupProperties.DefaultItem = True
        Me.popupProperties.Index = 0
        Me.popupProperties.Name = "popupProperties"
        Me.popupProperties.Text = "&Properties"
        '
        'menuSeparator5
        '
        Me.menuSeparator5.Index = 1
        Me.menuSeparator5.Name = "menuSeparator5"
        Me.menuSeparator5.Text = "-"
        '
        'popupCreate
        '
        Me.popupCreate.Index = 2
        Me.popupCreate.Name = "popupCreate"
        Me.popupCreate.Text = "&Create Identity..."
        '
        'popupDelete
        '
        Me.popupDelete.Index = 3
        Me.popupDelete.Name = "popupDelete"
        Me.popupDelete.Text = "&Delete Identity..."
        '
        'menuSeparator6
        '
        Me.menuSeparator6.Index = 4
        Me.menuSeparator6.Name = "menuSeparator6"
        Me.menuSeparator6.Text = "-"
        '
        'popupExport
        '
        Me.popupExport.Index = 5
        Me.popupExport.Name = "popupExport"
        Me.popupExport.Text = "&Export Identity..."
        '
        'popupSaveIndentityInfo
        '
        Me.popupSaveIndentityInfo.Index = 6
        Me.popupSaveIndentityInfo.Name = "popupSaveIndentityInfo"
        Me.popupSaveIndentityInfo.Text = "&Save Identity Info..."
        '
        'imageList
        '
        Me.imageList.ImageSize = New System.Drawing.Size(16, 16)
        Me.imageList.ImageStream = CType(resources.GetObject("imageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList.Images.SetKeyName(0, "ImgIdentity.bmp")
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuIdentity, Me.menuView, Me.menuHelp})
        Me.mainMenu.Name = "mainMenu"
        '
        'menuFile
        '
        Me.menuFile.Index = 0
        Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuImport, Me.menuExport, Me.menuSeparator1, Me.menuSaveIdentityInfo, Me.menuSeparator2, Me.menuExit})
        Me.menuFile.Name = "menuFile"
        Me.menuFile.ShowShortcut = False
        Me.menuFile.Text = "&File"
        '
        'menuImport
        '
        Me.menuImport.Index = 0
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Shortcut = System.Windows.Forms.Shortcut.CtrlI
        Me.menuImport.ShowShortcut = False
        Me.menuImport.Text = "&Import Identity..."
        '
        'menuExport
        '
        Me.menuExport.Index = 1
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Shortcut = System.Windows.Forms.Shortcut.CtrlE
        Me.menuExport.ShowShortcut = False
        Me.menuExport.Text = "&Export Identity..."
        '
        'menuSeparator1
        '
        Me.menuSeparator1.Index = 2
        Me.menuSeparator1.Name = "menuSeparator1"
        Me.menuSeparator1.Text = "-"
        '
        'menuSaveIdentityInfo
        '
        Me.menuSaveIdentityInfo.Index = 3
        Me.menuSaveIdentityInfo.Name = "menuSaveIdentityInfo"
        Me.menuSaveIdentityInfo.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.menuSaveIdentityInfo.ShowShortcut = False
        Me.menuSaveIdentityInfo.Text = "&Save Identity Info..."
        '
        'menuSeparator2
        '
        Me.menuSeparator2.Index = 4
        Me.menuSeparator2.Name = "menuSeparator2"
        Me.menuSeparator2.Text = "-"
        '
        'menuExit
        '
        Me.menuExit.Index = 5
        Me.menuExit.Name = "menuExit"
        Me.menuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ
        Me.menuExit.ShowShortcut = False
        Me.menuExit.Text = "E&xit"
        '
        'menuIdentity
        '
        Me.menuIdentity.Index = 1
        Me.menuIdentity.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuIdentityCreate, Me.menuIdentityDelete})
        Me.menuIdentity.Name = "menuIdentity"
        Me.menuIdentity.Text = "&Identity"
        '
        'menuIdentityCreate
        '
        Me.menuIdentityCreate.Index = 0
        Me.menuIdentityCreate.Name = "menuIdentityCreate"
        Me.menuIdentityCreate.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.menuIdentityCreate.ShowShortcut = False
        Me.menuIdentityCreate.Text = "&Create..."
        '
        'menuIdentityDelete
        '
        Me.menuIdentityDelete.Index = 1
        Me.menuIdentityDelete.Name = "menuIdentityDelete"
        Me.menuIdentityDelete.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.menuIdentityDelete.ShowShortcut = False
        Me.menuIdentityDelete.Text = "&Delete"
        '
        'menuView
        '
        Me.menuView.Index = 2
        Me.menuView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuViewRefresh, Me.menuSeparator3, Me.menuProperties})
        Me.menuView.Name = "menuView"
        Me.menuView.Text = "&View"
        '
        'menuViewRefresh
        '
        Me.menuViewRefresh.Index = 0
        Me.menuViewRefresh.Name = "menuViewRefresh"
        Me.menuViewRefresh.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.menuViewRefresh.Text = "&Refresh"
        '
        'menuSeparator3
        '
        Me.menuSeparator3.Index = 1
        Me.menuSeparator3.Name = "menuSeparator3"
        Me.menuSeparator3.Text = "-"
        '
        'menuProperties
        '
        Me.menuProperties.Index = 2
        Me.menuProperties.Name = "menuProperties"
        Me.menuProperties.ShowShortcut = False
        Me.menuProperties.Text = "&Properties..."
        '
        'menuHelp
        '
        Me.menuHelp.Index = 3
        Me.menuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAbout})
        Me.menuHelp.Name = "menuHelp"
        Me.menuHelp.Text = "&Help"
        '
        'menuAbout
        '
        Me.menuAbout.Index = 0
        Me.menuAbout.Name = "menuAbout"
        Me.menuAbout.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.menuAbout.ShowShortcut = False
        Me.menuAbout.Text = "&About"
        '
        'FormMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(494, 217)
        Me.Controls.Add(Me.listViewMain)
        Me.Menu = Me.mainMenu
        Me.Name = "FormMain"
        Me.Text = "Identity Viewer"
        Me.ResumeLayout(False)

    End Sub 'InitializeComponent

        Friend Shared ReadOnly Property GetInstance() As FormMain
            Get
                If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                    SyncLock m_SyncObject
                        If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                            m_DefaultInstance = New FormMain
                        End If
                    End SyncLock
                End If
                Return m_DefaultInstance
            End Get
        End Property

        Private Shared m_DefaultInstance As FormMain
        Private Shared m_SyncObject As New Object

#End Region


    ' The main entry point for the application.
    '
    <STAThread()> _
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New FormMain)
    End Sub 'Main

    Private Sub menuImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuImport.Click
        CmdImport()
    End Sub 'menuImport_Click

    Private Sub menuExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExport.Click
        CmdExport()
    End Sub 'menuExport_Click

    Private Sub menuSaveIdentityInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveIdentityInfo.Click
        CmdSaveIdentityInfo()
    End Sub 'menuSaveIdentityInfo_Click

    Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
        Me.Close()
    End Sub 'menuExit_Click

    Private Sub menuIdentityCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuIdentityCreate.Click
        CmdCreate()
    End Sub 'menuIdentityCreate_Click

    Private Sub menuIdentityDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuIdentityDelete.Click
        CmdDelete()
    End Sub 'menuIdentityDelete_Click

    Private Sub menuProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuProperties.Click
        CmdProperties()
    End Sub 'menuProperties_Click

    Private Sub menuViewRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuViewRefresh.Click
        CmdRefreshView()
    End Sub 'menuViewRefresh_Click

    Private Sub menuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAbout.Click
        CmdAbout()
    End Sub 'menuAbout_Click

    Private Sub popupProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupProperties.Click
        CmdProperties()
    End Sub 'popupProperties_Click

    Private Sub popupCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupCreate.Click
        CmdCreate()
    End Sub 'popupCreate_Click

    Private Sub popupDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupDelete.Click
        CmdDelete()
    End Sub 'popupDelete_Click

    Private Sub popupExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupExport.Click
        CmdExport()
    End Sub 'popupExport_Click

    Private Sub popupSaveIndentityInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupSaveIndentityInfo.Click
        CmdSaveIdentityInfo()
    End Sub 'popupSaveIndentityInfo_Click

    Private Sub menuIdentity_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuIdentity.Popup
        menuIdentityDelete.Enabled = IsAtLeastOneSelected
    End Sub 'menuIdentity_Popup

    Private Sub menuView_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuView.Popup
        menuProperties.Enabled = IsAtLeastOneSelected
    End Sub 'menuView_Popup

    Private Sub popupMenu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles popupMenu.Popup
        popupDelete.Enabled = IsAtLeastOneSelected
        popupProperties.Enabled = IsAtLeastOneSelected
    End Sub 'popupMenu_Popup

    Private Sub listViewMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewMain.DoubleClick
        CmdProperties()
    End Sub 'listViewMain_DoubleClick

    Private Sub FormMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CmdRefreshView()
    End Sub

    ' Return the PeerIdentity based on the ListViewItem
    '
    Private Function IdentityFromLvi(ByVal lvi As ListViewItem) As PeerIdentity
        Return CType(lvi.Tag, PeerIdentity)
    End Function 'IdentityFromLvi

    ' The currently selected identity in the list.
    '
    Public Property SelectedIdentity() As PeerIdentity
        Get
            ' Return the first selected PeerIdentity or null if there is no selection.
            Dim identity As PeerIdentity = Nothing
            If listViewMain.SelectedItems.Count > 0 Then
                identity = IdentityFromLvi(listViewMain.SelectedItems(0))
            End If
            Return identity
        End Get

        Set(ByVal Value As PeerIdentity)
            ' Try to find the identity in the listview and select it.
            Dim identity As PeerIdentity = Value
            Dim lvi As ListViewItem
            For Each lvi In listViewMain.Items
                ' Compare PeerIdentity objects using .Equals instead of ==
                ' so the values are compared, not the object instances.
                If identity.Equals(IdentityFromLvi(lvi)) Then
                    lvi.Selected = True
                    Exit For
                End If
            Next lvi
        End Set
    End Property

    ' True if at least one line in the list view is selected.
    '
    Public ReadOnly Property IsAtLeastOneSelected() As [Boolean]
        Get
            Return listViewMain.SelectedItems.Count >= 1
        End Get
    End Property

    ' Create a new ListViewItem for the PeerIdentity.
    '
    Public Function CreateListViewItemForIdentity(ByVal identity As PeerIdentity) As ListViewItem
        Dim lvi As New ListViewItem(identity.FriendlyName, 0)
        lvi.Tag = identity

        lvi.SubItems.Add(identity.PeerName.Classifier)
        lvi.SubItems.Add(identity.PeerName.Authority)

        Return lvi

    End Function 'CreateListViewItemForIdentity

    ' Refresh the view by retrieving all of the identities and
    ' adding them to the list.
    '
    Public Sub CmdRefreshView()
        ' This may take awhile, so change the cursor
        Dim cursorSave As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            listViewMain.Items.Clear()

            Dim identities As List(Of PeerIdentity)
            identities = PeerIdentity.GetIdentities()

            Dim identity As PeerIdentity
            For Each identity In identities
                Dim lvi As ListViewItem = CreateListViewItemForIdentity(identity)

                listViewMain.Items.Add(lvi)
            Next identity
        Finally
            ' Restore the cursor
            Me.Cursor = cursorSave
        End Try

    End Sub 'CmdRefreshView

    ' Display the properties for each of the selected identities.
    '
    Public Sub CmdProperties()
        Dim lvi As ListViewItem
        For Each lvi In listViewMain.SelectedItems
            Dim identity As PeerIdentity = IdentityFromLvi(lvi)
            Dim form As New FormIdentity(identity)

            form.Show()
        Next lvi
    End Sub 'CmdProperties

    ' Show the dialog to create a new identity
    '
    Public Sub CmdCreate()
        Dim dlg As New DlgNewIdentity

        If DialogResult.OK = dlg.ShowDialog(Me) Then
            CmdRefreshView()
            SelectedIdentity = dlg.Identity
        End If
    End Sub 'CmdCreate

    ' Delete the selected identities.
    '
    Public Sub CmdDelete()
        ' Confirm the user want to delete the selected item(s)
        If listViewMain.SelectedItems.Count = 1 Then
            Dim identity As PeerIdentity = IdentityFromLvi(listViewMain.SelectedItems(0))
            If DialogResult.Ok <> MessageBox.Show(Me, _
                "Are you sure you want to delete the Identity" & vbCrLf & "'" & identity.FriendlyName & "'?", _
                "Delete Identity", MessageBoxButtons.OkCancel, MessageBoxIcon.Question) Then
                Return
            End If
        Else
            If DialogResult.Ok <> MessageBox.Show(Me, _
                "Are you sure you want to delete the " & listViewMain.SelectedItems.Count.ToString() & " selected identities?", _
                "Delete Identities", MessageBoxButtons.OkCancel, MessageBoxIcon.Question) Then
                Return
            End If
        End If

        Dim lvi As ListViewItem
        For Each lvi In listViewMain.SelectedItems
            Try
                Dim identity As PeerIdentity = IdentityFromLvi(lvi)
                identity.Delete()
            Catch e As PeerNotFoundException
                ' The identity has already been deleted - ignore this error
            Catch e As Exception
                Utilities.DisplayException(e, Me)
            End Try
        Next lvi

        CmdRefreshView()

    End Sub 'CmdDelete

    ' Show some version information about this application.
    '
    Public Sub CmdAbout()
        MessageBox.Show(Me, "Version 1.0", "About IdentityViewer", _
            MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub 'CmdAbout

    ' Show the dialog to import an identity.
    '
    Public Sub CmdImport()
        Dim dlg As New DlgImportIdentity
        If DialogResult.OK = dlg.ShowDialog(Me) Then
            CmdRefreshView()
            SelectedIdentity = dlg.Identity
        End If
    End Sub 'CmdImport

    ' Show the dialog to export an identity.
    '
    Public Sub CmdExport()
        Dim dlg As New DlgExportIdentity(SelectedIdentity)
        dlg.ShowDialog(Me)
    End Sub 'CmdExport

    ' Show the dialog to save the public information for an identity.
    '
    Public Sub CmdSaveIdentityInfo()
        Dim dlg As New DlgSaveIdentityInfo(SelectedIdentity)
        dlg.ShowDialog(Me)
    End Sub 'CmdSaveIdentityInfo

End Class 'FormMain