using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenWindow
{

    public partial class Pane2 : NavigationWindow
    {
       void btnNavigate(object sender, ClickEventArgs args)
       {
          myWindow.Navigate(new Uri("Pane3.xaml", false, true));
       }

       void btnClose(object sender, ClickEventArgs args)
       {
          myWindow.Close();
       }
    }
}