'=====================================================================
' File:      PnrpPresence.vb
'
' Summary:   Application to register and retrieve information using PNRP.
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

Imports System.Collections.Generic
Imports System.Collections
Imports System.Net
Imports System.Net.PeerToPeer
Imports System.Text
Imports System.Windows.Forms

Public Class FormMain
    Inherits System.Windows.Forms.Form
    Private peerNameQueue As Queue ' simple list of PeerNames to resolve
    Private resolver As PnrpEndPointResolver ' Resolves PeerNames from the queue
    Private regDomain As PnrpEndPointRegistration ' 0.PnrpPresence = <name>
    Private regName As PnrpEndPointRegistration ' 0.PnrpPresence.<name> = <authority>
    Private regStatus As PnrpEndPointRegistration ' <authority>.<name> = <status>
    Private peerNamePnrpPresence As PeerName

    Private listViewMain As System.Windows.Forms.ListView
    Private colName As System.Windows.Forms.ColumnHeader
    Private colStatus As System.Windows.Forms.ColumnHeader
    Private colAuthority As System.Windows.Forms.ColumnHeader
    Private colAddress As System.Windows.Forms.ColumnHeader

    Private labelIdentity As System.Windows.Forms.Label
    Private dropListIdentity As System.Windows.Forms.ComboBox
    Private labelAddress As System.Windows.Forms.Label
    Private textBoxAddress As System.Windows.Forms.TextBox
    Private labelStatus As System.Windows.Forms.Label
    Private dropListStatus As System.Windows.Forms.ComboBox
    Private panelSettings As System.Windows.Forms.Panel
    Private labelCloud As System.Windows.Forms.Label
    Private dropListCloud As System.Windows.Forms.ComboBox
    Private WithEvents buttonRefresh As System.Windows.Forms.Button
    Private WithEvents buttonRegister As System.Windows.Forms.Button

    Private components As System.ComponentModel.IContainer = Nothing


    ' This is the constructor for the application form.
    ' It initializes the UI, registers some default information,
    ' and begins the resolution process.
    '
    Public Sub New()
        InitializeComponent()

        InitializeIdentityList()
        InitializeCloudList()
        InitializeUserSettings()
        InitializeResolve()

    End Sub 'New


    ' Initialize the drop list of identities available for the user.
    '
    Public Sub InitializeIdentityList()
        ' Get all of the available identities
        Dim identities As List(Of PeerIdentity)
        identities = PeerIdentity.GetIdentities()

        ' Add each identity to the list box.
        Dim identity As PeerIdentity
        For Each identity In identities
            dropListIdentity.Items.Add(identity)
        Next identity

        ' Select the first item
        If dropListIdentity.Items.Count > 0 Then
            dropListIdentity.SelectedIndex = 0
        End If

    End Sub 'InitializeIdentityList


    ' Initialize the drop list of available clouds.
    '
    Public Sub InitializeCloudList()
        ' Get all of the available clouds
        Dim clouds As List(Of Cloud)
        clouds = CloudWatcher.GetClouds()

        ' Fill the list box
        Dim cloud As Cloud
        For Each cloud In clouds
            Dim iItem As Integer = dropListCloud.Items.Add(cloud)

            ' Select the global cloud as the default
            If cloud.Scope = Scope.Global Then
                dropListCloud.SelectedIndex = iItem
            End If
        Next cloud

    End Sub 'InitializeCloudList


    ' Initialize the user's current IPAddress and status
    '
    Public Sub InitializeUserSettings()
        textBoxAddress.Text = LocalAddress.ToString()
        dropListStatus.SelectedIndex = 0

        peerNamePnrpPresence = New PeerName("0.PnrpPresence")

    End Sub 'InitializeUserSettings


    ' Initialize the resolve objects.
    '
    Public Sub InitializeResolve()
        peerNameQueue = New Queue

        resolver = New PnrpEndPointResolver

        resolver.MaxResults = 1000 ' We don't expect more than 1000 results
        resolver.TimeOut = New TimeSpan(0, 1, 0) ' Timeout after 1 minute
        resolver.ResolutionCriteria = ResolutionCriteria.All

        resolver.SynchronizingObject = Me ' Callbacks happen on the UI thread
        AddHandler resolver.PeerNameFound, AddressOf OnPeerNameFound
        AddHandler resolver.ResolutionCompleted, AddressOf OnResolutionCompleted
    End Sub 'InitializeResolve


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

    ' <summary>
    ' Required method for Designer support - do not modify
    ' the contents of this method with the code editor.
    ' </summary>
    Private Sub InitializeComponent()
        Me.listViewMain = New System.Windows.Forms.ListView
        Me.colName = New System.Windows.Forms.ColumnHeader
        Me.colStatus = New System.Windows.Forms.ColumnHeader
        Me.colAddress = New System.Windows.Forms.ColumnHeader
        Me.colAuthority = New System.Windows.Forms.ColumnHeader
        Me.panelSettings = New System.Windows.Forms.Panel
        Me.buttonRegister = New System.Windows.Forms.Button
        Me.labelCloud = New System.Windows.Forms.Label
        Me.dropListCloud = New System.Windows.Forms.ComboBox
        Me.labelStatus = New System.Windows.Forms.Label
        Me.buttonRefresh = New System.Windows.Forms.Button
        Me.dropListStatus = New System.Windows.Forms.ComboBox
        Me.dropListIdentity = New System.Windows.Forms.ComboBox
        Me.labelIdentity = New System.Windows.Forms.Label
        Me.textBoxAddress = New System.Windows.Forms.TextBox
        Me.labelAddress = New System.Windows.Forms.Label
        Me.panelSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'listViewMain
        '
        Me.listViewMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colStatus, Me.colAddress, Me.colAuthority})
        Me.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewMain.FullRowSelect = True
        Me.listViewMain.Location = New System.Drawing.Point(0, 77)
        Me.listViewMain.Name = "listViewMain"
        Me.listViewMain.Size = New System.Drawing.Size(573, 157)
        Me.listViewMain.TabIndex = 1
        Me.listViewMain.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 113
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 78
        '
        'colAddress
        '
        Me.colAddress.Text = "Address"
        Me.colAddress.Width = 183
        '
        'colAuthority
        '
        Me.colAuthority.Text = "Authority"
        Me.colAuthority.Width = 289
        '
        'panelSettings
        '
        Me.panelSettings.Controls.Add(Me.buttonRegister)
        Me.panelSettings.Controls.Add(Me.labelCloud)
        Me.panelSettings.Controls.Add(Me.dropListCloud)
        Me.panelSettings.Controls.Add(Me.labelStatus)
        Me.panelSettings.Controls.Add(Me.buttonRefresh)
        Me.panelSettings.Controls.Add(Me.dropListStatus)
        Me.panelSettings.Controls.Add(Me.dropListIdentity)
        Me.panelSettings.Controls.Add(Me.labelIdentity)
        Me.panelSettings.Controls.Add(Me.textBoxAddress)
        Me.panelSettings.Controls.Add(Me.labelAddress)
        Me.panelSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelSettings.Location = New System.Drawing.Point(0, 0)
        Me.panelSettings.Name = "panelSettings"
        Me.panelSettings.Size = New System.Drawing.Size(573, 77)
        Me.panelSettings.TabIndex = 0
        '
        'buttonRegister
        '
        Me.buttonRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonRegister.Location = New System.Drawing.Point(482, 9)
        Me.buttonRegister.Name = "buttonRegister"
        Me.buttonRegister.Size = New System.Drawing.Size(78, 23)
        Me.buttonRegister.TabIndex = 8
        Me.buttonRegister.Text = "&Register"
        '
        'labelCloud
        '
        Me.labelCloud.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelCloud.Location = New System.Drawing.Point(262, 43)
        Me.labelCloud.Name = "labelCloud"
        Me.labelCloud.Size = New System.Drawing.Size(40, 16)
        Me.labelCloud.TabIndex = 6
        Me.labelCloud.Text = "Cloud:"
        '
        'dropListCloud
        '
        Me.dropListCloud.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListCloud.Location = New System.Drawing.Point(307, 43)
        Me.dropListCloud.Name = "dropListCloud"
        Me.dropListCloud.Size = New System.Drawing.Size(147, 21)
        Me.dropListCloud.Sorted = True
        Me.dropListCloud.TabIndex = 7
        '
        'labelStatus
        '
        Me.labelStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelStatus.Location = New System.Drawing.Point(262, 9)
        Me.labelStatus.Name = "labelStatus"
        Me.labelStatus.Size = New System.Drawing.Size(42, 16)
        Me.labelStatus.TabIndex = 4
        Me.labelStatus.Text = "Status:"
        '
        'buttonRefresh
        '
        Me.buttonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonRefresh.Location = New System.Drawing.Point(482, 43)
        Me.buttonRefresh.Name = "buttonRefresh"
        Me.buttonRefresh.Size = New System.Drawing.Size(78, 23)
        Me.buttonRefresh.TabIndex = 9
        Me.buttonRefresh.Text = "Re&fresh"
        '
        'dropListStatus
        '
        Me.dropListStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListStatus.Items.AddRange(New Object() {"Active", "Be right back", "Busy", "On the phone", "Out to lunch"})
        Me.dropListStatus.Location = New System.Drawing.Point(307, 9)
        Me.dropListStatus.Name = "dropListStatus"
        Me.dropListStatus.Size = New System.Drawing.Size(147, 21)
        Me.dropListStatus.Sorted = True
        Me.dropListStatus.TabIndex = 5
        '
        'dropListIdentity
        '
        Me.dropListIdentity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListIdentity.Location = New System.Drawing.Point(58, 9)
        Me.dropListIdentity.Name = "dropListIdentity"
        Me.dropListIdentity.Size = New System.Drawing.Size(173, 21)
        Me.dropListIdentity.Sorted = True
        Me.dropListIdentity.TabIndex = 1
        '
        'labelIdentity
        '
        Me.labelIdentity.Location = New System.Drawing.Point(5, 9)
        Me.labelIdentity.Name = "labelIdentity"
        Me.labelIdentity.Size = New System.Drawing.Size(49, 16)
        Me.labelIdentity.TabIndex = 0
        Me.labelIdentity.Text = "Identity:"
        '
        'textBoxAddress
        '
        Me.textBoxAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxAddress.Location = New System.Drawing.Point(58, 43)
        Me.textBoxAddress.Name = "textBoxAddress"
        Me.textBoxAddress.Size = New System.Drawing.Size(173, 19)
        Me.textBoxAddress.TabIndex = 3
        '
        'labelAddress
        '
        Me.labelAddress.Location = New System.Drawing.Point(5, 43)
        Me.labelAddress.Name = "labelAddress"
        Me.labelAddress.Size = New System.Drawing.Size(49, 16)
        Me.labelAddress.TabIndex = 2
        Me.labelAddress.Text = "Address:"
        '
        'FormMain
        '
        Me.AcceptButton = Me.buttonRefresh
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(573, 234)
        Me.Controls.Add(Me.listViewMain)
        Me.Controls.Add(Me.panelSettings)
        Me.MinimumSize = New System.Drawing.Size(500, 200)
        Me.Name = "FormMain"
        Me.Text = "PNRP Presence"
        Me.panelSettings.ResumeLayout(False)
        Me.panelSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub 'InitializeComponent
