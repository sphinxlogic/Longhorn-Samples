'=====================================================================
'  File:      FormIdentity.vb
'
'  Summary:   Display a PeerIdentity in a form.
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


' This for displays the information associated with an Identity.

Public Class FormIdentity
    Inherits System.Windows.Forms.Form

    Private textBoxName As System.Windows.Forms.TextBox
    Private textBoxClassifier As System.Windows.Forms.TextBox
    Private textBoxAuthority As System.Windows.Forms.TextBox
    Private textBoxXml As System.Windows.Forms.TextBox
    Private WithEvents buttonClose As System.Windows.Forms.Button
    Private WithEvents labelName As System.Windows.Forms.Label
    Private WithEvents labelClassifier As System.Windows.Forms.Label
    Private WithEvents labelAuthority As System.Windows.Forms.Label
    Private WithEvents labelXml As System.Windows.Forms.Label

    Private components As System.ComponentModel.Container = Nothing


    ' Constructor to create a form to display the information for a PeerIdentity.
    '
    Public Sub New(ByVal identity As PeerIdentity)
        InitializeComponent()

        ' Fill out the text fields
        Me.textBoxName.Text = identity.FriendlyName
        Me.textBoxAuthority.Text = identity.PeerName.Authority
        Me.textBoxClassifier.Text = identity.PeerName.Classifier

        Try
            Me.textBoxXml.Text = identity.Key.ToXmlString(True)
        Catch e As Exception
            Me.textBoxXml.Text = e.Message
        End Try


        ' Set the window title to the friendly name of this identity.
        Me.Text = identity.FriendlyName

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
        Me.labelClassifier = New System.Windows.Forms.Label
        Me.textBoxClassifier = New System.Windows.Forms.TextBox
        Me.labelAuthority = New System.Windows.Forms.Label
        Me.textBoxAuthority = New System.Windows.Forms.TextBox
        Me.labelXml = New System.Windows.Forms.Label
        Me.textBoxXml = New System.Windows.Forms.TextBox
        Me.buttonClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'labelName
        '
        Me.labelName.Location = New System.Drawing.Point(7, 12)
        Me.labelName.Name = "labelName"
        Me.labelName.Size = New System.Drawing.Size(66, 17)
        Me.labelName.TabIndex = 1
        Me.labelName.Text = "Name:"
        '
        'textBoxName
        '
        Me.textBoxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxName.Location = New System.Drawing.Point(83, 12)
        Me.textBoxName.Name = "textBoxName"
        Me.textBoxName.ReadOnly = True
        Me.textBoxName.Size = New System.Drawing.Size(248, 19)
        Me.textBoxName.TabIndex = 2
        '
        'labelClassifier
        '
        Me.labelClassifier.Location = New System.Drawing.Point(7, 38)
        Me.labelClassifier.Name = "labelClassifier"
        Me.labelClassifier.Size = New System.Drawing.Size(66, 17)
        Me.labelClassifier.TabIndex = 3
        Me.labelClassifier.Text = "Classifier:"
        '
        'textBoxClassifier
        '
        Me.textBoxClassifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxClassifier.Location = New System.Drawing.Point(83, 39)
        Me.textBoxClassifier.Name = "textBoxClassifier"
        Me.textBoxClassifier.ReadOnly = True
        Me.textBoxClassifier.Size = New System.Drawing.Size(248, 19)
        Me.textBoxClassifier.TabIndex = 4
        '
        'labelAuthority
        '
        Me.labelAuthority.Location = New System.Drawing.Point(7, 64)
        Me.labelAuthority.Name = "labelAuthority"
        Me.labelAuthority.Size = New System.Drawing.Size(66, 17)
        Me.labelAuthority.TabIndex = 5
        Me.labelAuthority.Text = "Authority:"
        '
        'textBoxAuthority
        '
        Me.textBoxAuthority.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxAuthority.Location = New System.Drawing.Point(83, 66)
        Me.textBoxAuthority.Name = "textBoxAuthority"
        Me.textBoxAuthority.ReadOnly = True
        Me.textBoxAuthority.Size = New System.Drawing.Size(248, 19)
        Me.textBoxAuthority.TabIndex = 6
        '
        'labelXml
        '
        Me.labelXml.Location = New System.Drawing.Point(7, 90)
        Me.labelXml.Name = "labelXml"
        Me.labelXml.Size = New System.Drawing.Size(66, 17)
        Me.labelXml.TabIndex = 7
        Me.labelXml.Text = "XML:"
        '
        'textBoxXml
        '
        Me.textBoxXml.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBoxXml.Location = New System.Drawing.Point(83, 93)
        Me.textBoxXml.Multiline = True
        Me.textBoxXml.Name = "textBoxXml"
        Me.textBoxXml.ReadOnly = True
        Me.textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBoxXml.Size = New System.Drawing.Size(248, 66)
        Me.textBoxXml.TabIndex = 8
        '
        'buttonClose
        '
        Me.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonClose.Location = New System.Drawing.Point(133, 173)
        Me.buttonClose.Name = "buttonClose"
        Me.buttonClose.TabIndex = 0
        Me.buttonClose.Text = "Close"
        '
        'FormIdentity
        '
        Me.AcceptButton = Me.buttonClose
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.buttonClose
        Me.ClientSize = New System.Drawing.Size(341, 205)
        Me.Controls.Add(Me.buttonClose)
        Me.Controls.Add(Me.textBoxXml)
        Me.Controls.Add(Me.labelXml)
        Me.Controls.Add(Me.textBoxAuthority)
        Me.Controls.Add(Me.labelAuthority)
        Me.Controls.Add(Me.textBoxClassifier)
        Me.Controls.Add(Me.labelClassifier)
        Me.Controls.Add(Me.textBoxName)
        Me.Controls.Add(Me.labelName)
        Me.MinimumSize = New System.Drawing.Size(329, 232)
        Me.Name = "FormIdentity"
        Me.Text = "FormIdentity"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub 'InitializeComponent
#End Region

    '  Close the dialog.
    '
    Private Sub buttonClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonClose.Click
        Me.Close()
    End Sub 'buttonClose_Click
End Class 'FormIdentity