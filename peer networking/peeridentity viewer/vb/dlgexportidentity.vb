'=====================================================================
'  File:      DlgExportIdentity.vb
'
'  Summary:   Dialog to export a PeerIdentity as a file.
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
Imports System.Net.PeerToPeer
Imports System.Windows.Forms

Public Class DlgExportIdentity
    Inherits System.Windows.Forms.Form

    Private dropListIdentity As System.Windows.Forms.ComboBox
    Private textBoxFile As System.Windows.Forms.TextBox
    Private textBoxPassword As System.Windows.Forms.TextBox
    Private saveFileDialog As System.Windows.Forms.SaveFileDialog
    Private WithEvents buttonOK As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private WithEvents buttonBrowse As System.Windows.Forms.Button
    Private WithEvents labelIdentity As System.Windows.Forms.Label
    Private WithEvents labelFile As System.Windows.Forms.Label
    Private WithEvents labelPassword As System.Windows.Forms.Label

    Private components As System.ComponentModel.Container = Nothing

    '  Constructor that creates the dialog with the PeerIdentity selected.
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
        Me.dropListIdentity = New System.Windows.Forms.ComboBox
        Me.buttonBrowse = New System.Windows.Forms.Button
        Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.textBoxPassword = New System.Windows.Forms.TextBox
        Me.labelPassword = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'labelIdentity
        '
        Me.labelIdentity.Location = New System.Drawing.Point(4, 19)
        Me.labelIdentity.Name = "labelIdentity"
        Me.labelIdentity.Size = New System.Drawing.Size(59, 13)
        Me.labelIdentity.TabIndex = 0
        Me.labelIdentity.Text = "&Identity:"
        '
        'buttonOK
        '
        Me.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonOK.Location = New System.Drawing.Point(60, 103)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.TabIndex = 8
        Me.buttonOK.Text = "OK"
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(156, 103)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.TabIndex = 9
        Me.buttonCancel.Text = "Cancel"
        '
        'textBoxFile
        '
        Me.textBoxFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxFile.Location = New System.Drawing.Point(61, 47)
        Me.textBoxFile.Name = "textBoxFile"
        Me.textBoxFile.Size = New System.Drawing.Size(196, 19)
        Me.textBoxFile.TabIndex = 4
        '
        'labelFile
        '
        Me.labelFile.Location = New System.Drawing.Point(4, 47)
        Me.labelFile.Name = "labelFile"
        Me.labelFile.Size = New System.Drawing.Size(59, 13)
        Me.labelFile.TabIndex = 3
        Me.labelFile.Text = "&File:"
        '
        'dropListIdentity
        '
        Me.dropListIdentity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dropListIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dropListIdentity.Location = New System.Drawing.Point(61, 19)
        Me.dropListIdentity.Name = "dropListIdentity"
        Me.dropListIdentity.Size = New System.Drawing.Size(226, 21)
        Me.dropListIdentity.Sorted = True
        Me.dropListIdentity.TabIndex = 1
        '
        'buttonBrowse
        '
        Me.buttonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonBrowse.Location = New System.Drawing.Point(265, 47)
        Me.buttonBrowse.Name = "buttonBrowse"
        Me.buttonBrowse.Size = New System.Drawing.Size(22, 19)
        Me.buttonBrowse.TabIndex = 5
        Me.buttonBrowse.Text = "..."
        '
        'saveFileDialog
        '
        Me.saveFileDialog.DefaultExt = "idx"
        Me.saveFileDialog.Filter = "Exported identity files|*.idx|All files|*.*"
        Me.saveFileDialog.Title = "Save Identity Information as..."
        '
        'textBoxPassword
        '
        Me.textBoxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxPassword.Location = New System.Drawing.Point(61, 73)
        Me.textBoxPassword.Name = "textBoxPassword"
        Me.textBoxPassword.Size = New System.Drawing.Size(226, 19)
        Me.textBoxPassword.TabIndex = 7
        '
        'labelPassword
        '
        Me.labelPassword.Location = New System.Drawing.Point(4, 73)
        Me.labelPassword.Name = "labelPassword"
        Me.labelPassword.Size = New System.Drawing.Size(59, 13)
        Me.labelPassword.TabIndex = 6
        Me.labelPassword.Text = "&Password:"
        '
        'DlgExportIdentity
        '
        Me.AcceptButton = Me.buttonOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(290, 137)
        Me.Controls.Add(Me.textBoxPassword)
        Me.Controls.Add(Me.labelPassword)
        Me.Controls.Add(Me.buttonBrowse)
        Me.Controls.Add(Me.dropListIdentity)
        Me.Controls.Add(Me.textBoxFile)
        Me.Controls.Add(Me.labelFile)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOK)
        Me.Controls.Add(Me.labelIdentity)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(999, 164)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(298, 164)
        Me.Name = "DlgExportIdentity"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export Identity"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub 'InitializeComponent
#End Region

    '  Try to export the selected PeerIdentity to the file.
    '
    Private Sub buttonOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOK.Click
        If ExportIdentity() Then
            Me.Close()
        End If

    End Sub 'buttonOK_Click

    ' Try to export the identity.
    '
    Private Function ExportIdentity() As Boolean
        Dim success As Boolean = False

        Try
            Dim identity As PeerIdentity = CType(dropListIdentity.SelectedItem, PeerIdentity)
            Dim exportedData As String = identity.Export(textBoxPassword.Text)
            Utilities.WriteTextFile(textBoxFile.Text, exportedData)
            success = True
        Catch e As Exception
            Utilities.DisplayException(e, Me)
        End Try

        Return success

    End Function 'ExportIdentity

    ' Close the dialog without exporting any information.
    '
    Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
        Me.Close()
    End Sub 'buttonCancel_Click

    '  Select a filename.
    '
    Private Sub buttonBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonBrowse.Click
        If textBoxFile.TextLength = 0 Then
            ' default the file name to the selected identity.
            saveFileDialog.FileName = dropListIdentity.Text
        End If

        If DialogResult.OK = saveFileDialog.ShowDialog(Me) Then
            textBoxFile.Text = saveFileDialog.FileName
        End If

    End Sub 'buttonBrowse_Click

End Class 'DlgExportIdentity