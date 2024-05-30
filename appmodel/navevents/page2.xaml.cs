using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class Page2
	{
		NavigationApplication myApp;
		NavigationWindow navWindow;
		bool bFirstTime= true;
    
		void Init(object sender, EventArgs args)
		{
			myApp = (NavigationApplication) System.Windows.Application.Current;
		}
    
		void btnGoTo1(object sender, ClickEventArgs args)
		{
			navWindow = (NavigationWindow) myApp.MainWindow;
			navWindow.Navigating += new NavigationService.NavigatingCancelEventHandler(OnNavigating);
			navWindow.Navigate(new Uri("Default.xaml",false,true));
		}
    
		void OnNavigating(object sender, NavigatingCancelEventArgs e)
		{
			if(bFirstTime)
			{
				e.Cancel=true;
				GoTo1.Content="Try Again";
				bFirstTime= false;
			}
			return;
		}
	}
}