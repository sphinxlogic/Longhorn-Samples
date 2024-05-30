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

			// Create a canvas to contain the shapes and
			// add the canvas to the window.
			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;

			System.Windows.Shapes.Rectangle myRectangle = 
				new System.Windows.Shapes.Rectangle();
			myRectangle.RectangleTop = new System.Windows.Length(100);
			myRectangle.RectangleLeft = new System.Windows.Length(10);
			myRectangle.RectangleWidth = new System.Windows.Length(50);
			myRectangle.RectangleHeight = new System.Windows.Length(50);
			myRectangle.Fill = 
				new System.Windows.Media.RadialGradientBrush(
					System.Windows.Media.Colors.Yellow, 
					System.Windows.Media.Colors.Orange);

			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			myLengthAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			myLengthAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;
			myLengthAnimation.AutoReverse = true;
			myLengthAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(2000));
			
			myLengthAnimation.InterpolationMethod = 
				System.Windows.Media.Animation.InterpolationMethod.Spline;

			System.Windows.Media.Animation.LengthKeyFrame aKeyFrame = 
				new System.Windows.Media.Animation.LengthKeyFrame();
			aKeyFrame.Value = new System.Windows.Length(10);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(0.0);
			aKeyFrame.KeySpline = new System.Windows.Media.Animation.KeySpline(
				new System.Windows.Point(0.25,0.5), 
				new System.Windows.Point(0.75,1));
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

			aKeyFrame.Value = new System.Windows.Length(500);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(1.0);
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

			myRectangle.AddAnimation(
				System.Windows.Shapes.Rectangle.RectangleLeftProperty, myLengthAnimation);


			// Create another rectangle that has a non-splined animation
			// for comparison.
			System.Windows.Shapes.Rectangle otherRectangle = 
				new System.Windows.Shapes.Rectangle();
			otherRectangle.RectangleLeft = new System.Windows.Length(10);
			otherRectangle.RectangleTop = new System.Windows.Length(150);
			otherRectangle.RectangleWidth = new System.Windows.Length(50);
			otherRectangle.RectangleHeight = new System.Windows.Length(50);
			otherRectangle.Fill = System.Windows.Media.Brushes.Blue;
			otherRectangle.Opacity = 0.5;

			System.Windows.Media.Animation.LengthAnimation otherAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			otherAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			otherAnimation.From = new System.Windows.Length(10);
			otherAnimation.To = new System.Windows.Length(500);
			otherAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;
			otherAnimation.AutoReverse = true;
			otherAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(2000));
			
			otherRectangle.AddAnimation(
				System.Windows.Shapes.Rectangle.RectangleLeftProperty, otherAnimation);

			myCanvas.Children.Add(myRectangle);
			myCanvas.Children.Add(otherRectangle);


			// Label the rectangles.
			System.Windows.Controls.Text myText = 
				new System.Windows.Controls.Text();
			myText.FontSize = new System.Windows.FontSize(14);
			System.Windows.Controls.Canvas.SetLeft(
				myText, new System.Windows.Length(10));
			System.Windows.Controls.Canvas.SetTop(
				myText, new System.Windows.Length(100));
			myText.TextContent = "Splined Animation";
			myText.FontWeight = System.Windows.FontWeight.ExtraBold;
			myCanvas.Children.Add(myText);

			myText = new System.Windows.Controls.Text();
			myText.FontSize = new System.Windows.FontSize(14);
			System.Windows.Controls.Canvas.SetLeft(
				myText, new System.Windows.Length(10));
			System.Windows.Controls.Canvas.SetTop(
				myText, new System.Windows.Length(150));
			myText.TextContent = "Animation Without Spline";
			myText.FontWeight = System.Windows.FontWeight.ExtraBold;
			myCanvas.Children.Add(myText);

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
