using System;
using System.Security;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Explorer.Dialogs;

namespace ImageView
{
    /// <summary>
    /// Interaction logic for ChgDir.xaml
    /// </summary>

    public partial class ChgDir : DockPanel
    {
        // The OnLoaded handler can be run automatically when the class is loaded. To use it, add Loaded="OnLoaded" to the attributes of the root element of the .xaml file and uncomment the following line.
        // private void OnLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
        // private void ButtonClick(object sender, ClickEventArgs e) {}

		private void OnLoaded(object sender, EventArgs e)
		{
			((DockPanel)sender).Width = Application.MainWindow.Width;
			txtCurrent.Text = ((ImageView.MyApp)System.Windows.Application.Current).Files._runDir;

			//FileDialogPermission fileDialogPerm = new FileDialogPermission(FileDialogPermissionAccess.OpenSave);
			//fileDialogPerm.Assert();

			/*
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileOk += new FileOkEventHandler(ChooseDirectory);
			ofd.Title = "Select Directory";

			if (ofd.ShowDialog() == System.Windows.DialogResult.OK)
			{	
			}
			*/			//CodeAccessPermission.RevertAssert();
		}

		private void ChooseDirectory(object sender, FileOkEventArgs a)
		{
			txtNew.Text = ((OpenFileDialog)sender).Result.Parent.FileSysPath;
		}

		private void CancelDir(object sender, ClickEventArgs e)
		{
			((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).GoBack();
		}

		private void SetDir(object sender, ClickEventArgs e)
		{
			((ImageView.MyApp)System.Windows.Application.Current).Files = new ImageView.FileList(txtNew.Text);
			((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged = true;
			((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).GoBack();
		}
    }
}