Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace BtnColor_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        Sub OnClick1(ByVal sender As Object, ByVal e As System.Windows.Controls.ClickEventArgs)
            btn1.Background = System.Windows.Media.Brushes.LightBlue
        End Sub

        Sub OnClick2(ByVal sender As Object, ByVal e As System.Windows.Controls.ClickEventArgs)
            btn2.Background = System.Windows.Media.Brushes.Pink
        End Sub

        Sub OnClick3(ByVal sender As Object, ByVal e As System.Windows.Controls.ClickEventArgs)
            btn1.Background = System.Windows.Media.Brushes.Pink
            btn2.Background = System.Windows.Media.Brushes.LightBlue
        End Sub

    End Class

End Namespace