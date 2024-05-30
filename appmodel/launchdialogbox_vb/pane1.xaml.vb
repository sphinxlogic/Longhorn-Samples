Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace LaunchDialog_VB

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
    Sub Launch_Dialog(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
      Dim dlgResult As System.Windows.DialogResult
      Dim nextUri As System.Uri
      Dim DialogWindow As System.Windows.Navigation.NavigationWindow

      Dim myApp As System.Windows.Navigation.NavigationApplication
      Dim navWindow As System.Windows.Navigation.NavigationWindow
      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      navWindow = CType(myApp.MainWindow, System.Windows.Navigation.NavigationWindow)

      DialogWindow = New System.Windows.Navigation.NavigationWindow()
      nextUri = New System.Uri("DialogBox.xaml", False, True)
      DialogWindow.Navigate(nextUri)
      dlgResult = DialogWindow.ShowDialog()

      'After ShowDialog returns, display results
      txtReturnValue.TextContent += " " + dlgResult.ToString()
      txtUserData.TextContent += " " + CStr(myApp.Properties("UserData"))
    End Sub 'Launch_Dialog

    End Class

End Namespace