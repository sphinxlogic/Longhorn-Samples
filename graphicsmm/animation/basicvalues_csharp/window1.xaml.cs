//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace basicvalues_csharp
{
    /// <summary>
    /// Animates lines using different animation attributes.
    /// </summary>

    public partial class Window1 : Window
    {
       
        private void WindowLoaded(object sender, EventArgs e) {
			
			/** From/to Animation Example  **/
			// Create and animate a line.
			System.Windows.Shapes.Line fromToAnimatedLine = 
					new System.Windows.Shapes.Line();
			fromToAnimatedLine.X1 = new System.Windows.Length(10);
			fromToAnimatedLine.Y1 = new System.Windows.Length(20);
			fromToAnimatedLine.X2 = new System.Windows.Length(100);
			fromToAnimatedLine.Y2 = new System.Windows.Length(20);
			fromToAnimatedLine.Stroke = System.Windows.Media.Brushes.Black;
			fromToAnimatedLine.StrokeThickness = new System.Windows.Length(20);

			// Animate the line.
			System.Windows.Media.Animation.LengthAnimation fromToAnimation 
				= new System.Windows.Media.Animation.LengthAnimation();
			fromToAnimation.From = new System.Windows.Length(50);
			fromToAnimation.To = new System.Windows.Length(300);
			fromToAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			fromToAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			fromToAnimatedLine.AddAnimation(
								System.Windows.Shapes.Line.X2Property, 
								fromToAnimation);

			// Add the line to the canvas.
			fromToExampleCanvas.Children.Add(fromToAnimatedLine);

			
			/** To Animation Example  **/
			// Create and animate a line.
			System.Windows.Shapes.Line toAnimatedLine = new System.Windows.Shapes.Line();

			toAnimatedLine.X1 = new System.Windows.Length(10);
			toAnimatedLine.Y1 = new System.Windows.Length(20);
			toAnimatedLine.X2 = new System.Windows.Length(100);
			toAnimatedLine.Y2 = new System.Windows.Length(20);
			toAnimatedLine.Stroke = System.Windows.Media.Brushes.Black;
			toAnimatedLine.StrokeThickness = new System.Windows.Length(20);

			// Animate the line.
			System.Windows.Media.Animation.LengthAnimation toAnimation = 
					new System.Windows.Media.Animation.LengthAnimation();
			toAnimation.To = new System.Windows.Length(300);
			toAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			toAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			toAnimatedLine.AddAnimation(
					System.Windows.Shapes.Line.X2Property, 
					toAnimation);

			// Add the line to the canvas.
			toExampleCanvas.Children.Add(toAnimatedLine);


			/** By Animation Example  **/
			// Create and animate a line.
			System.Windows.Shapes.Line byAnimatedLine = new System.Windows.Shapes.Line();
			byAnimatedLine.X1 = new System.Windows.Length(10);
			byAnimatedLine.Y1 = new System.Windows.Length(20);
			byAnimatedLine.X2 = new System.Windows.Length(100);
			byAnimatedLine.Y2 = new System.Windows.Length(20);
			byAnimatedLine.Stroke = System.Windows.Media.Brushes.Black;
			byAnimatedLine.StrokeThickness = new System.Windows.Length(20);

			// Animate the line.
			System.Windows.Media.Animation.LengthAnimation byAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			byAnimation.By = new System.Windows.Length(300);
			byAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			byAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			byAnimatedLine.AddAnimation(System.Windows.Shapes.Line.X2Property, byAnimation);

			// Add the line to the canvas.
			byExampleCanvas.Children.Add(byAnimatedLine);

			/** From/By Animation Example  **/
			// Create and animate a line.
			System.Windows.Shapes.Line fromByAnimatedLine = 
						new System.Windows.Shapes.Line();
			fromByAnimatedLine.X1 = new System.Windows.Length(10);
			fromByAnimatedLine.Y1 = new System.Windows.Length(20);
			fromByAnimatedLine.X2 = new System.Windows.Length(100);
			fromByAnimatedLine.Y2 = new System.Windows.Length(20);
			fromByAnimatedLine.Stroke = System.Windows.Media.Brushes.Black;
			fromByAnimatedLine.StrokeThickness = new System.Windows.Length(20);

			// Animate the line.
			System.Windows.Media.Animation.LengthAnimation fromByAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			fromByAnimation.From = new System.Windows.Length(50);
			fromByAnimation.By = new System.Windows.Length(300);
			fromByAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			fromByAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			fromByAnimatedLine.AddAnimation(
				System.Windows.Shapes.Line.X2Property, 
				fromByAnimation);

			// Add the line to the canvas.
			fromByExampleCanvas.Children.Add(fromByAnimatedLine);

			/** From Animation Example  **/
			// Create and animate a line.
			System.Windows.Shapes.Line fromAnimatedLine = 
						new System.Windows.Shapes.Line();
			fromAnimatedLine.X1 = new System.Windows.Length(10);
			fromAnimatedLine.Y1 = new System.Windows.Length(20);
			fromAnimatedLine.X2 = new System.Windows.Length(100);
			fromAnimatedLine.Y2 = new System.Windows.Length(20);
			fromAnimatedLine.Stroke = System.Windows.Media.Brushes.Black;
			fromAnimatedLine.StrokeThickness = new System.Windows.Length(20);

			// Animate the line.
			System.Windows.Media.Animation.LengthAnimation fromAnimation = 
				new System.Windows.Media.Animation.LengthAnimation();
			fromAnimation.From = new System.Windows.Length(50);
			fromAnimation.Duration = new System.Windows.Media.Animation.Time(10000);
			fromAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			fromAnimatedLine.AddAnimation(
				System.Windows.Shapes.Line.X2Property, 
				fromAnimation);

			// Add the line to the canvas.
			fromExampleCanvas.Children.Add(fromAnimatedLine);
		}
        

    }
}