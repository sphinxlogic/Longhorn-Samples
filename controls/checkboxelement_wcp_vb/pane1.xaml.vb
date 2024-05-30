Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace CheckBoxElement_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        Sub HandleChange(ByVal sender As Object, ByVal e As System.Windows.Controls.IsCheckedChangedEventArgs)

            btn.Background = System.Windows.Media.Brushes.LightBlue

        End Sub

        Sub HandleChange2(ByVal sender As Object, ByVal e As System.Windows.Controls.IsCheckedChangedEventArgs)

            btn.Content = "Hello World!"

        End Sub

    End Class

End Namespace