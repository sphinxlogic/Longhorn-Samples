// This sample is comprised of the following files:
//
//		default.cs:		
//			Used to layout and initialize the timing
//			and animation samples.
//
//		AnimationAndTimlineLinkingExample.cs
//			Demonstrates how to use a single
//			Timeline to control other timelines
//			and animations.
//
//		BeginTimesExample.cs
//			Demonstrates the behavior of the Begin property.
//
//		EndTimesExample
//			Demonstrates the use of implicit end times on a timeline.
//
//		SynchronizeTimelinesExample.cs
//			Demonstrates how to synchronize timeline objects.
//
//		MyClock.cs
//			Used to visually represent a timeline.
//


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Documents;


namespace MMGraphicsSamples {

	public class MyApp : Application
	{
		#region Members
		System.Windows.Window mainWindow;
		System.Windows.Controls.GridPanel mainPanel;

		TransformDecorator secondSampleTransformDecorator;
		TransformDecorator thirdSampleTransformDecorator;
		TransformDecorator fourthSampleTransformDecorator;
		#endregion

		protected override void OnStartingUp (StartingUpCancelEventArgs e) {
			base.OnStartingUp(e);
			BeginApplication();
		}

		public void BeginApplication ()
		{

			mainWindow = new Window();
			mainPanel = new GridPanel();
			mainPanel.CellSpacing = new Length(20);
			
			mainPanel.Columns = 1;
			try
			{
				initializeFirstSample();
				initializeSecondSample();
				initializeThirdSample();
				initializeFourthSample();
				initializeFifthSample();
				// initializeSixthSample();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			
			ScrollViewer sViewer = new ScrollViewer();
			sViewer.Content = mainPanel;
			mainWindow.Content = sViewer;
			mainWindow.Show();		
		}

		#region Layout and Display First Sample

		private void initializeFirstSample()
		{
			GridPanel samplePanel = new GridPanel();
			System.Windows.Documents.TextPanel myTextPanel = new System.Windows.Documents.TextPanel();
			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(12, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "Animation and Timeline Linking Example";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.AnimationAndTimelineLinkingExample firstSample = new TimingExamples.AnimationAndTimelineLinkingExample();	
			samplePanel.Children.Add(firstSample);
			mainPanel.Children.Add(samplePanel);
		}

		#endregion

		#region Layout and Display Second Sample
		private void initializeSecondSample()
		{
			GridPanel samplePanel = new GridPanel();
			System.Windows.Documents.TextPanel myTextPanel = new System.Windows.Documents.TextPanel();

			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(12, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "Begin Time Example";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.BeginTimeExample secondSample = new TimingExamples.BeginTimeExample();
			samplePanel.Children.Add(secondSample);
			mainPanel.Children.Add(samplePanel);
		}

	
		#endregion

		#region Layout and Display Third Sample
		
		private void initializeThirdSample()
		{
			GridPanel samplePanel = new GridPanel();
			TextPanel myTextPanel = new TextPanel();
			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(14, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "Timeline Sync Example";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.SynchronizeTimelinesExample thirdSample = new TimingExamples.SynchronizeTimelinesExample();

			samplePanel.Children.Add(thirdSample);
			mainPanel.Children.Add(samplePanel);
		}

		#endregion

		#region Layout and Display Fourth Sample
		private void initializeFourthSample()
		{
			GridPanel samplePanel = new GridPanel();
			TextPanel myTextPanel = new TextPanel();

			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(12, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "End Times Example (AllChildren)";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.TimeEndSyncAllChildrenExample fourthSample = new TimingExamples.TimeEndSyncAllChildrenExample();

			samplePanel.Children.Add(fourthSample);
			mainPanel.Children.Add(samplePanel);
		}

		#endregion
	
		#region Layout and Display Fifth Sample
		private void initializeFifthSample()
		{
			GridPanel samplePanel = new GridPanel();
			TextPanel myTextPanel = new TextPanel();

			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(12, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "End Times Example (LastChild)";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.TimeEndSyncLastChildExample fifthSample = new TimingExamples.TimeEndSyncLastChildExample();

			samplePanel.Children.Add(fifthSample);
			mainPanel.Children.Add(samplePanel);
		}

		#endregion



		#region Layout and Display Sixth Sample
		private void initializeSixthSample()
		{
			GridPanel samplePanel = new GridPanel();
			TextPanel myTextPanel = new TextPanel();

			myTextPanel.Width = new Length(500);
			myTextPanel.Height = new Length(20);
			myTextPanel.FontSize = new FontSize(12, FontSizeType.Point);
			myTextPanel.Background = Brushes.LightGray;
			myTextPanel.Foreground = Brushes.Black;
			myTextPanel.TextContent = "Unresolved End Time Example (AllChildren)";
			samplePanel.Children.Add(myTextPanel);

			TimingExamples.AnotherTimeEndSyncAllChildrenExample sixthSample = new TimingExamples.AnotherTimeEndSyncAllChildrenExample();

			samplePanel.Children.Add(sixthSample);
			mainPanel.Children.Add(samplePanel);
		}

		#endregion
	
	}



	internal sealed class EntryClass
	{
		[System.STAThread()]
		private static void Main ()
		{

			System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
			MyApp app = new MyApp ();
			app.Run();
		}
	}
}
