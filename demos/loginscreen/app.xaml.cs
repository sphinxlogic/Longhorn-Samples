namespace LoginScreen
{
	using System;
	using System.Windows;
	using System.Windows.Navigation;

	public partial class LoginScreenDemo
	{
		private readonly int _width = 1024;
		private readonly int _height = 768;
		
		protected override void OnStartingUp (System.Windows.StartingUpCancelEventArgs e)
		{
			// Setup the application window.
			System.Windows.Navigation.NavigationWindow window = new System.Windows.Navigation.NavigationWindow ();

			window.CanResize = true;
			window.Text = "Login Screen";
			window.Width = new System.Windows.Length (_width);
			window.Height = new System.Windows.Length (_height);

			// Show!
			window.Show ();

			// Navigate to the startup page 
			((System.Windows.Navigation.INavigator)window).Navigate (new Uri ("default.xaml", false, true));
		}

		
	}
}

