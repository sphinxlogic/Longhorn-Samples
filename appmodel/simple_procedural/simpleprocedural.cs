using System;
using System.Windows;
using System.Windows.Documents;
using System.Threading;
using System.Windows.Navigation;

namespace SimpleProcedural
{
	public class MyApp : Application
	{
    System.Windows.Controls.Text txtElement;
    System.Windows.Controls.DockPanel dPanel;
    System.Windows.Window win;

    protected override void OnStartingUp(StartingUpCancelEventArgs e)
    {
      win = new NavigationWindow();
      dPanel = new System.Windows.Controls.DockPanel();

      txtElement = new System.Windows.Controls.Text();
      txtElement.TextContent = "Some Text";
      win.Content = dPanel;
      dPanel.Children.Add(txtElement);

      win.Show();
    }
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