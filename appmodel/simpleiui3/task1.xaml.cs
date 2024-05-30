using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace WCSample
{
  public partial class Task1: IntWrapperPageFunction
  {
    NavigationApplication myApp;
    NavigationWindow mainWindow;
    string stringFromDefault;

    public Task1(string s) : this()
    {
      stringFromDefault = s;
      task1Passed.TextContent = " " + stringFromDefault;
      KeepAlive = true;
    }

    private void NextTask(object sender, ClickEventArgs e)
    {
      myApp = (NavigationApplication) System.Windows.Application.Current;
      mainWindow = (NavigationWindow) myApp.MainWindow;

      if (sender == task2)
      {
        Task2 next = new Task2();

        next.NonGenericReturn += new StringReturnEventHandler(task2_Return);
        mainWindow.Navigate(next);
      }

      if (sender == back)
      {
        IntWrapperReturnEventArgs ra = new IntWrapperReturnEventArgs();

        IntWrapper returnValue = new IntWrapper(Int32.Parse(returnedInt.Text));
        ra.NonGenericResult = returnValue;
        OnFinish(ra);
      }
    }
    public void task2_Return(object sender, StringReturnEventArgs e)
    {
      task2Return.TextContent = " " + e.NonGenericResult;
    }
  }
}
