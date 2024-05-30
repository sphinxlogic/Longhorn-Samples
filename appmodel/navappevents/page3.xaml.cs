
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class Page3
	{
		NavigationApplication myApp;
    
		void btnClose(object sender, ClickEventArgs args)
		{
            myApp = (NavigationApplication) System.Windows.Application.Current;
			Window3.Close();
		}
	}
}