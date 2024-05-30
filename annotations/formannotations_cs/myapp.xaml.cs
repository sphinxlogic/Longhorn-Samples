#define DEBUG  //Workaround - This allows Debug.Write to work.

//This is a list of commonly used namespaces for an application class.
using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace FormAnnotations
{
    /// <summary>
    /// Interaction logic for Application.xaml
    /// </summary>

    public partial class MyApp : Application
    {
        void AppStartingUp(object sender, StartingUpCancelEventArgs e)
        {
            Window mainWindow = new Window1();

            mainWindow.Width  = new Length(500);
            mainWindow.Height = new Length(315);

            mainWindow.Show();
		}
    }
}