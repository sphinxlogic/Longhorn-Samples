//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace BtnColor_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : FlowPanel
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
		void OnClick1(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			btn1.Background = System.Windows.Media.Brushes.LightBlue;
		}

		void OnClick2(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			btn2.Background = System.Windows.Media.Brushes.Pink;
		}

		void OnClick3(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			btn1.Background = System.Windows.Media.Brushes.Pink;
			btn2.Background = System.Windows.Media.Brushes.LightBlue;
		}
	}
}