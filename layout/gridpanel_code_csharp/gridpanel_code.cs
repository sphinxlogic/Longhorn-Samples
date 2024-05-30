using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace GridPanel_Demo
{
	public class MyApp : System.Windows.Application
	{

		System.Windows.Controls.Text header1;
		System.Windows.Controls.GridPanel gridPanel;
		System.Windows.Controls.Text txt1;
		System.Windows.Controls.Text txt2;
		System.Windows.Controls.Text txt3;
		System.Windows.Controls.Text txt4;
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


			// Create the GridPanel
			gridPanel = new GridPanel ();
			gridPanel.Width = new Length (200);
			gridPanel.Height = new Length (200);
			gridPanel.Columns = 2;

			// Add the header text
			header1 = new System.Windows.Controls.Text ();
			header1.TextContent = "This is a Simple Grid";
			header1.FontSize = new System.Windows.FontSize(18,System.Windows.FontSizeType.Pixel); 
			header1.FontWeight = System.Windows.FontWeight.Bold;
			System.Windows.Controls.GridPanel.SetColumnSpan(header1, 2);
			gridPanel.Children.Add (header1);

			// Add the first text cell to the GridPanel
			txt1 = new System.Windows.Controls.Text ();
			txt1.TextContent = "Data 1";
			txt1.FontSize = new System.Windows.FontSize(12,System.Windows.FontSizeType.Pixel); 
			txt1.FontWeight = System.Windows.FontWeight.Normal;
			gridPanel.Children.Add (txt1);

			// Add the second text cell to the GridPanel
			txt2 = new System.Windows.Controls.Text ();
			txt2.TextContent = "Data 2";
			txt2.FontSize = new System.Windows.FontSize(12,System.Windows.FontSizeType.Pixel); 
			txt2.FontWeight = System.Windows.FontWeight.Normal;
			gridPanel.Children.Add (txt2);

			// Add the third text cell to the GridPanel
			txt3 = new System.Windows.Controls.Text ();
			txt3.TextContent = "Data 3";
			txt3.FontSize = new System.Windows.FontSize(12,System.Windows.FontSizeType.Pixel); 
			txt3.FontWeight = System.Windows.FontWeight.Normal;
			gridPanel.Children.Add (txt3);

			// Add the fourth text cell to the GridPanel
			txt4 = new System.Windows.Controls.Text ();
			txt4.TextContent = "Data 4";
			txt4.FontSize = new System.Windows.FontSize(12,System.Windows.FontSizeType.Pixel); 
			txt4.FontWeight = System.Windows.FontWeight.Normal;
			gridPanel.Children.Add (txt4);
            mainWindow.Content = gridPanel;
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
