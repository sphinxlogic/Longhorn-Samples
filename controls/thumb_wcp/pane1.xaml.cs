//This is a list of commonly used namespaces for a pane.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Thumb_wcp
{
    /// <summary>
    /// Interaction logic for Pane1.xaml
    /// </summary>

    public partial class Pane1 : Canvas
    {
        // To use PaneLoaded put Loaded="PaneLoaded" in root element of .xaml file.
        // private void PaneLoaded(object sender, EventArgs e) {}
        // Event handler:  
		void ShowDelta(object sender, DragDeltaEventArgs e)
		{
			Thumb thumb = sender as Thumb;

			changes.Text = e.HorizontalChange.ToString() + ", " + e.VerticalChange.ToString();
		}

    }
}