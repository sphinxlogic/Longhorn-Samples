using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SimpleNav
{
	public partial class Pane1 : DockPanel
	{
		NavigationApplication myApp;

		NavigationWindow navWindow;

		void btnGoTo2(object sender, ClickEventArgs args)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;
			navWindow = (NavigationWindow)myApp.MainWindow;

			bool retval = navWindow.Navigate(new Uri("Pane2.xaml", false, true));
		}

		void btnGoToObj(object sender, ClickEventArgs args)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;
			navWindow = (NavigationWindow)myApp.MainWindow;

			string someText = "Some Useful Text";
			Pane2 nextPane = new Pane2(someText);
			bool retval = navWindow.Navigate(nextPane);
		}
	}
}