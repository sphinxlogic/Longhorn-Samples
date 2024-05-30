namespace ExternalResources
{
       using System;
       using System.Collections;
       using System.IO;
       using System.Windows;
       using System.Windows.Controls;
       using System.Windows.Media;
       using System.Windows.Navigation;
       using System.Windows.Serialization;
       using System.Windows.Input;

	public partial class Page1
	
	{
	private static FrameworkElement _newResources;
        private static bool resourceLoaded = false;
        private static string _resourceXaml = "resources.xaml";
		

	private void Init (object sender, System.EventArgs args)
		{			
			Stream stream = File.OpenRead (_resourceXaml);
			_newResources = (FrameworkElement)Parser.LoadXml (stream);
			_newResources.Resources.Seal ();
			body.Resources = _newResources.Resources;
		}
	}

}