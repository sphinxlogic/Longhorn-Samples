using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MMGraphicsSamples
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Window mainWindow;

		protected override void OnStartingUp (StartingUpCancelEventArgs e)
		{
			base.OnStartingUp(e);
			CreateAndShowMainWindow();
		}

		private void CreateAndShowMainWindow ()
		{
			// Create the application's main window.
			mainWindow = new Window();

			// Create a canvas to contain the shapes.
			Canvas myCanvas = new Canvas();
			mainWindow.Content = myCanvas;
	
			// Create a PointCollection to contain the points of the
			// Polygon shapes.
			PointCollection myPointCollection = new PointCollection();
			myPointCollection.Add(new Point(60, 60));
			myPointCollection.Add(new Point(70, 70));
			myPointCollection.Add(new Point(70, 110));
			myPointCollection.Add(new Point(110, 110));
			myPointCollection.Add(new Point(110, 70));
			myPointCollection.Add(new Point(70, 70));

			// Create the first Polygon and add it to the Canvas.
			Polygon box1 = new Polygon();
			box1.Points = myPointCollection;
			box1.Stroke = Brushes.Black;
			box1.StrokeThickness = new Length(5);
			myCanvas.Children.Add(box1);
	
			// Create the second Polygon. This polygon contains the same
			// points as the first, but is rotated.
			Polygon box2 = new Polygon();
			box2.Points = myPointCollection;
			box2.Stroke = Brushes.Blue;
			box2.StrokeThickness = new Length(5);
			
			// Create a TransformDecorator to transform the shape.
			TransformDecorator transformer = new TransformDecorator();
			transformer.AffectsLayout = false;
			
			// Create the scale transformation.
			RotateTransform myRotateTransform = new RotateTransform(45, new Point(110,110));

			// Set the TransformDecorator.Transform property
			transformer.Transform = myRotateTransform;

			// Add box2 as a child of the TransformDecorator and add
			// the TransformDecorator to the Canvas.
			transformer.Child = box2;
			myCanvas.Children.Add(transformer);
	
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
