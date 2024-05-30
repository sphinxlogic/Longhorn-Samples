Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace ListBoxCode_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub
        Dim Num As Integer = 1
        Sub OnClick(ByVal sender As Object, ByVal args As ClickEventArgs)
            Dim lb As New ListBox()
            Dim li1 As New ListItem()
            li1.Content = ("ListItem " & Num)
            lb.Items.Add(li1)
            root.Children.Add(lb)
            root.SetDock(lb, Dock.Top)
            Num = (Num + 1)
        End Sub


    End Class

End Namespace