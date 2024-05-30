

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
		private int radius = 0;
		private int amount = 0;

		private void WindowLoaded(object sender, EventArgs e)
		{
			ImageData id = ImageData.Create(@"myData\Sunset.jpg");

			myOriginalImage.Width = new Length((double)id.PixelWidth / 3);
			myOriginalImage.Height = new Length((double)id.PixelHeight / 3);

			myModifiedImage.Width = new Length((double)id.PixelWidth / 3);
			myModifiedImage.Height = new Length((double)id.PixelHeight / 3);

			textRadius.TextContent = "Radius : " + radius.ToString();
			textAmount.TextContent = "Amount : " + amount.ToString();

			myOriginalImage.Source = id;
		}

		private void applyImageEffectFlipRotate_Click(object sender, ClickEventArgs e)
		{
			ImageEffectFlipRotate fliprotate = new ImageEffectFlipRotate();

			fliprotate.Input = myOriginalImage.Source;

			fliprotate.FlipX = checkFlipX.IsChecked;
			fliprotate.FlipY = checkFlipY.IsChecked;
			if (rb0.IsChecked) fliprotate.Rotation = 0;
			if (rb90.IsChecked) fliprotate.Rotation = 90;
			if (rb180.IsChecked) fliprotate.Rotation = 180;
			if (rb270.IsChecked) fliprotate.Rotation = 270;
//			if (rb360.IsChecked) fliprotate.Rotation = 360;

			myModifiedImage.Source = fliprotate.Output;
		}


	}
}