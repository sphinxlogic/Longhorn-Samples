Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace Thumb_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        ' Event handler:  
        Sub ShowDelta(ByVal sender As Object, ByVal e As DragDeltaEventArgs)
            Dim thumb As Thumb = sender
            Dim horz As String = e.HorizontalChange.ToString()
            Dim vert As String = e.VerticalChange.ToString()
            changes.Text = horz & ", " & vert
        End Sub

    End Class

End Namespace