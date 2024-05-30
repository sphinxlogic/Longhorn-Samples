//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ListBoxEvent_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : FlowPanel
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
		void PrintText(object sender, SelectionChangedEventArgs args)
		{
			ListItem li = ((sender as ListBox).SelectedItem as ListItem);

			tb.Text = "   You selected " + li.Content.ToString() + ".";
		}

    }
}