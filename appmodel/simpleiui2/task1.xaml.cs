using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class Task1 :StringPageFunction
	{
        StringReturnEventArgs ra;
        
		private void NextTask (object sender, ClickEventArgs e)
		{
			NavigationApplication myApp;
			NavigationWindow navWindow;

			myApp = (NavigationApplication)System.Windows.Application.Current;
			navWindow = (NavigationWindow)myApp.MainWindow;

			if (sender == task2)
			{
				Task2 next = new Task2 ();

				next.NonGenericReturn += new BoolWrapperReturnEventHandler(task2_Return);
				navWindow.Navigate (next);
			}

			if (sender == back)
			{
				ra = new StringReturnEventArgs();
        ra.NonGenericResult = txtReturnValue.Text;
				OnFinish (ra);
			}
		}

		public void task2_Return (object sender, BoolWrapperReturnEventArgs e)
		{
			task2Return.TextContent = " " + e.NonGenericResult.Value.ToString();
		}
	}
}
