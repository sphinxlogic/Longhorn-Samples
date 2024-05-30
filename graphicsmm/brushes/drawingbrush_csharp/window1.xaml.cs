// Window1.xaml defines the shapes to which the drawing brushes
// created in this sample are applied.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;

namespace DrawingBrush_csharp
{
    /// <summary>
    /// Demonstrates the use of the DrawingBrush class.
    /// </summary>

    public partial class Window1 : Window
    {
		private void WindowLoaded(object sender, EventArgs args)
		{
			// The shapes used in these examples are definied in
			// Window1.xaml.
			fillShapesWithDrawingBrush(firstShape, secondShape);
			fillShapeWithCheckeredPattern(checkeredShape);
			fillDifferentSizedShapes(squareShape, wideShape, tallShape);
			fillShapesWithDifferentPatterns(firstCheckeredShape, secondCheckeredShape, thirdCheckeredShape);
			transformDrawingBrush(noTransformFillShape, transformFillShape);
		}

		// Fills a shape with a DrawingBrush and fills a second shape
		// with a tiled DrawingBrush.
		private void fillShapesWithDrawingBrush(Shape myShape, Shape anotherShape)
		{
			// Create a Drawing to describe the contents of the DrawingBrush.
			Drawing myDrawing = new Drawing();

			// Obtain a DrawingContext from the Drawing object.
			DrawingContext myDrawingContext = myDrawing.Open();
			
			// Use the DrawingContext to draw some shapes. 
			// All values are relative when specifying the dimension
			// of shapes for a DrawingBrush.
			// A value of 1 means 100% of the output area,
			// a value of 0.5 means 50% of the output area, 
			// and so on.
			myDrawingContext.DrawLine(
				new Pen(Brushes.Orange, 0.25),
				new Point(0, 0.225),
				new Point(1, 0.225));

			myDrawingContext.DrawRectangle(
				new LinearGradientBrush(Colors.MediumBlue, Colors.Purple, 90),
				null,
				new Rect(0.5, 0.5, 0.5, 0.5));

			myDrawingContext.DrawEllipse(
				Brushes.LimeGreen,
				null,
				new Point(0.5, 0.5),
				0.2,
				0.2);

			// Important: close the DrawingContext after drawing is finished.
			myDrawingContext.Close();
			
			// Create a DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			myShape.Fill = myDrawingBrush;

			// Set the tiling options on the DrawingBrush and use it
			// to fill another shape. The following code
			// creates tiles that are 50% the width of the output
			// area and 50% of the height of the output area.
			myDrawingBrush.ViewPort = new Rect(0, 0, 0.5, 0.5);
			myDrawingBrush.TileMode = TileMode.Tile;

			anotherShape.Fill = myDrawingBrush;
		}

		// Uses a DrawingBrush to create a checkered pattern.
		private void fillShapeWithCheckeredPattern(Shape myShape)
		{
			// Create a Drawing to describe the contents of the DrawingBrush.
			Drawing myDrawing = new Drawing();

			// Obtain a DrawingContext from the Drawing object.
			DrawingContext myDrawingContext = myDrawing.Open();

			// Use the DrawingContext to draw some shapes. 
			// All values are relative when specifying the dimension
			// of shapes for a DrawingBrush.
			// A value of 1 means 100% of the output area,
			// a value of 0.5 means 50% of the output area, 
			// and so on.
			myDrawingContext.DrawRectangle(
				new LinearGradientBrush(Colors.Purple, Colors.Gray, 90),
				null,
				new Rect(0, 0, 1, 1));

			myDrawingContext.DrawRectangle(
				new LinearGradientBrush(Colors.Green, Colors.LimeGreen, 90),
				null,
				new Rect(0, 0, 0.5, 0.5));

			myDrawingContext.DrawRectangle(
				new LinearGradientBrush(Colors.Green, Colors.LimeGreen, 90),
				null,
				new Rect(0.5, 0.5, 0.5, 0.5));

			// Important: close the DrawingContext after drawing is finished.
			myDrawingContext.Close();

			// Create a DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			// Set the tiling options on the DrawingBrush and use it
			// to fill the shape.
			myDrawingBrush.ViewPort = new Rect(0, 0, 0.5, 0.5);
			myDrawingBrush.TileMode = TileMode.Tile;
			myShape.Fill = myDrawingBrush;
		}

