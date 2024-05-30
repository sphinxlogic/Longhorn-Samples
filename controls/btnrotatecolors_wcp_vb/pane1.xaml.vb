Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace BtnRotateColors_wcp_vb

    '@ <summary>
    '@ Interaction logic for Pane1_xaml.xaml
    '@ </summary>
    Expands Class Pane1
        ' To add a handler for the Loaded event, put Loaded="OnLoaded" in root element of .xaml file.
        ' Private Sub OnLoaded(ByVal sender As Object, ByVal e As EventArg)
        ' End Sub

        Sub OnClick1(ByVal sender As Object, ByVal e As System.Windows.Controls.ClickEventArgs)
            Static NewColor As Integer = 0

            Select Case NewColor

                Case "0"
                    btn1.Background = System.Windows.Media.Brushes.Red

                Case "1"
                    btn1.Background = System.Windows.Media.Brushes.CadetBlue

                Case "2"
                    btn1.Background = System.Windows.Media.Brushes.LightGreen

                Case "3"
                    btn1.Background = System.Windows.Media.Brushes.Pink

                Case "4"
                    btn1.Background = System.Windows.Media.Brushes.Yellow

            End Select

            NewColor = NewColor + 1
            If NewColor > 4 Then
                NewColor = 0
            End If
        End Sub

    End Class

End Namespace