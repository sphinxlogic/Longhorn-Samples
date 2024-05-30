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

			// Create a canvas to contain the rectangle.
			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;

			System.Windows.Shapes.Rectangle myRectangle 
				= new System.Windows.Shapes.Rectangle();
			myRectangle.RectangleTop = new System.Windows.Length(100);
			myRectangle.RectangleLeft = new System.Windows.Length(10);
			myRectangle.RectangleWidth = new System.Windows.Length(50);
			myRectangle.RectangleHeight = new System.Windows.Length(50);
			System.Windows.Media.SolidColorBrush myBrush = 
				new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
			myBrush.Opacity = 0.4;
			myRectangle.Fill = myBrush;
			myRectangle.Stroke = System.Windows.Media.Brushes.Black;
			myRectangle.StrokeThickness = new System.Windows.Length(5);

			
			// Create a LengthAnimation.
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			myLengthAnimation.Duration = 
				new System.Windows.Media.Animation.Time(15000);
			myLengthAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;
			myLengthAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(2000));

			// Create KeyFrames for the animation.
			
			System.Windows.Media.Animation.LengthKeyFrame aKeyFrame = 
				new System.Windows.Media.Animation.LengthKeyFrame();
			aKeyFrame.Value = new System.Windows.Length(10);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(0.0);
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

			aKeyFrame.Value = new System.Windows.Length(500);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(0.5);
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

			aKeyFrame.Value = new System.Windows.Length(200);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(0.75);
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

			aKeyFrame.Value = new System.Windows.Length(350);
			aKeyFrame.KeyTime = new System.Windows.Media.Animation.KeyTime(1.0);
			myLengthAnimation.KeyFrames.Add(aKeyFrame);

				
			myRectangle.AddAnimation(
				System.Windows.Shapes.Rectangle.RectangleLeftProperty, myLengthAnimation);
			
			// Mark the KeyFrame points for illustrative purposes.

			// Mark the first KeyFrame location.
			System.Windows.Shapes.Ellipse marker = 
				new System.Windows.Shapes.Ellipse();
			marker.RadiusX = new System.Windows.Length(10);
			marker.RadiusY = new System.Windows.Length(10);
			marker.CenterX = new System.Windows.Length(35);
			marker.CenterY = new System.Windows.Length(125);
			marker.Fill = System.Windows.Media.Brushes.Black;

			System.Windows.Controls.Text myText = 
					new System.Windows.Controls.Text();
			myText.TextContent = "1. Value = 10 Time = 0";
			System.Windows.Controls.Canvas.SetLeft(myText, new System.Windows.Length(10));
			System.Windows.Controls.Canvas.SetTop(myText, new System.Windows.Length(160));
			myCanvas.Children.Add(marker);
			myCanvas.Children.Add(myText);

			// Mark the second KeyFrame location.
			marker = new System.Windows.Shapes.Ellipse();
			marker.RadiusX = new System.Windows.Length(10);
			marker.RadiusY = new System.Windows.Length(10);
			marker.CenterX = new System.Windows.Length(525);
			marker.CenterY = new System.Windows.Length(125);
			marker.Fill = System.Windows.Media.Brushes.Gray;
			marker.Opacity = 1;
			
			myText = new System.Windows.Controls.Text();
			myText.TextContent = "2. Value = 500 Time = 0.5";
			System.Windows.Controls.Canvas.SetLeft(myText, new System.Windows.Length(500));
			System.Windows.Controls.Canvas.SetTop(myText, new System.Windows.Length(160));
			myCanvas.Children.Add(marker);
			myCanvas.Children.Add(myText);

			// Mark the third KeyFrame location
			marker = new System.Windows.Shapes.Ellipse();
			marker.RadiusX = new System.Windows.Length(10);
			marker.RadiusY = new System.Windows.Length(10);
			marker.CenterX = new System.Windows.Length(225);
			marker.CenterY = new System.Windows.Length(125);
			marker.Fill = System.Windows.Media.Brushes.LightGray;
			myText = new System.Windows.Controls.Text();
			myText.TextContent = "3. Value = 200 Time = 0.75";
			System.Windows.Controls.Canvas.SetLeft(myText, new System.Windows.Length(200));
			System.Windows.Controls.Canvas.SetTop(myText, new System.Windows.Length(160));
			myCanvas.Children.Add(marker);
			myCanvas.Children.Add(myText);

			// Mark the final KeyFrame location.
			marker = new System.Windows.Shapes.Ellipse();
			marker.RadiusX = new System.Windows.Length(10);
			marker.RadiusY = new System.Windows.Length(10);
			marker.CenterX = new System.Windows.Length(375);
			marker.CenterY = new System.Windows.Length(125);
			marker.Fill = 
				new System.Windows.Media.RadialGradientBrush(
					System.Windows.Media.Colors.White, 
					System.Windows.Media.Colors.LightGray);
			myText = new System.Windows.Controls.Text();
			myText.TextContent = "4. Value = 350 Time = 1";
			System.Windows.Controls.Canvas.SetLeft(myText, 
				new System.Windows.Length(350));
			System.Windows.Controls.Canvas.SetTop(myText, 
				new System.Windows.Length(160));
			myCanvas.Children.Add(marker);
			myCanvas.Children.Add(myText);

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
