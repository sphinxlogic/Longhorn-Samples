using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class Task2 :BoolWrapperPageFunction
	{
    BoolWrapperReturnEventArgs ra;

		private void NextTask (object sender, ClickEventArgs e)
		{
      if (sender == done)
			{
				System.Windows.Application.Current.Shutdown();
			}

			if (sender == back)
			{
        bool taskResult = true;
        if (rdbtnFalse.IsChecked == true)
          taskResult = false;
				ra = new BoolWrapperReturnEventArgs();
				ra.NonGenericResult = new BoolWrapper(taskResult);
				OnFinish (ra);
			}
		}
	}
}