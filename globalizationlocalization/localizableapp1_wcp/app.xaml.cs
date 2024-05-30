namespace SimpleApp
{
	using System;
	using System.Windows;
	using System.Windows.Navigation;
	using System.Globalization;
	using System.Threading;
	using System.Resources;
	using System.Reflection;


     public partial class MyApp
     {
            protected override void OnStartingUp(StartingUpCancelEventArgs e)
            {

	Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

                NavigationWindow window = new NavigationWindow();

                window.CanResize = false;  

		ResourceManager rm = new ResourceManager ("stringtable", Assembly.GetExecutingAssembly());

		String str = rm.GetString("Title");
		

                window.Text = str;

                window.Width  = new Length(_width);
                window.Height = new Length(_height);

                window.Show();

                // Navigate to the startup page 

                ((INavigator)window).Navigate(new Uri("page1.xaml", false, true));

            }

            private readonly int _width  = 640;
            private readonly int _height = 480;
     }
}
