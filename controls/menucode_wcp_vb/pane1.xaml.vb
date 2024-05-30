Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace MenuCode_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        Sub OnClick(ByVal sender As Object, ByVal args As ClickEventArgs)
            Dim mn As New Menu()
            Dim mi As New MenuItem()
            mi.Header = New String("File")

            Dim mi1 As New MenuItem()
            mi1.Header = New String("New...")

            Dim mi2 As New MenuItem()
            mi2.Header = New String("Open...")

            Dim sep As New MenuItem()
            sep.Mode = MenuItemMode.Separator

            Dim mi3 As New MenuItem()
            mi3.Header = New String("Exit")
            mi3.ToolTip = New String("ToolTip information")

            mn.Items.Add(mi)
            mi.Items.Add(mi1)
            mi.Items.Add(mi2)
            mi.Items.Add(sep)
            mi.Items.Add(mi3)

            root.Children.Add(mn)
            root.SetDock(mn, Dock.Top)
        End Sub



    End Class

End Namespace