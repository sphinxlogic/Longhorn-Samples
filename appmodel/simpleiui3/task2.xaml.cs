using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WCSample
{
  public partial class Task2: StringPageFunction
  {
    NavigationApplication myApp;

    private void NextTask(object sender, ClickEventArgs e)
    {
      myApp = (NavigationApplication) System.Windows.Application.Current;

      if (sender == done)
      {
        myApp.Shutdown();
      }

      if (sender == back)
      {
        StringReturnEventArgs ra = new StringReturnEventArgs();
        ra.NonGenericResult = txtReturnValue.Text;
        OnFinish(ra);
      }
    }
  }
}
