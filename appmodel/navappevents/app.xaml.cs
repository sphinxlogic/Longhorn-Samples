using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCSample
{
	public partial class NavApp
	{
		protected override void OnStartingUp(System.Windows.StartingUpCancelEventArgs e)
		{
			//Application Events
			this.Activate += new EventHandler(OnActivate);
			this.Deactivate += new EventHandler(OnDeactivate);
			this.SessionEnding += new SessionEndingCancelEventHandler (OnSessionEnding);
			this.ShuttingDown += new ShuttingDownEventHandler (OnShuttingDown);
			this.StartingUp += new StartingUpCancelEventHandler (OnStartingUp);
			
			//Navigation Events
			this.LoadCompleted += new NavigationService.LoadCompletedEventHandler (OnLoadCompleted);
			this.Navigated += new NavigationService.NavigatedEventHandler (OnNavigated);
			this.Navigating += new NavigationService.NavigatingCancelEventHandler (OnNavigating);
			this.NavigationError += new NavigationService.NavigationErrorCancelEventHandler (OnNavigationError);
			this.NavigationProgress += new NavigationService.NavigationProgressEventHandler (OnNavigationProgress);
			this.NavigationStopped += new NavigationService.NavigationStoppedEventHandler (OnNavigationStopped);

		}

		void OnActivate (object sender, EventArgs e)
		{
			//Process Activate
			return;
		}

		void OnDeactivate (object sender, EventArgs e)
		{
			//Process Deactivate
			return;
		}

		void OnSessionEnding (object sender, SessionEndingCancelEventArgs e)
		{
			//Process SessionEnding
			return;
		}

		void OnShuttingDown (object sender, ShuttingDownEventArgs e)
		{
			//Process ShuttingDown
			return;
		}

		void OnStartingUp (object sender, StartingUpCancelEventArgs e)
		{
			//Process StartingUp
			return;
		}

		void OnLoadCompleted (object sender, NavigationEventArgs e)
		{
			//Process LoadCompleted
			return;
		}

		void OnNavigated (object sender, NavigationEventArgs e)
		{
			//Process Navigated
			return;
		}

		void OnNavigating (object sender, NavigatingCancelEventArgs e)
			{
				//Process Navigating

				return;
			}

		void OnNavigationError (object sender, NavigationErrorCancelEventArgs e)
		{
			//Process NavigationError
			return;
		}
		void OnNavigationProgress (object sender, NavigationProgressEventArgs e)
		{
			//Process NavigationProgress
			return;
		}
		void OnNavigationStopped (object sender, NavigationEventArgs e)
		{
			//Process NavigationStopped
			return;
		}
		}
}