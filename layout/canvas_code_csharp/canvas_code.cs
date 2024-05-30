using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace Canvas_Demo
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Controls.Text txt1;
		System.Windows.Controls.Canvas canvas;
		System.Windows.Controls.Text txt2;
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

			// Create a canvas sized to fill the window
			canvas = new Canvas ();
			canvas.Background = System.Windows.Media.Brushes.LightSteelBlue;
			canvas.Width = new Length (100f, UnitType.Percent);
			canvas.Height = new Length (100f, UnitType.Percent);

			// Add a "Hello World!" text element to the Canvas
			txt1 = new System.Windows.Controls.Text ();
        		txt1.FontSize = new System.Windows.FontSize(14,System.Windows.FontSizeType.Pixel);
			txt1.TextContent = "Hello World!";
			System.Windows.Controls.Canvas.SetTop(txt1, new System.Windows.Length(100));
			System.Windows.Controls.Canvas.SetLeft(txt1, new System.Windows.Length(10));
			canvas.Children.Add (txt1);


			// Add a second text element to show how absolute positioning works in a Canvas
			txt2 = new System.Windows.Controls.Text ();
        		txt2.FontSize = new System.Windows.FontSize(22,System.Windows.FontSizeType.Pixel);
			txt2.TextContent = "Isn't absolute positioning handy?";
			System.Windows.Controls.Canvas.SetTop(txt2, new System.Windows.Length(200));
			System.Windows.Controls.Canvas.SetLeft(txt2, new System.Windows.Length(75));
			canvas.Children.Add (txt2);
            mainWindow.Content= canvas;
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
