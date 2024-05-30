
using System;


namespace PathElementAndGeometries_csharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
       
        private void WindowLoaded(object sender, EventArgs e) {
			createLineSegment(myLineExampleCanvas);
			createHorizontalLineSegment(myHorizontalLineExampleCanvas);
			createVerticalLineSegment(myVerticalLineExampleCanvas);
			createBezierCurve(myBezierExampleCanvas);
			createQuadraticBezierCurve(myQuadraticBezierExampleCanvas);
			createEllipticalArc(myEllipticalArcExampleCanvas);
			createClosedPath(myClosedPathExampleCanvas);
		}

		private void createLineSegment(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = 
				new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = 
				new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 50));
			myPathFigure.LineTo(new System.Windows.Point(200, 70));
			
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createHorizontalLineSegment(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 50));
			myPathFigure.LineTo(new System.Windows.Point(200, 50));
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createVerticalLineSegment(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 50));
			myPathFigure.LineTo(new System.Windows.Point(10, 200));
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createBezierCurve(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 100));
			myPathFigure.BezierTo(
				new System.Windows.Point(100, 0),
				new System.Windows.Point(200, 200),
				new System.Windows.Point(300, 100));
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createQuadraticBezierCurve(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 100));
			myPathFigure.QuadraticBezierTo(
				new System.Windows.Point(200, 200), 
				new System.Windows.Point(300, 100));
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createEllipticalArc(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 100));
			myPathFigure.ArcTo(new System.Windows.Point(200, 100), new System.Windows.Size(100, 50), 45, true, false);
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}

		private void createClosedPath(System.Windows.Controls.Canvas myCanvas)
		{
			// Create a Path element to render the shapes.
			System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(1);

			// Create a PathGeometry to contain the PathFigure.
			System.Windows.Media.PathGeometry myPathGeometry = new System.Windows.Media.PathGeometry();

			// Create a PathFigure to describe the shape.
			System.Windows.Media.PathFigure myPathFigure = new System.Windows.Media.PathFigure();

			myPathFigure.StartAt(new System.Windows.Point(10, 100));
			myPathFigure.LineTo(new System.Windows.Point(100, 100));
			myPathFigure.LineTo(new System.Windows.Point(100, 50));
			myPathFigure.Close();
			myPathGeometry.AddFigure(myPathFigure);
			myPath.Data = myPathGeometry;
			myCanvas.Children.Add(myPath);
		}


    }
}