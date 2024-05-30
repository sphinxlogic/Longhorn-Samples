//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace OpenWindow
{
    public partial class Pane1 : DockPanel
    {
       NavigationWindow navWindow;
       NavigationApplication myApp;

       void Init(object sender, EventArgs args)
       {
          myApp = (NavigationApplication)System.Windows.Application.Current;
       }

       void btnGoTo2(object sender, ClickEventArgs args)
       {
          navWindow = (NavigationWindow)myApp.MainWindow;
          navWindow.Navigate(new Uri("Pane2.xaml", false, true));
       }
    }
}