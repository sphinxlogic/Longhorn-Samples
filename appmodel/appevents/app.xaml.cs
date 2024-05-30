using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class AppEvents
	{
        public  void OnStartup(object sender, StartingUpCancelEventArgs e)
        {
			Properties["TextFromOnStartup"] = DateTime.Now.ToString();
		}
        
        public void OnShutdown(object sender, ShuttingDownEventArgs e)
        {
    		//Perform Shutdown procedures.
    	}

		public void OnActivate (object sender, EventArgs e)
		{
			//Perform activation procedures.
		}

		public void OnDeactivate (object sender, EventArgs e)
		{
			//Perform activation procedures.
		}

		public void OnSessionEnd (object sender, SessionEndingCancelEventArgs e)
		{
			//Perform activation procedures.
		}
	}
}