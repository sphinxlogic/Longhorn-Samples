using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class StartPage
	{
		NavigationApplication myApp;
    
		void Init(object sender, EventArgs args)
		{
            myApp = (NavigationApplication) System.Windows.Application.Current;
			txtLabel.TextContent += (String) myApp.Properties["TextFromOnStartup"];
		}
	}
}