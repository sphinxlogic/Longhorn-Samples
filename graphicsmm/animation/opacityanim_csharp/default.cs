
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
			mainWindow.Text = "Opacity Animation Example";

			// Create a canvas to contain the shapes.
			System.Windows.Controls.Canvas myCanvas = 
			    new System.Windows.Controls.Canvas();
			
			mainWindow.Content =  myCanvas;

			System.Windows.Shapes.Rectangle myRectangle = 
				new System.Windows.Shapes.Rectangle();
			myRectangle.RectangleTop = new System.Windows.Length(35);
			myRectangle.RectangleLeft = new System.Windows.Length(35);
			myRectangle.RectangleWidth = new System.Windows.Length(50);
			myRectangle.RectangleHeight = new System.Windows.Length(50);
			myRectangle.Fill = System.Windows.Media.Brushes.Blue;
			
			System.Windows.Shapes.Rectangle myAnimatedRectangle = 
				new System.Windows.Shapes.Rectangle();
			myAnimatedRectangle.RectangleTop = new System.Windows.Length(10);
			myAnimatedRectangle.RectangleLeft = new System.Windows.Length(10);
			myAnimatedRectangle.RectangleWidth = new System.Windows.Length(50);
			myAnimatedRectangle.RectangleHeight = new System.Windows.Length(50);
			myAnimatedRectangle.Fill = System.Windows.Media.Brushes.Red;

			// Create an animation to animate the opacity of the second rectangle.
			System.Windows.Media.Animation.DoubleAnimation myDoubleAnimation =
				 new System.Windows.Media.Animation.DoubleAnimation(
														1, // Starting Value
														0, // End Value
														new System.Windows.Media.Animation.Time(5000) // Duration
														);
			myDoubleAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			
			// Apply the animation to the rectangle's opacity property.
			myAnimatedRectangle.AddAnimation(
				System.Windows.Shapes.Rectangle.OpacityProperty, 
				myDoubleAnimation);

			myCanvas.Children.Add(myRectangle);
			myCanvas.Children.Add(myAnimatedRectangle);
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
