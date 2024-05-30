using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace WCSample
{
	public class MyApp : System.Windows.Application
	{
		System.Windows.Controls.HorizontalSlider hslider;

		System.Windows.Controls.VerticalSlider vslider;

		System.Windows.Window mainWindow;

		System.Windows.Controls.Canvas cv;

		System.Windows.Controls.Text txt1, txt2;

		protected override void OnStartingUp(StartingUpCancelEventArgs e)
		{
			base.OnStartingUp(e);
			CreateAndShowMainWindow();
		}

		private void CreateAndShowMainWindow()
		{
			// Create the application's main window
			mainWindow = new System.Windows.Window();

			// Create two sliders
			cv = new Canvas();
			mainWindow.Content = cv;
			hslider = new HorizontalSlider();
			vslider = new VerticalSlider();
			cv.Children.Add(hslider);
			cv.Children.Add(vslider);
			hslider.Width = new Length(250);
			vslider.Height = new Length(250);
			System.Windows.Controls.Canvas.SetTop(hslider, new Length(10));
			System.Windows.Controls.Canvas.SetTop(vslider, new Length(50));
			txt1 = new Text();
			txt1.TextRange.Text = "Horizontal Slider";
			cv.Children.Add(txt1);
			System.Windows.Controls.Canvas.SetTop(txt1, new Length(25));
			System.Windows.Controls.Canvas.SetLeft(txt1, new Length(60));
			txt2 = new Text();
			txt2.TextRange.Text = "Vertical Slider";
			System.Windows.Controls.Canvas.SetTop(txt2, new Length(175));
			System.Windows.Controls.Canvas.SetLeft(txt2, new Length(20));
			cv.Children.Add(txt2);
			mainWindow.Show();
		}

		internal sealed class EntryClass
		{
			[System.STAThread()]
			private static void Main()
			{
				Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

				MyApp app = new MyApp();

				app.Run();
			}
		}
	}
}
