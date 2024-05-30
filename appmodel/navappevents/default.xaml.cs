using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class StartPage
	{
		NavigationApplication myApp;
		NavigationWindow mainWindow;
    
		void Init(object sender, EventArgs args)
		{
			myApp = (NavigationApplication) System.Windows.Application.Current;
		}

		void btnGoTo2(object sender, ClickEventArgs args)
		{
			mainWindow = (NavigationWindow) myApp.MainWindow;
			mainWindow.Navigate(new Uri("Page2.xaml", false, true));
		}

		void btnShutdown (object sender, ClickEventArgs args)
		{
			myApp.Shutdown();
		}
	}
}