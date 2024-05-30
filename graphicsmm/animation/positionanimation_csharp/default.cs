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

			// Create a canvas to contain the rectangles and
			// add the canvas to the window.
			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;
	
			System.Windows.Shapes.Rectangle myRectangle = 
			new System.Windows.Shapes.Rectangle();
			myRectangle.RectangleLeft = new System.Windows.Length(10);
			myRectangle.RectangleTop = new System.Windows.Length(10);
			myRectangle.RectangleWidth = new System.Windows.Length(50);
			myRectangle.RectangleHeight = new System.Windows.Length(100);
			myRectangle.StrokeThickness = new System.Windows.Length(10);
			myRectangle.Stroke = System.Windows.Media.Brushes.Blue;
			myRectangle.Fill = System.Windows.Media.Brushes.LimeGreen;
			
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			myLengthAnimation.From = new System.Windows.Length(10);
			myLengthAnimation.To = new System.Windows.Length(150);
			myLengthAnimation.Begin = 
				new System.Windows.Media.Animation.Time(10000);
			myLengthAnimation.Duration = 
				new System.Windows.Media.Animation.Time(10000);
			myLengthAnimation.Fill = 
				System.Windows.Media.Animation.TimeFill.Freeze;
			
			myRectangle.AddAnimation(
				System.Windows.Shapes.Rectangle.RectangleLeftProperty, 
				myLengthAnimation);

			myCanvas.Children.Add(myRectangle);	
			
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
