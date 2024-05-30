//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ButtonwithImage_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : Canvas
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
		void OnClick(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			btn1.Background = System.Windows.Media.Brushes.Black;
			btn2.Content = "My favorite photo";
		}
     

    }
}