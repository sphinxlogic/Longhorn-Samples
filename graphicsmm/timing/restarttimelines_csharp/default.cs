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
		System.Windows.Media.Animation.Timeline myTimeline;

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

			
			myTimeline = new Timeline();
			myTimeline.Duration = Time.Indefinite;

			// Important: Set the StatusOfNextUse of the Timeline to ChangeableReference
			// so that a reference to the original Timeline is maintained.
			myTimeline.StatusOfNextUse = UseStatus.ChangeableReference;
	
			Line myLine = new Line();
			myLine.X1 = new Length(20);
			myLine.Y1 = new Length(50);
			myLine.X2 = new Length(30);
			myLine.Y2 = new Length(50);
			myLine.Stroke = Brushes.Blue;
			myLine.StrokeThickness = new Length(10);

			Line myOtherLine = new Line();
			myOtherLine.X1 = new Length(20);
			myOtherLine.Y1 = new Length(80);
			myOtherLine.X2 = new Length(30);
			myOtherLine.Y2 = new Length(80);
			myOtherLine.Stroke = Brushes.Blue;
			myOtherLine.StrokeThickness = new Length(10);


			// Create an animation that runs for five seconds.
			LengthAnimation myLengthAnimation1 = new LengthAnimation();
			myLengthAnimation1.Duration = new Time(5000);
			myLengthAnimation1.From = new Length(30);
			myLengthAnimation1.To = new Length(300);
			myLengthAnimation1.RepeatDuration = Time.Indefinite;

			// Create a setter to contain the animation.
			Setter myAnimationSetter1 = new Setter(myLine, Line.X2Property);
			myAnimationSetter1.Children.Add(myLengthAnimation1);

			// Connect the setter to the timeline.
			myTimeline.Children.Add(myAnimationSetter1);

			// Change the duration of the LengthAnimation for the other line.
			// All other values are identical.
			LengthAnimation myLengthAnimation2 = new LengthAnimation();
			myLengthAnimation2.Duration = new Time(3000);
			myLengthAnimation2.From = new Length(30);
			myLengthAnimation2.To = new Length(300);
			myLengthAnimation2.RepeatDuration = Time.Indefinite;

			// Create a setter to contain the animation.
			Setter myAnimationSetter2 = new Setter(myOtherLine, Line.X2Property);
			myAnimationSetter2.Children.Add(myLengthAnimation2);

			// Connect the Setter to the timeline.
			myTimeline.Children.Add(myAnimationSetter2);

			Button myButton = new Button();
			myButton.Width = new Length(100);
			myButton.Height = new Length(25);
			Canvas.SetLeft(myButton, new Length(20));
			Canvas.SetTop(myButton, new Length(150));
			myButton.Content = "Restart";

			Button myOtherButton = new Button();
			myOtherButton.Width = new Length(100);
			myOtherButton.Height = new Length(25);
			Canvas.SetLeft(myOtherButton, new Length(140));
			Canvas.SetTop(myOtherButton, new Length(150));
			myOtherButton.Content = "Stop";


			myButton.Click += RestartTimelines;
			myOtherButton.Click += StopTimelines;

			myCanvas.Children.Add(myLine);
			myCanvas.Children.Add(myOtherLine);
			myCanvas.Children.Add(myButton);
			myCanvas.Children.Add(myOtherButton);

			// Hook up the timeline to the timing tree.
			Timeline.Root.Children.Add(myTimeline);
			mainWindow.Show();
 
			
		}

		public void RestartTimelines(object sender, System.Windows.Controls.ClickEventArgs e){
	
			
			myTimeline.BeginIn(0);
			
		}


		public void StopTimelines(object sender, System.Windows.Controls.ClickEventArgs e){

			myTimeline.EndIn(0);

			
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
