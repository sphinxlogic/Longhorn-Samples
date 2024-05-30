namespace MySampleApp
{
    using System;
    using System.Windows;
	using System.Windows.Controls;
    using System.Windows.Navigation;
    using System.Globalization;
    using System.Threading;
    using System.Resources;
    using System.Reflection;
    using System.Windows.Resources;


     public partial class MyApp
     {
            protected override void OnStartingUp(StartingUpCancelEventArgs e)
            {
                // Setup the application window.
                Window Mywindow = new Window();
				

				Mywindow.CanResize = true;  

		ResourceManager rm = new ResourceManager ("stringtable", Assembly.GetExecutingAssembly());

		Mywindow.Text = rm.GetString("Title");

                Mywindow.Width  = new Length(_width);
                Mywindow.Height = new Length(_height);

                FrameworkElement root;

		// Using ResourceLoader to load pages by yourself

                IResourceLoader rsld = new ResourceLoader( ) as IResourceLoader;

                root = rsld.LoadResource("page1.xaml") as FrameworkElement;

                Mywindow.Content=root;

                Mywindow.Show();

            }

            private readonly int _width  = 500;
            private readonly int _height = 420;
     }
}
