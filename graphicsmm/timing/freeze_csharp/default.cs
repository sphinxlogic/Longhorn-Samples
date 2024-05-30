using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

			// Create a canvas and add it to the window.
			Canvas myCanvas = new Canvas();
			myCanvas.Background = Brushes.White;
			mainWindow.Content = myCanvas;
			
			Rectangle myRect = new Rectangle();
			myRect.RectangleHeight = new Length(100);
			myRect.RectangleWidth = new Length(100);
			myRect.RectangleTop = new Length(20);
			myRect.RectangleLeft = new Length(20);

			myRect.Fill = new RadialGradientBrush(Colors.Blue, Colors.Green);

			// Create an animation and set its Fill to freeze.
			LengthAnimation myLengthAnimation = new LengthAnimation();
			myLengthAnimation.Duration = new Time(5000);
			myLengthAnimation.To = new Length(30);
			myLengthAnimation.Fill = TimeFill.Freeze;
		
			myRect.AddAnimation(Rectangle.RectangleWidthProperty, myLengthAnimation);

			myCanvas.Children.Add(myRect);

			
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
