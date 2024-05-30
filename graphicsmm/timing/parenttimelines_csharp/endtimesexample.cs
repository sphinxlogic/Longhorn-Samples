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
	/// This example demonstrates the use of the
	/// TimeEndSync.AllChildren setting.
	/// </summary>
	public class TimeEndSyncAllChildrenExample : GridPanel
	{
		private Text mainTimelineStatus;
		private Timeline t1;

		public TimeEndSyncAllChildrenExample()
		{
			this.Columns = 2;
			this.Width = new Length(300);

			TextPanel explanationText = new TextPanel();

			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent = 
				"This example demonstrates the use of implicit end times." +
				" Two Timelines, t2 and t3, are connected to a third Timeline, t1. " +
				" t1's TimeEndSync property is set to TimeEndSync.AllChildren, " +
				" so t1 ends after t2 and t3 end. ";
			this.Children.Add(explanationText);
			
			implicitEndTimes();

			Button restartButton = new Button();
			restartButton.Content = "Restart Sample";
			restartButton.Click += new ClickEventHandler(restartButton_Click);
			this.Children.Add(restartButton);
		}

		// Demonstrates some implicit end time settings.
		private void implicitEndTimes()
		{
			// Set t1 to end only after all its child timelines have ended.	
			t1 = new Timeline();
			t1.StatusOfNextUse = UseStatus.ChangeableReference;
			t1.EndSync = TimeEndSync.AllChildren;

			Timeline t2 = new Timeline();
			t2.StatusOfNextUse = UseStatus.ChangeableReference;

			t2.Duration = new Time(5000);
			t1.Children.Add(t2);

			Timeline t3 = new Timeline();
			t3.StatusOfNextUse = UseStatus.ChangeableReference;
			t3.Duration = new Time(15000);
			t1.Children.Add(t3);

			// Create a visualization for t1.
			mainTimelineStatus = new Text();
			mainTimelineStatus.TextContent = " Running";
			Text label = new Text();
			label.TextContent = "t1 Status: ";
			FlowPanel labelPanel = new FlowPanel();
			labelPanel.Children.Add(label);
			labelPanel.Children.Add(mainTimelineStatus);
			t1.Ended += new EventHandler(t1_Ended);
			t1.Begun += new EventHandler(t1_Begun);

			// Create visualizations for the child timelines.
			MyClock t2Clock = new MyClock(t2);
			MyClock t3Clock = new MyClock(t3);

			GridPanel samplesPanel = new GridPanel();
			samplesPanel.Columns = 4;
			samplesPanel.CellSpacing = new Length(10);
			GridPanel.SetColumnSpan(labelPanel, 4);
			samplesPanel.Children.Add(labelPanel);
			
			label = new Text();
			label.TextContent = "t2";
			samplesPanel.Children.Add(label);
			samplesPanel.Children.Add(t2Clock);

			label = new Text();
			label.TextContent = "t3";
			samplesPanel.Children.Add(label);
			samplesPanel.Children.Add(t3Clock);

			this.Children.Add(samplesPanel);

			// Connect the timelines to the timing tree.
			Timeline.Root.Children.Add(t1);

		}

		private void t1_Ended(object sender, EventArgs args)
		{
			mainTimelineStatus.TextContent = " Ended";
		}

		private void t1_Begun(object sender, EventArgs args)
		{
			mainTimelineStatus.TextContent = " Running";
		}

		private void restartButton_Click(object sender, ClickEventArgs args){
			t1.BeginIn(0);
			mainTimelineStatus.TextContent = " Running";
		}
	}

	/// <summary>
	/// This example demonstrates the use of the
	/// TimeEndSync.LastChild setting.
	/// </summary>
	public class TimeEndSyncLastChildExample : GridPanel
	{
		private Timeline t1;
		private Text t1Tracker;

		public TimeEndSyncLastChildExample(){
			this.Columns = 2;
			this.Width = new Length(300);

			TextPanel explanationText = new TextPanel();

			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent = 
				"This example demonstrates the use of implicit end times." +
				" Two Timelines, t2 and t3, are connected to a third Timeline, t1. " +
				" t1's TimeEndSync property is set to TimeEndSync.LastChild, " +
				" so t1 ends after t2 ends; the duration of t3 is ignored because it has an unresolved begin time. ";
			this.Children.Add(explanationText);

			beginExample();

			Button restartButton = new Button();
			restartButton.Content = "Restart Sample";
			restartButton.Click += new ClickEventHandler(restartButton_Click);
			this.Children.Add(restartButton);
		}

		private void beginExample(){
			// In this example, t1 ends after t2 ends; the duration of 
			// t3 is ignored because it has an unresolved begin time.
		
			t1 = new Timeline();
			t1.EndSync = TimeEndSync.LastChild;
			t1.StatusOfNextUse = UseStatus.ChangeableReference;

			Timeline t2 = new Timeline();
			t2.StatusOfNextUse = UseStatus.ChangeableReference;
			t2.Begin = new TimeSyncValue(new Time(1000));
			t2.Duration = new Time(10000);
			t1.Children.Add(t2);

			Timeline t3 = new Timeline();
			t3.StatusOfNextUse = UseStatus.ChangeableReference;
			t3.Begin = new TimeSyncValue(Time.Indefinite);
			t1.Children.Add(t3);


			// Add visualizations.
			GridPanel gPanel = new GridPanel();
			gPanel.CellSpacing = new Length(10);
			gPanel.Columns = 4;
			Text label = new Text();
			label.TextContent = "t1 Status: ";
			
			
			t1Tracker = new Text();
			t1Tracker.TextContent = "Running";
			FlowPanel labels = new FlowPanel();
			labels.Children.Add(label);
			labels.Children.Add(t1Tracker);
			GridPanel.SetColumnSpan(labels, 4);
			gPanel.Children.Add(labels);
			
			label = new Text();
			label.TextContent = "t2";
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			gPanel.Children.Add(label);
			MyClock t2Clock = new MyClock(t2);
			gPanel.Children.Add(t2Clock);

			label = new Text();
			label.TextContent = "t3";
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			gPanel.Children.Add(label);
			MyClock t3Clock = new MyClock(t3);
			gPanel.Children.Add(t3Clock);

			this.Children.Add(gPanel);

			t1.Begun += new EventHandler(t1_Begun);
			t1.Ended += new EventHandler(t1_Ended);

			Timeline.Root.Children.Add(t1);

		}

		private void t1_Begun(object sender, EventArgs args){
			
			t1Tracker.TextContent = " Running";

		}

		private void t1_Ended(object sender, EventArgs args){

			t1Tracker.TextContent = " Ended";
		}

		private void restartButton_Click(object sender, ClickEventArgs args){
			
			t1.BeginIn(0);
			t1Tracker.TextContent = " Running";
			
		}

	}

	/// <summary>
	/// This example demonstrates the use of the
	/// TimeEndSync.LastChild setting.
	/// </summary>
	public class AnotherTimeEndSyncAllChildrenExample : GridPanel
	{
		private Timeline t1;

		private Text t1Tracker;

		public AnotherTimeEndSyncAllChildrenExample()
		{
			this.Columns = 2;
			this.Width = new Length(300);

			TextPanel explanationText = new TextPanel();

			explanationText.Margin = new Thickness(new Length(10));
			explanationText.Width = new Length(150);
			explanationText.TextContent = 
				"This example demonstrates the use of implicit end times." + 
				" Two Timelines, t2 and t3, are connected to a third Timeline, t1. " + 
				" t1's TimeEndSync property is set to TimeEndSync.AllChildren, " + 
				" so t1's end time is never resolved because t3 has an unresolved begin time. ";
			this.Children.Add(explanationText);
			beginExample();

			Button restartButton = new Button();

			restartButton.Content = "Restart Sample";
			restartButton.Click += new ClickEventHandler(restartButton_Click);
			this.Children.Add(restartButton);
		}

		private void beginExample()
		{
			// In this example, t1's end time is unresolved, because
			// t3 has an unresolved begin time.
			t1 = new Timeline();
			t1.EndSync = TimeEndSync.AllChildren;
			t1.StatusOfNextUse = UseStatus.ChangeableReference;

			Timeline t2 = new Timeline();

			t2.StatusOfNextUse = UseStatus.ChangeableReference;
			t2.Begin = new TimeSyncValue(new Time(1000));
			t2.Duration = new Time(10000);
			t1.Children.Add(t2);

			Timeline t3 = new Timeline();

			t3.StatusOfNextUse = UseStatus.ChangeableReference;
			t3.Begin = new TimeSyncValue(Time.Indefinite);
			t1.Children.Add(t3);

			// Add visualizations.
			GridPanel gPanel = new GridPanel();

			gPanel.CellSpacing = new Length(10);
			gPanel.Columns = 4;

			Text label = new Text();

			label.TextContent = "t1 Status: ";
			t1Tracker = new Text();
			t1Tracker.TextContent = "Running";

			FlowPanel labels = new FlowPanel();

			labels.Children.Add(label);
			labels.Children.Add(t1Tracker);
			GridPanel.SetColumnSpan(labels, 4);
			gPanel.Children.Add(labels);
			label = new Text();
			label.TextContent = "t2";
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			gPanel.Children.Add(label);

			MyClock t2Clock = new MyClock(t2);

			gPanel.Children.Add(t2Clock);
			label = new Text();
			label.TextContent = "t3";
			label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
			gPanel.Children.Add(label);

			MyClock t3Clock = new MyClock(t3);

			gPanel.Children.Add(t3Clock);
			this.Children.Add(gPanel);
			t1.Begun += new EventHandler(t1_Begun);
			t1.Ended += new EventHandler(t1_Ended);
			Timeline.Root.Children.Add(t1);
		}

		private void t1_Begun(object sender, EventArgs args)
		{
			t1Tracker.TextContent = " Running";
		}

		private void t1_Ended(object sender, EventArgs args)
		{
			t1Tracker.TextContent = " Ended";
		}

		private void restartButton_Click(object sender, ClickEventArgs args)
		{
			t1.BeginIn(0);
			t1Tracker.TextContent = " Running";
		}
	}
}
