
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
		private int amount = 0;
		private int hue = 0;
		private float gammaRed = 0.0F;
		private float gammaGreen = 0.0F;
		private float gammaBlue = 0.0F;


		private void WindowLoaded(object sender, EventArgs e)
		{
			ImageData id = ImageData.Create(@"myData\Sunset.jpg");

			myOriginalImage.Width = new Length((double)id.PixelWidth / 3);
			myOriginalImage.Height = new Length((double)id.PixelHeight / 3);

			myModifiedImage.Width = new Length((double)id.PixelWidth / 3);
			myModifiedImage.Height = new Length((double)id.PixelHeight / 3);

			textAmount.TextContent = "Amount : " + amount.ToString();
			textHue.TextContent = "Hue : " + hue.ToString();
			textGammaRed.TextContent = "gammaRed : " + gammaRed.ToString();
			textGammaGreen.TextContent = "gammaGreen : " + gammaGreen.ToString();
			textGammaBlue.TextContent = "gammaBlue : " + gammaBlue.ToString();

			myOriginalImage.Source = id;
		}


		private void increaseHue_Click(object sender, ClickEventArgs e)
		{
			if (hue < 50)
			{
				hue++;
			}

			textHue.TextContent = "Hue : " + hue.ToString();
		}


		private void decreaseHue_Click(object sender, ClickEventArgs e)
		{
			if (hue > -50)
			{
				hue--;
			}

			textHue.TextContent = "Hue : " + hue.ToString();
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


		
		private void increaseGammaRed_Click(object sender, ClickEventArgs e)
		{
			if (gammaRed < 1)
			{
				gammaRed += 0.1F;
			}

			textGammaRed.TextContent = "gammaRed : " + gammaRed.ToString();
		}


		private void decreaseGammaRed_Click(object sender, ClickEventArgs e)
		{
			if (gammaRed > 0)
			{
				gammaRed -= 0.1F;
			}

			textGammaRed.TextContent = "gammaRed : " + gammaRed.ToString();
		}


		private void increaseGammaGreen_Click(object sender, ClickEventArgs e)
		{
			if (gammaGreen < 1)
			{
				gammaGreen += 0.1F;
			}

			textGammaGreen.TextContent = "gammaGreen : " + gammaGreen.ToString();
		}


		private void decreaseGammaGreen_Click(object sender, ClickEventArgs e)
		{
			if (gammaGreen > 0)
			{
				gammaGreen -= 0.1F;
			}

			textGammaGreen.TextContent = "gammaGreen : " + gammaGreen.ToString();
		}


		private void increaseGammaBlue_Click(object sender, ClickEventArgs e)
		{
			if (gammaBlue < 1)
			{
				gammaBlue += 0.1F;
			}

			textGammaBlue.TextContent = "gammaBlue : " + gammaBlue.ToString();
		}


		private void decreaseGammaBlue_Click(object sender, ClickEventArgs e)
		{
			if (gammaBlue > 0)
			{
				gammaBlue -= 0.1F;
			}

			textGammaBlue.TextContent = "gammaBlue : " + gammaBlue.ToString();
		}

		
		private void applyImageEffectGammaCorrect_Click(object sender, ClickEventArgs e)
		{
			ImageEffectGammaCorrect gamma = new ImageEffectGammaCorrect();

			gamma.Input = myOriginalImage.Source;
			
			gamma.RedGamma = gammaRed;
			gamma.GreenGamma = gammaGreen;
			gamma.BlueGamma = gammaBlue;

			myModifiedImage.Source = gamma.Output;
		}

		private void applyImageEffectTint_Click(object sender, ClickEventArgs e)
		{
			ImageEffectTint tint = new ImageEffectTint();

			tint.Input = myOriginalImage.Source;

			tint.Amount = amount;
			tint.Hue = hue;

			myModifiedImage.Source = tint.Output;
		}
	}
}