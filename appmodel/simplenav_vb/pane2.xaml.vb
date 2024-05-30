Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace SimpleNav_VB

    Expands Class Pane2
    Sub btnGoTo1(ByVal Sender As Object, ByVal args As ClickEventArgs)
      Dim myApp As NavigationApplication
      Dim navWindow As NavigationWindow
      Dim nextUri As System.Uri

      myApp = CType(System.Windows.Application.Current, NavigationApplication)
      navWindow = CType(myApp.MainWindow, NavigationWindow)
      nextUri = New System.Uri("Pane1.xaml", False, True)
      navWindow.Navigate(nextUri)
    End Sub

    End Class

End Namespace