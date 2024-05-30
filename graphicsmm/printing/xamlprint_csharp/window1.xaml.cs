//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;

namespace xamlprint_csharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
       
        private void WindowLoaded(object sender, EventArgs e) {
		
		
		}
	

		void printToDefaultPrinterButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try
			{
				using (Context.Access())
				{
					PrintExample.PrintPaginatedVisualToDefaultPrinter();
				}
			}
			catch (System.Printing.PrintSubSystem.PrintServerException ex)
			{
				MessageBox.Show("There is no default printer. Please select a default printer and try again.");
			}
		}

		void printToSelectedPrinterButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try {
				using (Context.Access())
				{
					PrintExample.PrintPaginatedVisualToSelectedPrinter();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		void matchVisualToPaperSizeAndPrintButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try {
			using (Context.Access())
			{
				PrintExample.QueryPaperSize();
			}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		void mixedPageOrientationVisualPrintButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try
			{
				using (Context.Access())
				{
					PrintExample.MixedOrientationPrint();
				}
			}
			catch (System.Printing.PrintSubSystem.PrintServerException ex)
			{
				MessageBox.Show("There is no default printer. Please select a default printer and try again.");
			}
		}

		void eventDrivenVisualPrintButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try {
				using (Context.Access())
				{
					PrintExample.EventDriven();
				}
			}
			catch (System.Printing.PrintSubSystem.PrintServerException ex)
			{
				MessageBox.Show("There is no default printer. Please select a default printer and try again.");
			}
		}

		void printToFileButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try
			{
				PrintExample.PrintToFile();
			}
			catch (System.Printing.PrintSubSystem.PrintServerException ex)
			{
				MessageBox.Show("There is no default printer. Please select a default printer and try again.");
			}
		}

		void printElementButton_click(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			try
			{
				PrintExample.PrintXamlFile(@"data\printme.xaml");
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.ToString());
			}
		}


    }
}