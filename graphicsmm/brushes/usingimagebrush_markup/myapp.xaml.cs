#define DEBUG  //Workaround - This allows Debug.Write to work.

//This is a list of commonly used namespaces for an application class.
using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace UsingImageBrush
{
    /// <summary>
    /// Interaction logic for Application.xaml
    /// </summary>

    public partial class MyApp : Application
    {

		

		void AppStartingUp(object sender, StartingUpCancelEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Window mainWindow = new MyWindow();
			mainWindow.Show();
			mainWindow.Height = new Length(600);
			MainWindow.Width = new Length(800);
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			MessageBox.Show("Unhandled exception: " + args.ExceptionObject.ToString());
		}

    }
}