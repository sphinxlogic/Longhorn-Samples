Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace OnStartup_VB

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
    Sub Init(ByVal sender As Object, ByVal args As System.EventArgs)
      Dim myApp As System.Windows.Navigation.NavigationApplication
      Dim startString As String

      myApp = CType(System.Windows.Application.Current, System.Windows.Navigation.NavigationApplication)
      startString = CStr(myApp.Properties("StartingText"))
      txtLabel.TextContent += startString
    End Sub 'Init
    End Class

End Namespace