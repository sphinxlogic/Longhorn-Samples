Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace QuickStart_4_VB
    Partial Class Pane1
        Sub HandleClick(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
            Button1.Content = "Hello World"
        End Sub 'btnGoTo2
    End Class

End Namespace