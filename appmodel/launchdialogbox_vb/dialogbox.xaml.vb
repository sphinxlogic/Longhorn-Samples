Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace LaunchDialog_VB

    '@ <summary>
    '@ Interaction logic for DialogBox_xaml.xaml
    '@ </summary>
    Expands Class DialogBox
    Sub YesButton_Click(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
      Dim myApp As System.Windows.Navigation.NavigationApplication
      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      Dim dlgWindow As System.Windows.Navigation.NavigationWindow

      dlgWindow = CType(myApp.Windows(1), System.Windows.Navigation.NavigationWindow)
      myApp.Properties("UserData") = User_Data.Text
      dlgWindow.DialogResult = System.Windows.DialogResult.Yes
    End Sub 'YesButton_Click

    Sub CancelButton_Click(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
      Dim myApp As System.Windows.Navigation.NavigationApplication
      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      Dim dlgWindow As System.Windows.Navigation.NavigationWindow

      dlgWindow = CType(myApp.Windows(1), System.Windows.Navigation.NavigationWindow)
      dlgWindow.DialogResult = System.Windows.DialogResult.Cancel
    End Sub 'CancelButton_Click
    End Class

End Namespace