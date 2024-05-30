using System;
   using System.Windows;
   using System.Windows.Controls;
   using System.Windows.Documents;
   using System.Windows.Navigation;
   using System.Windows.Shapes;
   using System.Windows.Data;

   namespace ExpenseIt
     {
        public partial class Pane1: DockPanel
             {
             private void CreateReport(object sender, ClickEventArgs args)
		  {
			NavigationWindow window =
                  (NavigationWindow)Application.Windows[0];
			Pane2 pane2 = new Pane2();
			pane2.DataContext = ListBox1.SelectedItem;
			((INavigator)window).Navigate(pane2);
		}
          }
      }