using System;

namespace MMGraphicsSamples
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Window mainWindow;

		protected override void OnStartingUp (
			System.Windows.StartingUpCancelEventArgs e)
		{
			base.OnStartingUp(e);
			CreateAndShowMainWindow();
		}

		private void CreateAndShowMainWindow ()
		{
			// Create the application's main window.
			mainWindow = new System.Windows.Window();

			// Create a canvas to contain the geometries.
			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;
	
			// Use the Geometry objects to create some simple shapes.
			System.Windows.Media.LineGeometry myLineGeometry = 
				new System.Windows.Media.LineGeometry(
					new System.Windows.Point(50, 50), 
					new System.Windows.Point(300, 50));
			System.Windows.Media.EllipseGeometry myEllipseGeometry = 
				new System.Windows.Media.EllipseGeometry(
					new System.Windows.Point(440, 100), 40, 75);
			System.Windows.Media.RectangleGeometry myRectangleGeometry = 
				new System.Windows.Media.RectangleGeometry(
					new System.Windows.Rect(
						new System.Windows.Point(400, 225), 
						new System.Windows.Size(100, 50)));

			// Use a PathGeometry to create a complex shape.
			System.Windows.Media.PathGeometry myPathGeometry = 
				new System.Windows.Media.PathGeometry();
			
			// Each PathGeometry is comprised of one or more PathFigure
			// objects.
			System.Windows.Media.PathFigure myPathFigure = 
				new System.Windows.Media.PathFigure();

			// Each PathFigure is comprised of one or more path segments.
			myPathFigure.StartAt(new System.Windows.Point(200, 50));
			
			myPathFigure.BezierTo(
				new System.Windows.Point(400, 100), 
				new System.Windows.Point(400, 200), 
				new System.Windows.Point(200, 300));
			myPathFigure.BezierTo(
				new System.Windows.Point(400, 300), 
				new System.Windows.Point(400, 100), 
				new System.Windows.Point(200, 50));
			myPathFigure.BezierTo(
				new System.Windows.Point(0, 100), 
				new System.Windows.Point(0, 200), 
				new System.Windows.Point(200, 300));
			
			System.Windows.Media.BezierSegment myBezierSegment = 
				new System.Windows.Media.BezierSegment(
					new System.Windows.Point(0, 300), 
					new System.Windows.Point(0, 100), 
					new System.Windows.Point(200, 50), true);
			myPathFigure.AddSegment(myBezierSegment);
			
			// Add the PathFigure to the PathGeometry
			myPathGeometry.Figures.Add(myPathFigure);
			
			// Add the geometries to a GeometryCollection and then use a 
			// Path shape to display it.
			System.Windows.Media.GeometryCollection myGeometryCollection = 
				new System.Windows.Media.GeometryCollection();
			myGeometryCollection.Add(myLineGeometry);
			myGeometryCollection.Add(myEllipseGeometry);
			myGeometryCollection.Add(myRectangleGeometry);
			myGeometryCollection.Add(myPathGeometry);

			System.Windows.Shapes.Path myPath = 
				new System.Windows.Shapes.Path();
			myPath.Data = myGeometryCollection;
			
			myPath.Stroke = System.Windows.Media.Brushes.Blue;
			myPath.StrokeThickness = new System.Windows.Length(5);
			System.Windows.Media.SolidColorBrush solidFill = 
				new System.Windows.Media.SolidColorBrush();
			myPath.Fill = 
				new System.Windows.Media.RadialGradientBrush(
					System.Windows.Media.Colors.Orange, 
					System.Windows.Media.Colors.Red);
			myCanvas.Children.Add(myPath);
			mainWindow.Show();
		}
	}

	internal sealed class EntryClass
	{
		[System.STAThread()]
		private static void Main ()
		{

			System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

			MyApp app = new MyApp ();

			app.Run ();
		}
	}
}
