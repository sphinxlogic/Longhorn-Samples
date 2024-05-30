using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
	
namespace WCSample
{
  public partial class Default: StringPageFunction
  {
    NavigationApplication myApp;
    NavigationWindow mainWindow;

    private void NextTask(object sender, ClickEventArgs e)
    {
      myApp = (NavigationApplication) System.Windows.Application.Current;
      mainWindow = (NavigationWindow) myApp.MainWindow;

      if (sender == task1)
      {
        Task1 next = new Task1("String from Default.xaml");
        next.NonGenericReturn += new IntWrapperReturnEventHandler(task1_Return);        
        mainWindow.Navigate(next);
      }

      if (sender == done)
      {
        myApp.Shutdown();
      }
    }
    public void task1_Return(object sender, IntWrapperReturnEventArgs e)
    {
      task1Return.TextContent = " " + e.NonGenericResult.Value.ToString();
    }
  }
}
