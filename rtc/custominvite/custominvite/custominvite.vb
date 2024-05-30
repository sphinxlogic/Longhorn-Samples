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
Imports System.Diagnostics
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Collaboration
Imports Microsoft.Collaboration
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text

Namespace Microsoft.Collaboration.Samples.CustomInvite
    Public NotInheritable Class CustomInviteForm
        Inherits System.Windows.Forms.Form
        Implements IMediaNegotiation

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            Me.InitializeComponent()

            'Add any initialization after the InitializeComponent() call

            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            'RtcProvider manages RtcProfile and RtcPeerToPeerProfile.
            'It provides methods to create and remove profiles.
            Me.profileProvider = New RtcProvider
            Try
                'RtcPeerToPeerProfile is the entry point to all collaboration functionality.
                'For peer to peer, only signaling session (RtcProfileSignaling) is supported.
                'RtcProfilePresence or RtcProfilePersistedData is not available.
                Me.myProfile = Me.profileProvider.CreateRealTimePeerToPeerProfile(SystemInformation.ComputerName)
                AddHandler Me.myProfile.Signaling.SessionReceived, AddressOf Me.OnSessionReceived
                Me.ListeningState()
                Me.Show()
                Me.BringToFront()
            Catch ecp As Exception
                MessageBox.Show(Me, "Failed to create profile " + ecp.ToString(), "Error")
            End Try
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                ''''
                'Begin Microsoft.Collaboration Functionality Code
                ''''
                If Not (Me.signalingSession Is Nothing) Then
                    Try
                        'Dispose signaling session resources
                        Me.signalingSession.Leave()
                    Catch
                    End Try
                    Me.signalingSession = Nothing
                End If

                If Not (Me.myProfile Is Nothing) Then
                    Try
                        'Dispose profile resources
                        Me.profileProvider.RemoveRealTimeProfile(Me.myProfile)
                    Catch
                    End Try
                    Me.myProfile = Nothing
                End If
                ''''
                'End Microsoft.Collaboration Functionality Code
                ''''

            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified Imports the Windows Form Designer.  
        'Do not modify it Imports the code editor.
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
        Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
        Friend WithEvents fileMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents createSessionMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents reinviteMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents terminateSessionMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents rcvdContentDescTextBox As System.Windows.Forms.TextBox
        Friend WithEvents rcvdContentTypeTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents sentPeerAddressTextBox As System.Windows.Forms.TextBox
        Friend WithEvents sentContentDescTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents sentContentTypeTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents exitMenuItem As System.Windows.Forms.MenuItem
        Friend WithEvents StatusBarPanel As System.Windows.Forms.StatusBarPanel
        Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
        Friend WithEvents rcvdPeerAddressTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.TabPage1 = New System.Windows.Forms.TabPage
            Me.TabPage2 = New System.Windows.Forms.TabPage
            Me.StatusBar = New System.Windows.Forms.StatusBar
            Me.StatusBarPanel = New System.Windows.Forms.StatusBarPanel
            Me.MainMenu1 = New System.Windows.Forms.MainMenu
            Me.fileMenuItem = New System.Windows.Forms.MenuItem
            Me.createSessionMenuItem = New System.Windows.Forms.MenuItem
            Me.reinviteMenuItem = New System.Windows.Forms.MenuItem
            Me.terminateSessionMenuItem = New System.Windows.Forms.MenuItem
            Me.exitMenuItem = New System.Windows.Forms.MenuItem
            Me.MenuItem1 = New System.Windows.Forms.MenuItem
            Me.GroupBox4 = New System.Windows.Forms.GroupBox
            Me.Label5 = New System.Windows.Forms.Label
            Me.rcvdContentDescTextBox = New System.Windows.Forms.TextBox
            Me.rcvdContentTypeTextBox = New System.Windows.Forms.TextBox
            Me.Label7 = New System.Windows.Forms.Label
            Me.GroupBox1 = New System.Windows.Forms.GroupBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.sentPeerAddressTextBox = New System.Windows.Forms.TextBox
            Me.sentContentDescTextBox = New System.Windows.Forms.TextBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.sentContentTypeTextBox = New System.Windows.Forms.TextBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.rcvdPeerAddressTextBox = New System.Windows.Forms.TextBox
            Me.Label6 = New System.Windows.Forms.Label
            CType(Me.StatusBarPanel, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox4.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabPage1
            '
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Size = New System.Drawing.Size(416, 230)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Outgoing Session"
            '
            'TabPage2
            '
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Size = New System.Drawing.Size(440, 247)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Incoming Session"
            '
            'StatusBar
            '
            Me.StatusBar.Location = New System.Drawing.Point(0, 293)
            Me.StatusBar.Name = "StatusBar"
            Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel})
            Me.StatusBar.ShowPanels = True
            Me.StatusBar.Size = New System.Drawing.Size(432, 22)
            Me.StatusBar.TabIndex = 23
            Me.StatusBar.Text = "StatusBar1"
            '
            'StatusBarPanel
            '
            Me.StatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.StatusBarPanel.Text = "StatusBarPanel"
            Me.StatusBarPanel.Width = 416
            '
            'MainMenu1
            '
            Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.fileMenuItem, Me.MenuItem1})
            '
            'fileMenuItem
            '
            Me.fileMenuItem.Index = 0
            Me.fileMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.createSessionMenuItem, Me.reinviteMenuItem, Me.terminateSessionMenuItem, Me.exitMenuItem})
            Me.fileMenuItem.Text = "&File"
            '
            'createSessionMenuItem
            '
            Me.createSessionMenuItem.Index = 0
            Me.createSessionMenuItem.Text = "&Create Session..."
            '
            'reinviteMenuItem
            '
            Me.reinviteMenuItem.Index = 1
            Me.reinviteMenuItem.Text = "&ReInvite"
            '
            'terminateSessionMenuItem
            '
            Me.terminateSessionMenuItem.Index = 2
            Me.terminateSessionMenuItem.Text = "&Terminate Session"
            '
            'exitMenuItem
            '
            Me.exitMenuItem.Index = 3
            Me.exitMenuItem.Text = "&Exit"
            '
            'MenuItem1
            '
            Me.MenuItem1.Index = 1
            Me.MenuItem1.Text = ""
            '
            'GroupBox4
            '
            Me.GroupBox4.Controls.Add(Me.Label5)
            Me.GroupBox4.Controls.Add(Me.rcvdPeerAddressTextBox)
            Me.GroupBox4.Controls.Add(Me.rcvdContentDescTextBox)
            Me.GroupBox4.Controls.Add(Me.Label6)
            Me.GroupBox4.Controls.Add(Me.rcvdContentTypeTextBox)
            Me.GroupBox4.Controls.Add(Me.Label7)
            Me.GroupBox4.Location = New System.Drawing.Point(0, 16)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(208, 272)
            Me.GroupBox4.TabIndex = 31
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "Received Session Information"
            '
            'Label5
            '
            Me.Label5.Location = New System.Drawing.Point(8, 96)
            Me.Label5.Name = "Label5"
            Me.Label5.TabIndex = 22
            Me.Label5.Text = "Content Type"
            '
            'rcvdContentDescTextBox
            '
            Me.rcvdContentDescTextBox.Location = New System.Drawing.Point(8, 184)
            Me.rcvdContentDescTextBox.Multiline = True
            Me.rcvdContentDescTextBox.Name = "rcvdContentDescTextBox"
            Me.rcvdContentDescTextBox.ReadOnly = True
            Me.rcvdContentDescTextBox.Size = New System.Drawing.Size(192, 72)
            Me.rcvdContentDescTextBox.TabIndex = 25
            Me.rcvdContentDescTextBox.Text = ""
            '
            'rcvdContentTypeTextBox
            '
            Me.rcvdContentTypeTextBox.Location = New System.Drawing.Point(8, 120)
            Me.rcvdContentTypeTextBox.Name = "rcvdContentTypeTextBox"
            Me.rcvdContentTypeTextBox.ReadOnly = True
            Me.rcvdContentTypeTextBox.Size = New System.Drawing.Size(192, 20)
            Me.rcvdContentTypeTextBox.TabIndex = 24
            Me.rcvdContentTypeTextBox.Text = ""
            '
            'Label7
            '
            Me.Label7.Location = New System.Drawing.Point(8, 160)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(120, 23)
            Me.Label7.TabIndex = 23
            Me.Label7.Text = "Content Description"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.sentPeerAddressTextBox)
            Me.GroupBox1.Controls.Add(Me.sentContentDescTextBox)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.sentContentTypeTextBox)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Location = New System.Drawing.Point(224, 16)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(208, 272)
            Me.GroupBox1.TabIndex = 32
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Sent Session Information"
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(8, 96)
            Me.Label1.Name = "Label1"
            Me.Label1.TabIndex = 22
            Me.Label1.Text = "Content Type"
            '
            'sentPeerAddressTextBox
            '
            Me.sentPeerAddressTextBox.Location = New System.Drawing.Point(8, 56)
            Me.sentPeerAddressTextBox.Name = "sentPeerAddressTextBox"
            Me.sentPeerAddressTextBox.ReadOnly = True
            Me.sentPeerAddressTextBox.Size = New System.Drawing.Size(192, 20)
            Me.sentPeerAddressTextBox.TabIndex = 21
            Me.sentPeerAddressTextBox.Text = ""
            '
            'sentContentDescTextBox
            '
            Me.sentContentDescTextBox.Location = New System.Drawing.Point(8, 184)
            Me.sentContentDescTextBox.Multiline = True
            Me.sentContentDescTextBox.Name = "sentContentDescTextBox"
            Me.sentContentDescTextBox.ReadOnly = True
            Me.sentContentDescTextBox.Size = New System.Drawing.Size(192, 72)
            Me.sentContentDescTextBox.TabIndex = 25
            Me.sentContentDescTextBox.Text = ""
            '
            'Label2
            '
            Me.Label2.Location = New System.Drawing.Point(8, 32)
            Me.Label2.Name = "Label2"
            Me.Label2.TabIndex = 20
            Me.Label2.Text = "Peer Address"
            '
            'sentContentTypeTextBox
            '
            Me.sentContentTypeTextBox.Location = New System.Drawing.Point(8, 120)
            Me.sentContentTypeTextBox.Name = "sentContentTypeTextBox"
            Me.sentContentTypeTextBox.ReadOnly = True
            Me.sentContentTypeTextBox.Size = New System.Drawing.Size(192, 20)
            Me.sentContentTypeTextBox.TabIndex = 24
            Me.sentContentTypeTextBox.Text = ""
            '
            'Label3
            '
            Me.Label3.Location = New System.Drawing.Point(8, 160)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(120, 23)
            Me.Label3.TabIndex = 23
            Me.Label3.Text = "Content Description"
            '
            'rcvdPeerAddressTextBox
            '
            Me.rcvdPeerAddressTextBox.Location = New System.Drawing.Point(8, 56)
            Me.rcvdPeerAddressTextBox.Name = "rcvdPeerAddressTextBox"
            Me.rcvdPeerAddressTextBox.ReadOnly = True
            Me.rcvdPeerAddressTextBox.Size = New System.Drawing.Size(192, 20)
            Me.rcvdPeerAddressTextBox.TabIndex = 21
            Me.rcvdPeerAddressTextBox.Text = ""
            '
            'Label6
            '
            Me.Label6.Location = New System.Drawing.Point(8, 32)
            Me.Label6.Name = "Label6"
            Me.Label6.TabIndex = 20
            Me.Label6.Text = "Peer Address"
            '
            'CustomInviteForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(432, 315)
            Me.Controls.Add(Me.GroupBox4)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.StatusBar)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Menu = Me.MainMenu1
            Me.Name = "CustomInviteForm"
            Me.Text = "Custom Invite"
            CType(Me.StatusBarPanel, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Property PeerAddress() As String
            Get
                If (Me.isOriginator) Then
                    Return Me.sentPeerAddressTextBox.Text
                Else
                    Return Me.rcvdPeerAddressTextBox.Text
                End If
            End Get
            Set(ByVal value As String)
                If (Me.isOriginator) Then
                    Me.sentPeerAddressTextBox.Text = value
                Else
                    Me.rcvdPeerAddressTextBox.Text = value
                End If
            End Set
        End Property

        Private Property SentContentType() As String
            Get
                Return Me.sentContentTypeTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.sentContentTypeTextBox.Text = value
            End Set
        End Property

        Private Property SentContentDescription() As String
            Get
                Return Me.sentContentDescTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.sentContentDescTextBox.Text = value
            End Set
        End Property

        Private WriteOnly Property RcvdContentType() As String
            Set(ByVal value As String)
                Me.rcvdContentTypeTextBox.Text = value
            End Set
        End Property

        Private Property RcvdContentDescription() As String
            Get
                Return Me.rcvdContentDescTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.rcvdContentDescTextBox.Text = value
            End Set
        End Property

        'Handle incoming session request
        Private Sub OnSessionReceived(ByVal sender As Object, ByVal e As IncomingSessionEventArgs)
            Dim session As SignalingSession
            session = e.Session

            If (Me.signalingSession Is Nothing And TypeOf session.GetIncomingDescription() Is CustomMediaDescription) Then
                Dim description As CustomMediaDescription
                description = session.GetIncomingDescription()

                Dim dlgAccept As New AcceptSessionDialog
                dlgAccept.RcvdContentType = description.DescriptionType
                dlgAccept.RcvdPeerAddress = session.Inviter.RealTimeAddress
                dlgAccept.RcvdContentDescription = description.Description

                If dlgAccept.ShowDialog = DialogResult.OK Then
                    Me.isOriginator = False
                    Me.RcvdContentType = description.DescriptionType
                    Me.PeerAddress = session.Inviter.RealTimeAddress
                    Me.RcvdContentDescription = description.Description
                    ''''
                    'Begin Microsoft.Collaboration Functionality Code
                    ''''
                    Me.signalingSession = session
                    'Add event handlers to variou session events
                    Me.InitSession(session)

                    Try
                        'Session is accepted. 
                        session.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), session)
                        Me.ConnectingState()
                    Catch ecp As Exception
                        MessageBox.Show(Me, "Failed to accept incoming session " + ecp.ToString(), "Error")
                        'Session resoruce is released
                        Me.ListeningState()
                    End Try
                    ''''
                    'End Microsoft.Collaboration Functionality Code
                    ''''
                    Return
                End If
            End If

            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Try
                'Session is terminated
                session.Decline()
                'Session resoruce is released
                session.Leave()
            Catch
            End Try
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub Exit_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles exitMenuItem.Click
            Me.Close()
        End Sub

        Private Sub CreateSession_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles createSessionMenuItem.Click
            Trace.WriteLine("Entering CreateSession_Clicked")

            Dim dlg As New CreateSessionDialog
            dlg.Text = "Create Session"
            dlg.SentContentType = "application/xml"
            dlg.SentContentDescription = "<example></example>"

            If dlg.ShowDialog = DialogResult.OK Then
                Me.isOriginator = True
                Me.PeerAddress = dlg.SentPeerAddress
                Me.SentContentType = dlg.SentContentType
                Me.SentContentDescription = dlg.SentContentDescription

                ''''
                'Begin Microsoft.Collaboration Functionality Code
                ''''
                Try
                    'Session is created
                    Me.signalingSession = Me.myProfile.Signaling.CreateSession("Generic", Nothing, Nothing)
                    'Add event handlers to variou session events
                    Me.InitSession(Me.signalingSession)
                    'Invite peer to participate the session
                    Me.signalingSession.BeginInvite(Me.PeerAddress, New AsyncCallback(AddressOf InviteCallback), Me.signalingSession)
                    Me.ConnectingState()
                Catch ecp As Exception
                    MessageBox.Show(Me, "Failed to invite peer " + ecp.ToString(), "Error")
                    Me.ListeningState()
                End Try
                ''''
                'End Microsoft.Collaboration Functionality Code
                ''''
            End If
        End Sub

        Private Sub AcceptCallback(ByVal result As IAsyncResult)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim session As SignalingSession
            session = result.AsyncState

            Try
                'Ended accepting session
                session.EndAccept(result)
            Catch ecp As Exception
                MessageBox.Show(Me, "Failed to accept session " + ecp.ToString(), "Error")
                Me.ListeningState()
            End Try
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub InviteCallback(ByVal result As IAsyncResult)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim session As SignalingSession
            session = result.AsyncState

            Try
                'Ended inviting peer
                session.EndInvite(result)
            Catch ecp As Exception
                MessageBox.Show(Me, "Failed to invite peer " + ecp.ToString(), "Error")
                Me.ListeningState()
            End Try
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub Reinvite_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles reinviteMenuItem.Click
            If (Me.remoteParticipant Is Nothing) Then
                MessageBox.Show(Me, "peer is not connected", "Error")
                Return
            End If

            Dim dlg As New CreateSessionDialog
            dlg.Text = "ReInvite Session Dialog"

            dlg.sentPeerAddressTextBox.Text = Me.PeerAddress
            dlg.sentPeerAddressTextBox.Enabled = False

            If dlg.ShowDialog = DialogResult.OK Then
                Me.SentContentType = dlg.SentContentType
                Me.SentContentDescription = dlg.SentContentDescription

                ''''
                'Begin Microsoft.Collaboration Functionality Code
                ''''
                Try
                    'Begin REINVITE peer with new content type and description
                    Me.signalingSession.BeginRenegotiateDescription(Me.remoteParticipant, New AsyncCallback(AddressOf ReinviteCallback), Me.signalingSession)
                Catch ecp As Exception
                    MessageBox.Show(Me, "Failed to reinvite " + ecp.ToString(), "Error")
                End Try
                ''''
                'End Microsoft.Collaboration Functionality Code
                ''''
            End If
        End Sub

        Private Sub ReinviteCallback(ByVal result As IAsyncResult)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim session As SignalingSession
            session = result.AsyncState

            Try
                'Ended reinviting peer
                session.EndRenegotiateDescription(result)
            Catch ecp As Exception
                MessageBox.Show(Me, "Failed to reinvite peer " + ecp.ToString(), "Error")
                Me.ListeningState()
            End Try
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub TerminateSession_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles terminateSessionMenuItem.Click
            Me.ListeningState()
        End Sub

        Private Sub RemindListeningState()
            Dim Message As String = "Session with " + Me.PeerAddress + " was terminated."
            Dim Caption As String = "Session Terminated"

            Me.ListeningState()
            MessageBox.Show(Me, Message, Caption)
        End Sub

        Private Sub ListeningState()
            If Not (Me.signalingSession Is Nothing) Then
                Try
                    Me.signalingSession.Leave()
                    Me.signalingSession = Nothing
                Catch
                End Try
            End If

            Me.PeerAddress = Nothing
            Me.RcvdContentType = Nothing
            Me.RcvdContentDescription = Nothing

            Me.PeerAddress = Nothing
            Me.SentContentType = Nothing
            Me.SentContentDescription = Nothing

            Me.createSessionMenuItem.Enabled = True
            Me.reinviteMenuItem.Enabled = False
            Me.terminateSessionMenuItem.Enabled = False

            Me.StatusBarPanel.Text = "Listening for incoming session..."
        End Sub

        Private Sub ConnectingState()
            Me.createSessionMenuItem.Enabled = False
            Me.reinviteMenuItem.Enabled = False
            Me.terminateSessionMenuItem.Enabled = True

            Me.StatusBarPanel.Text = "Session Connecting..."
        End Sub

        Private Sub ConnectedState()
            Me.createSessionMenuItem.Enabled = False
            Me.reinviteMenuItem.Enabled = True
            Me.terminateSessionMenuItem.Enabled = True

            Me.StatusBarPanel.Text = "Session Connected..."
        End Sub

        Private Sub InitSession(ByVal session As System.Collaboration.SignalingSession)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            session.MediaNegotiation = Me
            'Add participant state change handler to each participan
            'if we receive an incoming session, the session will have one or more
            'participants.
            For Each participant As SignalingParticipant In session.Participants
                AddHandler participant.StateChanged, AddressOf Me.OnParticipantStateChanged
            Next

            'Add new participan handler
            AddHandler session.Participants.ItemAdded, AddressOf Me.OnNewParticipant
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub OnNewParticipant(ByVal sender As Object, ByVal e As CollectionEventArgs)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim p As SignalingParticipant
            p = e.Target
            If (Not p Is Nothing) Then
                OnParticipantStateChanged(p, EventArgs.Empty)
                'Add participant state change handler to each participan
                AddHandler p.StateChanged, AddressOf Me.OnParticipantStateChanged
            End If
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private Sub OnParticipantStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim p As SignalingParticipant = sender
            If (Not p Is Me.signalingSession.LocalParticipant) Then
                'Save peer participan for renegotiate content type and description
                If (p.State = ParticipantState.Connected) Then
                    Me.remoteParticipant = p
                Else
                    Me.remoteParticipant = Nothing
                End If
            End If

            'Sessoin state depends only on LocalParticipant state
            'we are interested in only LocalParticipant state
            If (Me.signalingSession.LocalParticipant.State = ParticipantState.Connected) Then
                'Session connected
                Me.ConnectedState()
            ElseIf (Me.signalingSession.LocalParticipant.State = ParticipantState.Disconnected) Then
                Me.RemindListeningState()
            End If
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        'The Micorsoft Collaboration Provider will call this method to get
        'Media Description when this application accepts an incoming 
        'session. The Provider will then send this Media Description to the invitor.
        Public Function GetMediaAnswer(ByVal participant As System.Collaboration.SignalingParticipant, ByVal descriptionRequest As System.Collaboration.MediaDescription) As System.Collaboration.MediaDescription Implements System.Collaboration.IMediaNegotiation.GetMediaAnswer
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            Dim description As CustomMediaDescription

            'Display the received Media Description to the User
            If (TypeOf descriptionRequest Is CustomMediaDescription) Then
                description = descriptionRequest
                Me.RcvdContentType = description.DescriptionType
                Me.RcvdContentDescription = description.Description
            Else
                Me.RcvdContentType = "Received WRONG Media Answer"
                Me.RcvdContentDescription = descriptionRequest.GetType().ToString
            End If

            Dim dlg As New CreateSessionDialog
            dlg.Text = "Session Information to Send"
            dlg.sentPeerAddressTextBox.Text = Me.PeerAddress
            dlg.sentPeerAddressTextBox.Enabled = False

            If dlg.ShowDialog = DialogResult.OK Then
                'Set the Media Description that the User wants to send back
                description = New CustomMediaDescription
                description.DescriptionType = dlg.SentContentType
                description.Description = dlg.SentContentDescription

                Me.SentContentType = dlg.SentContentType
                Me.SentContentDescription = dlg.SentContentDescription

                Return description
            Else
                'The User choose to reject the Media Description.
                'The session itself is still up
                Return Nothing
            End If
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Function

        'The Micorsoft Collaboration Provider will call this method to get
        'Media Description when this application originates a session 
        'and invites a participant. The Provider will then send this returned
        'Media Description to the invitee.
        Public Function GetMediaOffer(ByVal participant As System.Collaboration.SignalingParticipant) As System.Collaboration.MediaDescription Implements System.Collaboration.IMediaNegotiation.GetMediaOffer
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            'Return media description based on what the User has input when
            'he chooses to create the session.
            Dim description As CustomMediaDescription
            description = New CustomMediaDescription
            description.DescriptionType = Me.SentContentType
            description.Description = Me.SentContentDescription
            Return description
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Function

        'The Micorsoft Collaboration Provider will call this method to set
        'Media Description after the Provider gets the SDP description from
        'the invitee
        Public Sub SetMediaAnswer(ByVal participant As System.Collaboration.SignalingParticipant, ByVal descriptionResponse As System.Collaboration.MediaDescription) Implements System.Collaboration.IMediaNegotiation.SetMediaAnswer
            ''''
            'Begin Microsoft.Collaboration Functionality Code
            ''''
            If (descriptionResponse Is Nothing) Then
                'The NULL object means the peer rejects the Media Description we sent
                Me.RcvdContentType = "Media Description was rejected"
                Me.RcvdContentDescription = ""
            ElseIf (TypeOf descriptionResponse Is CustomMediaDescription) Then
                'Display the answer we received from the peer
                Dim description As CustomMediaDescription
                description = descriptionResponse
                Me.RcvdContentType = description.DescriptionType
                Me.RcvdContentDescription = description.Description
            Else
                'We expect only CustomMediaDescription
                Me.RcvdContentType = "Received WRONG Media Answer"
                Me.RcvdContentDescription = descriptionResponse.GetType().ToString
            End If
            ''''
            'End Microsoft.Collaboration Functionality Code
            ''''
        End Sub

        Private profileProvider As RtcProvider
        Private myProfile As RtcPeerToPeerProfile
        Private signalingSession As signalingSession
        Private isOriginator As Boolean
        Private remoteParticipant As SignalingParticipant
    End Class
End Namespace
