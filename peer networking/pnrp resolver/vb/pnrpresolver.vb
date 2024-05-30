'=====================================================================
' File:      PnrpResolver.vb
'
' Summary:   Sample application to resolve a PeerName.
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
Imports System.Net
Imports System.Net.PeerToPeer
Imports System.Windows.Forms

<DefaultInstanceProperty("GetInstance")> Public Class FormMain
    Inherits System.Windows.Forms.Form

    Private resolver As PnrpEndPointResolver

    Private panel As System.Windows.Forms.Panel
    Private labelPeerName As System.Windows.Forms.Label
    Private textBoxPeerName As System.Windows.Forms.TextBox
    Private labelCloud As System.Windows.Forms.Label
    Private dropListCloud As System.Windows.Forms.ComboBox
    Private labelMaxTime As System.Windows.Forms.Label
    Private textBoxTimeout As System.Windows.Forms.NumericUpDown
    Private labelMaxResults As System.Windows.Forms.Label
    Private textBoxMaxResults As System.Windows.Forms.NumericUpDown
    Private labelCriteria As System.Windows.Forms.Label
    Private dropListCriteria As System.Windows.Forms.ComboBox
    Private WithEvents buttonResolve As System.Windows.Forms.Button
    Private textBoxResults As System.Windows.Forms.TextBox

    Private components As System.ComponentModel.Container = Nothing


    ' Constructor for this form.
    '
    Public Sub New()
        InitializeComponent()

        ' Initialize the lists.
        InitializeCloudList()
        InitializeCriteria()
        InitializeResolver()

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

    ' <summary>
    ' Required method for Designer support - do not modify
    ' the contents of this method with the code editor.
    ' </summary>
    Private Sub InitializeComponent()
        Me.panel = New System.Windows.Forms.Panel
        Me.dropListCriteria = New System.Windows.Forms.ComboBox
        Me.labelCriteria = New System.Windows.Forms.Label
        Me.labelMaxTime = New System.Windows.Forms.Label
        Me.dropListCloud = New System.Windows.Forms.ComboBox
        Me.labelPeerName = New System.Windows.Forms.Label
        Me.textBoxPeerName = New System.Windows.Forms.TextBox
        Me.labelMaxResults = New System.Windows.Forms.Label
        Me.textBoxMaxResults = New System.Windows.Forms.NumericUpDown
        Me.textBoxTimeout = New System.Windows.Forms.NumericUpDown
        Me.labelCloud = New System.Windows.Forms.Label
        Me.buttonResolve = New System.Windows.Forms.Button
        Me.textBoxResults = New System.Windows.Forms.TextBox
        Me.panel.SuspendLayout()
        CType(Me.textBoxMaxResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textBoxTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panel
        '
        Me.panel.Controls.Add(Me.dropListCriteria)
        Me.panel.Controls.Add(Me.labelCriteria)
        Me.panel.Controls.Add(Me.labelMaxTime)
        Me.panel.Controls.Add(Me.dropListCloud)
        Me.panel.Controls.Add(Me.labelPeerName)
        Me.panel.Controls.Add(Me.textBoxPeerName)
        Me.panel.Controls.Add(Me.labelMaxResults)
        Me.panel.Controls.Add(Me.textBoxMaxResults)
        Me.panel.Controls.Add(Me.textBoxTimeout)
        Me.panel.Controls.Add(Me.labelCloud)
        Me.panel.Controls.Add(Me.buttonResolve)
        Me.panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel.Location = New System.Drawing.Point(0, 0)
        Me.panel.Name = "panel"
        Me.panel.Size = New System.Drawing.Size(517, 111)
        Me.panel.TabIndex = 0
        '
        'dropListCriteria
        '
        Me.dropListCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListCriteria.Location = New System.Drawing.Point(76, 77)
        Me.dropListCriteria.Name = "dropListCriteria"
        Me.dropListCriteria.Size = New System.Drawing.Size(243, 21)
        Me.dropListCriteria.Sorted = True
        Me.dropListCriteria.TabIndex = 5
        '
        'labelCriteria
        '
        Me.labelCriteria.Location = New System.Drawing.Point(10, 77)
        Me.labelCriteria.Name = "labelCriteria"
        Me.labelCriteria.Size = New System.Drawing.Size(54, 16)
        Me.labelCriteria.TabIndex = 4
        Me.labelCriteria.Text = "Cr&iteria"
        '
        'labelMaxTime
        '
        Me.labelMaxTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelMaxTime.Location = New System.Drawing.Point(329, 46)
        Me.labelMaxTime.Name = "labelMaxTime"
        Me.labelMaxTime.Size = New System.Drawing.Size(94, 16)
        Me.labelMaxTime.TabIndex = 8
        Me.labelMaxTime.Text = "Maximum &Time:"
        '
        'dropListCloud
        '
        Me.dropListCloud.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListCloud.Location = New System.Drawing.Point(76, 46)
        Me.dropListCloud.Name = "dropListCloud"
        Me.dropListCloud.Size = New System.Drawing.Size(243, 21)
        Me.dropListCloud.Sorted = True
        Me.dropListCloud.TabIndex = 3
        '
        'labelPeerName
        '
        Me.labelPeerName.Location = New System.Drawing.Point(10, 20)
        Me.labelPeerName.Name = "labelPeerName"
        Me.labelPeerName.Size = New System.Drawing.Size(61, 16)
        Me.labelPeerName.TabIndex = 0
        Me.labelPeerName.Text = "&PeerName:"
        '
        'textBoxPeerName
        '
        Me.textBoxPeerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxPeerName.Location = New System.Drawing.Point(76, 17)
        Me.textBoxPeerName.MaxLength = 255
        Me.textBoxPeerName.Name = "textBoxPeerName"
        Me.textBoxPeerName.Size = New System.Drawing.Size(243, 19)
        Me.textBoxPeerName.TabIndex = 1
        Me.textBoxPeerName.Text = "0.0"
        '
        'labelMaxResults
        '
        Me.labelMaxResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelMaxResults.Location = New System.Drawing.Point(329, 16)
        Me.labelMaxResults.Name = "labelMaxResults"
        Me.labelMaxResults.Size = New System.Drawing.Size(98, 16)
        Me.labelMaxResults.TabIndex = 6
        Me.labelMaxResults.Text = "Maximum &Results:"
        '
        'textBoxMaxResults
        '
        Me.textBoxMaxResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxMaxResults.Location = New System.Drawing.Point(429, 16)
        Me.textBoxMaxResults.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.textBoxMaxResults.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.textBoxMaxResults.Name = "textBoxMaxResults"
        Me.textBoxMaxResults.Size = New System.Drawing.Size(75, 20)
        Me.textBoxMaxResults.TabIndex = 7
        Me.textBoxMaxResults.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'textBoxTimeout
        '
        Me.textBoxTimeout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxTimeout.Location = New System.Drawing.Point(429, 46)
        Me.textBoxTimeout.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.textBoxTimeout.Name = "textBoxTimeout"
        Me.textBoxTimeout.Size = New System.Drawing.Size(75, 20)
        Me.textBoxTimeout.TabIndex = 9
        Me.textBoxTimeout.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'labelCloud
        '
        Me.labelCloud.Location = New System.Drawing.Point(10, 46)
        Me.labelCloud.Name = "labelCloud"
        Me.labelCloud.Size = New System.Drawing.Size(38, 16)
        Me.labelCloud.TabIndex = 2
        Me.labelCloud.Text = "&Cloud:"
        '
        'buttonResolve
        '
        Me.buttonResolve.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonResolve.Location = New System.Drawing.Point(429, 77)
        Me.buttonResolve.Name = "buttonResolve"
        Me.buttonResolve.TabIndex = 10
        Me.buttonResolve.Text = "Resolve"
        '
        'textBoxResults
        '
        Me.textBoxResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.textBoxResults.Location = New System.Drawing.Point(0, 111)
        Me.textBoxResults.Multiline = True
        Me.textBoxResults.Name = "textBoxResults"
        Me.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxResults.Size = New System.Drawing.Size(517, 200)
        Me.textBoxResults.TabIndex = 1
        Me.textBoxResults.TabStop = False
        '
        'FormMain
        '
        Me.AcceptButton = Me.buttonResolve
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(517, 311)
        Me.Controls.Add(Me.textBoxResults)
        Me.Controls.Add(Me.panel)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "FormMain"
        Me.Text = "PNRP Resolver"
        Me.panel.ResumeLayout(False)
        Me.panel.PerformLayout()
        CType(Me.textBoxMaxResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textBoxTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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


    ' Initialize the Cloud drop down list.
    '
    Private Sub InitializeCloudList()
        Dim clouds As List(Of Cloud)
        clouds = CloudWatcher.GetClouds()

        Dim cloud As Cloud
        For Each cloud In clouds
            dropListCloud.Items.Add(cloud)
        Next cloud

        ' Always select the first one (Global)
        dropListCloud.SelectedIndex = 0

    End Sub 'InitializeCloudList

    ' Initialize the ResolutionCriteria drop down list.
    '
    Private Sub InitializeCriteria()
        Dim obj As [Object]
        For Each obj In [Enum].GetValues(GetType(ResolutionCriteria))
            dropListCriteria.Items.Add(obj)
        Next obj

        dropListCriteria.SelectedIndex = 0
    End Sub 'InitializeCriteria

    ' Initialize the global resolver object and associated callbacks.
    '
    Private Sub InitializeResolver()
        resolver = New PnrpEndPointResolver
        resolver.SynchronizingObject = Me
        AddHandler resolver.PeerNameFound, AddressOf OnPeerNameFound
        AddHandler resolver.ResolutionCompleted, AddressOf OnResolutionCompleted
    End Sub 'InitializeResolver

    ' Retrieve the currently selected cloud.
    '
    Public ReadOnly Property SelectedCloud() As Cloud
        Get
            Return CType(dropListCloud.SelectedItem, Cloud)
        End Get
    End Property

    ' Handle the button click to start or stop the resolution process.
    '
    Private Sub buttonResolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonResolve.Click
        If resolver.Resolving Then
            StopResolve()
        Else
            StartResolve()
        End If

        UpdateControls()

    End Sub 'buttonResolve_Click

    ' Stop the resolve process.
    '
    Private Sub StopResolve()
        If resolver.Resolving Then
            resolver.EndResolve()
        End If
    End Sub 'StopResolve

    ' Start the resolve process.
    '
    Private Sub StartResolve()
        ClearMessages()

        Try
            ' Retrieve the settings and fill out the resolver properties.
            Dim peerName As New PeerName(textBoxPeerName.Text)
            resolver.PeerName = peerName
            resolver.Cloud = SelectedCloud
            resolver.MaxResults = CType(Fix(textBoxMaxResults.Value), Integer)
            resolver.TimeOut = New TimeSpan(0, 0, CType(Fix(textBoxTimeout.Value), Integer))
            resolver.ResolutionCriteria = CType(dropListCriteria.SelectedItem, ResolutionCriteria)

            DisplayMessage("Resolving for " & peerName.ToString())
            resolver.BeginResolve()
        Catch e As Exception
            ' Display all exceptions.
            MessageBox.Show(Me, e.Message & vbCrLf & vbCrLf & e.StackTrace, _
                "Exception from " & e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub 'StartResolve

    ' Enable (or disable) the controls.
    '
    Private Sub UpdateControls()
        ' Disable the controls if we are resolving
        Dim resolving As Boolean = resolver.Resolving

        Dim control As Control
        For Each control In panel.Controls
            If Not TypeOf control Is Label Then
                If Not resolving Then
                    If TypeOf control.Tag Is Boolean Then
                        ' Restore the previous state
                        control.Enabled = CBool(control.Tag)
                    End If
                Else
                    ' Save the previous state
                    control.Tag = control.Enabled
                    control.Enabled = False
                End If
            End If
        Next control

        ' Update the button (always enabled)
        If (resolving) Then
            buttonResolve.Text = "Stop"
        Else
            buttonResolve.Text = "Resolve"
        End If
        buttonResolve.Enabled = True

    End Sub 'UpdateControls

    ' Handle the event of finding a result.
    '
    Private Sub OnPeerNameFound(ByVal sender As Object, ByVal e As PeerNameFoundEventArgs)
        DisplayMessage("")
        DisplayMessage("Found result with comment='" & e.PnrpEndPoint.Comment & "'")

        ' Display each address
        Dim ipEndPoints As IPEndPoint() = e.PnrpEndPoint.IPEndPoints
        Dim i As Integer
        For i = 1 To ipEndPoints.Length
            DisplayMessage("  IPEndPoint " & i.ToString() & ": " & e.PnrpEndPoint.IPEndPoints(i - 1).ToString())
        Next i

    End Sub 'OnPeerNameFound

    ' Handle the event that is raised when the resolve is complete.
    '
    Private Sub OnResolutionCompleted(ByVal sender As Object, ByVal e As ResolutionCompletedEventArgs)
        DisplayMessage("")
        DisplayMessage("Resolve Completed: " & e.Reason.ToString())

        UpdateControls()
    End Sub 'OnResolutionCompleted

    ' Display a message in the text control.
    '
    Private Sub DisplayMessage(ByVal message As String)
        textBoxResults.AppendText(message)
        textBoxResults.AppendText(vbCrLf)
    End Sub 'DisplayMessage

    ' Clear the message area.
    '
    Private Sub ClearMessages()
        textBoxResults.Clear()
    End Sub 'ClearMessages

End Class 'FormMain