//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace RepeatButton_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : DockPanel
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        //Event handler:  
		void Increase(object sender, ClickEventArgs e)
		{
			Int32 Num = Convert.ToInt32(txt.TextContent);

			txt.TextContent = ((Num + 1).ToString());
		}

		void Decrease(object sender, ClickEventArgs e)
		{
			Int32 Num = Convert.ToInt32(txt.TextContent);

			txt.TextContent = ((Num - 1).ToString());
		}
	}
}