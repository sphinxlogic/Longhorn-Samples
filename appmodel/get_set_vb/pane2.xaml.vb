Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace Get_Set_VB

    '@ <summary>
    '@ Interaction logic for Pane2_xaml.xaml
    '@ </summary>
    Expands Class Pane2
    Sub Init(ByVal sender As Object, ByVal args As System.EventArgs)
      Dim myApp As System.Windows.Navigation.NavigationApplication
      Dim stringFrom1 As String

      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      stringFrom1 = CStr(myApp.Properties("TextFromPage1"))
      txtFromPage1.TextContent += stringFrom1
    End Sub 'Init

    Sub btnClose(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
      Dim myApp As System.Windows.Navigation.NavigationApplication
      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)

      myApp.Shutdown()
    End Sub 'btnClose
    End Class

End Namespace