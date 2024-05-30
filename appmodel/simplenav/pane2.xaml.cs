using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SimpleNav
{
	public partial class Pane2 : DockPanel
	{
		NavigationApplication myApp;
		NavigationWindow navWindow;
		string stringFrom1;

		public Pane2(string s) : this()
		{
			//This constructor is called after the default constructor, which
			//initializes the page.
			string stringFrom1 = s;
			txtFrom1.TextContent = "Text from Pane 1: " + stringFrom1;
		}

		void btnGoTo1(object sender, ClickEventArgs args)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;
			navWindow = (NavigationWindow)myApp.MainWindow;
			navWindow.Navigate(new Uri("Pane1.xaml", false, true));
		}
	}
}