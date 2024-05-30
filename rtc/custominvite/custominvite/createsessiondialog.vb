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

    Public NotInheritable Class CreateSessionDialog
        Inherits System.Windows.Forms.Form
        Public ReadOnly Property SentPeerAddress() As String
            Get
                Return Me.sentPeerAddressTextBox.Text
            End Get
        End Property

        Public Property SentContentType() As String
            Get
                Return Me.sentContentTypeTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.sentContentTypeTextBox.Text = value
            End Set
        End Property

        Public Property SentContentDescription() As String
            Get
                Return Me.sentContentDescTextBox.Text()
            End Get
            Set(ByVal value As String)
                Me.sentContentDescTextBox.Text = value
            End Set
        End Property

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call
            Me.myOkButton.DialogResult = DialogResult.OK
            Me.myCancelButton.DialogResult = DialogResult.Cancel

            Me.sentPeerAddressTextBox.Focus()
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
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents sentPeerAddressTextBox As System.Windows.Forms.TextBox
        Friend WithEvents sentContentDescTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents sentContentTypeTextBox As System.Windows.Forms.TextBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents myOkButton As System.Windows.Forms.Button
        Friend WithEvents myCancelButton As System.Windows.Forms.Button
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.Label1 = New System.Windows.Forms.Label
            Me.sentPeerAddressTextBox = New System.Windows.Forms.TextBox
            Me.sentContentDescTextBox = New System.Windows.Forms.TextBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.sentContentTypeTextBox = New System.Windows.Forms.TextBox
            Me.Label3 = New System.Windows.Forms.Label
            Me.myOkButton = New System.Windows.Forms.Button
            Me.myCancelButton = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(8, 40)
            Me.Label1.Name = "Label1"
            Me.Label1.TabIndex = 22
            Me.Label1.Text = "Content Type"
            '
            'sentPeerAddressTextBox
            '
            Me.sentPeerAddressTextBox.Location = New System.Drawing.Point(128, 8)
            Me.sentPeerAddressTextBox.Name = "sentPeerAddressTextBox"
            Me.sentPeerAddressTextBox.Size = New System.Drawing.Size(192, 20)
            Me.sentPeerAddressTextBox.TabIndex = 0
            Me.sentPeerAddressTextBox.Text = ""
            '
            'sentContentDescTextBox
            '
            Me.sentContentDescTextBox.Location = New System.Drawing.Point(128, 72)
            Me.sentContentDescTextBox.Multiline = True
            Me.sentContentDescTextBox.Name = "sentContentDescTextBox"
            Me.sentContentDescTextBox.Size = New System.Drawing.Size(192, 72)
            Me.sentContentDescTextBox.TabIndex = 2
            Me.sentContentDescTextBox.Text = ""
            '
            'Label2
            '
            Me.Label2.Location = New System.Drawing.Point(8, 8)
            Me.Label2.Name = "Label2"
            Me.Label2.TabIndex = 20
            Me.Label2.Text = "Peer Address"
            '
            'sentContentTypeTextBox
            '
            Me.sentContentTypeTextBox.Location = New System.Drawing.Point(128, 40)
            Me.sentContentTypeTextBox.Name = "sentContentTypeTextBox"
            Me.sentContentTypeTextBox.Size = New System.Drawing.Size(192, 20)
            Me.sentContentTypeTextBox.TabIndex = 1
            Me.sentContentTypeTextBox.Text = ""
            '
            'Label3
            '
            Me.Label3.Location = New System.Drawing.Point(8, 72)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(120, 23)
            Me.Label3.TabIndex = 23
            Me.Label3.Text = "Content Description"
            '
            'myOkButton
            '
            Me.myOkButton.Location = New System.Drawing.Point(152, 152)
            Me.myOkButton.Name = "myOkButton"
            Me.myOkButton.TabIndex = 3
            Me.myOkButton.Text = "Ok"
            '
            'myCancelButton
            '
            Me.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.myCancelButton.Location = New System.Drawing.Point(240, 152)
            Me.myCancelButton.Name = "myCancelButton"
            Me.myCancelButton.TabIndex = 4
            Me.myCancelButton.Text = "Cancel"
            '
            'CreateSessionDialog
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.CancelButton = Me.myCancelButton
            Me.ClientSize = New System.Drawing.Size(322, 183)
            Me.Controls.Add(Me.myCancelButton)
            Me.Controls.Add(Me.myOkButton)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.sentPeerAddressTextBox)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.sentContentTypeTextBox)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.sentContentDescTextBox)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "CreateSessionDialog"
            Me.Text = "Create Session Dialog"
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles myOkButton.Click
            Me.Close()
        End Sub

        Private Sub CreateSessionDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub
    End Class
End Namespace