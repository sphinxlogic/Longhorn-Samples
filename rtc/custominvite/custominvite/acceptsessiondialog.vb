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

Namespace Microsoft.Collaboration.Samples.CustomInvite
    Public NotInheritable Class AcceptSessionDialog
        Inherits System.Windows.Forms.Form

        Public Property RcvdPeerAddress() As String
            Get
                Return Me.rcvdPeerAddressTextBox.Text
            End Get
            Set(ByVal value As String)
                Me.rcvdPeerAddressTextBox.Text = value
            End Set
        End Property

        Public Property RcvdContentType() As String
            Get
                Return Me.rcvdContentTypeTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.rcvdContentTypeTextBox.Text = value
            End Set
        End Property

        Public Property RcvdContentDescription() As String
            Get
                Return Me.rcvdContentDescTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.rcvdContentDescTextBox.Text = value
            End Set
        End Property


#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call
            Me.myOkButton.DialogResult = DialogResult.OK
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents myCancelButton As System.Windows.Forms.Button
        Friend WithEvents myOkButton As System.Windows.Forms.Button
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents rcvdPeerAddressTextBox As System.Windows.Forms.TextBox
        Friend WithEvents rcvdContentDescTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents rcvdContentTypeTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label7 As System.Windows.Forms.Label
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.myCancelButton = New System.Windows.Forms.Button
            Me.myOkButton = New System.Windows.Forms.Button
            Me.Label5 = New System.Windows.Forms.Label
            Me.rcvdPeerAddressTextBox = New System.Windows.Forms.TextBox
            Me.rcvdContentDescTextBox = New System.Windows.Forms.TextBox
            Me.Label6 = New System.Windows.Forms.Label
            Me.rcvdContentTypeTextBox = New System.Windows.Forms.TextBox
            Me.Label7 = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'myCancelButton
            '
            Me.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.myCancelButton.Location = New System.Drawing.Point(232, 160)
            Me.myCancelButton.Name = "myCancelButton"
            Me.myCancelButton.TabIndex = 1
            Me.myCancelButton.Text = "Reject"
            '
            'myOkButton
            '
            Me.myOkButton.Location = New System.Drawing.Point(144, 160)
            Me.myOkButton.Name = "myOkButton"
            Me.myOkButton.TabIndex = 0
            Me.myOkButton.Text = "Accept"
            '
            'Label5
            '
            Me.Label5.Location = New System.Drawing.Point(8, 40)
            Me.Label5.Name = "Label5"
            Me.Label5.TabIndex = 39
            Me.Label5.Text = "Content Type"
            '
            'rcvdPeerAddressTextBox
            '
            Me.rcvdPeerAddressTextBox.Location = New System.Drawing.Point(120, 8)
            Me.rcvdPeerAddressTextBox.Name = "rcvdPeerAddressTextBox"
            Me.rcvdPeerAddressTextBox.ReadOnly = True
            Me.rcvdPeerAddressTextBox.Size = New System.Drawing.Size(192, 20)
            Me.rcvdPeerAddressTextBox.TabIndex = 38
            Me.rcvdPeerAddressTextBox.Text = ""
            '
            'rcvdContentDescTextBox
            '
            Me.rcvdContentDescTextBox.Location = New System.Drawing.Point(120, 72)
            Me.rcvdContentDescTextBox.Multiline = True
            Me.rcvdContentDescTextBox.Name = "rcvdContentDescTextBox"
            Me.rcvdContentDescTextBox.ReadOnly = True
            Me.rcvdContentDescTextBox.Size = New System.Drawing.Size(192, 72)
            Me.rcvdContentDescTextBox.TabIndex = 42
            Me.rcvdContentDescTextBox.Text = ""
            '
            'Label6
            '
            Me.Label6.Location = New System.Drawing.Point(8, 8)
            Me.Label6.Name = "Label6"
            Me.Label6.TabIndex = 37
            Me.Label6.Text = "Peer Address"
            '
            'rcvdContentTypeTextBox
            '
            Me.rcvdContentTypeTextBox.Location = New System.Drawing.Point(120, 40)
            Me.rcvdContentTypeTextBox.Name = "rcvdContentTypeTextBox"
            Me.rcvdContentTypeTextBox.ReadOnly = True
            Me.rcvdContentTypeTextBox.Size = New System.Drawing.Size(192, 20)
            Me.rcvdContentTypeTextBox.TabIndex = 41
            Me.rcvdContentTypeTextBox.Text = ""
            '
            'Label7
            '
            Me.Label7.Location = New System.Drawing.Point(8, 72)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(112, 23)
            Me.Label7.TabIndex = 40
            Me.Label7.Text = "Content Description"
            '
            'AcceptSessionDialog
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(314, 189)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.rcvdPeerAddressTextBox)
            Me.Controls.Add(Me.rcvdContentDescTextBox)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.rcvdContentTypeTextBox)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.myCancelButton)
            Me.Controls.Add(Me.myOkButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "AcceptSessionDialog"
            Me.Text = "Incoming Session"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub rcvdContentDescTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rcvdContentDescTextBox.TextChanged

        End Sub
    End Class
End Namespace