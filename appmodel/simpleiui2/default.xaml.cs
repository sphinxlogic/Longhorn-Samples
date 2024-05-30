using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class StartPage
	{
		private void NextTask (object sender, ClickEventArgs e)
		{
			NavigationApplication myApp;
			NavigationWindow navWindow;	
			
			myApp = (NavigationApplication)System.Windows.Application.Current;
			navWindow = (NavigationWindow)myApp.MainWindow;

			if (sender == task1)
			{
				WCSample.Task1 next = new WCSample.Task1 ();

				next.NonGenericReturn += new StringReturnEventHandler (task1_Return);
				navWindow.Navigate (next);
			}

			if (sender == done)
			{
				System.Windows.Application.Current.Shutdown ();
			}
		}

		public void task1_Return (object sender, StringReturnEventArgs e)
		{
      task1Return.TextContent = " " + e.NonGenericResult;
    }
	}
}