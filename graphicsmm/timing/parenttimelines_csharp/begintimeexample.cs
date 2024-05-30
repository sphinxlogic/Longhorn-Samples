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
	/// This example demonstrates the behavior of the
	/// Begin attribute.
	/// </summary>
	public class BeginTimeExample : GridPanel
	{
		System.Windows.Media.Animation.Timeline beginExampleTimeline;
		// lengthAnimationTracker is used to display the status of the LengthAnimation.
		Text lengthAnimationTracker;

		public BeginTimeExample()
		{
			// Initialize layout and provide a description.
			this.Columns = 2;
			this.Width = new Length(300);
			TextPanel explanationText = new TextPanel();
			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent = 
				"This example demonstrates the behavior of the Begin property." +
				" A LengthAnimation's Begin property is set to 10 seconds, and " +
				" its speed property is set to 2. The animation begins after " +
				" 10 seconds (not 5), because the Begin property is measured " +
				" in the animation's parent's time. In this case, the animation's " +
				" parent has a Speed of 1.";
			this.Children.Add(explanationText);
			
			// The interesting code begins here.
			initalizeBeginTimeExample();
		}

	
		private void initalizeBeginTimeExample()
		{
			// Set a LengthAnimation to begin after 10 seconds.
			// Even though its speed is set to 2, the animation
			// starts after 10 seconds, not 5, because the Begin
			// time is relative to the animation's parent timeline
			// (in this case, the document timeline).
			LengthAnimation myLengthAnimation = new LengthAnimation();
			myLengthAnimation.Speed = 2;
			myLengthAnimation.Begin = new TimeSyncValue(new Time(10000));
			myLengthAnimation.Duration = new Time(5000);
			myLengthAnimation.To = new Length(150);
			myLengthAnimation.Fill = TimeFill.Freeze;
			myLengthAnimation.Begun += new EventHandler(lengthAnimation_Begun);
			myLengthAnimation.Ended += new EventHandler(lengthAnimation_Ended);

			// Create a line to demonstrate the animation.
			Canvas myCanvas = new Canvas();
			myCanvas.Width = new Length(200);
			Line myLine = new Line();
			myLine.Stroke = Brushes.MediumBlue;
			myLine.StrokeThickness = new Length(10);
			myLine.X1 = new Length(10);
			myLine.X2 = new Length(15);
			myLine.Y1 = new Length(10);
			myLine.Y2 = new Length(10);

			// Wrap the animation in a Setter because it's going to
			// be used to animate a dependency property and connected
			// to another timeline.
			// If it weren't going to be connected to another timeline,
			// the animation could be applied with the following code:
			// myLine.AddAnimation(Line.X2Property, myLengthAnimation);
			Setter mySetter = new Setter(myLine, Line.X2Property);
			mySetter.Children.Add(myLengthAnimation);

			// Initialize the timeline we'll use to control the animation.
			beginExampleTimeline = new Timeline();
			beginExampleTimeline.Duration = new Time(17000);
			beginExampleTimeline.RepeatDuration = Time.Indefinite;

			// Connect the animation to the timeline.
			beginExampleTimeline.Children.Add(mySetter);

			beginExampleTimeline.Repeated += new EventHandler(beginExampleTimeline_Repeated);
			myCanvas.Children.Add(myLine);

			GridPanel myGridPanel = new GridPanel();
			myGridPanel.CellSpacing = new Length(10);
			setupBeginExampleVisuals(myGridPanel);
			myGridPanel.Children.Add(myCanvas);
			this.Children.Add(myGridPanel);

			// Connect the timeline to the timing tree.
			Timeline.Root.Children.Add(beginExampleTimeline);
		}

			#region beginTimeExample Visualization Helper Methods
		private void setupBeginExampleVisuals(GridPanel parentPanel)
		{
			// Create a line to show the progress of the timeline.
			// Create a line to demonstrate the animation.
			Canvas myTimelineProgressCanvas = new Canvas();

			myTimelineProgressCanvas.Width = new Length(200);

			Line myTimelineProgressLine = new Line();

			myTimelineProgressLine.Stroke = Brushes.Black;
			myTimelineProgressLine.StrokeThickness = new Length(10);
			myTimelineProgressLine.X1 = new Length(10);
			myTimelineProgressLine.X2 = new Length(110);
			myTimelineProgressLine.Y1 = new Length(10);
			myTimelineProgressLine.Y2 = new Length(10);
			myTimelineProgressCanvas.Children.Add(myTimelineProgressLine);
			myTimelineProgressLine = new Line();
			myTimelineProgressLine.Stroke = Brushes.Orange;
			myTimelineProgressLine.StrokeThickness = new Length(10);
			myTimelineProgressLine.X1 = new Length(10);
			myTimelineProgressLine.X2 = new Length(10);
			myTimelineProgressLine.Y1 = new Length(10);
			myTimelineProgressLine.Y2 = new Length(10);

			LengthAnimation progressAnimation = new LengthAnimation(new Length(110), beginExampleTimeline.Duration);
			Setter myProgressAnimationSetter = new Setter(myTimelineProgressLine, Line.X2Property);

			myProgressAnimationSetter.Children.Add(progressAnimation);
			beginExampleTimeline.Children.Add(myProgressAnimationSetter);
			myTimelineProgressCanvas.Children.Add(myTimelineProgressLine);
			myTimelineProgressCanvas.Height = new Length(30);

			Text timelineLabel = new Text();

			timelineLabel.TextContent = "Parent Timeline Progress: ";

			Text myLabel = new Text();

			myLabel.TextContent = "Animation Status: ";
			lengthAnimationTracker = new Text();
			lengthAnimationTracker.TextContent = " Not Started";

			FlowPanel animationLabels = new FlowPanel();

			animationLabels.Children.Add(myLabel);
			animationLabels.Children.Add(lengthAnimationTracker);

			parentPanel.Children.Add(timelineLabel);
			parentPanel.Children.Add(myTimelineProgressCanvas);
			parentPanel.Children.Add(animationLabels);
		}

		private void beginExampleTimeline_Repeated(object sender, EventArgs args)
		{
			lengthAnimationTracker.TextContent = " Not Started";
		}

		private void lengthAnimation_Begun(object sender, EventArgs args)
		{
			lengthAnimationTracker.TextContent = " Begun";
		}

		private void lengthAnimation_Ended(object sender, EventArgs args)
		{
			lengthAnimationTracker.TextContent = " Ended";
		}

			#endregion
		
	}
}
