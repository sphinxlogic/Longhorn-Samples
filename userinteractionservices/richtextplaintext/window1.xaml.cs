//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Commands;

namespace RichTextPlainText
{
    /// <summary>
    /// This sample shows how to enable and disable text formatting in a textbox. When text formatting is disabled,
    /// any formatting already applied to the text is removed.
    /// </summary>

    public partial class Window1 : Window
    {
        private void WindowLoaded(object sender, EventArgs e) {
			//richtext is on by default. Only enable button once plain text has been toggled on.
			btnrichtext.IsEnabled = false;
		}
		//these commands will only work if the command links are enabled. In plain text mode, the buttons
		//that call this method are disabled, but they wouldn't work even if the buttons could be 
		//clicked. 
		//In richt text mode, the buttons are enabled, as are the command links.
		private void doCommand(object sender, System.Windows.Controls.ClickEventArgs e)
		{
			string currentID = ((System.Windows.Controls.Button)sender).ID;

			switch (currentID)
			{
				case "btnbold":
					try
					{
						tb1.RaiseCommand(System.Windows.Commands.StandardCommands.BoldCommand);
					}
					catch (System.Exception exception)
					{
						System.Windows.MessageBox.Show(exception.Message);
					}
					break;

				case "btnitalic":
					try
					{
						tb1.RaiseCommand(System.Windows.Commands.StandardCommands.ItalicCommand);
					}
					catch (System.Exception exception)
					{
						System.Windows.MessageBox.Show(exception.Message);
					}
					break;

				case "btnunderline":
					try
					{
						tb1.RaiseCommand(System.Windows.Commands.StandardCommands.UnderlineCommand);
					}
					catch (System.Exception exception)
					{
						System.Windows.MessageBox.Show(exception.Message);
					}
					break;

				default:
					break;
			}
		}

		private void RichText(object sender, ClickEventArgs args)
		{
			foreach (CommandLink link in tb1.CommandLinks)
			{
				Command command = link.Command;

				if (command == StandardCommands.BoldCommand || command == StandardCommands.ItalicCommand || command == StandardCommands.UnderlineCommand)
					link.Enabled = true;
			}

			//set the state of the toolbar buttons
			btnbold.IsEnabled = true;
			btnitalic.IsEnabled = true;
			btnunderline.IsEnabled = true;
			btnplaintext.IsEnabled = true;
			btnrichtext.IsEnabled = false;
		}

		private void PlainText(object sender, ClickEventArgs args)
		{
			foreach (CommandLink link in tb1.CommandLinks)
			{
				Command command = link.Command;

				if (command == StandardCommands.BoldCommand || command == StandardCommands.ItalicCommand || command == StandardCommands.UnderlineCommand)
					link.Enabled = false;
			}
			//set the state of the toolbar buttons
			btnbold.IsEnabled = false;
			btnitalic.IsEnabled = false;
			btnunderline.IsEnabled = false;
			btnplaintext.IsEnabled = false;
			btnrichtext.IsEnabled = true;

			//replace the formatted text with plain text. The Text property is a string so it effectively removes formatting
			//by not storing the formatting information
			string s = tb1.Text;
			tb1.Clear();
			tb1.Text = s;
		}
	}
    
}