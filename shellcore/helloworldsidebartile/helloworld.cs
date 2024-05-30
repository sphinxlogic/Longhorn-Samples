namespace MySidebarTiles
{
	using System.Windows;
	using System.Windows.Controls;  // Text, ContextMenuEventHandler, DockPanel, Button, 
	using System;
	using System.Windows.Desktop;     // BaseTile
	using System.Windows.Media;     // SolidColorBrush

	class HelloWorldTile : BaseTile
	{
		private Text displayText;

		public override void Initialize()
		{
			// Sidebar properties cannot be initialized in the constructor.
			// Tile supports properties view
			HasProperties = true;

			// Add a context menu event handler
			this.Sidebar.ContextMenuEvent += new ContextMenuEventHandler(ContextMenuHandler);

			// Add Foreground view
			displayText = new Text();
			displayText.TextContent = "Hello World";
			displayText.Foreground = new SolidColorBrush(Colors.White);
			this.Foreground = displayText;
		}

		public override FrameworkElement CreateFlyout()
		{
			Text flyout = new Text();

			flyout.TextContent = "Hello World Flyout";
			return flyout;
		}

		public override FrameworkElement CreateProperties()
		{
			DockPanel mainPanel = new DockPanel();
			Text property = new Text();

			property.TextContent = "Hello World Properties";
			DockPanel.SetDock(property, Dock.Top);
			mainPanel.Children.Add(property);

			// Add OK button to Property View.
			Button ok = new Button();

			ok.Content = "OK";
			ok.Click += new ClickEventHandler(OnClick);
			DockPanel.SetDock(ok, Dock.Right);
			mainPanel.Children.Add(ok);
			mainPanel.Height = new Length(75, UnitType.Pixel);
			mainPanel.Width = new Length(125, UnitType.Pixel);
			return mainPanel;
		}

		// When the OK button is clicked, close the property window.
		private void OnClick(object e, ClickEventArgs args)
		{
			this.Sidebar.CloseProperties();
		}

		private void ContextMenuHandler(object sender, ContextMenuEventArgs args)
		{
			MenuItem customMenu = new MenuItem();

			customMenu.Header = "Italic Text";
			customMenu.Mode = MenuItemMode.Checkable;
		
			if (displayText.FontStyle == FontStyle.Italic)
			{
				customMenu.IsChecked = true;
			}

			args.ContextMenu.Items.Add(customMenu);
			customMenu.Click += new ClickEventHandler(MakeItalicText);
		}

		private void MakeItalicText(object e, ClickEventArgs args)
		{
			if (displayText.FontStyle != FontStyle.Italic)
			{
				displayText.FontStyle = FontStyle.Italic;
			}
			else
			{
				displayText.FontStyle = FontStyle.Normal;
			}
		}
	}
}
