
using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace Get_Set
{
	public partial class Pane2 : DockPanel
	{
		NavigationApplication myApp;
    
		void Init(object sender, EventArgs args)
		{
			if(null == myApp)
			{
    		    myApp = (NavigationApplication) System.Windows.Application.Current;
				txtFromPage1.TextContent += (String) myApp.Properties["TextFromPage1"];
			}
		}
    
		void btnClose(object sender, ClickEventArgs args)
		{
			myApp.Shutdown();
		}
	}
}