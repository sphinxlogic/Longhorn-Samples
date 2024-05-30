Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace RepeatButton_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        ' Event handler:  
        Dim Num As Integer
        Sub Increase(ByVal sender As Object, ByVal e As ClickEventArgs)

            Num = CInt(txt.TextContent)
            txt.TextContent = ((Num + 1).ToString())
        End Sub

        Sub Decrease(ByVal sender As Object, ByVal e As ClickEventArgs)

            Num = CInt(txt.TextContent)
            txt.TextContent = ((Num - 1).ToString())
        End Sub

    End Class

End Namespace