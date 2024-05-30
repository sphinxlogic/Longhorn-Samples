Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace OpacityMasking_vb

    '@ <summary>
    '@ Interaction logic for Window1_xaml.xaml
    '@ </summary>
    Partial Class Window1

        Private Sub WindowLoaded(ByVal sender As Object, ByVal e As EventArgs)
            applyOpacityMaskToPanelExample(myPanel)
            applyOpacityMaskToShapeExample(myRectangle)
            applyOpacityMaskToImageExample(myImage)

        End Sub

        ' The elements to which the opacity mask are applied are
        ' defined in Window1.xaml.
        Private Sub applyOpacityMaskToPanelExample(ByVal myPanel As System.Windows.Controls.Panel)
            Dim myRadialGradientBrush As _
                New System.Windows.Media.RadialGradientBrush( _
                    System.Windows.Media.Color.FromScRGB(1, 0, 0, 0), _
                    System.Windows.Media.Color.FromScRGB(0, 0, 0, 0))
            myPanel.OpacityMask = myRadialGradientBrush
        End Sub

        Private Sub applyOpacityMaskToShapeExample(ByVal myShape As System.Windows.Shapes.Shape)
            Dim myLinearGradientBrush As _
                New System.Windows.Media.LinearGradientBrush( _
                System.Windows.Media.Colors.Black, _
                System.Windows.Media.Colors.Transparent, 0)
            myShape.OpacityMask = myLinearGradientBrush
        End Sub

        Private Sub applyOpacityMaskToImageExample(ByVal myImage As System.Windows.Controls.Image)

            Dim myLinearGradientBrush As _
                New System.Windows.Media.LinearGradientBrush()
            myLinearGradientBrush.AddStop( _
                System.Windows.Media.Colors.Black, 0)
            myLinearGradientBrush.AddStop( _
                System.Windows.Media.Colors.Transparent, 1)
            myLinearGradientBrush.StartPoint = New System.Windows.Point(0, 0)
            myLinearGradientBrush.EndPoint = New System.Windows.Point(0.75, 0.75)
            myImage.OpacityMask = myLinearGradientBrush
        End Sub

    End Class

End Namespace