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

			// Create a canvas to contain the ellipses and
			// add the canvas to the window.
			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;
	
			System.Windows.Shapes.Line myLine = 
				new System.Windows.Shapes.Line();
			myLine.X1 = new System.Windows.Length(10);
			myLine.Y1 = new System.Windows.Length(20);
			myLine.X2 = new System.Windows.Length(50);
			myLine.Y2 = new System.Windows.Length(20);
			myLine.StrokeThickness = new System.Windows.Length(10);
			myLine.Stroke = System.Windows.Media.Brushes.Blue;
			
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			myLengthAnimation.From = new System.Windows.Length(30);
			myLengthAnimation.To = new System.Windows.Length(300);
			myLengthAnimation.Duration = 
				new System.Windows.Media.Animation.Time(10000);
			myLengthAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;
			
			myLine.AddAnimation(
				System.Windows.Shapes.Line.X2Property, myLengthAnimation);

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
