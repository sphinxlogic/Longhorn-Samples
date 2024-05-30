using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Threading;

namespace SimpleButton
{
	public class MyApp : Application
	{
		System.Windows.Controls.Button btn;

		System.Windows.Controls.DockPanel dPanel;

		System.Windows.Window win;

		protected override void OnStartingUp(StartingUpCancelEventArgs e)
		{
			win = new System.Windows.Window();
			dPanel = new System.Windows.Controls.DockPanel();
			btn = new System.Windows.Controls.Button();
			btn.Content = "Click to change the color of the button.";
			btn.Background = Brushes.Red;
			btn.Width = new Length(250);
			btn.Height = new Length(50);
			win.Content = dPanel;
			dPanel.Children.Add(btn);
			win.Show();
			btn.Click += (ChangeColor);
		}

		void ChangeColor(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			btn.Background = Brushes.Green;
		}

		internal sealed class TestMain
		{
			[System.STAThread()]
			public static void Main()
			{
				Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

				MyApp app = new MyApp();

				app.Run();
			}
		}
	}
}