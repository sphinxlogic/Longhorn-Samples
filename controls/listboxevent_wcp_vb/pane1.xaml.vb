Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace ListBoxEvent_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        Sub PrintText(ByVal sender As Object, ByVal args As SelectionChangedEventArgs)
            Dim lbsender As ListBox = CType(sender, ListBox)
            Dim li As ListItem
            li = lbsender.SelectedItem
            tb.Text = "   You selected " & li.Content.ToString & "."
        End Sub

    End Class

End Namespace