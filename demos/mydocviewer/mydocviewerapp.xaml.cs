namespace MyDocViewerApplication
{
	using System;
	using System.Collections;
	using System.Windows;
	using System.Windows.Data;	
	using System.Windows.Navigation;			
	using System.IO;
	using System.Reflection;

	public partial class Viewer
	{		
		public FileList Files;

		// Setup the application window.
		protected override void OnStartingUp(StartingUpCancelEventArgs e)
		{
			Files = new FileList();

			NavigationWindow window = new NavigationWindow();

			window.CanResize = true;
			window.Text = "Longhorn Explorer";		

			// Show!
			window.Show();

			// Navigate to the startup page 
			((INavigator)window).Navigate(new Uri("ListBoxUI.xaml", false, true));
		}
	}

	public delegate int BeforeDeleteHandler(object sender, int index);

	public class FileList : ArrayListDataCollection
	{
		internal static string _runDir;
		public FileList()
		{
			string[] files = null;
			try
			{
				//get current directory
				_runDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

				//add existing xaml files to the directory list
				files = Directory.GetFiles(_runDir, "*.xaml");
				for(int i = 0; i < files.Length; i++)
				{					
					FileInfo tempFileInfo = new FileInfo(Path.GetFileName(files[i]));
					this.Add(tempFileInfo);
					if(i > 6)
					    break;
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message, e.GetType().Name);
			}
		}
	}
}
