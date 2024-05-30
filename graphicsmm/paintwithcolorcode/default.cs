using System;

namespace MMGraphicsSamples
{
	public class MyApp :System.Windows.Application
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
			mainWindow = new System.Windows.Window ();

			// Create a canvas to contain the ellipses and
			// add the canvas to the window.
			System.Windows.Controls.Canvas myCanvas = new System.Windows.Controls.Canvas();

			mainWindow.Content = myCanvas;
	

			// Create the ellipses.
			System.Windows.Shapes.Ellipse e1 = new System.Windows.Shapes.Ellipse();
			System.Windows.Shapes.Ellipse e2 = new System.Windows.Shapes.Ellipse();
			System.Windows.Shapes.Ellipse e3 = new System.Windows.Shapes.Ellipse();

			// Set the fill value for the interior of each ellipse in 
			// different ways that have identical results.
			e1.Fill = System.Windows.Media.Brushes.Blue;
			e2.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
			e3.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromScRGB(1, 0, 0, 1));

			// Set the stroke value for the interior of each ellipse in 
			// different ways that have identical results.
			e1.Stroke = System.Windows.Media.Brushes.Black;
			e2.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
			e3.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromScRGB(1, 0, 0, 0));

			// Set the thickness of the stroke.
			e1.StrokeThickness = new System.Windows.Length(10);
			e2.StrokeThickness = new System.Windows.Length(10);
			e3.StrokeThickness = new System.Windows.Length(10);

			// Set the size and position of the ellipses.
			e1.CenterX = new System.Windows.Length(100);
			e1.CenterY = new System.Windows.Length(75);
			e1.RadiusX = new System.Windows.Length(50);
			e1.RadiusY = new System.Windows.Length(50);

			e2.CenterX = new System.Windows.Length(220);
			e2.CenterY = new System.Windows.Length(75);
			e2.RadiusX = new System.Windows.Length(50);
			e2.RadiusY = new System.Windows.Length(50);

			e3.CenterX = new System.Windows.Length(340);
			e3.CenterY = new System.Windows.Length(75);
			e3.RadiusX = new System.Windows.Length(50);
			e3.RadiusY = new System.Windows.Length(50);

			// Add the ellipses to the canvas.
			myCanvas.Children.Add(e1);
			myCanvas.Children.Add(e2);
			myCanvas.Children.Add(e3);
			
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
