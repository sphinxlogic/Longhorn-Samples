'=====================================================================
'  File:      DlgSaveIdentityInfo.vb
'
'  Summary:   Dialog to save the information for a PeerIdentity.
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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Net.PeerToPeer

Public Class DlgSaveIdentityInfo
    Inherits System.Windows.Forms.Form

    Private textBoxFile As System.Windows.Forms.TextBox
    Private dropListIdentity As System.Windows.Forms.ComboBox
    Private WithEvents buttonOK As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private WithEvents buttonNew As System.Windows.Forms.Button
    Private WithEvents buttonBrowse As System.Windows.Forms.Button
    Private WithEvents labelIdentity As System.Windows.Forms.Label
    Private WithEvents labelFile As System.Windows.Forms.Label
    Private saveFileDialog As System.Windows.Forms.SaveFileDialog

    Private components As System.ComponentModel.Container = Nothing


    ' Constructor to create a dialog with the selected PeerIdentity.
    '
    Public Sub New(ByVal identity As PeerIdentity)
        InitializeComponent()

        Utilities.FillIdentityComboBox(dropListIdentity, identity)

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
        Me.labelIdentity = New System.Windows.Forms.Label
        Me.buttonOK = New System.Windows.Forms.Button
        Me.buttonCancel = New System.Windows.Forms.Button
        Me.textBoxFile = New System.Windows.Forms.TextBox
        Me.labelFile = New System.Windows.Forms.Label
        Me.buttonNew = New System.Windows.Forms.Button
        Me.dropListIdentity = New System.Windows.Forms.ComboBox
        Me.buttonBrowse = New System.Windows.Forms.Button
        Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'labelIdentity
        '
        Me.labelIdentity.Location = New System.Drawing.Point(11, 19)
        Me.labelIdentity.Name = "labelIdentity"
        Me.labelIdentity.Size = New System.Drawing.Size(50, 13)
        Me.labelIdentity.TabIndex = 0
        Me.labelIdentity.Text = "&Identity:"
        '
        'buttonOK
        '
        Me.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonOK.Location = New System.Drawing.Point(97, 80)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.TabIndex = 6
        Me.buttonOK.Text = "OK"
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(193, 80)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.TabIndex = 7
        Me.buttonCancel.Text = "Cancel"
        '
        'textBoxFile
        '
        Me.textBoxFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxFile.Location = New System.Drawing.Point(61, 47)
        Me.textBoxFile.Name = "textBoxFile"
        Me.textBoxFile.Size = New System.Drawing.Size(219, 19)
        Me.textBoxFile.TabIndex = 4
        '
        'labelFile
        '
        Me.labelFile.Location = New System.Drawing.Point(10, 47)
        Me.labelFile.Name = "labelFile"
        Me.labelFile.Size = New System.Drawing.Size(50, 13)
        Me.labelFile.TabIndex = 3
        Me.labelFile.Text = "&File:"
        '
        'buttonNew
        '
        Me.buttonNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonNew.Location = New System.Drawing.Point(292, 19)
        Me.buttonNew.Name = "buttonNew"
        Me.buttonNew.Size = New System.Drawing.Size(60, 23)
        Me.buttonNew.TabIndex = 2
        Me.buttonNew.Text = "&New..."
        '
        'dropListIdentity
        '
        Me.dropListIdentity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListIdentity.Location = New System.Drawing.Point(61, 19)
        Me.dropListIdentity.Name = "dropListIdentity"
        Me.dropListIdentity.Size = New System.Drawing.Size(219, 21)
        Me.dropListIdentity.Sorted = True
        Me.dropListIdentity.TabIndex = 1
        '
        'buttonBrowse
        '
        Me.buttonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonBrowse.Location = New System.Drawing.Point(292, 47)
        Me.buttonBrowse.Name = "buttonBrowse"
        Me.buttonBrowse.Size = New System.Drawing.Size(60, 23)
        Me.buttonBrowse.TabIndex = 5
        Me.buttonBrowse.Text = "&Browse..."
        '
        'saveFileDialog
        '
        Me.saveFileDialog.DefaultExt = "idt"
        Me.saveFileDialog.FileName = "myIdentity"
        Me.saveFileDialog.Filter = "Identity Information Files|*.idt|All files|*.*"
        Me.saveFileDialog.Title = "Save Identity Information as..."
        '
        'DlgSaveIdentityInfo
        '
        Me.AcceptButton = Me.buttonOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(360, 114)
        Me.Controls.Add(Me.buttonBrowse)
        Me.Controls.Add(Me.dropListIdentity)
        Me.Controls.Add(Me.buttonNew)
        Me.Controls.Add(Me.textBoxFile)
        Me.Controls.Add(Me.labelFile)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOK)
        Me.Controls.Add(Me.labelIdentity)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(999, 141)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 141)
        Me.Name = "DlgSaveIdentityInfo"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Save Identity Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub 'InitializeComponent
#End Region

    ' Save the information for a PeerIdentity to a file.
    '
    Private Sub buttonOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOK.Click
        If SaveIdentityInformation() Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub 'buttonOK_Click

    ' Try to save the information associated with an identity
    '
    Private Function SaveIdentityInformation() As Boolean
        Dim success As Boolean = False

        Try
            Dim identity As PeerIdentity = CType(dropListIdentity.SelectedItem, PeerIdentity)
            Dim info As PeerIdentityInfo = identity.GetInfo()
            Dim xmlIdentityInfo As String = info.ToXmlString()

            ' Save the XML as a Unicode Text file to be compatibile
            ' with the Win32 samples.
            Utilities.WriteTextFile(textBoxFile.Text, xmlIdentityInfo)
            success = True
        Catch e As Exception
            Utilities.DisplayException(e, Me)
        End Try

        Return success

    End Function 'SaveIdentityInformation

    ' Close the dialog without saving any information.
    '
    Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
        Me.Close()

    End Sub 'buttonCancel_Click

    ' Show the subdialog to create a new PeerIdentity.
    '
    Private Sub buttonNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonNew.Click
        Dim dlg As New DlgNewIdentity
        If DialogResult.OK = dlg.ShowDialog(Me) Then
            Utilities.FillIdentityComboBox(dropListIdentity, dlg.Identity)
        End If

    End Sub 'buttonNew_Click

    ' Select a filename to use.
    '
    Private Sub buttonBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonBrowse.Click
        If textBoxFile.TextLength = 0 Then
            ' default the filename to the selected PeerIdentity.
            saveFileDialog.FileName = dropListIdentity.Text
        End If

        If DialogResult.OK = saveFileDialog.ShowDialog(Me) Then
            textBoxFile.Text = saveFileDialog.FileName
        End If

    End Sub 'buttonBrowse_Click
End Class 'DlgSaveIdentityInfo