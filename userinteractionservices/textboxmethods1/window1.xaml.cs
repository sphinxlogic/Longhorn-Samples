//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace TextBoxMethods
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
		

		private void UpDownFunctions(object sender, ClickEventArgs e) {
	string currentID = ((Button)sender).ID;
			switch (currentID)
			{
				case "pdown":
					tb1.PageDown();
					break;
				case "pup":
					tb1.PageUp();
					break;
				case "ldown":
					tb1.LineDown();
					break;
				case "lup":
					tb1.LineUp();
					break;
				case "pscrollend":
					tb1.ScrollToEnd();
					break;
				case "pscrollhome":
					tb1.ScrollToHome();
					break;
				default:
					break;
			}
		
		}

		private void RLFunctions(object sender, ClickEventArgs e)
		{
			string currentID = ((Button)sender).ID;

			switch (currentID)
			{
				
				case "pright":
					tb2.PageRight();
					break;
				case "pleft":
					tb2.PageLeft();
					break;
				case "lright":
					tb2.LineRight();
					break;
				case "lleft":
					tb2.LineLeft();
					break;
				default:
					break;
			}
		}

    }
}