'=====================================================================
'  File:      DlgImportIdentity.vb
'
'  Summary:   Dialog to import a PeerIdentity from a file.
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


Public Class DlgImportIdentity
    Inherits System.Windows.Forms.Form

    Private exportedIdentity As ExportedPeerIdentity
    Private identityValue As PeerIdentity

    Private textBoxPassword As System.Windows.Forms.TextBox
    Private textBoxAuthority As System.Windows.Forms.TextBox
    Private textBoxClassifier As System.Windows.Forms.TextBox
    Private textBoxXml As System.Windows.Forms.TextBox
    Private WithEvents buttonOK As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private WithEvents textBoxFile As System.Windows.Forms.TextBox
    Private WithEvents buttonBrowse As System.Windows.Forms.Button
    Private WithEvents labelName As System.Windows.Forms.Label
    Private WithEvents labelPassword As System.Windows.Forms.Label
    Private WithEvents labelXml As System.Windows.Forms.Label
    Private WithEvents labelClassifier As System.Windows.Forms.Label
    Private WithEvents labelAuthority As System.Windows.Forms.Label
    Private WithEvents informationGroupBox As System.Windows.Forms.GroupBox
    Private openFileDialog As System.Windows.Forms.OpenFileDialog
    Private components As System.ComponentModel.Container = Nothing

    ' Constructor to create a dialog to import an identity.
    '
    Public Sub New()
        InitializeComponent()
    End Sub 'New

    ' The PeerIdentity that was imported.
    '
    Public ReadOnly Property Identity() As PeerIdentity
        Get
            Return identityValue
        End Get
    End Property

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
        Me.buttonOK = New System.Windows.Forms.Button
        Me.buttonCancel = New System.Windows.Forms.Button
        Me.textBoxFile = New System.Windows.Forms.TextBox
        Me.labelName = New System.Windows.Forms.Label
        Me.buttonBrowse = New System.Windows.Forms.Button
        Me.textBoxPassword = New System.Windows.Forms.TextBox
        Me.labelPassword = New System.Windows.Forms.Label
        Me.informationGroupBox = New System.Windows.Forms.GroupBox
        Me.textBoxXml = New System.Windows.Forms.TextBox
        Me.labelXml = New System.Windows.Forms.Label
        Me.textBoxClassifier = New System.Windows.Forms.TextBox
        Me.labelClassifier = New System.Windows.Forms.Label
        Me.textBoxAuthority = New System.Windows.Forms.TextBox
        Me.labelAuthority = New System.Windows.Forms.Label
        Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.informationGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'buttonOK
        '
        Me.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonOK.Location = New System.Drawing.Point(97, 218)
        Me.buttonOK.Name = "buttonOK"
        Me.buttonOK.TabIndex = 6
        Me.buttonOK.Text = "OK"
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(193, 218)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.TabIndex = 7
        Me.buttonCancel.Text = "Cancel"
        '
        'textBoxFile
        '
        Me.textBoxFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxFile.Location = New System.Drawing.Point(61, 12)
        Me.textBoxFile.Name = "textBoxFile"
        Me.textBoxFile.Size = New System.Drawing.Size(219, 19)
        Me.textBoxFile.TabIndex = 1
        '
        'labelName
        '
        Me.labelName.Location = New System.Drawing.Point(4, 12)
        Me.labelName.Name = "labelName"
        Me.labelName.Size = New System.Drawing.Size(59, 13)
        Me.labelName.TabIndex = 0
        Me.labelName.Text = "&File:"
        '
        'buttonBrowse
        '
        Me.buttonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonBrowse.Location = New System.Drawing.Point(292, 12)
        Me.buttonBrowse.Name = "buttonBrowse"
        Me.buttonBrowse.Size = New System.Drawing.Size(60, 23)
        Me.buttonBrowse.TabIndex = 2
        Me.buttonBrowse.Text = "&Browse..."
        '
        'textBoxPassword
        '
        Me.textBoxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxPassword.Location = New System.Drawing.Point(61, 38)
        Me.textBoxPassword.Name = "textBoxPassword"
        Me.textBoxPassword.Size = New System.Drawing.Size(219, 19)
        Me.textBoxPassword.TabIndex = 4
        '
        'labelPassword
        '
        Me.labelPassword.Location = New System.Drawing.Point(4, 38)
        Me.labelPassword.Name = "labelPassword"
        Me.labelPassword.Size = New System.Drawing.Size(59, 13)
        Me.labelPassword.TabIndex = 3
        Me.labelPassword.Text = "&Password:"
        '
        'informationGroupBox
        '
        Me.informationGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.informationGroupBox.Controls.Add(Me.textBoxXml)
        Me.informationGroupBox.Controls.Add(Me.labelXml)
        Me.informationGroupBox.Controls.Add(Me.textBoxClassifier)
        Me.informationGroupBox.Controls.Add(Me.labelClassifier)
        Me.informationGroupBox.Controls.Add(Me.textBoxAuthority)
        Me.informationGroupBox.Controls.Add(Me.labelAuthority)
        Me.informationGroupBox.Location = New System.Drawing.Point(8, 67)
        Me.informationGroupBox.Name = "informationGroupBox"
        Me.informationGroupBox.Size = New System.Drawing.Size(345, 138)
        Me.informationGroupBox.TabIndex = 5
        Me.informationGroupBox.TabStop = False
        Me.informationGroupBox.Text = "Indentity information"
        '
        'textBoxXml
        '
        Me.textBoxXml.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxXml.Location = New System.Drawing.Point(75, 69)
        Me.textBoxXml.Multiline = True
        Me.textBoxXml.Name = "textBoxXml"
        Me.textBoxXml.ReadOnly = True
        Me.textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxXml.Size = New System.Drawing.Size(260, 60)
        Me.textBoxXml.TabIndex = 5
        Me.textBoxXml.TabStop = False
        '
        'labelXml
        '
        Me.labelXml.Location = New System.Drawing.Point(8, 69)
        Me.labelXml.Name = "labelXml"
        Me.labelXml.Size = New System.Drawing.Size(59, 13)
        Me.labelXml.TabIndex = 4
        Me.labelXml.Text = "XML:"
        '
        'textBoxClassifier
        '
        Me.textBoxClassifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxClassifier.Location = New System.Drawing.Point(75, 43)
        Me.textBoxClassifier.Name = "textBoxClassifier"
        Me.textBoxClassifier.ReadOnly = True
        Me.textBoxClassifier.Size = New System.Drawing.Size(260, 19)
        Me.textBoxClassifier.TabIndex = 3
        Me.textBoxClassifier.TabStop = False
        '
        'labelClassifier
        '
        Me.labelClassifier.Location = New System.Drawing.Point(8, 43)
        Me.labelClassifier.Name = "labelClassifier"
        Me.labelClassifier.Size = New System.Drawing.Size(59, 13)
        Me.labelClassifier.TabIndex = 2
        Me.labelClassifier.Text = "Classifier:"
        '
        'textBoxAuthority
        '
        Me.textBoxAuthority.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxAuthority.Location = New System.Drawing.Point(75, 18)
        Me.textBoxAuthority.Name = "textBoxAuthority"
        Me.textBoxAuthority.ReadOnly = True
        Me.textBoxAuthority.Size = New System.Drawing.Size(260, 19)
        Me.textBoxAuthority.TabIndex = 1
        Me.textBoxAuthority.TabStop = False
        '
        'labelAuthority
        '
        Me.labelAuthority.Location = New System.Drawing.Point(8, 18)
        Me.labelAuthority.Name = "labelAuthority"
        Me.labelAuthority.Size = New System.Drawing.Size(59, 13)
        Me.labelAuthority.TabIndex = 0
        Me.labelAuthority.Text = "Authority:"
        '
        'openFileDialog
        '
        Me.openFileDialog.DefaultExt = "*.idx"
        Me.openFileDialog.Filter = "Exported identity files|*.idx|All files|*.*"
        '
        'DlgImportIdentity
        '
        Me.AcceptButton = Me.buttonOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.buttonCancel
        Me.ClientSize = New System.Drawing.Size(360, 252)
        Me.Controls.Add(Me.informationGroupBox)
        Me.Controls.Add(Me.textBoxPassword)
        Me.Controls.Add(Me.labelPassword)
        Me.Controls.Add(Me.buttonBrowse)
        Me.Controls.Add(Me.textBoxFile)
        Me.Controls.Add(Me.labelName)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonOK)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(368, 279)
        Me.Name = "DlgImportIdentity"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Identity"
        Me.informationGroupBox.ResumeLayout(False)
        Me.informationGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub 'InitializeComponent
