Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace PathElementAndGeometries_vb

    '@ <summary>
    '@ Interaction logic for Window1_xaml.xaml
    '@ </summary>
    Expands Class Window1

        Private Sub WindowLoaded(ByVal sender As Object, ByVal e As EventArgs)
            createLineSegment(myLineExampleCanvas)
            createHorizontalLineSegment(myHorizontalLineExampleCanvas)
            createVerticalLineSegment(myVerticalLineExampleCanvas)
            createBezierCurve(myBezierExampleCanvas)
            createQuadraticBezierCurve(myQuadraticBezierExampleCanvas)
            createEllipticalArc(myEllipticalArcExampleCanvas)
            createClosedPath(myClosedPathExampleCanvas)
        End Sub

        Private Sub createLineSegment(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 50))
            myPathFigure.LineTo(New System.Windows.Point(200, 70))

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createHorizontalLineSegment(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 50))
            myPathFigure.LineTo(New System.Windows.Point(200, 50))

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createVerticalLineSegment(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 50))
            myPathFigure.LineTo(New System.Windows.Point(10, 200))

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createBezierCurve(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 100))
            myPathFigure.BezierTo( _
             New System.Windows.Point(100, 0), _
             New System.Windows.Point(200, 200), _
             New System.Windows.Point(300, 100))

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createQuadraticBezierCurve(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 100))
            myPathFigure.QuadraticBezierTo( _
             New System.Windows.Point(200, 200), _
             New System.Windows.Point(300, 100))

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createEllipticalArc(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 100))
            myPathFigure.ArcTo(New System.Windows.Point(200, 100), _
                New System.Windows.Size(100, 50), _
                         45, True, False)

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub

        Private Sub createClosedPath(ByRef myCanvas As System.Windows.Controls.Canvas)

            ' Create a Path element to render the shapes.
            Dim myPath = New System.Windows.Shapes.Path()
            myPath.Stroke = System.Windows.Media.Brushes.Black
            myPath.StrokeThickness = New System.Windows.Length(1)

            ' Create a PathGeometry to contain the PathFigure.
            Dim myPathGeometry = New System.Windows.Media.PathGeometry()

            ' Create a PathFigure to describe the shape.
            Dim myPathFigure = New System.Windows.Media.PathFigure()

            myPathFigure.StartAt(New System.Windows.Point(10, 100))
            myPathFigure.LineTo(New System.Windows.Point(100, 100))
            myPathFigure.LineTo(New System.Windows.Point(100, 50))
            myPathFigure.Close()

            myPathGeometry.AddFigure(myPathFigure)
            myPath.Data = myPathGeometry
            myCanvas.Children.Add(myPath)
        End Sub


    End Class

End Namespace