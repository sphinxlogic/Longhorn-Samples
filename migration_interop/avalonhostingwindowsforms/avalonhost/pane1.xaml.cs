using System;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using MyControlLibrary;

namespace AvalonHost
{
	public partial class Pane1 : DockPanel
	{
		private void BeforeMessageBox(object sender, EventArgs args)
		{
			(wfh.Controls["mc"] as MyControl).MessageText = "Hello Avalon!";
		}
	}
}

