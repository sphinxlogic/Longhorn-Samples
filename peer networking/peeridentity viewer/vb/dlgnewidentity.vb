'=====================================================================
'  File:      DlgNewIdentityInfo.vb
'
'  Summary:   Dialog to create a new PeerIdentity.
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

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Net.PeerToPeer
Imports System.Windows.Forms


Public Class DlgNewIdentity
    Inherits System.Windows.Forms.Form

    Private identityValue As PeerIdentity = Nothing

    Private textBoxName As System.Windows.Forms.TextBox
    Private textBoxClassifier As System.Windows.Forms.TextBox
    Private WithEvents buttonOk As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private WithEvents labelName As System.Windows.Forms.Label
    Private WithEvents labelClassifier As System.Windows.Forms.Label

    Private components As System.ComponentModel.Container = Nothing

    ' Constructor to create the dialog.
    '
    Public Sub New()
        InitializeComponent()

        ' Default the name to the user's login name.
        textBoxName.Text = System.Environment.UserName

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
        Me.labelName = New System.Windows.Forms.Label
        Me.textBoxName = New System.Windows.Forms.TextBox
        Me.textBoxClassifier = New System.Windows.Forms.TextBox
        Me.labelClassifier = New System.Windows.Forms.Label
        Me.buttonOk = New System.Windows.Forms.Button
        Me.buttonCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'labelName
        '
        Me.labelName.Location = New System.Drawing.Point(16, 16)
        Me.labelName.Name = "labelName"
        Me.labelName.Size = New System.Drawing.Size(48, 16)
        Me.labelName.TabIndex = 0
        Me.labelName.Text = "Name:"
        '
        'textBoxName
        '
        Me.textBoxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxName.Location = New System.Drawing.Point(72, 16)
        Me.textBoxName.Name = "textBoxName"
        Me.textBoxName.Size = New System.Drawing.Size(204, 19)
        Me.textBoxName.TabIndex = 1
        '
        'textBoxClassifier
        '
        Me.textBoxClassifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxClassifier.Location = New System.Drawing.Point(72, 56)
        Me.textBoxClassifier.Name = "textBoxClassifier"
        Me.textBoxClassifier.Size = New System.Drawing.Size(204, 19)
        Me.textBoxClassifier.TabIndex = 3
        '
        'labelClassifier
        '
        Me.labelClassifier.Location = New System.Drawing.Point(16, 56)
        Me.labelClassifier.Name = "labelClassifier"
        Me.labelClassifier.Size = New System.Drawing.Size(56, 16)
        Me.labelClassifier.TabIndex = 2
        Me.labelClassifier.Text = "Classifier:"
        '
        'buttonOk
        '
        Me.buttonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonOk.Location = New System.Drawing.Point(71, 96)
        Me.buttonOk.Name = "buttonOk"
        Me.buttonOk.TabIndex = 4
        Me.buttonOk.Text = "OK"
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(159, 96)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.TabIndex = 5
        Me.buttonCancel.Text = "Cancel"
        '
        'DlgNewIdentity
        '
        Me.AcceptButton = Me.buttonOk
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(292, 133)
        Me.ControlBox = False
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOk)
        Me.Controls.Add(Me.textBoxName)
        Me.Controls.Add(Me.labelName)
        Me.Controls.Add(Me.textBoxClassifier)
        Me.Controls.Add(Me.labelClassifier)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "DlgNewIdentity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create New Identity"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub 'InitializeComponent
#End Region

    ' The PeerIdentity that was imported.
    '
    Public ReadOnly Property Identity() As PeerIdentity
        Get
            Return identityValue
        End Get
    End Property

    ' Validate the data in the dialog and create the Identity.
    '
    Private Sub buttonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOk.Click
        If CreateIdentity() Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub 'buttonOk_Click

    ' Try to create the identity.
    '
    Private Function CreateIdentity() As Boolean
        ' Validate the classifier
        textBoxClassifier.Text = textBoxClassifier.Text.Trim()
        If textBoxClassifier.TextLength >= 150 Then
            MessageBox.Show(Me, "The classifier must be less than 150 characters", "Invalid Classifier", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' Validate the friendly name
        textBoxName.Text = textBoxName.Text.Trim()
        If textBoxName.TextLength >= 256 Then
            MessageBox.Show(Me, "Identity friendly must be less than 256 characters", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Try
            identityValue = New PeerIdentity(textBoxName.Text, textBoxClassifier.Text)
            Return True
        Catch e As Exception
            Utilities.DisplayException(e, Me)
            Return False
        End Try

    End Function 'CreateIdentity

    ' Close the dialog without creating a new identity.
    '
    Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
        Me.Close()
    End Sub 'buttonCancel_Click

End Class 'DlgNewIdentity