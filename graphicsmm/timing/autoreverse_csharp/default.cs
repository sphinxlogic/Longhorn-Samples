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
			
			Line myLine = new Line();
			myLine.X1 = new Length(20);
			myLine.Y1 = new Length(50);
			myLine.X2 = new Length(30);
			myLine.Y2 = new Length(50);
			myLine.Stroke = Brushes.Blue;
			myLine.StrokeThickness = new Length(10);

			// Create an animation.
			LengthAnimation myLengthAnimation = new LengthAnimation();
			myLengthAnimation.Duration = new Time(10000);
			myLengthAnimation.From = new Length(10);
			myLengthAnimation.To = new Length(300);
			myLengthAnimation.RepeatDuration = Time.Indefinite;
			myLengthAnimation.AutoReverse = true;

			myLine.AddAnimation(Line.X2Property, myLengthAnimation);

			myCanvas.Children.Add(myLine);
	
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
