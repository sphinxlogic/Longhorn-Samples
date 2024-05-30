
/*

This sample demonstrates how to animate the position of a Geometry using
a PointAnimation.

*/


using System;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;

namespace MMGraphicsSamples
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Window mainWindow;

		protected override void OnStartingUp (System.Windows.StartingUpCancelEventArgs e)
		{
			base.OnStartingUp(e);
			CreateAndShowMainWindow();
		}
			

		private void CreateAndShowMainWindow ()
		{
			// Create the application's main window.
			mainWindow = new System.Windows.Window();

			// Create a canvas to contain the shape.
			System.Windows.Controls.Canvas myCanvas = 
			    new System.Windows.Controls.Canvas();
			
			mainWindow.Content =  myCanvas;


			// Create a System.Windows.Shapes.Path to contain
			// the geometry.
			System.Windows.Shapes.Path myPath = 
				new System.Windows.Shapes.Path();
			myPath.Fill = System.Windows.Media.Brushes.Blue;
			myPath.Stroke = System.Windows.Media.Brushes.Black;
			myPath.StrokeThickness = new System.Windows.Length(5);
			
			// Create a Geometry.
			System.Windows.Media.EllipseGeometry myEllipseGeometry = 
			new System.Windows.Media.EllipseGeometry();
			myEllipseGeometry.Center = new System.Windows.Point(200, 200);
			myEllipseGeometry.RadiusX = 25;
			myEllipseGeometry.RadiusY = 50;

			// Create a PointAnimation.
			System.Windows.Media.Animation.PointAnimation myPointAnimation = 
			new System.Windows.Media.Animation.PointAnimation();
			myPointAnimation.From = new System.Windows.Point(200, 200);
			myPointAnimation.To = new System.Windows.Point(50, 50);
			myPointAnimation.Duration = new System.Windows.Media.Animation.Time(5000);
			myPointAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(0));
			myPointAnimation.AutoReverse = true;
			myPointAnimation.RepeatCount = 20;

			// Apply the PointAnimation to the Geometry.
			myEllipseGeometry.CenterAnimations.Add(myPointAnimation);
			myPath.Data = myEllipseGeometry;
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
