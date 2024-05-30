

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




		private void increaseAmount_Click(object sender, ClickEventArgs e)
		{
			if (amount < 50)
			{
				amount++;
			}

			textAmount.TextContent = "Amount : " + amount.ToString();
		}


		private void decreaseAmount_Click(object sender, ClickEventArgs e)
		{
			if (amount > 0)
			{
				amount--;
			}

			textAmount.TextContent = "Amount : " + amount.ToString();
		}


		private void increaseRadius_Click(object sender, ClickEventArgs e)
		{
			if (radius < 50)
			{
				radius++;
			}
			textRadius.TextContent = "Radius : " + radius.ToString();
		}


		private void decreaseRadius_Click(object sender, ClickEventArgs e)
		{
			if (radius > 0)
			{
				radius--;
			}
			textRadius.TextContent = "Radius : " + radius.ToString();
		}

		

		
		private void applyImageEffectBlur_Click(object sender, ClickEventArgs e)
		{
			ImageEffectBlur blur = new ImageEffectBlur();

			blur.Input = myOriginalImage.Source;

			blur.Radius = (float)radius;

            blur.Expand = checkBlurExpand.IsChecked;

			myModifiedImage.Source = blur.Output;
		}



		private void applyImageEffectSharpen_Click(object sender, ClickEventArgs e)
		{
			ImageEffectSharpen sharpen = new ImageEffectSharpen();

			sharpen.Input = myOriginalImage.Source;

			sharpen.Amount = amount;
			sharpen.Radius = radius;

			myModifiedImage.Source = sharpen.Output;
		}


	}
}