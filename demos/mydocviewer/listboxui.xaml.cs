namespace MyDocViewerApplication
{			
	using System.Windows.Data;		

	public partial class ListBoxUi
	{
		private void Init (object sender, System.EventArgs args)
		{		    
			TOC.DataContext=((MyDocViewerApplication.Viewer)System.Windows.Application.Current).Files;
		}
	}
}

