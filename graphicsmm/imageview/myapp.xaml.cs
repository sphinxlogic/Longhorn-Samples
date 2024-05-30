#define Debug  //Workaround - This allows Debug.Write to work.

//This is a list of commonly used namespaces for an application class.
using System;
using System.Windows;
using System.Windows.Navigation;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Windows.Data;
using System.IO;
using System.Reflection;
using System.Collections;

namespace ImageView
{
    /// <summary>
    /// Interaction logic for Application.xaml
    /// </summary>

	public class FileList: ArrayListDataCollection , IComparer
	{
		public string _runDir;
		public bool _contentChanged;

		public FileList(string _Dir)
		{
			string[] files = null;

			try
			{
				//get current directory + Images folder
				//_runDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

				if (_Dir != "")
				{
					_runDir = _Dir;
				}
				else
					_runDir = @"C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures";
				   //_runDir = @"C:\Documents and Settings\dcurtis\My Documents\My Pictures";

				//add existing image files to the directory list
				//files = Directory.GetFiles(_runDir, "*.jp*g *.png *.gif *.tif* *.bmp");

				files = Directory.GetFiles(_runDir, "*.jp*g");

				int i;
				for (i = 0; i < files.Length; i++)
				{
					FileInfo tempFileInfo = new FileInfo(files[i]);
					
					this.Add(tempFileInfo);
				}
				
				//add existing image files to the directory list
				files = Directory.GetFiles(_runDir, "*.png");
				for (i = 0; i < files.Length; i++)
				{
					FileInfo tempFileInfo = new FileInfo(files[i]);
					
					this.Add(tempFileInfo);
				}
				//add existing image files to the directory list
				files = Directory.GetFiles(_runDir, "*.gif");
				for (i = 0; i < files.Length; i++)
				{
					FileInfo tempFileInfo = new FileInfo(files[i]);
					
					this.Add(tempFileInfo);
				}
				//add existing image files to the directory list
				files = Directory.GetFiles(_runDir, "*.tif*");
				for (i = 0; i < files.Length; i++)
				{
					FileInfo tempFileInfo = new FileInfo(files[i]);
					
					this.Add(tempFileInfo);
				}
				//add existing image files to the directory list
				files = Directory.GetFiles(_runDir, "*.bmp");
				for (i = 0; i < files.Length; i++)
				{
					FileInfo tempFileInfo = new FileInfo(files[i]);
					
					this.Add(tempFileInfo);
				}
				this.Sort(this);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, e.GetType().Name);
			}
		}
		int IComparer.Compare(object x, object y)
		{
			return string.Compare(((FileInfo)x).Name, ((FileInfo)y).Name);
		}
	}

    public partial class MyApp : NavigationApplication
    {
		public FileList Files;
		public string _CurrentImageString;
		public FileInfo _CurrentImageInfo;

		protected void OnStartingUp(object sender, StartingUpCancelEventArgs e)
		{
			Files = new FileList("");
		}
    }
}