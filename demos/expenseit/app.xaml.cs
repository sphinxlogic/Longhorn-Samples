namespace ExpenseItApp
{
	using System;
	using System.Windows;
	using System.Windows.Navigation;

	public partial class AppModelDemo
	{
		NavigationApplication myApp;
		NavigationWindow mainWindow;

//		public System.Collections.Hashtable ExpenseSummary = new System.Collections.Hashtable ();
//		private readonly int _width = 800;
//		private readonly int _height = 600;
		protected override void OnStartingUp (StartingUpCancelEventArgs e)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;

        }


		public void Navigate (object sender)
		{
			mainWindow = (NavigationWindow)myApp.MainWindow;

		    //Navigate
			string buttonID = ((FrameworkElement) sender).ID;

			switch (buttonID)
			{
				case "NAV_BACK":
					mainWindow.GoBack();
					break;

				case "NAV_FWD":
					mainWindow.GoForward();
					break;

				//case "Close":
				//	mainWindow.Shutdown ();
				//	break;

				case "Minimize":
					mainWindow.WindowState = WindowState.Minimized;
					break;
			}
		}
	}
}

