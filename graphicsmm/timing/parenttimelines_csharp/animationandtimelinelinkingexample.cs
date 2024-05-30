using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Documents;

namespace TimingExamples 
{
	/// <summary>
	/// This example demonstrates how to use a timeline to control
	/// other timelines and animations.
	/// </summary>
	public class AnimationAndTimelineLinkingExample : GridPanel
	{
		System.Windows.Media.Animation.Timeline myTimeline;
		Button restartButton;
		Button pauseButton;
		Button stopButton;


		public AnimationAndTimelineLinkingExample()
		{
			this.Columns = 2;
			this.Width = new Length(300);

			TextPanel explanationText = new TextPanel();
			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent =
				"This example demonstrates how to " +
				"link animations and timelines together, using" +
				" a Timeline to control another timeline and two animations. " +
				"The restart, pause, and stop buttons are connected to " +
				"a timeline that is the parent of the two animations.";
			this.Children.Add(explanationText);

			linkAnimationsAndTimelines();
		}

		/// <summary>
		///  This sample creates two animations and links
		///	 them to the same timeline (anotherTimeline);
		///  This timeline is then used to control
		///  the two animations.
		/// </summary>
		private void linkAnimationsAndTimelines()
		{
			
			// Initialize the main Timeline.
			myTimeline = new Timeline();

			// Set the StatusOfNextUse of the Timelines so that a handle
			// to the original object is retained even after it's used.
			// This is important if you wish to dynamically restart or
			// end the Timeline at a later point.
			myTimeline.StatusOfNextUse = UseStatus.ChangeableReference;

			// Create another timeline and link it to myTimeline.
			Timeline anotherTimeline = new Timeline();
			anotherTimeline.StatusOfNextUse = UseStatus.ChangeableReference;
			myTimeline.Children.Add(anotherTimeline);

			// Create an animation and connect it to the second timeline.
			ColorAnimation myColorAnimation = new ColorAnimation(Colors.Red, new Time(10000));
			myColorAnimation.StatusOfNextUse = UseStatus.ChangeableReference;
			myColorAnimation.RepeatDuration = Time.Indefinite;
			anotherTimeline.Children.Add(myColorAnimation);

			// Apply the animation to a brush.
			SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
			myBrush.ColorAnimations.Add(myColorAnimation);

			// Use the animated brush to fill a rectangle.
			Rectangle myRectangle = new Rectangle();
			myRectangle.RectangleHeight = new Length(100);
			myRectangle.RectangleWidth = new Length(100);
			myRectangle.Fill = myBrush;
			
			// Create a button an animate its opacity.
			Button aButton = new Button();
			aButton.Content = "aButton";
			aButton.Width = new Length(100);
			aButton.Opacity = 1;

			DoubleAnimation myDoubleAnimation = new DoubleAnimation(0, new Time(10000));
			myDoubleAnimation.RepeatDuration = Time.Indefinite;

			// If the animation is going to be used to set a dependency property,
			// you must wrap it in a Setter.
			Setter mySetter = new Setter(aButton, UIElement.OpacityProperty);
			mySetter.Children.Add(myDoubleAnimation);

			// Link the animation to the timeline.
			anotherTimeline.Children.Add(mySetter);		

			// Connect the parent timeline to the timing tree.
			Timeline.Root.Children.Add(myTimeline);

			// Add some buttons to control the Timeline.
			restartButton = new Button();
			pauseButton = new Button();
			stopButton = new Button();
			restartButton.Content = "Restart";
			pauseButton.Content = "Pause";
			stopButton.Content = "Stop";
			restartButton.Click += new ClickEventHandler(restartButton_Click);
			pauseButton.Click += new ClickEventHandler(pauseButton_Click);
			stopButton.Click += new ClickEventHandler(stopButton_Click);


			GridPanel myGridPanel = new GridPanel();
			myGridPanel.Columns = 2;
			myGridPanel.Width = new Length(250);
			myGridPanel.CellSpacing = new Length(10);
			myGridPanel.Children.Add(myRectangle);
			myGridPanel.Children.Add(aButton);

			FlowPanel f = new FlowPanel();
			f.Children.Add(restartButton);
			f.Children.Add(pauseButton);
			f.Children.Add(stopButton);
			GridPanel.SetColumnSpan(f, 2);
			myGridPanel.Children.Add(f);

			this.Children.Add(myGridPanel);
		}

		/// <summary>
		///  Restarts the timelines.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void restartButton_Click(object sender, ClickEventArgs args)
		{
			myTimeline.BeginIn(0);

			// Restarting a timeline also unpauses it.
			pauseButton.Content = "Pause";
		}

		/// <summary>
		///  Pauses or resumes the timelines.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void pauseButton_Click(object sender, ClickEventArgs args)
		{
			// Test to see if the timeline is already paused.
			if (myTimeline.IsPaused)
			{
				myTimeline.Resume();
				((Button)sender).Content = "Pause";
			}
			else
			{
				myTimeline.Pause();
				((Button)sender).Content = "Resume";
			}
		}

		/// <summary>
		/// Stops the timelines.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void stopButton_Click(object sender, ClickEventArgs args)
		{
			myTimeline.EndIn(0);
		}
	

	}
}
