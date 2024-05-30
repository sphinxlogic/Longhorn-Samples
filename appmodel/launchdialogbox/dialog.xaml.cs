using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
namespace WCSample
{
	public partial class Dialog
	{
		NavigationApplication myApp;
		NavigationWindow dlgWindow;

		public void Init(object sender, EventArgs args)
		{
		    myApp = (NavigationApplication) System.Windows.Application.Current;
		}

		private void YesButton_Click(object sender, ClickEventArgs e)
		{
			dlgWindow =  (NavigationWindow) myApp.Windows[1];
			myApp.Properties["UserData"] = User_Data.Text;
			dlgWindow.DialogResult = DialogResult.Yes;
		}

		private void CancelButton_Click(object sender, ClickEventArgs e) 
		{
			dlgWindow =  (NavigationWindow) myApp.Windows[1];
			dlgWindow.DialogResult = DialogResult.Cancel;
		}
	}
}