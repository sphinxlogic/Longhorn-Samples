using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace Get_Set
{
	public partial class Pane1 : DockPanel
	{
		NavigationApplication myApp;
		NavigationWindow navWindow;
    
		void btnGoTo2(object sender, ClickEventArgs args)
		{
		    myApp = (NavigationApplication) System.Windows.Application.Current;
			navWindow = (NavigationWindow) myApp.MainWindow;
			myApp.Properties["TextFromPage1"] = txtBox.Text;
			navWindow.Navigate(new System.Uri("Pane2.xaml",false,true));
		}
	}
}