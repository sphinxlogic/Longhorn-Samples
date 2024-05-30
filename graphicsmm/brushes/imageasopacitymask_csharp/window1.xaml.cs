// Window1.xaml is used to define and layout the elements in this sample.
// The opacity masks are applied in this file, Window1.xaml.cs.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageAsOpacityMask_csharp
{
    /// <summary>
    /// Demonstrates the creation and use of an image-based
    /// opacity mask.
    /// </summary>

    public partial class Window1 : Window
    {
        // Applies opacity masks to the elements defined in
		// Window1.xaml.
        private void WindowLoaded(object sender, EventArgs e) {
			
			applyImageBasedOpacityMaskToElement(myImage);
			applyTiledImageBasedOpacityMaskToElement(anotherImage);
			applyImageBasedOpacityMaskToElement(myButton);
		}

		// Uses an image as an opacity mask.
		private void applyImageBasedOpacityMaskToElement(UIElement myElement)
		{

			try
			{
				ImageData myImageData = ImageData.Create(@"Data\tornedges.png");

				ImageBrush myImageBrush = new ImageBrush(myImageData);

				myElement.OpacityMask = myImageBrush;
			}
			catch (System.IO.FileNotFoundException fileNotFoundEx)
			{
				MessageBox.Show("Unable to find image file: " + fileNotFoundEx.ToString());
			}
			catch (System.IO.FileLoadException fileLoadEx)
			{
				MessageBox.Show("Unable to open image file: " + fileLoadEx.ToString());
			}
			catch (System.Security.SecurityException securityEx)
			{
				MessageBox.Show("Inadequite security permissions: " + securityEx.ToString());
			}
		}

		// Uses a tiled image as an opacity mask;
		private void applyTiledImageBasedOpacityMaskToElement(UIElement myElement)
		{
			

			try
			{
				ImageData myImageData = ImageData.Create(@"Data\tornedges.png");

				ImageBrush myImageBrush = new ImageBrush(myImageData);
				
				// Set the tile size and behavior.
				myImageBrush.ViewPort = new Rect(0,0,50,50);
				myImageBrush.ViewPortUnits = BrushMappingMode.Absolute;
				myImageBrush.TileMode = TileMode.Tile;

				myElement.OpacityMask = myImageBrush;
			}
			catch (System.IO.FileNotFoundException fileNotFoundEx)
			{
				MessageBox.Show("Unable to find image file: " + fileNotFoundEx.ToString());
			}
			catch (System.IO.FileLoadException fileLoadEx)
			{
				MessageBox.Show("Unable to open image file: " + fileLoadEx.ToString());
			}
			catch (System.Security.SecurityException securityEx)
			{
				MessageBox.Show("Inadequite security permissions: " + securityEx.ToString());
			}
		}
	
	}
}