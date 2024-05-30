#define Debug  //Workaround - This allows Debug.Write to work.

//This is a list of commonly used namespaces for an application class.
using System;
using System.Windows;
using System.Windows.Navigation;
using System.Data;
using System.Xml;
using System.Configuration;


namespace WordGame1
{
    /// <summary>
    /// Interaction logic for Application.xaml
    /// </summary>

    public partial class MyApp : NavigationApplication
    {
      string [] wordList = {"member", "object", "simple", "syntax", "attach"};

      public void AppStartUp(object sender, StartingUpCancelEventArgs e)
      {
        this.Properties["wordList"] = wordList;
      }

    }
}