using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class StartPage
	{
		NavigationApplication myApp;
		NavigationWindow navWindow;
    
		void Init(object sender, EventArgs args)
		{
			myApp = (NavigationApplication) System.Windows.Application.Current;
		}
    
		void btnGoTo2(object sender, ClickEventArgs args)
		{
			navWindow = (NavigationWindow) myApp.MainWindow;
    		navWindow.Navigate(new Uri("Page2.xaml",false,true));
		}
	}
}