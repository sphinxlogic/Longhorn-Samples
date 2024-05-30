//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace OpacityMasking_csharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
       
         private void WindowLoaded(object sender, EventArgs e) {
			 applyOpacityMaskToPanelExample(myPanel);
			 applyOpacityMaskToShapeExample(myRectangle);
			 applyOpacityMaskToImageExample(myImage);
		 }


		/* The elements to which the opacity mask are applied are
		   defined in Window1.xaml.*/
		private void applyOpacityMaskToPanelExample(System.Windows.Controls.Panel myPanel)
		{
			System.Windows.Media.RadialGradientBrush myRadialGradientBrush = 
				new System.Windows.Media.RadialGradientBrush(
					System.Windows.Media.Color.FromScRGB(1,0,0,0),
					System.Windows.Media.Color.FromScRGB(0, 0, 0, 0));
			myPanel.OpacityMask = myRadialGradientBrush;
		}

		private void applyOpacityMaskToShapeExample(System.Windows.Shapes.Shape myShape)
		{
			System.Windows.Media.LinearGradientBrush myLinearGradientBrush = 
				new System.Windows.Media.LinearGradientBrush(
					System.Windows.Media.Colors.Black, 
					System.Windows.Media.Colors.Transparent, 0);
			myShape.OpacityMask = myLinearGradientBrush;
		}

		private void applyOpacityMaskToImageExample(System.Windows.Controls.Image myImage)
		{
			System.Windows.Media.LinearGradientBrush myLinearGradientBrush = 
				new System.Windows.Media.LinearGradientBrush();
			myLinearGradientBrush.AddStop(System.Windows.Media.Colors.Black, 0);
			myLinearGradientBrush.AddStop(System.Windows.Media.Colors.Transparent, 1);
			myLinearGradientBrush.StartPoint = new System.Windows.Point(0, 0);
			myLinearGradientBrush.EndPoint = new System.Windows.Point(0.75, 0.75);
			myImage.OpacityMask = myLinearGradientBrush;
		}
       

    }
}