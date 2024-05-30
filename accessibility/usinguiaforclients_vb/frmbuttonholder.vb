
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms


Namespace SDKSample
   '/ <summary>
   '/ Summary description for frmButtonHolder.
   '/ </summary>
   
   Public Class frmButtonHolder
      Inherits System.Windows.Forms.Form
      Private WithEvents button1 As System.Windows.Forms.Button
      
      '/ <summary>
      '/ Required designer variable.
      '/ </summary>
      Private components As System.ComponentModel.IContainer = Nothing
      
      
      Public Sub New()
         '
         ' Required for Windows Form Designer support
         '
         InitializeComponent()
      End Sub 'New
       
      '
      ' TODO: Add any constructor code after InitializeComponent call
      '
      
      '/ <summary>
      '/ Clean up any resources being used.
      '/ </summary>
      Protected Overrides Sub Dispose(disposing As Boolean)
         If disposing Then
            If Not (components Is Nothing) Then
               components.Dispose()
            End If
         End If
         MyBase.Dispose(disposing)
      End Sub 'Dispose
      
      #Region "Windows Form Designer generated code"
      
      '/ <summary>
      '/ Required method for Designer support - do not modify
      '/ the contents of this method with the code editor.
      '/ </summary>
      Private Sub InitializeComponent()
         Me.button1 = New System.Windows.Forms.Button()
         Me.SuspendLayout()
         
         ' 
         ' button1
         ' 
         Me.button1.Location = New System.Drawing.Point(173, 87)
         Me.button1.Name = "button1"
         Me.button1.Size = New System.Drawing.Size(80, 33)
         Me.button1.TabIndex = 0
         Me.button1.Text = "Button1"
         
         ' 
         ' frmButtonHolder
         ' 
         Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
         Me.ClientSize = New System.Drawing.Size(292, 269)
         Me.Controls.Add(button1)
         Me.Name = "frmButtonHolder"
         Me.Text = "frmButtonHolder"
         Me.ResumeLayout(False)
      End Sub 'InitializeComponent
      #End Region
      
      
      Private Sub button1_Click(sender As Object, e As System.EventArgs) Handles button1.Click
         MessageBox.Show("Button1 invoked.")
      End Sub 'button1_Click
      
      
      Private Sub frmButtonHolder_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
      End Sub 'frmButtonHolder_Load
   End Class 'frmButtonHolder 
End Namespace 'SDKSample