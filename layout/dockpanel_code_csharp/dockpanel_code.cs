using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace Canvas_Demo
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Shapes.Rectangle rect1;
		System.Windows.Shapes.Rectangle rect2;
		System.Windows.Shapes.Rectangle rect3;
		System.Windows.Shapes.Rectangle rect4;
		System.Windows.Shapes.Rectangle rect5;
		System.Windows.Controls.DockPanel dockPanel;
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

			// Create a DockPanel with a width and height of 500 pixels
			dockPanel = new DockPanel ();
			dockPanel.Background = System.Windows.Media.Brushes.Snow;
			dockPanel.Width = new Length (500);
			dockPanel.Height = new Length (500);

			// Add the first rectangle to the DockPanel
			rect1 = new System.Windows.Shapes.Rectangle ();
			rect1.Stroke = System.Windows.Media.Brushes.Black;
			rect1.Fill = System.Windows.Media.Brushes.CadetBlue;
			rect1.Width = new Length (500);
			rect1.Height = new Length (25);
			System.Windows.Controls.DockPanel.SetDock(rect1, System.Windows.Controls.Dock.Top);
			dockPanel.Children.Add (rect1);

			// Add the second rectangle to the DockPanel
			rect2 = new System.Windows.Shapes.Rectangle ();
			rect2.Stroke = System.Windows.Media.Brushes.Black;
			rect2.Fill = System.Windows.Media.Brushes.LightSteelBlue;
			rect2.Width = new Length (500);
			rect2.Height = new Length (25);
			System.Windows.Controls.DockPanel.SetDock(rect2, System.Windows.Controls.Dock.Top);
			dockPanel.Children.Add (rect2);

			// Add the third rectangle to the DockPanel
			rect4 = new System.Windows.Shapes.Rectangle ();
			rect4.Stroke = System.Windows.Media.Brushes.Black;
			rect4.Fill = System.Windows.Media.Brushes.Teal;
			rect4.Width = new Length (500);
			rect4.Height = new Length (50);
			System.Windows.Controls.DockPanel.SetDock(rect4, System.Windows.Controls.Dock.Bottom);
			dockPanel.Children.Add (rect4);

			// Add the fourth rectangle to the DockPanel
			rect3 = new System.Windows.Shapes.Rectangle ();
			rect3.Stroke = System.Windows.Media.Brushes.Black;
			rect3.Fill = System.Windows.Media.Brushes.DarkSeaGreen;
			rect3.Width = new Length (200);
			rect3.Height = new Length (400);
			System.Windows.Controls.DockPanel.SetDock(rect3, System.Windows.Controls.Dock.Left);
			dockPanel.Children.Add (rect3);

			// Add the final rectangle to the DockPanel
			rect5 = new System.Windows.Shapes.Rectangle ();
			rect5.Stroke = System.Windows.Media.Brushes.Black;
			rect5.Fill = System.Windows.Media.Brushes.SlateGray;
			System.Windows.Controls.DockPanel.SetDock(rect5, System.Windows.Controls.Dock.Fill);
			dockPanel.Children.Add (rect5);
            mainWindow.Content = dockPanel;
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
