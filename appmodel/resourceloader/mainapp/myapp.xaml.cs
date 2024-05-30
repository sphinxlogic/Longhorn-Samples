using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace Application1
{

    public partial class MyApp : Application
    {

	FlowPanel fPanel;
	NavigationWindow win;

	void AppStartingUp(object sender, StartingUpCancelEventArgs e)
	{
		System.Windows.Resources.ResourceLoaderService rlsvc = ((System.Windows.Resources.ResourceLoaderService)(this.GetService(typeof(System.Windows.Resources.ResourceLoaderService))));
            rlsvc.RegisterResourceLoader(new Library1.ResourceLoader());
		win = new NavigationWindow();
		win.Navigate(new Uri("Page1.xaml", true, true));
		win.Show();
	}
    }
}