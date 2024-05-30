//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Input;

namespace TextBoxOverview1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
		//global variables
		Boolean changedFlag;
	private void WindowLoaded(object sender, EventArgs e) {
			TextBox tb = new TextBox();
			tb.Height = new Length(50, UnitType.Pixel);
			tb.Width = new Length(200);
			tb.Margin = new Thickness(new Length(20), new Length(150), new Length(0), new Length(0));
			tb.Text = "Type here";
			CanvasOne.Children.Add(tb);

			TextBox tb2 = new TextBox();
			tb2.Height = new Length(50, UnitType.Pixel);
			tb2.Width = new Length(200);
			tb2.FontFamily = "Palatino";
			tb2.FontSize = new FontSize(22, FontSizeType.Pixel);
			tb2.FontStyle = FontStyle.Oblique;
			tb2.Margin = new Thickness(new Length(20), new Length(200), new Length(0), new Length(0));
			CanvasOne.Children.Add(tb2);
			changedFlag = false;
		}

		private void focused(object sender, FocusChangedEventArgs args)
		{
			//cast sender from object to type button
			((TextBox)sender).SelectAll();
		}

		private void setChangedFlag(object sender, TextChangedEventArgs args)
		{
			changedFlag = true;
		}

		private void ValidateChangedText(object sender, FocusChangedEventArgs args)
		{
			if (changedFlag)
			{
				//add validation logic here
				changedFlag = false;
			}
		}

    }
}