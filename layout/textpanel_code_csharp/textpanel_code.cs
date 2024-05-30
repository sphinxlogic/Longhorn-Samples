using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Threading;

namespace TextPanel_Demo
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Documents.TextPanel textPanel;
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


			// Create a TextPanel and add Text using the TextRange property.
			textPanel = new TextPanel ();
			textPanel.Width = new Length (400);
			textPanel.Height = new Length (300);
			textPanel.TextIndent = new Length(15);
			textPanel.ColumnCount = 2;
			textPanel.ColumnGap = new Length(5);
			textPanel.ColumnWidth = new Length (195);
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			textPanel.TextContent += ("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
            mainWindow.Content = textPanel;
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
