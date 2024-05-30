
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class StartPage
	{
		NavigationWindow navWindow;
		NavigationApplication myApp;
		
		private void Launch_Dialog(object sender, ClickEventArgs e)
		{
			DialogResult dlgResult;

		    myApp = (NavigationApplication) System.Windows.Application.Current;
			navWindow = (NavigationWindow) myApp.MainWindow; 

			NavigationWindow DialogWindow = new NavigationWindow();
			DialogWindow.Navigate(new Uri("Dialog.xaml",false,true));
			dlgResult = DialogWindow.ShowDialog();
            
			//After ShowDialog returns, display results
			txtReturnValue.TextContent += " " + dlgResult.ToString();
			txtUserData.TextContent += " " + (string) myApp.Properties["UserData"];
		}
	}
}