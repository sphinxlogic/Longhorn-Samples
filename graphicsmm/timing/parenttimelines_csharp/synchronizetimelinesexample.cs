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
	/// This example demonstrates how to synchronize timelines.
	/// Animated drawings of clocks are used to show the 
	/// timelines' behavior.
	/// </summary>
	public class SynchronizeTimelinesExample : GridPanel
	{
		private Timeline t1;
		public SynchronizeTimelinesExample()
		{
			// Initialize layout and provide a description.
			this.Columns = 2;
			this.Width = new Length(300);

			TextPanel explanationText = new TextPanel();

			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent = 
				"This example demonstrates how to synchronize timelines. " +
				"Timeline t2 and t3 are connected to Timeline t1. " +
				"Timeline t3 is set to begin when t2 ends. " + 
				"Animated drawings of clocks are used to show the " +
				" timelines' behavior.";
			this.Children.Add(explanationText);

			SyncTimelines();

			Button restartButton = new Button();
			restartButton.Content = "Restart Sample";
			restartButton.Click += new ClickEventHandler(restartButton_Click);
			this.Children.Add(restartButton);
		}

		// Create and sync three timelines.
		private void SyncTimelines()
		{
			t1 = new Timeline();
			Timeline t2 = new Timeline();
			Timeline t3 = new Timeline();

			t1.StatusOfNextUse = UseStatus.ChangeableReference;
			t2.StatusOfNextUse = UseStatus.ChangeableReference;
			t3.StatusOfNextUse = UseStatus.ChangeableReference;

			t1.EndSync = TimeEndSync.AllChildren;
			t1.RepeatDuration = Time.Indefinite;
			t2.Duration = new Time(10000);
			t3.Duration = new Time(5000);
			t3.Begin = new TimeSyncValue(t2, TimeSyncBase.End, 0);
			t1.Children.Add(t2);
			t1.Children.Add(t3);

			// Create visualizations for the timelines.
			TimingExamples.MyClock masterTimelineClock = new TimingExamples.MyClock(t1);
			TimingExamples.MyClock firstChildClock = new TimingExamples.MyClock(t2);
			TimingExamples.MyClock secondChildClock = new TimingExamples.MyClock(t3);
			

			// Layout the clocks and label them.
			GridPanel myGridPanel = new GridPanel();
			myGridPanel.Columns = 4;
			Text label = new Text();
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			label.TextContent = "t1";
			label.FontWeight = FontWeight.Bold;
			GridPanel.SetColumnSpan(label, 2);
			myGridPanel.Children.Add(label);
			GridPanel.SetColumnSpan(masterTimelineClock, 2);
			myGridPanel.Children.Add(masterTimelineClock);

			label = new Text();
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			label.TextContent = "t2";
			label.FontWeight = FontWeight.Bold;
			myGridPanel.Children.Add(label);
			myGridPanel.Children.Add(firstChildClock);

			label = new Text();
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			label.FontWeight = FontWeight.Bold;
			label.TextContent = "t3";
			myGridPanel.Children.Add(label);
			myGridPanel.Children.Add(secondChildClock);
			
			this.Children.Add(myGridPanel);

			// Connect the main timeline to the timing tree.
			Timeline.Root.Children.Add(t1);
		}

		private void restartButton_Click(object sender, ClickEventArgs args)
		{
			t1.BeginIn(0);
		}
	}
}
