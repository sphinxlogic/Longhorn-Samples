Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace RadioButtonList_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        ' Event handler:  
        Sub WriteText(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)

            Dim rbsender As RadioButtonList = CType(sender, RadioButtonList)
            Dim li As RadioButton
            li = rbsender.SelectedItem

            btn.Content = "You clicked " + li.Content.ToString()
        End Sub

    End Class

End Namespace