using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace WCSample
{

	public partial class SimpleIUI3 : NavigationApplication
	{
    ResourceLoaderService theService;
    CustomResourceLoader theCustomLoader;

		protected override void OnStartingUp(System.Windows.StartingUpCancelEventArgs e)
		{
			theService = (System.Windows.Resources.ResourceLoaderService) this.GetService(typeof(System.Windows.Resources.ResourceLoaderService));
			theCustomLoader = new CustomResourceLoader();
			theService.RegisterResourceLoader(theCustomLoader);
		}

		public class CustomResourceLoader : IResourceLoader 
		{
			public object LoadResource(string resID)
			{
				switch(resID)
				{
					case "default.pf" :
					{
						Default pf = new Default();
//            theService.UnregisterResourceLoader(theCustomLoader);
						return pf;
            break;
					}

					default :
						return null;
				}
			}
		}
	}
}
