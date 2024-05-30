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

namespace DrawingAsOpacityMask_csharp
{
    /// <summary>
    /// Demonstrates the creation and use of a DrawingBrush-based
    /// opacity mask.
    /// </summary>

    public partial class Window1 : Window
    {
        // Applies opacity masks to the elements defined in
		// Window1.xaml.
        private void WindowLoaded(object sender, EventArgs e) {
			applyDrawingBrushBasedOpacityMaskToElement(myImage);
			applyDrawingBrushBasedOpacityMaskToElement2(anotherImage);
			applyTiledDrawingBrushOpacityMask(myButton);
		}

		// Uses a DrawingBrush as an opacity mask.
		private void applyDrawingBrushBasedOpacityMaskToElement(UIElement myElement)
		{
			// Create a Drawing and use it to obtain a DrawingContext.
			Drawing myDrawing = new Drawing();
			DrawingContext myDrawingContext = myDrawing.Open();

			myDrawingContext.DrawRectangle(
				Brushes.Transparent,
				new Pen(Brushes.Black, 0.1),
				new Rect(0.05, 0.05, 0.9, 0.9));

			myDrawingContext.DrawRoundedRectangle(
				new RadialGradientBrush(Colors.Black, Colors.Transparent),
				new Pen(Brushes.Transparent, 0.1),
				new Rect(0.05, 0.05, 0.9, 0.9),
				0.25,
				0.25);
			
			myDrawingContext.Close();

			// Use the DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);
			myElement.OpacityMask = myDrawingBrush;
		}

	
		private void applyDrawingBrushBasedOpacityMaskToElement2(UIElement myElement)
		{
			// Create a Drawing and use it to obtain a DrawingContext.
			Drawing myDrawing = new Drawing();
			DrawingContext myDrawingContext = myDrawing.Open();

			myDrawingContext.DrawEllipse(
				Brushes.Black, 
				null, 
				new Point(0.5,0.5),
				0.5,
				0.25);
			myDrawingContext.DrawEllipse(
				Brushes.Black, 
				null, 
				new Point(0.5,0.5),
				0.25,
				0.5);
				
			myDrawingContext.Close();

			// Use the DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			myElement.OpacityMask = myDrawingBrush;
		}


		// Uses a tiled image as an opacity mask;
		private void applyTiledDrawingBrushOpacityMask(UIElement myElement)
		{
			// Create a Drawing and use it to obtain a DrawingContext.
			Drawing myDrawing = new Drawing();
			DrawingContext myDrawingContext = myDrawing.Open();

			myDrawingContext.DrawRectangle(
				Brushes.Transparent,
				new Pen(Brushes.Black, 0.1),
				new Rect(0.05, 0.05, 0.9, 0.9));

			myDrawingContext.DrawRoundedRectangle(
				new RadialGradientBrush(Colors.Black, Colors.Transparent),
				new Pen(Brushes.Transparent, 0.1),
				new Rect(0.05, 0.05, 0.9, 0.9),
				0.25,
				0.25);
			myDrawingContext.DrawRoundedRectangle(
				new LinearGradientBrush(Colors.Black, Colors.Transparent, 90),
				new Pen(Brushes.Transparent, 0.1),
				new Rect(0.05, 0.05, 0.9, 0.9),
				0.25,
				0.25);

			myDrawingContext.Close();

			// Use the DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			myDrawingBrush.ViewPort = new Rect(0, 0, 0.25, 0.25);
			myDrawingBrush.TileMode = TileMode.Tile;

			// Use the DrawingBrush as an opacity mask.
			myElement.OpacityMask = myDrawingBrush;
		}
	
	}
}