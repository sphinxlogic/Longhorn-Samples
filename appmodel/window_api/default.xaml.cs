using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.ComponentModel;

namespace WCSample
{
	public partial class StartPage
	{
		NavigationApplication myApp;
		NavigationWindow navWindow;
		int elementHeight, elementWidth, contentHeight, contentWidth;
		int windowTop, windowLeft;
		int maxHeight, maxWidth; //Size of the window, when maximized

		void Init (object sender, EventArgs args)
		{
			myApp = (NavigationApplication)System.Windows.Application.Current;
			myApp.LoadCompleted += new NavigationService.LoadCompletedEventHandler (OnLoadCompleted);
		}

		void OnLoadCompleted (object sender, NavigationEventArgs e)
		{
			navWindow = (NavigationWindow)myApp.MainWindow;
			navWindow.WindowState = WindowState.Maximized;
			maxHeight = (int) navWindow.Height.Value;
			maxWidth = (int) navWindow.Width.Value;
			elementWidth = (int)navWindow.Width.Value;
			elementHeight = (int)navWindow.Height.Value;
			contentHeight = (int)navWindow.ContentSize.Height;
			contentWidth = (int)navWindow.ContentSize.Width;
			windowTop = (int)navWindow.Top.Value;
			windowLeft = (int)navWindow.Left.Value;
			txtWidth.Text = elementWidth.ToString ();
			txtHeight.Text = elementHeight.ToString ();
			txtContentWidth.Text = contentWidth.ToString ();
			txtContentHeight.Text = contentHeight.ToString ();
			txtTop.Text = windowTop.ToString ();
			txtLeft.Text = windowLeft.ToString ();
			txtText.Text = navWindow.Text;
			rdbtnMaximized.IsChecked = true;
			rdbtnSingleBorder.IsChecked = true;

			//Window event hookup
			navWindow.Activated += new EventHandler (OnActivated);
			navWindow.Closed += new EventHandler (OnClosed);
			navWindow.Closing += new CancelEventHandler (OnClosing);
			navWindow.Deactivated += new EventHandler (OnDeactivated);
			navWindow.Loading += new EventHandler (OnLoading);
			navWindow.LocationChanged += new EventHandler (OnLocationChanged);
			navWindow.SizeChanged += new EventHandler (OnSizeChanged);
			navWindow.StateChanged += new EventHandler (OnStateChanged);
		}

		void Apply_ClickHandler (object sender, ClickEventArgs e)
		{
			//Window State
			if (rdbtnDefault.IsChecked)
				navWindow.WindowState = WindowState.Normal;
			else if (rdbtnMaximized.IsChecked)
				navWindow.WindowState = WindowState.Maximized;
			else
			{
				navWindow.WindowState = WindowState.Minimized;
				System.Threading.Thread.Sleep (1500);
				rdbtnMaximized.IsChecked = true;
				navWindow.WindowState = WindowState.Maximized;
			}

			//ClientSize
			if (elementHeight != Int32.Parse (txtHeight.Text) || 
				elementWidth != Int32.Parse (txtWidth.Text))
			{
				navWindow.WindowState = WindowState.Normal;
				navWindow.Height = new Length (Int32.Parse (txtHeight.Text));
				elementHeight = (int)navWindow.Height.Value;
				navWindow.Width = new Length (Int32.Parse (txtWidth.Text));
				elementWidth = (int)navWindow.Width.Value;
			}
			else if (contentHeight != Int32.Parse (txtContentHeight.Text) || 
					contentWidth != Int32.Parse (txtContentWidth.Text))
			{
				navWindow.WindowState = WindowState.Normal;
				navWindow.ContentSize = new Size (Int32.Parse (txtContentHeight.Text), Int32.Parse (txtContentWidth.Text));
				contentHeight = (int)navWindow.ContentSize.Height;
				contentWidth = (int)navWindow.ContentSize.Width;
			}

				//The window may have been resized with the mouse or maximized
			else
			{
				//Update from the window properties
				elementWidth = (int)navWindow.Width.Value;
				elementHeight = (int)navWindow.Height.Value;
				contentHeight = (int)navWindow.ContentSize.Width;
				contentWidth = (int)navWindow.ContentSize.Height;

				//Update the text boxes
				txtWidth.Text = elementWidth.ToString ();
				txtHeight.Text = elementHeight.ToString ();
				txtContentWidth.Text = contentWidth.ToString ();
				txtContentHeight.Text = contentHeight.ToString ();
			}

			//Window Location
			if (windowTop != Int32.Parse (txtTop.Text) || windowLeft != Int32.Parse (txtLeft.Text))
			{
				navWindow.WindowState = WindowState.Normal;
				navWindow.Top = new Length (Int32.Parse (txtTop.Text));
				windowTop = (int)navWindow.Top.Value;
				navWindow.Left = new Length (Int32.Parse (txtLeft.Text));
				windowLeft = (int)navWindow.Left.Value;
			}

			//The window may have been moved with the mouse
			else
			{
				//Update from the window properties
				windowTop = (int)navWindow.Top.Value;
				windowLeft = (int)navWindow.Left.Value;

				//Update the text boxes
				txtTop.Text = windowTop.ToString ();
				txtLeft.Text = windowLeft.ToString ();
			}

			if (navWindow.WindowState == WindowState.Normal)
			{
				rdbtnDefault.IsChecked = true;
			}

			//Set maximize, minimize, and control box
			navWindow.HasIcon = false;
			navWindow.HasMaximizeBox = false;
			navWindow.HasMinimizeBox = false;
			if (chkHasIcon.CheckState == CheckState.Checked)
				navWindow.HasIcon = true;

      if (chkHasMaximize.CheckState == CheckState.Checked)
        navWindow.HasMaximizeBox = true;

      if (chkHasMinimize.CheckState == CheckState.Checked)
        navWindow.HasMinimizeBox = true;

			//Window Styles
      if (rdbtnNone.IsChecked == true)
        navWindow.WindowStyle = WindowStyle.None;
      else if (rdbtnSingleBorder.IsChecked == true)
        navWindow.WindowStyle = WindowStyle.SingleBorderWindow;
      else if (rdbtn3DBorder.IsChecked == true)
        navWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
			else
				navWindow.WindowStyle = WindowStyle.ToolWindow;

			//Caption
			navWindow.Text = txtText.Text;
			return;
		}

		void Hide_ClickHandler (object sender, ClickEventArgs e)
		{
			navWindow.Hide ();
			System.Threading.Thread.Sleep (1500);
			navWindow.Show ();
		}

		void Shutdown_ClickHandler (object sender, ClickEventArgs e)
		{
			myApp.Shutdown ();
		}

		void OnActivated (object sender, EventArgs e)
		{
			//Handle window activation
		}

		void OnClosed (object sender, EventArgs e)
		{
			//Handle closed event
		}

		void OnClosing (object sender, CancelEventArgs e)
		{
			//Handle closing
		}

		void OnDeactivated (object sender, EventArgs e)
		{
			//Handle window deactivation
		}

		void OnLoading (object sender, EventArgs e)
		{
			//Handle window loading
		}

		void OnLocationChanged (object sender, EventArgs e)
		{
			//Handle window location change
		}

		void OnSizeChanged (object sender, EventArgs e)
		{
			//Handle window size change
		}

		void OnStateChanged (object sender, EventArgs e)
		{
			//Handle window state change
		}
	}
}