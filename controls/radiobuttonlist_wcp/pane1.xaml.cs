//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace RadioButtonList_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : Canvas
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
		void WriteText(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			System.Windows.Controls.RadioButton li = ((sender as System.Windows.Controls.RadioButtonList).SelectedItem as System.Windows.Controls.RadioButton);

			btn.Content = "You clicked " + li.Content.ToString();
		}

    }
}