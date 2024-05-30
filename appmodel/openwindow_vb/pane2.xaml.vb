Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace OpenWindow_VB

    '@ <summary>
    '@ Interaction logic for Pane2_xaml.xaml
    '@ </summary>
    Expands Class Pane2
    Sub btnClose(ByVal Sender As Object, ByVal args As System.Windows.Controls.ClickEventArgs)
      Dim myApp As NavigationApplication
      myApp = CType(System.Windows.Application.Current, NavigationApplication)

      myWindow.Close()
    End Sub 'btnClose  
    End Class

End Namespace