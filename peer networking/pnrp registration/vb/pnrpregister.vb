'=====================================================================
' File:      PnrpRegister.vb
'
' Summary:   Application to register information with PNRP
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

    Private registration As PnrpEndPointRegistration

    Private WithEvents buttonRegister As System.Windows.Forms.Button
    Private groupBoxPeerName As System.Windows.Forms.GroupBox
    Private textBoxPeerName As System.Windows.Forms.TextBox
    Private WithEvents checkBoxSecure As System.Windows.Forms.CheckBox

    Private groupBoxEndPoints As System.Windows.Forms.GroupBox
    Private WithEvents checkBoxAddress1 As System.Windows.Forms.CheckBox
    Private textBoxAddress1 As System.Windows.Forms.TextBox
    Private WithEvents checkBoxAddress2 As System.Windows.Forms.CheckBox
    Private textBoxAddress2 As System.Windows.Forms.TextBox
    Private textBoxAddress3 As System.Windows.Forms.TextBox
    Private WithEvents checkBoxAddress3 As System.Windows.Forms.CheckBox
    Private textBoxAddress4 As System.Windows.Forms.TextBox
    Private WithEvents checkBoxAddress4 As System.Windows.Forms.CheckBox

    Private groupBoxProperties As System.Windows.Forms.GroupBox
    Private labelComment As System.Windows.Forms.Label
    Private textBoxComment As System.Windows.Forms.TextBox
    Private labelCloud As System.Windows.Forms.Label
    Private dropListCloud As System.Windows.Forms.ComboBox
    Private labelIdentity As System.Windows.Forms.Label
    Private WithEvents dropListIdentity As System.Windows.Forms.ComboBox
    Private labelClassifier As System.Windows.Forms.Label
    Private WithEvents textBoxClassifier As System.Windows.Forms.TextBox

    Private components As System.ComponentModel.Container = Nothing


    ' Constructor for the form.
    '
    Public Sub New()
        InitializeComponent()


        ' Create the object that will be registered.
        registration = New PnrpEndPointRegistration

        ' Initialize the lists.
        InitializeCloudList()
        InitializeIdentityList()
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
        Me.buttonRegister = New System.Windows.Forms.Button
        Me.groupBoxEndPoints = New System.Windows.Forms.GroupBox
        Me.textBoxAddress1 = New System.Windows.Forms.TextBox
        Me.checkBoxAddress1 = New System.Windows.Forms.CheckBox
        Me.textBoxAddress2 = New System.Windows.Forms.TextBox
        Me.checkBoxAddress2 = New System.Windows.Forms.CheckBox
        Me.textBoxAddress3 = New System.Windows.Forms.TextBox
        Me.checkBoxAddress3 = New System.Windows.Forms.CheckBox
        Me.textBoxAddress4 = New System.Windows.Forms.TextBox
        Me.checkBoxAddress4 = New System.Windows.Forms.CheckBox
        Me.groupBoxPeerName = New System.Windows.Forms.GroupBox
        Me.checkBoxSecure = New System.Windows.Forms.CheckBox
        Me.textBoxPeerName = New System.Windows.Forms.TextBox
        Me.groupBoxProperties = New System.Windows.Forms.GroupBox
        Me.textBoxComment = New System.Windows.Forms.TextBox
        Me.labelComment = New System.Windows.Forms.Label
        Me.labelCloud = New System.Windows.Forms.Label
        Me.dropListCloud = New System.Windows.Forms.ComboBox
        Me.dropListIdentity = New System.Windows.Forms.ComboBox
        Me.labelIdentity = New System.Windows.Forms.Label
        Me.textBoxClassifier = New System.Windows.Forms.TextBox
        Me.labelClassifier = New System.Windows.Forms.Label
        Me.groupBoxEndPoints.SuspendLayout()
        Me.groupBoxPeerName.SuspendLayout()
        Me.groupBoxProperties.SuspendLayout()
        Me.SuspendLayout()

        ' 
        ' buttonRegister
        ' 
        Me.buttonRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonRegister.Location = New System.Drawing.Point(124, 376)
        Me.buttonRegister.Name = "buttonRegister"
        Me.buttonRegister.TabIndex = 3
        Me.buttonRegister.Text = "&Register"

        ' 
        ' groupBoxEndPoints
        ' 
        Me.groupBoxEndPoints.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.groupBoxEndPoints.Controls.Add(Me.textBoxAddress1)
        Me.groupBoxEndPoints.Controls.Add(Me.checkBoxAddress1)
        Me.groupBoxEndPoints.Controls.Add(Me.textBoxAddress2)
        Me.groupBoxEndPoints.Controls.Add(Me.checkBoxAddress2)
        Me.groupBoxEndPoints.Controls.Add(Me.textBoxAddress3)
        Me.groupBoxEndPoints.Controls.Add(Me.checkBoxAddress3)
        Me.groupBoxEndPoints.Controls.Add(Me.textBoxAddress4)
        Me.groupBoxEndPoints.Controls.Add(Me.checkBoxAddress4)
        Me.groupBoxEndPoints.Location = New System.Drawing.Point(8, 248)
        Me.groupBoxEndPoints.Name = "groupBoxEndPoints"
        Me.groupBoxEndPoints.Size = New System.Drawing.Size(312, 118)
        Me.groupBoxEndPoints.TabIndex = 2
        Me.groupBoxEndPoints.TabStop = False
        Me.groupBoxEndPoints.Text = "IPEndPoints"

        ' 
        ' textBoxAddress1
        ' 
        Me.textBoxAddress1.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxAddress1.Location = New System.Drawing.Point(95, 20)
        Me.textBoxAddress1.MaxLength = 100
        Me.textBoxAddress1.Name = "textBoxAddress1"
        Me.textBoxAddress1.Size = New System.Drawing.Size(203, 19)
        Me.textBoxAddress1.TabIndex = 1
        Me.textBoxAddress1.Text = "0.0.0.0"

        ' 
        ' checkBoxAddress1
        ' 
        Me.checkBoxAddress1.Checked = True
        Me.checkBoxAddress1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkBoxAddress1.Location = New System.Drawing.Point(8, 20)
        Me.checkBoxAddress1.Name = "checkBoxAddress1"
        Me.checkBoxAddress1.Size = New System.Drawing.Size(87, 17)
        Me.checkBoxAddress1.TabIndex = 0
        Me.checkBoxAddress1.Text = "Address &1:"

        ' 
        ' textBoxAddress2
        ' 
        Me.textBoxAddress2.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxAddress2.Enabled = False
        Me.textBoxAddress2.Location = New System.Drawing.Point(95, 42)
        Me.textBoxAddress2.MaxLength = 100
        Me.textBoxAddress2.Name = "textBoxAddress2"
        Me.textBoxAddress2.Size = New System.Drawing.Size(203, 19)
        Me.textBoxAddress2.TabIndex = 3

        ' 
        ' checkBoxAddress2
        ' 
        Me.checkBoxAddress2.Location = New System.Drawing.Point(8, 42)
        Me.checkBoxAddress2.Name = "checkBoxAddress2"
        Me.checkBoxAddress2.Size = New System.Drawing.Size(87, 17)
        Me.checkBoxAddress2.TabIndex = 2
        Me.checkBoxAddress2.Text = "Address &2:"

        ' 
        ' textBoxAddress3
        ' 
        Me.textBoxAddress3.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxAddress3.Enabled = False
        Me.textBoxAddress3.Location = New System.Drawing.Point(95, 64)
        Me.textBoxAddress3.MaxLength = 100
        Me.textBoxAddress3.Name = "textBoxAddress3"
        Me.textBoxAddress3.Size = New System.Drawing.Size(203, 19)
        Me.textBoxAddress3.TabIndex = 5

        ' 
        ' checkBoxAddress3
        ' 
        Me.checkBoxAddress3.Location = New System.Drawing.Point(8, 64)
        Me.checkBoxAddress3.Name = "checkBoxAddress3"
        Me.checkBoxAddress3.Size = New System.Drawing.Size(87, 17)
        Me.checkBoxAddress3.TabIndex = 4
        Me.checkBoxAddress3.Text = "Address &3:"

        ' 
        ' textBoxAddress4
        ' 
        Me.textBoxAddress4.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxAddress4.Enabled = False
        Me.textBoxAddress4.Location = New System.Drawing.Point(95, 86)
        Me.textBoxAddress4.MaxLength = 100
        Me.textBoxAddress4.Name = "textBoxAddress4"
        Me.textBoxAddress4.Size = New System.Drawing.Size(203, 19)
        Me.textBoxAddress4.TabIndex = 7

        ' 
        ' checkBoxAddress4
        ' 
        Me.checkBoxAddress4.Location = New System.Drawing.Point(8, 86)
        Me.checkBoxAddress4.Name = "checkBoxAddress4"
        Me.checkBoxAddress4.Size = New System.Drawing.Size(87, 17)
        Me.checkBoxAddress4.TabIndex = 6
        Me.checkBoxAddress4.Text = "Address &4:"

        ' 
        ' groupBoxPeerName
        ' 
        Me.groupBoxPeerName.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.groupBoxPeerName.Controls.Add(Me.checkBoxSecure)
        Me.groupBoxPeerName.Controls.Add(Me.textBoxPeerName)
        Me.groupBoxPeerName.Location = New System.Drawing.Point(8, 8)
        Me.groupBoxPeerName.Name = "groupBoxPeerName"
        Me.groupBoxPeerName.Size = New System.Drawing.Size(310, 71)
        Me.groupBoxPeerName.TabIndex = 0
        Me.groupBoxPeerName.TabStop = False
        Me.groupBoxPeerName.Text = "PeerName"

        ' 
        ' checkBoxSecure
        ' 
        Me.checkBoxSecure.Location = New System.Drawing.Point(11, 45)
        Me.checkBoxSecure.Name = "checkBoxSecure"
        Me.checkBoxSecure.Size = New System.Drawing.Size(68, 16)
        Me.checkBoxSecure.TabIndex = 1
        Me.checkBoxSecure.Text = "&Secure"

        ' 
        ' textBoxPeerName
        ' 
        Me.textBoxPeerName.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxPeerName.Location = New System.Drawing.Point(11, 18)
        Me.textBoxPeerName.MaxLength = 39
        Me.textBoxPeerName.Name = "textBoxPeerName"
        Me.textBoxPeerName.ReadOnly = True
        Me.textBoxPeerName.Size = New System.Drawing.Size(289, 19)
        Me.textBoxPeerName.TabIndex = 0
        Me.textBoxPeerName.TabStop = False

        ' 
        ' groupBoxProperties
        ' 
        Me.groupBoxProperties.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.groupBoxProperties.Controls.Add(Me.textBoxComment)
        Me.groupBoxProperties.Controls.Add(Me.labelComment)
        Me.groupBoxProperties.Controls.Add(Me.labelCloud)
        Me.groupBoxProperties.Controls.Add(Me.dropListCloud)
        Me.groupBoxProperties.Controls.Add(Me.dropListIdentity)
        Me.groupBoxProperties.Controls.Add(Me.labelIdentity)
        Me.groupBoxProperties.Controls.Add(Me.textBoxClassifier)
        Me.groupBoxProperties.Controls.Add(Me.labelClassifier)
        Me.groupBoxProperties.Location = New System.Drawing.Point(8, 89)
        Me.groupBoxProperties.Name = "groupBoxProperties"
        Me.groupBoxProperties.Size = New System.Drawing.Size(312, 143)
        Me.groupBoxProperties.TabIndex = 1
        Me.groupBoxProperties.TabStop = False
        Me.groupBoxProperties.Text = "Properties"

        ' 
        ' textBoxComment
        ' 
        Me.textBoxComment.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxComment.Location = New System.Drawing.Point(81, 80)
        Me.textBoxComment.MaxLength = 39
        Me.textBoxComment.Name = "textBoxComment"
        Me.textBoxComment.Size = New System.Drawing.Size(219, 19)
        Me.textBoxComment.TabIndex = 5

        ' 
        ' labelComment
        ' 
        Me.labelComment.Location = New System.Drawing.Point(8, 83)
        Me.labelComment.Name = "labelComment"
        Me.labelComment.Size = New System.Drawing.Size(53, 16)
        Me.labelComment.TabIndex = 4
        Me.labelComment.Text = "C&omment:"

        ' 
        ' labelCloud
        ' 
        Me.labelCloud.Location = New System.Drawing.Point(8, 116)
        Me.labelCloud.Name = "labelCloud"
        Me.labelCloud.Size = New System.Drawing.Size(53, 16)
        Me.labelCloud.TabIndex = 6
        Me.labelCloud.Text = "&Cloud:"

        ' 
        ' dropListCloud
        ' 
        Me.dropListCloud.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListCloud.Location = New System.Drawing.Point(81, 111)
        Me.dropListCloud.Name = "dropListCloud"
        Me.dropListCloud.Size = New System.Drawing.Size(219, 21)
        Me.dropListCloud.Sorted = True
        Me.dropListCloud.TabIndex = 7

        ' 
        ' dropListIdentity
        ' 
        Me.dropListIdentity.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListIdentity.Location = New System.Drawing.Point(81, 17)
        Me.dropListIdentity.Name = "dropListIdentity"
        Me.dropListIdentity.Size = New System.Drawing.Size(219, 21)
        Me.dropListIdentity.Sorted = True
        Me.dropListIdentity.TabIndex = 1

        ' 
        ' labelIdentity
        ' 
        Me.labelIdentity.Location = New System.Drawing.Point(8, 22)
        Me.labelIdentity.Name = "labelIdentity"
        Me.labelIdentity.Size = New System.Drawing.Size(53, 16)
        Me.labelIdentity.TabIndex = 0
        Me.labelIdentity.Text = "&Identity:"

        ' 
        ' textBoxClassifier
        ' 
        Me.textBoxClassifier.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.textBoxClassifier.Location = New System.Drawing.Point(81, 50)
        Me.textBoxClassifier.MaxLength = 139
        Me.textBoxClassifier.Name = "textBoxClassifier"
        Me.textBoxClassifier.Size = New System.Drawing.Size(219, 19)
        Me.textBoxClassifier.TabIndex = 3
        Me.textBoxClassifier.Text = "0"

        ' 
        ' labelClassifier
        ' 
        Me.labelClassifier.Location = New System.Drawing.Point(8, 53)
        Me.labelClassifier.Name = "labelClassifier"
        Me.labelClassifier.Size = New System.Drawing.Size(53, 16)
        Me.labelClassifier.TabIndex = 2
        Me.labelClassifier.Text = "C&lassifier:"

        ' 
        ' FormMain
        ' 
        Me.AcceptButton = Me.buttonRegister
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(323, 407)
        Me.Controls.Add(groupBoxProperties)
        Me.Controls.Add(groupBoxPeerName)
        Me.Controls.Add(groupBoxEndPoints)
        Me.Controls.Add(buttonRegister)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(999, 434)
        Me.MinimumSize = New System.Drawing.Size(331, 434)
        Me.Name = "FormMain"
        Me.Text = "PNRP Registration"
        Me.groupBoxEndPoints.ResumeLayout(False)
        Me.groupBoxEndPoints.PerformLayout()
        Me.groupBoxPeerName.ResumeLayout(False)
        Me.groupBoxPeerName.PerformLayout()
        Me.groupBoxProperties.ResumeLayout(False)
        Me.groupBoxProperties.PerformLayout()
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

    ' Initialize the cloud drop list.
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

    ' Initialize the Identity drop down list.
    '
    Public Sub InitializeIdentityList()
        Dim identities As List(Of PeerIdentity)
        identities = PeerIdentity.GetIdentities()

        Dim identity As PeerIdentity
        For Each identity In identities
            dropListIdentity.Items.Add(identity)
        Next identity

        ' Ensure an item is selected, if possible.
        If identities.Count > 0 Then
            dropListIdentity.SelectedIndex = 0
        End If

    End Sub 'InitializeIdentityList

    ' Get the PeerName to register.
    '
    Private ReadOnly Property SelectedPeerName() As PeerName
        Get
            ' Try to create a PeerName from the text in the control.
            Return New PeerName(textBoxPeerName.Text)
        End Get
    End Property

    ' Retrieve the selected identity.
    '
    Public ReadOnly Property SelectedIdentity() As PeerIdentity
        Get
            Return CType(dropListIdentity.SelectedItem, PeerIdentity)
        End Get
    End Property

    ' Retrieve the selected cloud.
    '
    Public ReadOnly Property SelectedCloud() As Cloud
        Get
            Return CType(dropListCloud.SelectedItem, Cloud)
        End Get
    End Property

    ' Retrieve the set of IPEndPoints
    '
    Public ReadOnly Property IPEndPoints() As IPEndPoint()
        Get
            Dim data(3) As IPEndPoint
            Dim cItem As Integer = 0

            ' Add each IPEndPoint to the temporary array
            Dim control As Control
            For Each control In groupBoxEndPoints.Controls
                If TypeOf control Is TextBox Then
                    If control.Enabled Then
                        Try
                            Dim address As IPAddress = IPAddress.Parse(control.Text)
                            data(cItem) = New IPEndPoint(address, 0)
                            cItem += 1
                        Catch e As Exception
                            ' Display an error message if there was a problem with the IPEndPoint
                            DisplayException(e)
                        End Try
                    End If
                End If
            Next control

            ' Create a new array with the selected IPEndPoints
            Dim cElements As Integer = cItem - 1
            If cElements < 0 Then cElements = 0
            Dim result(cElements) As IPEndPoint
            Array.Copy(data, result, cItem)
            Return result
        End Get
    End Property

    ' Handle the button press by registering or unregistering the PeerName
    ' and then update the controls.
    '
    Private Sub buttonRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonRegister.Click
        If registration.State = RegistrationState.Registered Then
            Unregister()
        Else
            Register()
        End If

        UpdateControls()
    End Sub 'buttonRegister_Click

    ' For each Address checkbox, enable/disable the assocated text control.
    '
    Private Sub checkBoxAddress1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkBoxAddress1.CheckedChanged
        textBoxAddress1.Enabled = checkBoxAddress1.Checked
    End Sub 'checkBoxAddress1_CheckedChanged

    Private Sub checkBoxAddress2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkBoxAddress2.CheckedChanged
        textBoxAddress2.Enabled = checkBoxAddress2.Checked
    End Sub 'checkBoxAddress2_CheckedChanged

    Private Sub checkBoxAddress3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkBoxAddress3.CheckedChanged
        textBoxAddress3.Enabled = checkBoxAddress3.Checked
    End Sub 'checkBoxAddress3_CheckedChanged

    Private Sub checkBoxAddress4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkBoxAddress4.CheckedChanged
        textBoxAddress4.Enabled = checkBoxAddress4.Checked
    End Sub 'checkBoxAddress4_CheckedChanged

    ' Handle a change in the classifier by updating the PeerName.
    '
    Private Sub textBoxClassifier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles textBoxClassifier.TextChanged
        UpdatePeerName()
    End Sub 'textBoxClassifier_TextChanged

    ' Handle a change in the selected identity by updating the PeerName.
    '
    Private Sub dropListIdentity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropListIdentity.SelectedIndexChanged
        UpdatePeerName()
    End Sub 'dropListIdentity_SelectedIndexChanged

    ' Handle a change in the "Secure" checkbox by updating the PeerName.
    '
    Private Sub checkBoxSecure_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkBoxSecure.CheckedChanged
        UpdatePeerName()
    End Sub 'checkBoxSecure_CheckedChanged

    ' Try to register with the current information.
    '
    Private Sub Register()
        Try
            Dim peerName As PeerName = SelectedPeerName
            Dim endPoint As New PnrpEndPoint(peerName, IPEndPoints, textBoxComment.Text)

            registration.PnrpEndPoint = endPoint
            registration.Cloud = SelectedCloud
            registration.Identity = SelectedIdentity

            registration.Register()
        Catch e As Exception
            DisplayException(e)
        End Try

    End Sub 'Register

    ' Make sure the object is not registered.
    '
    Private Sub Unregister()
        If registration.State = RegistrationState.Registered Then
            registration.Unregister()
        End If

    End Sub 'Unregister

    ' Update the display by enabling or disabling the controls.
    '
    Private Sub UpdateControls()
        ' Enable the controls if we are not registered.
        Dim enable As Boolean = registration.State <> RegistrationState.Registered

        checkBoxSecure.Enabled = enable
        dropListIdentity.Enabled = enable
        dropListCloud.Enabled = enable
        checkBoxAddress1.Enabled = enable
        checkBoxAddress2.Enabled = enable
        checkBoxAddress3.Enabled = enable
        checkBoxAddress4.Enabled = enable
        textBoxAddress1.ReadOnly = Not enable
        textBoxAddress2.ReadOnly = Not enable
        textBoxAddress3.ReadOnly = Not enable
        textBoxAddress4.ReadOnly = Not enable
        textBoxComment.ReadOnly = Not enable
        textBoxClassifier.ReadOnly = Not enable

        ' Update the button text
        If enable Then
            buttonRegister.Text = "&Register"
        Else
            buttonRegister.Text = "Un&register"
        End If

        ' The button is always enabled
        buttonRegister.Enabled = True

    End Sub 'UpdateControls

    ' Update the text in the PeerName control by either creating a
    '
    Private Sub UpdatePeerName()
        Dim peerName As PeerName

        Try
            If checkBoxSecure.Checked Then
                ' Create a secure PeerName based on the selected Identity
                peerName = SelectedIdentity.CreatePeerName(textBoxClassifier.Text)
            Else
                ' Create an insecure PeerName
                peerName = New PeerName("0." & textBoxClassifier.Text)
            End If
            textBoxPeerName.Text = peerName.ToString()
        Catch
            ' Handle all errors by blanking out the text.
            textBoxPeerName.Text = String.Empty
        End Try

    End Sub 'UpdatePeerName

    ' Utility routine to display an exception in a MessageBox.
    '
    Private Sub DisplayException(ByVal e As Exception)
        MessageBox.Show(Me, e.Message & vbCrLf & vbCrLf & e.StackTrace, _
            "Exception from " & e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub 'DisplayException
End Class 'FormMain