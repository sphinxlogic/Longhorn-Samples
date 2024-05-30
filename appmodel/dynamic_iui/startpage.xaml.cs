using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
  public partial class StartPage : StringPageFunction
  {
    private void StartTasks(object sender, ClickEventArgs e)
    {
      NavigationApplication myApp;
      NavigationWindow navWindow;

      myApp = (NavigationApplication)System.Windows.Application.Current;
      navWindow = (NavigationWindow)myApp.MainWindow;
      WCSample.NavHub next = new WCSample.NavHub();

      next.NonGenericReturn += new ObjectReturnEventHandler(navhub_Return);
      navWindow.Navigate(next);
    }
  
    private void ShutDown(object sender, ClickEventArgs e)
    {
       System.Windows.Application.Current.Shutdown();
    }

    public void navhub_Return(object sender, ObjectReturnEventArgs e)
    {
      string[] returnString = (string[]) e.NonGenericResult;
      navhubReturn.TextContent = returnString[0];
      navhubReturn1.TextContent = returnString[1];
      navhubReturn2.TextContent = returnString[2];
      navhubReturn3.TextContent = returnString[3];
    }
  }
}