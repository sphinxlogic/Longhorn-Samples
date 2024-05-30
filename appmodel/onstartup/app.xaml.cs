using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class App
	{
        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            Properties["SomeText"] = DateTime.Now.ToString();
        }
        
        protected override void OnShuttingDown(ShuttingDownEventArgs e)
        {
    		//Perform Shutdown procedures.
    	}
	}
}