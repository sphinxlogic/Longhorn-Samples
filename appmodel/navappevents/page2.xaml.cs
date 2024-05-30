
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class Page2
	{
		NavigationApplication myApp;

		void Init (object sender, EventArgs args)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;
		}

		void btnGoTo3 (object sender, ClickEventArgs args)
		{
			Window2.Navigate (new Uri ("page3.xaml", false, true));
		}

		void btnClose (object sender, ClickEventArgs args)
		{
			Window2.Close ();
		}
	}
}