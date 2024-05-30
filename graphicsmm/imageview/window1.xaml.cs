using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageView
{
	/// <summary>
	/// Interaction logic for Pane2.xaml
	/// </summary>
	public partial class Window1 : NavigationWindow
	{
		// The OnLoaded handler can be run automatically when the class is loaded. To use it, add Loaded="OnLoaded" to the attributes of the root element of the .xaml file and uncomment the following line.
		// private void OnLoaded(object sender, EventArgs e) {}
		// Sample event handler:  
		// private void ButtonClick(object sender, ClickEventArgs e) {}
		private void OnLoaded(object sender, EventArgs e)
		{
			DirName.TextContent = ((ImageView.MyApp)System.Windows.Application.Current).Files._runDir;
			TOC.DataContext = ((ImageView.MyApp)System.Windows.Application.Current).Files;
		}

		private void OnNavigated(object sender, NavigationEventArgs e)
		{
			if (((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged)
			{
				((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged = false;
				DirName.TextContent = ((ImageView.MyApp)System.Windows.Application.Current).Files._runDir;
				TOC.DataContext = ((ImageView.MyApp)System.Windows.Application.Current).Files;
			}
		}

		/*
		protected override void OnActivated(EventArgs e)
		{
			if (((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged)
			{
				((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged = false;
				DirName.TextContent = ((ImageView.MyApp)System.Windows.Application.Current).Files._runDir;
				TOC.DataContext = ((ImageView.MyApp)System.Windows.Application.Current).Files;
			}
		}
		*/
		private void OnClicked(object sender, ClickEventArgs args)
		{
			switch (((Button)sender).ID.ToString())
			{
				case "btnChgDir":
					System.Windows.Application.Current.MainWindow.Text = "Change Image Folder";
					((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).Navigate(new Uri("ChgDir.xaml", false, true));
					break;
/*
				case "btnMainPage":
					((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).Navigate(new Uri("Window1.xaml", false, true));
					break;
*/
			}
		}

		private void OnSelect(object sender, SelectionChangedEventArgs args)
		{
			ListBox list = ((ListBox)sender);
			if (list != null)
			{
				int index = list.SelectedIndex;	//Save the selected index 
				if (index >= 0)
				{
					string selection = list.SelectedItem.ToString();

					if ((selection != null) && (selection.Length != 0))
					{
						((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageString = selection;
						((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageInfo = (FileInfo)list.SelectedItem;
						((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).Navigate(new Uri("ViewImage.xaml", false, true));
					}
				}
			}
		}
	}
	internal class StringToImageDataThumb : IDataTransformer
	{
		public object Transform(object o, DependencyProperty dp, System.Globalization.CultureInfo culture)
		{
			string filename = o as string;

			if ((filename != null) && (filename.Length > 0))
			{
				filename = "Uri=" + filename + " DecodeWidth=120";
				return ImageData.Create(filename);
			}

			return null;
		}

		public object InverseTransform(object o, System.Reflection.PropertyInfo info, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
