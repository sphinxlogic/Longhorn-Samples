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

			// Create a canvas and add it to the window.
			System.Windows.Controls.Canvas myCanvas = new System.Windows.Controls.Canvas();
			myCanvas.Background = System.Windows.Media.Brushes.White;
			mainWindow.Content = myCanvas;
			
			// Create a rectangle.
			System.Windows.Shapes.Rectangle myRect = new System.Windows.Shapes.Rectangle();
			myRect.RectangleHeight = new System.Windows.Length(100);
			myRect.RectangleWidth = new System.Windows.Length(100);
			myRect.RectangleTop = new System.Windows.Length(20);
			myRect.RectangleLeft = new System.Windows.Length(20);

			// Create and animate a brush.
			System.Windows.Media.SolidColorBrush mySolidColorBrush = 
				new System.Windows.Media.SolidColorBrush();
			mySolidColorBrush.Color = System.Windows.Media.Colors.Blue;

			System.Windows.Media.Animation.ColorAnimation myColorAnimation = 
			new System.Windows.Media.Animation.ColorAnimation();
			myColorAnimation.From = System.Windows.Media.Colors.Red;
			myColorAnimation.To = System.Windows.Media.Colors.Yellow;
			myColorAnimation.InterpolationMethod = 
				System.Windows.Media.Animation.InterpolationMethod.Linear;
			myColorAnimation.Duration = new System.Windows.Media.Animation.Time(2000);
			myColorAnimation.AutoReverse = true;
			myColorAnimation.RepeatCount = 10;
			myColorAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(1000));
			myColorAnimation.Fill = System.Windows.Media.Animation.TimeFill.Freeze;

			mySolidColorBrush.ColorAnimations.Add(myColorAnimation);
			
			myRect.Fill = mySolidColorBrush;


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
