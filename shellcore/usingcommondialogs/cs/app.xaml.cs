namespace SDKSample
{
  using System;
  using System.Windows;
  using System.Windows.Navigation;

  public partial class MyApp
  {
    protected override void OnStartingUp(StartingUpCancelEventArgs e)
    {
      // Setup the application window.
      NavigationWindow window = new NavigationWindow();

      window.CanResize = false;

      window.Text = "Common File Dialog Sample";

      window.Width  = new Length(_width);
      window.Height = new Length(_height);

      // Show!
      window.Show();

      // Navigate to the startup page
      ((INavigator)window).Navigate(new Uri("default.xaml", false, true));

    }

    private readonly int _width  = 400;
    private readonly int _height = 500;
  }
}
