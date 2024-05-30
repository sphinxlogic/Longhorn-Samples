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

			System.Windows.Controls.Canvas myCanvas = 
				new System.Windows.Controls.Canvas();
			mainWindow.Content = myCanvas;
	
			// Create and set the Button.
			System.Windows.Controls.Button aButton = 
				new System.Windows.Controls.Button();
			System.Windows.Controls.Canvas.SetLeft(
				aButton, new System.Windows.Length(20));
			System.Windows.Controls.Canvas.SetTop(
				aButton, new System.Windows.Length(20));
			aButton.Width = new System.Windows.Length(200);
			aButton.Height = new System.Windows.Length(30);
			
			aButton.Content = "A Button";


			// Animate the Button's Width.
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			myLengthAnimation.To = new System.Windows.Length(50);
			myLengthAnimation.Duration = 
				new System.Windows.Media.Animation.Time(5000);
			myLengthAnimation.AutoReverse = true;
			myLengthAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;
			
			aButton.AddAnimation(System.Windows.Controls.Button.WidthProperty, myLengthAnimation);
			

			// Add the Button to the Canvas.
			myCanvas.Children.Add(aButton);

			//Create and set the second Button.
			System.Windows.Controls.Button anotherButton = 
				new System.Windows.Controls.Button();
			System.Windows.Controls.Canvas.SetLeft(
				anotherButton, new System.Windows.Length(20));
			System.Windows.Controls.Canvas.SetTop(
				anotherButton, new System.Windows.Length(70));
			anotherButton.Width = new System.Windows.Length(200);
			anotherButton.Height = new System.Windows.Length(30);
			anotherButton.Content = "Another Button";
			
			// Create and animate a Brush to set the Button's
			// fill.
			System.Windows.Media.SolidColorBrush myBrush = 
				new System.Windows.Media.SolidColorBrush();
			myBrush.Color = System.Windows.Media.Colors.Blue;
			
			System.Windows.Media.Animation.ColorAnimation myColorAnimation = 
				new System.Windows.Media.Animation.ColorAnimation();
			myColorAnimation.From = System.Windows.Media.Colors.Blue;
			myColorAnimation.To = System.Windows.Media.Colors.Red;
			myColorAnimation.Duration = 
				new System.Windows.Media.Animation.Time(7000);
			myColorAnimation.AutoReverse = true;
			myColorAnimation.RepeatDuration = 
				System.Windows.Media.Animation.Time.Indefinite;

			myBrush.ColorAnimations.Add(myColorAnimation);
			
			anotherButton.Background = myBrush;

			myCanvas.Children.Add(anotherButton);
			
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
