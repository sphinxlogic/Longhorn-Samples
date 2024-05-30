namespace LoginScreen
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
	   using System.Windows.Media.Animation; 

	public partial class Default
       {
	private double _scaleSize = 1;	//Current zoom magnification
        private static FrameworkElement _newResources;
        private static bool resourceLoaded = false;
		FrameworkElement _defaultStyle ;
		FrameworkElement _lunaStyle ;
		FrameworkElement _toonsStyle ;
		FrameworkElement _xBoxStyle ;
        
	
		private void Init (object sender, System.EventArgs args)
		{			
			_defaultStyle = (FrameworkElement)new Resources_Default();
			_lunaStyle = (FrameworkElement)new Resources_Luna();
			_toonsStyle = (FrameworkElement)new Resources_Toons();
			_xBoxStyle = (FrameworkElement)new Resources_XBox();


            Brush brush = (Brush)_defaultStyle.Resources["LogoBrush"];
            brush = (Brush)_lunaStyle.Resources["BlueBall"];
            brush = (Brush)_lunaStyle.Resources["LogoBrush"];
            brush = (Brush)_toonsStyle.Resources["LogoBrush"];
            brush = (Brush)_xBoxStyle.Resources["GoldBall"];
            brush = (Brush)_xBoxStyle.Resources["GreenBall"];
            brush = (Brush)_xBoxStyle.Resources["LogoBrush"];
			_newResources = _defaultStyle;
			_newResources.Resources.Seal ();
			body.Resources = _newResources.Resources;
			
		}

		private void OnMouseEnter (object sender, MouseEventArgs args)
		{
			resourceLoaded = true;
		}
	       
		void ChangeUser (object sender, SelectionChangedEventArgs e)
		{
				if (resourceLoaded==false)
					return;
			            
				switch (UserTilesListBox.SelectedIndex)

				{
					case 0:
						_newResources = _defaultStyle;
							break;

					case 1:
						_newResources = _lunaStyle;
						break;

					case 2:
						_newResources = _toonsStyle;
						break;

					case 3:
						_newResources = _xBoxStyle;
						break;
				}
			    
					_newResources.Resources.Seal();
					body.Resources = _newResources.Resources;
					

				}
		

		//This code enables the zoom functionality in the app. 
		//Press CTRL + Left Click to zoom in. CTRL + Right Click to zoom out.

		private void Zoom (object sender, MouseButtonEventArgs args)
		{
			Console.WriteLine ("Zoom");
			Console.WriteLine ("Left control=" + Keyboard.GetKeyState (Key.LControlKey).ToString ());
			Console.WriteLine ("Right control=" + Keyboard.GetKeyState (Key.RControlKey).ToString ());
			if (Keyboard.IsKeyDown (Key.LControlKey) || Keyboard.IsKeyDown (Key.RControlKey))
			{
				//Console.WriteLine ("Left control=" + Keyboard.GetKeyState (Key.LControlKey).ToString ());
				//Console.WriteLine ("Right control=" + Keyboard.GetKeyState (Key.RControlKey).ToString ());
				Point mousePosition = args.GetPosition (null);

				Console.WriteLine ("X=" + mousePosition.X.ToString () + " Y=" + mousePosition.Y);

				double oldScale = _scaleSize;

				if (args.LeftButton == MouseButtonState.Pressed)
				{
					Console.WriteLine ("LeftButton is Down");
					_scaleSize = _scaleSize * 2;
				}

				if (args.RightButton == MouseButtonState.Pressed)
				{
					Console.WriteLine ("Right Button is Down");
					_scaleSize = _scaleSize * .5;
					mousePosition = new Point (0, 0);
				}

				if (_scaleSize < 1)
					_scaleSize = 1;

				Console.WriteLine ("Scale is " + _scaleSize.ToString ());
				Console.WriteLine ("");

				DoubleAnimation danim = new DoubleAnimation (_scaleSize, 500);;
				danim.Begin = Time.Immediately;
				danim.Fill = TimeFill.Hold;
				RootDecorator.Transform = new ScaleTransform (oldScale, danim, oldScale, danim, mousePosition, PointAnimationCollection.Empty);
			}
		}	
	}

}