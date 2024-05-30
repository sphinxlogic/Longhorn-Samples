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
	
			Rectangle firstRectangle = new Rectangle();
			firstRectangle.RectangleTop = new Length(50);
			firstRectangle.RectangleLeft = new Length(50);
			firstRectangle.RectangleHeight = new Length(50);
			firstRectangle.RectangleWidth = new Length(50);
			firstRectangle.Fill = Brushes.Red;
			firstRectangle.Stroke = Brushes.Black;

			myCanvas.Children.Add(firstRectangle);

			Rectangle secondRectangle = new Rectangle();
			secondRectangle.RectangleTop = new Length(50);
			secondRectangle.RectangleLeft = new Length(150);
			secondRectangle.RectangleHeight = new Length(50);
			secondRectangle.RectangleWidth = new Length(50);
			secondRectangle.Fill = Brushes.Yellow;
			secondRectangle.Stroke = Brushes.Black;

			TransformDecorator transformation = new TransformDecorator();
			transformation.AffectsLayout = false;

			ScaleTransform myScaleTransform = new ScaleTransform();
			myScaleTransform.ScaleY = 2.0;

			transformation.Transform = myScaleTransform;
			transformation.Child = secondRectangle;
			myCanvas.Children.Add(transformation);
			
			Rectangle thirdRectangle = new Rectangle();
	
			thirdRectangle.RectangleTop = new Length(50);
			thirdRectangle.RectangleLeft = new Length(250);
			thirdRectangle.RectangleHeight = new Length(50);
			thirdRectangle.RectangleWidth = new Length(50);
		
			thirdRectangle.Fill = Brushes.Blue;
			thirdRectangle.Stroke = Brushes.Black;

			myScaleTransform = new ScaleTransform();
			myScaleTransform.Center = new Point(0, 50);
			myScaleTransform.ScaleY = 2.0;

			TransformDecorator secondTransformation = new TransformDecorator();

			secondTransformation.AffectsLayout = false;
			secondTransformation.Transform = myScaleTransform;
			secondTransformation.Child = thirdRectangle;
			myCanvas.Children.Add(secondTransformation);

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
