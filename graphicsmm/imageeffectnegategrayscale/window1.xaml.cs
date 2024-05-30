


//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Imaging.ImageEffects
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {

		private void WindowLoaded(object sender, EventArgs e)
		{
			ImageData id = ImageData.Create(@"myData\Sunset.jpg");

			myOriginalImage.Width = new Length((double)id.PixelWidth / 3);
			myOriginalImage.Height = new Length((double)id.PixelHeight / 3);

			myModifiedImage.Width = new Length((double)id.PixelWidth / 3);
			myModifiedImage.Height = new Length((double)id.PixelHeight / 3);

			myOriginalImage.Source = id;
		}





		private void applyImageEffectGrayscale_Click(object sender, ClickEventArgs e)
		{
			ImageEffectGrayscale gray = new ImageEffectGrayscale();

			gray.Input = myOriginalImage.Source;

			myModifiedImage.Source = gray.Output;
		}


		private void applyImageEffectNegate_Click(object sender, ClickEventArgs e)
		{
			ImageEffectNegate negate = new ImageEffectNegate();

			negate.Input = myOriginalImage.Source;
		
			myModifiedImage.Source = negate.Output;
		}


	}
}