		// Uses the same DrawingBrush to fill shapes of different sizes.
		private void fillDifferentSizedShapes(Shape smallShape, Shape wideShape, Shape tallShape)
		{
			// Create a Drawing to describe the contents of the DrawingBrush.
			Drawing myDrawing = new Drawing();

			// Obtain a DrawingContext from the Drawing object.
			DrawingContext myDrawingContext = myDrawing.Open();

			// Use the DrawingContext to draw some shapes. 
			// All values are relative when specifying the dimension
			// of shapes for a DrawingBrush.
			// A value of 1 means 100% of the output area,
			// a value of 0.5 means 50% of the output area, 
			// and so on.
			myDrawingContext.DrawRectangle(
				Brushes.Black,
				null,
				new Rect(0, 0, 0.5, 0.5));

			myDrawingContext.DrawRectangle(
				Brushes.Black,
				null,
				new Rect(0.5, 0.5, 0.5, 0.5));

			// Important: close the DrawingContext after drawing is finished.
			myDrawingContext.Close();

			// Create a DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			// Apply the DrawingBrush to the shapes.
			smallShape.Fill = myDrawingBrush;
			wideShape.Fill = myDrawingBrush;
			tallShape.Fill = myDrawingBrush;
		}

		// Uses different tiling options on the DrawingBrush.
		private void fillShapesWithDifferentPatterns(Shape firstShape, Shape secondShape, Shape thirdShape)
		{
			// Create a Drawing to describe the contents of the DrawingBrush.
			Drawing myDrawing = new Drawing();

			// Obtain a DrawingContext from the Drawing object.
			DrawingContext myDrawingContext = myDrawing.Open();

			// Use the DrawingContext to draw some shapes. 
			// All values are relative when specifying the dimension
			// of shapes for a DrawingBrush.
			// A value of 1 means 100% of the output area,
			// a value of 0.5 means 50% of the output area, 
			// and so on.
			myDrawingContext.DrawRectangle(
				Brushes.Black,
				null,
				new Rect(0, 0, 0.5, 0.5));

			myDrawingContext.DrawRectangle(
				Brushes.Black,
				null,
				new Rect(0.5, 0.5, 0.5, 0.5));

			// Important: close the DrawingContext after drawing is finished.
			myDrawingContext.Close();

			// Create a DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);

			// Set the tiling options on the DrawingBrush and use it
			// to fill a shape. The following code
			// creates tiles that are 25% the width of the output
			// area and 25% of the height of the output area.
			myDrawingBrush.ViewPort = new Rect(0, 0, 0.25, 0.25);
			myDrawingBrush.TileMode = TileMode.Tile;
			firstShape.Fill = myDrawingBrush;

			// The following code
			// creates tiles that are 50% the width of the output
			// area and 50% of the height of the output area.
			myDrawingBrush.ViewPort = new Rect(0, 0, 0.5, 0.5);
			myDrawingBrush.TileMode = TileMode.Tile;
			secondShape.Fill = myDrawingBrush;

			// The following code
			// creates tiles that are 20 pixels wide and 10 pixels tall.
			myDrawingBrush.ViewPort = new Rect(0, 0, 20, 10);
			myDrawingBrush.ViewPortUnits = BrushMappingMode.Absolute;
			myDrawingBrush.TileMode = TileMode.Tile;
			thirdShape.Fill = myDrawingBrush;
		}

		// Applies a transformed DrawingBrush to a shape. Another shape
		// is filled with the DrawingBrush before it is transformed
		// for comparison.
		private void transformDrawingBrush(Shape firstShape, Shape secondShape)
		{
			// Create a Drawing to describe the contents of the DrawingBrush.
			Drawing myDrawing = new Drawing();

			// Obtain a DrawingContext from the Drawing object.
			DrawingContext myDrawingContext = myDrawing.Open();

			// Use the DrawingContext to draw some shapes. 
			// All values are relative when specifying the dimension
			// of shapes for a DrawingBrush.
			// A value of 1 means 100% of the output area,
			// a value of 0.5 means 50% of the output area, 
			// and so on.
			myDrawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, 0.5, 0.5));
			myDrawingContext.DrawRectangle(Brushes.Black, null, new Rect(0.5, 0.5, 0.5, 0.5));

			// Important: close the DrawingContext after drawing is finished.
			myDrawingContext.Close();

			// Create a DrawingBrush to render the drawing.
			DrawingBrush myDrawingBrush = new DrawingBrush(myDrawing);


			// The following code
			// creates tiles that are 50% the width of the output
			// area and 50% of the height of the output area.
			myDrawingBrush.ViewPort = new Rect(0, 0, 0.5, 0.5);
			myDrawingBrush.TileMode = TileMode.Tile;
			firstShape.Fill = myDrawingBrush;


			// Transform the brush and use it to fill
			// a second shape.
			SkewTransform mySkewTransform = new SkewTransform();
			mySkewTransform.AngleX = 45;
			myDrawingBrush.Transform = mySkewTransform;
			secondShape.Fill = myDrawingBrush;
		}
	}
}