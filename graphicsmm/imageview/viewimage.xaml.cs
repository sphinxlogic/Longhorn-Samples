using System;
using System.IO;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ImageView
{
    /// <summary>
    /// Interaction logic for ViewImage.xaml
    /// </summary>

    public partial class ViewImage : DockPanel
    {
		private ImageSource m_Image;
		private Stream m_ImageStream;
		private double m_curWidth;

        // The OnLoaded handler can be run automatically when the class is loaded. To use it, add Loaded="OnLoaded" to the attributes of the root element of the .xaml file and uncomment the following line.
        // private void OnLoaded(object sender, EventArgs e) {}
        // Sample event handler:  
        // private void ButtonClick(object sender, ClickEventArgs e) {}
		private void OnLoaded(object sender, EventArgs e)
		{
			string sourceString = ((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageString;

			ImageName.TextContent = sourceString;

			ImageData id = ImageData.Create(sourceString);
			Image1.Source = id;

			FileIOPermission filePermission = new FileIOPermission(PermissionState.None);

			filePermission.AllLocalFiles = FileIOPermissionAccess.AllAccess;
			filePermission.Assert();

			// Create a non-cached ImageSource, if possible.
			if (m_ImageStream != null)
			{
				m_ImageStream.Close();
				m_ImageStream = null;
				m_Image = null;
			}
			string mimeType = null;
			m_ImageStream = new System.IO.FileStream(sourceString, FileMode.Open, FileAccess.Read, FileShare.Read);
			if (m_ImageStream != null)
			{
				CodecFilter filter = new CodecFilter();

				filter.ImageStream = m_ImageStream;
				filter.HasDecoder = true;

				IEnumerator codecEnumerator = ImageCodecCollection.Codecs.GetEnumerator(filter);

				if (!codecEnumerator.MoveNext())
				{
					m_Image = null;
					m_ImageStream.Close();
					m_ImageStream = null;
				}
				else
				{
					CodecInfo decoderInfo = (CodecInfo)codecEnumerator.Current;
					ImageDecoder decoder = decoderInfo.CreateDecoderInstance(m_ImageStream, true);
					IEnumerator enumMimes = decoder.Info.MimeTypes;
					if (enumMimes.MoveNext())
					{
						mimeType = (string)enumMimes.Current;
					}

					m_Image = decoder.GetFrame(0, false, IntegerRect.Empty);
				}
			}
			else
			{
				m_Image = null;
			}

			PixelFormat format = PixelFormats.Undefined;
			
			if (m_Image != null)
			{
				format = m_Image.Format;
			}

			CodeAccessPermission.RevertAssert();

			Image1MetadataFormat.TextContent += " " + format.ToString();

			if (mimeType != null)
			{
				Image1MetadataFormat.TextContent += "\nMime: " + mimeType;
			}
			
			int width = id.PixelWidth;
			int height = id.PixelHeight;

			m_curWidth = (double)width;
			Image1.Width = new Length(width);
			// Image1.Height = new Length(height);

			long imageSize = ((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageInfo.Length;

			Image1MetadataSize.TextContent += " " + width.ToString() + " x " + height.ToString();
			Image1MetadataFileSize.TextContent += " " + ((imageSize + 512) / 1024).ToString() + "k";

			ImageMetadata metadata = id.Metadata;
			ImageExchangeMetadata codecMetadata = null;

			if (metadata != null)
			{
				codecMetadata = metadata.CodecMetadata;
				if (codecMetadata != null)
				{
					ImageExchangeMetadataEnumerator enumerator = codecMetadata.GetEnumerator();

					while (enumerator.MoveNext())
					{
						ImageExchangeProperty property = enumerator.Current;

						if (property != null)
						{
							string sid = property.ImageExchangeID.StringID;

							if ((sid != null) && (sid.Length > 0))
							{
								if (property.Value is Array)
								{
									if (((Array)(property.Value)).Rank == 1)
									{
										Image1Metadata.TextContent += sid + ": " + ((Array)(property.Value)).GetValue(0).ToString() + "\n";
									}
								}
								else
								{
									Image1Metadata.TextContent += sid + ": " + property.Value.ToString() + "\n";
								}
							}
						}
					}
				}
			}
		}

		private void OnClicked(object sender, ClickEventArgs args)
		{
			switch (((Button)sender).ID.ToString())
			{
				case "btnZoomIn":
					m_curWidth *= 1.25;
					Image1.Width = new Length((int)(m_curWidth + 0.5));
					break;
				case "btnZoomOut":
					m_curWidth *= 0.80;
					Image1.Width = new Length((int)(m_curWidth + 0.5));
					break;
				case "btnZoom100":
					m_curWidth = (double)(m_Image.PixelWidth);
					Image1.Width = new Length (m_Image.PixelWidth);
					break;
/*
				case "btnMainPage":
					((NavigationWindow)((MyApp)(System.Windows.Application.Current)).MainWindow).Navigate(new Uri("Window1.xaml", false, true));
					break;
*/
			}
		}
		
		
		private void Jpeg24Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderJpeg();
			PixelFormat format = PixelFormats.RGB24;
			SaveFile(encoder, format, ".jpg");
		}

		private void Jpeg8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderJpeg();
			PixelFormat format = PixelFormats.Gray8;
			SaveFile(encoder, format, ".jpg");
		}

		private void Bmp1Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.Indexed1;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp4Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.Indexed4;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.Indexed8;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp555Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.RGB555;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp565Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.RGB565;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp24Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.RGB24;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp32Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.RGB32;
			SaveFile(encoder, format, ".bmp");
		}

		private void Bmp64Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderBmp();
			PixelFormat format = PixelFormats.ARGB64;
			SaveFile(encoder, format, ".bmp");
		}

		private void Gif8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderGif();
			PixelFormat format = PixelFormats.Indexed8;
			SaveFile(encoder, format, ".gif");
		}

		private void PngBWClicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.BlackWhite;
			SaveFile(encoder, format, ".png");
		}

		private void Png1Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.Indexed1;
			SaveFile(encoder, format, ".png");
		}

		private void Png4Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.Indexed4;
			SaveFile(encoder, format, ".png");
		}

		private void Png8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.Indexed8;
			SaveFile(encoder, format, ".png");
		}

		private void PngG8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.Gray8;
			SaveFile(encoder, format, ".png");
		}

		private void Png24Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.RGB24;
			SaveFile(encoder, format, ".png");
		}

		private void Png32Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderPng();
			PixelFormat format = PixelFormats.ARGB32;
			SaveFile(encoder, format, ".png");
		}

		private void TiffBWClicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.BlackWhite;
			SaveFile(encoder, format, ".tif");
		}

		private void Tiff4Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.Indexed4;
			SaveFile(encoder, format, ".tif");
		}

		private void Tiff8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.Indexed8;
			SaveFile(encoder, format, ".tif");
		}

		private void TiffG8Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.Gray8;
			SaveFile(encoder, format, ".tif");
		}

		private void Tiff24Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.RGB24;
			SaveFile(encoder, format, ".tif");
		}

		private void Tiff32Clicked(object sender, ClickEventArgs e)
		{
			ImageEncoder encoder = new ImageEncoderTiff();
			PixelFormat format = PixelFormats.ARGB32;
			SaveFile(encoder, format, ".tif");
		}

		private void SaveFile(ImageEncoder encoder, PixelFormat format, string extension)
		{
			FileIOPermission filePermission = new FileIOPermission(PermissionState.None);
			filePermission.AllLocalFiles = FileIOPermissionAccess.AllAccess;
			filePermission.Assert();

			string filename = ((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageInfo.FullName.Replace(((ImageView.MyApp)System.Windows.Application.Current)._CurrentImageInfo.Extension, " " + format.ToString() + extension);
			Stream imageStreamDest = new System.IO.FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);

			encoder.Add(m_Image);
			if (extension != ".gif")
			{
				encoder.EncodeFormat = format;
			}
			encoder.Save(imageStreamDest);
			imageStreamDest.Close();
			CodeAccessPermission.RevertAssert();

			((ImageView.MyApp)System.Windows.Application.Current).Files = new ImageView.FileList(((ImageView.MyApp)System.Windows.Application.Current).Files._runDir);
			((ImageView.MyApp)System.Windows.Application.Current).Files._contentChanged = true;
		}
	}
}