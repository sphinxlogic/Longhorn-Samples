//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using DependencyPropertyDemo;

namespace UsingDP
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
		private void WindowLoaded(object sender, EventArgs e)
		{
			mybtn.Content = mybtn.MagicString;
			mybtn2.Content = mybtn2.MagicString;
			mybtn3.Content = ClassDeriveFromObjectDirectly.GetNumber(mybtn3).ToString();

			MyClass myclass = new MyClass();

			myclass.Number = 99;
			tb.Text = myclass.Number.ToString();
		}
    }
}