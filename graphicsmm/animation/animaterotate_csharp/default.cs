/*

This sample shows how to create and use an animated RotateTransform.

*/

using System;

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
			mainWindow.Content = myCanvas;

			System.Windows.Shapes.Rectangle myRectangle = 
				new System.Windows.Shapes.Rectangle();
			myRectangle.RectangleTop = new System.Windows.Length(100);
			myRectangle.RectangleLeft = new System.Windows.Length(100);
			myRectangle.RectangleWidth = new System.Windows.Length(50);
			myRectangle.RectangleHeight = new System.Windows.Length(50);
			myRectangle.Fill = System.Windows.Media.Brushes.Blue;
			myRectangle.Stroke = System.Windows.Media.Brushes.Black;
			myRectangle.StrokeThickness = new System.Windows.Length(5);

			// Create and animate a RotateTransform.
			System.Windows.Media.RotateTransform myRotateTransform = 
			new System.Windows.Media.RotateTransform();
			myRotateTransform.Center = new System.Windows.Point(100, 100);
			myRotateTransform.Angle = 0;
			
			System.Windows.Media.Animation.DoubleAnimation myDoubleAnimation = 
				new System.Windows.Media.Animation.DoubleAnimation();
			myDoubleAnimation.From = 0;
			myDoubleAnimation.To = 360;
			myDoubleAnimation.Duration = new System.Windows.Media.Animation.Time(4000);
			myDoubleAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			myDoubleAnimation.Begin = new System.Windows.Media.Animation.Time(1000);

			myRotateTransform.AngleAnimations.Add(myDoubleAnimation);

			// Apply the RotateTransform to the TransformDecorator,
			// and use the TransformDecorator to transform the
			// rectangle.
			System.Windows.Controls.TransformDecorator myTransformDecorator = 
			new System.Windows.Controls.TransformDecorator();
			myTransformDecorator.Transform = myRotateTransform;
			myTransformDecorator.Child = myRectangle;
			
			myCanvas.Children.Add(myTransformDecorator);

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