#End Region


    ' The main entry point for the application.
    '
    <STAThread()> _
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New FormMain)

    End Sub 'Main

    ' This routine handles one result of a PnrpEndPointResolution
    '
    Private Sub OnPeerNameFound(ByVal sender As Object, ByVal e As PeerNameFoundEventArgs)
        ProcessPnrpEndPoint(e.PnrpEndPoint)
    End Sub 'OnPeerNameFound

    ' This routine handles the completion of one PnrpEndPointResolution
    ' by starting the timer to delay before processing the next entry.
    '
    Private Sub OnResolutionCompleted(ByVal sender As Object, ByVal e As ResolutionCompletedEventArgs)
        ProcessNextPeerName()
    End Sub 'OnResolutionCompleted

    ' This routine handles the Refresh button click by
    ' initializing the queue and starting the resolve process.
    '
    Private Sub buttonRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonRefresh.Click
        InitializeQueue()

        ' Disable the button until all of the PeerNames have been resolved.
        buttonRefresh.Enabled = False
    End Sub 'buttonRefresh_Click

    ' This routine handles the Register button click by
    ' either registering or unregistering the information with PNRP.
    '
    Private Sub buttonRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonRegister.Click
        If IsRegistered Then
            Unregister()
            buttonRegister.Text = "&Register"
            EnableControls(True)
        ElseIf Register() Then
            buttonRegister.Text = "Un&register"
            EnableControls(False)
        End If
    End Sub 'buttonRegister_Click

    ' Enable (or disable) the controls.
    '
    Private Sub EnableControls(ByVal enable As Boolean)
        dropListCloud.Enabled = enable
        dropListIdentity.Enabled = enable
        dropListStatus.Enabled = enable
        textBoxAddress.ReadOnly = Not enable
    End Sub 'EnableControls

    ' Find the entry with the matching authority.
    '
    Private Function FindEntry(ByVal authority As String) As ListViewItem
        Dim lvi As ListViewItem
        For Each lvi In listViewMain.Items
            If authority = lvi.SubItems(colAuthority.Index).Text Then
                Return lvi
            End If
        Next lvi

        Return Nothing

    End Function 'FindEntry


    ' Create a new ListViewItem and add it to the list.
    '
    Private Sub AddEntry(ByVal authority As String, ByVal name As String, ByVal endPoint As PnrpEndPoint, ByVal status As String)
        Dim lvi As New ListViewItem(name)
        lvi.SubItems.Add(status)
        lvi.SubItems.Add(endPoint.IPEndPoints(0).ToString())
        lvi.SubItems.Add(authority)

        ' This entry has been verified
        lvi.Checked = True

        listViewMain.Items.Add(lvi)
    End Sub 'AddEntry


    ' Convert the authority hex string into a base 64-encoded string.
    ' 
    Public Function ConvertAuthorityToString(ByVal authority As String) As String
        ' Convert the authority string to a byte array
        Dim data(19) As Byte
        Dim i As Integer
        For i = 0 To 19
            data(i) = System.Convert.ToByte(authority.Substring(i * 2, 2), 16)
        Next i

        ' Convert the byte array to Base 64 string
        Return System.Convert.ToBase64String(data)

    End Function 'ConvertAuthorityToString

    ' Convert the base 64-encoded string to a 40 character hex string
    ' 
    Public Function ConvertStringToAuthority(ByVal s As String) As String
        Dim data As Byte() = System.Convert.FromBase64String(s)

        Dim authority As New StringBuilder(40)
        Dim i As Integer
        For i = 0 To (data.Length - 1)
            authority.AppendFormat("{0:x2}", data(i))
        Next i

        Return authority.ToString()

    End Function 'ConvertStringToAuthority

    ' This retrieves the cloud the user has selected.
    '
    Public ReadOnly Property CurrentCloud() As Cloud
        Get
            Return CType(dropListCloud.SelectedItem, Cloud)
        End Get
    End Property

    ' This retrieves the local IP address for the machine.
    ' 
    Public ReadOnly Property LocalAddress() As IPAddress
        Get
            Dim hostEntry As IPHostEntry = Dns.GetHostByName(System.Environment.MachineName)

            ' No addressses - return nothing
            If hostEntry.AddressList.Length = 0 Then
                Return Nothing
            End If
            ' Just return the first address
            Return hostEntry.AddressList(0)
        End Get
    End Property

    ' Initialize the queue of PeerNames to resolve, clear the list,
    ' and start the resolution process.
    ' 
    Private Sub InitializeQueue()
        ' Initialize the queue with the general "0.PnrpPresence"
        peerNameQueue.Enqueue(peerNamePnrpPresence)

        ' Make sure we are resolving in the same cloud we are registered in.
        resolver.Cloud = CurrentCloud

        ' Clear the ListView
        listViewMain.Items.Clear()

        ' Start to process the first element in the queue
        ProcessNextPeerName()

    End Sub 'InitializeQueue

    ' Try to resolve the next item in the list.
    '
    Private Sub ProcessNextPeerName()
        If peerNameQueue.Count <> 0 Then
            ' Resolve the next item in the list
            resolver.PeerName = CType(peerNameQueue.Dequeue(), PeerName)
            resolver.BeginResolve()
        Else
            ' Enable the refresh button
            buttonRefresh.Enabled = True
        End If

    End Sub 'ProcessNextPeerName

    ' This routine adds a PeerName to the queue.
    '
    Private Sub AddPeerNameToQueue(ByVal authority As String, ByVal classifier As String)
        ' Create the peer name to resolve for.
        Dim securePeerName As New PeerName(authority & "." & classifier)
        peerNameQueue.Enqueue(securePeerName)

    End Sub 'AddPeerNameToQueue

    ' This routine processes a PnrpEndPoint by either adding a new peer name
    ' to the queue, adding a new entry to the list, or updating an existing entry.
    '
    Private Sub ProcessPnrpEndPoint(ByVal endPoint As PnrpEndPoint)
        Dim peerName As PeerName = endPoint.PeerName

        If peerName.Authority = "0" Then
            Dim iDot As Integer = peerName.Classifier.IndexOf(".")
            If iDot < 0 Then
                ' Found a result for "0.PnrpPresence"
                ' so get authority from the comment in the matching "0.PnrpPresence.<name>"
                AddPeerNameToQueue("0", peerName.Classifier & "." & endPoint.Comment)
            Else
                ' Found a result for "0.PnrpPresence.<name>"
                ' so get the status from the comment in the matching "<authority>.<name>"
                Dim name As String = peerName.Classifier.Substring(iDot + 1)
                Dim authority As String = ConvertStringToAuthority(endPoint.Comment)
                AddPeerNameToQueue(authority, name)
            End If
        Else
            ' Found a result for "<authority>.<name>"
            ' so add it to the display if it doesn't already exist.
            If (FindEntry(peerName.Authority) Is Nothing) Then
                AddEntry(peerName.Authority, peerName.Classifier, endPoint, endPoint.Comment)
            End If
        End If

    End Sub 'ProcessPnrpEndPoint

    ' Register the user's data.
    '
    ' PnrpEndPointRegistration   PeerName               Comment
    ' ------------------------   -------------          ----------
    '                regDomain   0.PnrpPresence         <name>
    '                regName     0.PnrpPresence.<name>  <authority>
    '                regStatus   <authority>.<name>     <status>
    '
    Public Function Register() As Boolean
        Dim registered As Boolean = False

        Try
            Dim identity As PeerIdentity = CType(dropListIdentity.SelectedItem, PeerIdentity)
            Dim name As String = identity.FriendlyName
            Dim securePeerName As PeerName = identity.CreatePeerName(name)
            Dim cloud As Cloud = CurrentCloud
            Dim status As String = dropListStatus.Text

            ' Well known insecure PeerName: 0.PnrpPresence
            Dim endPoint As PnrpEndPoint = CreatePnrpEndPoint(peerNamePnrpPresence, name)
            regDomain = New PnrpEndPointRegistration(endPoint, identity, cloud)

            ' Specific insecure PeerName: 0.PnrpPresence.<name>
            Dim insecurePeerName As New PeerName(peerNamePnrpPresence.PeerNameString & "." & name)
            endPoint = CreatePnrpEndPoint(insecurePeerName, ConvertAuthorityToString(securePeerName.Authority))
            regName = New PnrpEndPointRegistration(endPoint, identity, cloud)

            ' Secure PeerName: <authority>.<name>
            endPoint = CreatePnrpEndPoint(securePeerName, status)
            regStatus = New PnrpEndPointRegistration(endPoint, identity, cloud)

            ' Register the data
            regStatus.Register()
            regName.Register()
            regDomain.Register()

            registered = True
        Catch e As Exception
            ' Display the error message.
            MessageBox.Show(Me, e.Message & vbCrLf & vbCrLf & e.StackTrace, _
                "Exception from " & e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return registered

    End Function 'Register

    ' Ensure the information is not registered.
    '
    Public Sub Unregister()
        regDomain.Unregister()
        regName.Unregister()
        regStatus.Unregister()
    End Sub 'Unregister

    ' True if the information is registered
    '
    Public ReadOnly Property IsRegistered() As Boolean
        Get
            If (regDomain Is Nothing) Then
                Return False
            ElseIf (regDomain.State = RegistrationState.Registered) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ' Create a new PnrpEndPoint that will be registered.
    '
    Public Function CreatePnrpEndPoint(ByVal peerName As PeerName, ByVal comment As String) As PnrpEndPoint
        Dim address As IPAddress = IPAddress.Parse(textBoxAddress.Text)
        Dim ipEndPoints(0) As IPEndPoint

        ipEndPoints(0) = New IPEndPoint(address, 0)
        Return New PnrpEndPoint(peerName, ipEndPoints, comment)

    End Function 'CreatePnrpEndPoint

End Class 'FormMain