#End Region

    '  Try to create a PeerIdentity from the file.
    '
    Private Sub buttonOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonOK.Click
        identityValue = ImportIdentity(exportedIdentity, textBoxPassword.Text)
        If Not (identityValue Is Nothing) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub 'buttonOK_Click

    ' Try to import the exported identity.
    '
    Private Function ImportIdentity(ByVal exportedData As ExportedPeerIdentity, ByVal password As String) As PeerIdentity
        Dim identity As PeerIdentity = Nothing

        Try
            identity = PeerIdentity.Import(exportedData, password)
        Catch e As Exception
            Utilities.DisplayException(e, Me)
        End Try

        Return identity

    End Function 'ImportIdentity

    ' Close the dialog without importing any information.
    '
    Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
        Me.Close()
    End Sub 'buttonCancel_Click

    ' Select a filename.
    '
    Private Sub buttonBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonBrowse.Click
        If DialogResult.OK = openFileDialog.ShowDialog(Me) Then
            textBoxFile.Text = openFileDialog.FileName
            UpdateInformation()
        End If

    End Sub 'buttonBrowse_Click

    ' Handle a focus chance by updating the import information.
    '
    Private Sub textBoxFile_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles textBoxFile.Leave
        UpdateInformation()

    End Sub 'textBoxFile_Leave

    ' Refresh the import information based on the selected file.
    '
    Private Sub UpdateInformation()
        Dim fValid As Boolean = False

        Try
            Dim xml As String = Utilities.ReadTextFile(textBoxFile.Text)
            If 0 < xml.Length Then
                exportedIdentity = New ExportedPeerIdentity(xml)
                textBoxAuthority.Text = exportedIdentity.PeerName.Authority
                textBoxClassifier.Text = exportedIdentity.PeerName.Classifier
                textBoxXml.Text = xml
                fValid = True
            End If
        Catch
            ' Suppress all errors since the user may not have entered
            ' the correct filename.  The other text fields will be cleared.
        End Try

        If Not fValid Then
            exportedIdentity = Nothing
            textBoxAuthority.Text = String.Empty
            textBoxClassifier.Text = String.Empty
            textBoxXml.Text = String.Empty
        End If

    End Sub 'UpdateInformation

End Class 'DlgImportIdentity