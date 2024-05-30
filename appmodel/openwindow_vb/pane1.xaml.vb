Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace OpenWindow_VB

    Expands Class Pane1
    Sub btnGoTo2(ByVal Sender As Object, ByVal args As ClickEventArgs)
      Dim myApp As NavigationApplication
      Dim navWindow As NavigationWindow
      Dim nextUri As System.Uri

      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      navWindow = CType(myApp.MainWindow, NavigationWindow)
      nextUri = New System.Uri("Pane2.xaml", False, True)
      navWindow.Navigate(nextUri)
    End Sub 'btnGoTo2
    End Class

End Namespace