//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace SystemColorsAndBrushes_csharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        
        private void WindowLoaded(object sender, EventArgs e) {
			
			// Use a system color in a gradient:
			System.Windows.Media.LinearGradientBrush myGradientBrush = 
				new System.Windows.Media.LinearGradientBrush(
					System.Windows.SystemColors.ControlDark, 
					System.Windows.SystemColors.ControlLight, 90);
			myBorder.BorderBrush = myGradientBrush;

			listSystemBrushes();
			listGradientExamples();
		}

		// Demonstrates using system colors to fill rectangles and buttons.
		private void listSystemBrushes()
		{
			// The window style (defined in Window1.xaml) is used to
			// specify the height and width of each of the System.Windows.Shapes.Rectangles.
			System.Windows.Controls.Text t = new System.Windows.Controls.Text();
			t.TextContent = "ActiveBorder";
			System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ActiveBorder;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ActiveCaption";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ActiveCaption;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ActiveCaptionText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ActiveCaptionText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "AppWorkspace";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.AppWorkspace;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Control";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.Control;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlDark";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ControlDark;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlDarkDark";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ControlDarkDark;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlLight";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ControlLight;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlLightLight";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ControlLightLight;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ControlText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Desktop";
			r = new System.Windows.Shapes.Rectangle();
			//r.Fill = SystemBrushes.Desktop;
			r.SetResourceReference(System.Windows.Shapes.Shape.FillProperty, System.Windows.SystemResourceNames.DesktopBrush);

			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "GradientActiveCaption";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.GradientActiveCaption;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "GradientInactiveCaption";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.GradientInactiveCaption;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "GrayText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.GrayText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Highlight";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.Highlight;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "HighlightText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.HighlightText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "HotTrack";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.HotTrack;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "InactiveBorder";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.InactiveBorder;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "InactiveCaption";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.InactiveCaption;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "InactiveCaptionText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.InactiveCaptionText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Info";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.Info;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "InfoText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.InfoText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Menu";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.Menu;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "MenuBar";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.MenuBar;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "MenuHighlight";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.MenuHighlight;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "MenuText";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.MenuText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ScrollBar";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.ScrollBar;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Window";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = SystemBrushes.Window;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(r);

			// Try it out on a button.
			t = new System.Windows.Controls.Text();
			t.TextContent = "WindowFrame";
			System.Windows.Controls.Button b = new System.Windows.Controls.Button();
			b.Width = new Length(50);
			b.Height = new Length(20);
			b.Background = SystemBrushes.WindowFrame;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(b);

			t = new System.Windows.Controls.Text();
			t.TextContent = "WindowText";
			b = new System.Windows.Controls.Button();
			b.Width = new Length(50);
			b.Height = new Length(20);
			b.Background = SystemBrushes.WindowText;
			systemBrushesPanel.Children.Add(t);
			systemBrushesPanel.Children.Add(b);
		}

		// Demonstrates using system colors to create gradients.
		private void listGradientExamples()
		{
			// The window style (defined in Window1.xaml) is used to
			// specify the height and width of each of the System.Windows.Shapes.Rectangles.

			System.Windows.Controls.Text t = new System.Windows.Controls.Text();
			t.TextContent = "System Color Gradient Examples";
			t.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
			t.FontWeight = System.Windows.FontWeight.Bold;
			System.Windows.Controls.GridPanel.SetColumnSpan(t, 8);
			gradientExamplePanel.Children.Add(t);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlDark to ControlLight";
			System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
			r.Fill = new System.Windows.Media.RadialGradientBrush(System.Windows.SystemColors.ControlDark, System.Windows.SystemColors.ControlLight);
			gradientExamplePanel.Children.Add(t);
			gradientExamplePanel.Children.Add(r);

			t = new System.Windows.Controls.Text();
			t.TextContent = "ControlDarkDark to ControlLightLight";
			r = new System.Windows.Shapes.Rectangle();
			r.Fill = new System.Windows.Media.RadialGradientBrush(System.Windows.SystemColors.ControlDarkDark, System.Windows.SystemColors.ControlLightLight);
			gradientExamplePanel.Children.Add(t);
			gradientExamplePanel.Children.Add(r);
			
			// Try it out on a button.
			t = new System.Windows.Controls.Text();
			t.TextContent = "Desktop to AppWorkspace";
			System.Windows.Controls.Button b = new System.Windows.Controls.Button();
			b.Width = new Length(50);
			b.Height = new Length(20);
			b.Background = new System.Windows.Media.RadialGradientBrush(System.Windows.SystemColors.Desktop, System.Windows.SystemColors.AppWorkspace);
			gradientExamplePanel.Children.Add(t);
			gradientExamplePanel.Children.Add(b);

			t = new System.Windows.Controls.Text();
			t.TextContent = "Desktop to Control";
			b = new System.Windows.Controls.Button();
			b.Width = new Length(50);
			b.Height = new Length(20);
			b.Background = new System.Windows.Media.RadialGradientBrush(System.Windows.SystemColors.Desktop, System.Windows.SystemColors.Control);
			gradientExamplePanel.Children.Add(t);
			gradientExamplePanel.Children.Add(b);
			
		}

    }
}