' Window1.xaml is used to define and layout the elements in this sample.
' The opacity masks are applied in this file, Window1.xaml.vb.
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data
Imports System.Windows.Media

Namespace ImageAsOpacityMask_vb

    '@ <summary>
    '@ Demonstrates the creation and use of image-based opacity masks.
    '@ </summary>
    Expands Class Window1

        ' Applies opacity masks to the elements defined in
        ' Window1.xaml.
        Private Sub WindowLoaded(ByVal sender As Object, ByVal e As EventArgs)

            applyImageBasedOpacityMaskToElement(myImage)
            applyTiledImageBasedOpacityMaskToElement(anotherImage)
            applyImageBasedOpacityMaskToElement(myButton)

        End Sub

        ' Uses an image as an opacity mask.
        Private Sub applyImageBasedOpacityMaskToElement(ByVal myElement As UIElement)

            

            Try
                Dim myImageData = ImageData.Create("Data\\tornedges.png")

                Dim myImageBrush As New ImageBrush(myImageData)

                myElement.OpacityMask = myImageBrush

            Catch fileNotFoundEx As System.IO.FileNotFoundException

                MessageBox.Show("Unable to find image file: " + fileNotFoundEx.ToString())

            Catch fileLoadEx As System.IO.FileLoadException

                MessageBox.Show("Unable to open image file: " + fileLoadEx.ToString())

            Catch securityEx As System.Security.SecurityException

                MessageBox.Show("Inadequite security permissions: " + securityEx.ToString())

            End Try
        End Sub

        ' Uses a tiled image as an opacity mask;
        Private Sub applyTiledImageBasedOpacityMaskToElement(ByVal myElement As UIElement)

            

            Try

                Dim myImageData = ImageData.Create("Data\\tornedges.png")

                Dim myImageBrush = New ImageBrush(myImageData)

                ' Set the tile size and behavior.
                myImageBrush.ViewPort = New Rect(0, 0, 50, 50)
                myImageBrush.ViewPortUnits = BrushMappingMode.Absolute
                myImageBrush.TileMode = TileMode.Tile

                myElement.OpacityMask = myImageBrush

            Catch fileNotFoundEx As System.IO.FileNotFoundException

                MessageBox.Show("Unable to find image file: " + fileNotFoundEx.ToString())

            Catch fileLoadEx As System.IO.FileLoadException

                MessageBox.Show("Unable to open image file: " + fileLoadEx.ToString())

            Catch securityEx As System.Security.SecurityException

                MessageBox.Show("Inadequite security permissions: " + securityEx.ToString())

            End Try

        End Sub

    End Class

End Namespace