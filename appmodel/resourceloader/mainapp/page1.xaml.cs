using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Application1
{
  public partial class Pane1 : DockPanel
  {
 		private void OnBtnVersionClick(object sender, ClickEventArgs e)
		{
			this.txtVersion.TextContent = "Version 1.0.0.0";
		}
  }
}