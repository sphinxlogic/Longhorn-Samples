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
			mainWindow = new System.Windows.Window();

			// Create a canvas to contain the shapes.
			Canvas myCanvas = new Canvas();
			mainWindow.Content = myCanvas;
	
			// Create a PointCollection to contain the points of the
			// Polygon shapes.
			PointCollection myPointCollection = new PointCollection();
			myPointCollection.Add(new Point(176.5, 50));
			myPointCollection.Add(new Point(189, 155));
			myPointCollection.Add(new Point(286, 113));
			myPointCollection.Add(new Point(201, 177));
			myPointCollection.Add(new Point(286, 240));
			myPointCollection.Add(new Point(189, 198));
			myPointCollection.Add(new Point(176, 304));
			myPointCollection.Add(new Point(163, 198));
			myPointCollection.Add(new Point(66, 240));
			myPointCollection.Add(new Point(151, 177));
			myPointCollection.Add(new Point(66, 113));
			myPointCollection.Add(new Point(163, 155));

			// Create the first Polygon.
			Polygon firstPolygon = new Polygon();
			firstPolygon.Points = myPointCollection;
			firstPolygon.Stroke = Brushes.Black;
			firstPolygon.StrokeThickness = new Length(2);
			myCanvas.Children.Add(firstPolygon);
	
			// Create the second Polygon. This polygon contains the same
			// points as the first, but is scaled and translated.
			Polygon secondPolygon = new Polygon();
			secondPolygon.Points = myPointCollection;
			secondPolygon.Stroke = Brushes.Blue;
			secondPolygon.StrokeThickness = new Length(2);
			
			// Add a semi-transparent gradient fill to make the shape stand out.
			RadialGradientBrush myGradient = new RadialGradientBrush(Colors.Blue, Colors.LimeGreen);
			myGradient.Opacity = 0.4;
			secondPolygon.Fill = myGradient;
			
			// Create a TransformDecorator to transform secondPolygon.
			TransformDecorator transformer = new TransformDecorator();
			transformer.AffectsLayout = false;
			
			// Create the scale and translate transformations.
			ScaleTransform myScaleTransform = new ScaleTransform(1, 0.5);
			TranslateTransform myTranslateTransform = new TranslateTransform(150, 0);

			//Create a collection to contain the transformations.
			TransformCollection transformations = new TransformCollection();

			transformations.Add(myScaleTransform);
			transformations.Add(myTranslateTransform);

			transformer.Transform = transformations;

			// Add secondPolygon as a child of the TransformDecorator.
			transformer.Child = secondPolygon;

			// Add the decorator to the Canvas.
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
