using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace FlowPanel_Demo
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Controls.Text txt1;
		System.Windows.Controls.Text txt2;
		System.Windows.Controls.FlowPanel flowPanel;
		System.Windows.Window mainWindow;

		protected override void OnStartingUp (StartingUpCancelEventArgs e)
		{
			base.OnStartingUp (e);
			CreateAndShowMainWindow ();
		}

		private void CreateAndShowMainWindow ()
		{
			// Create the application's main window
			mainWindow = new System.Windows.Window ();

			// Instantiate a 300 by 200 pixel FlowPanel with a blue background
			flowPanel = new FlowPanel ();
			flowPanel.Background = System.Windows.Media.Brushes.AliceBlue;
			flowPanel.Width = new Length (300);
			flowPanel.Height = new Length (200);

			// Add text to be flowed
			txt1 = new System.Windows.Controls.Text ();
			txt2 = new System.Windows.Controls.Text ();
			txt1.TextWrap = System.Windows.TextWrap.Wrap;
			txt1.TextContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Lorem ipsum dolor sit amet, consectetuer adipiscing elit."; 
			txt2.TextContent = "Notice that the preceeding text flowed within the containing element. If you remove the TextWrap it will flow beyond the container, like this.";
			flowPanel.Children.Add (txt1);
			flowPanel.Children.Add (txt2);
			mainWindow.Content = flowPanel;
			mainWindow.Show ();
		}
	}

	internal sealed class EntryClass
	{
		[System.STAThread()]
		private static void Main ()
		{
			Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

			MyApp app = new MyApp ();

			app.Run ();
		}
	}
}
