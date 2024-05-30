//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace restartanimation_code_csharp
{
    /// <summary>
    /// Demonstrates how to interactively control an animation.
    /// </summary>

    public partial class MyWindow : Window
    {

		private System.Windows.Media.Animation.DoubleAnimation myAnimation;
       
        private void WindowLoaded(object sender, EventArgs e) {
			initializeExample();
		}

		private void initializeExample()
		{
			// Create an image to animate.
			System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();

			myImage.Width = new System.Windows.Length(300);
			myImage.Height = new System.Windows.Length(300);

			// Load the image file.
			myImage.Source = System.Windows.Media.ImageData.Create("data\\Waterlilies.jpg");

			// Initialize the DoubleAnimation and animate the opacity of the image.
			myAnimation = new System.Windows.Media.Animation.DoubleAnimation();
			myAnimation.To = 0;
			myAnimation.Duration = new System.Windows.Media.Animation.Time(5000);
			myAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;

			// Important: set the StatusOfNextUse of the animation to ChangeableReference.
			myAnimation.StatusOfNextUse = System.Windows.UseStatus.ChangeableReference;

			// Apply the animation to the image's opacity property.
			myImage.AddAnimation(System.Windows.Controls.Image.OpacityProperty, myAnimation);
			mainPanel.Children.Add(myImage);

			// Create some buttons to control the animation.
			System.Windows.Controls.Button restartButton = new System.Windows.Controls.Button();

			restartButton.Content = "Restart";
			restartButton.Click += new System.Windows.Controls.ClickEventHandler(restartAnimation);

			System.Windows.Controls.Button pauseButton = new System.Windows.Controls.Button();

			pauseButton.Content = "Pause";
			pauseButton.Click += new System.Windows.Controls.ClickEventHandler(pauseAnimation);

			System.Windows.Controls.Button resumeButton = new System.Windows.Controls.Button();

			resumeButton.Content = "Resume";
			resumeButton.Click += new System.Windows.Controls.ClickEventHandler(resumeAnimation);

			System.Windows.Controls.Button stopButton = new System.Windows.Controls.Button();

			stopButton.Content = "Stop";
			stopButton.Click += new System.Windows.Controls.ClickEventHandler(stopAnimation);

			// Set the buttons' layout and add them to a panel.	
			System.Windows.Controls.DockPanel buttonPanel = new System.Windows.Controls.DockPanel();

			System.Windows.Controls.DockPanel.SetDock(restartButton, System.Windows.Controls.Dock.Top);
			System.Windows.Controls.DockPanel.SetDock(pauseButton, System.Windows.Controls.Dock.Top);
			System.Windows.Controls.DockPanel.SetDock(resumeButton, System.Windows.Controls.Dock.Top);
			System.Windows.Controls.DockPanel.SetDock(stopButton, System.Windows.Controls.Dock.Top);
			buttonPanel.Children.Add(restartButton);
			buttonPanel.Children.Add(pauseButton);
			buttonPanel.Children.Add(resumeButton);
			buttonPanel.Children.Add(stopButton);
			mainPanel.Children.Add(buttonPanel);
		}

		private void restartAnimation(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			myAnimation.BeginIn(0);
		}

		private void pauseAnimation(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			myAnimation.Pause();
		}

		private void resumeAnimation(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			myAnimation.Resume();
		}

		private void stopAnimation(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			myAnimation.EndIn(0);
		}
	}
}