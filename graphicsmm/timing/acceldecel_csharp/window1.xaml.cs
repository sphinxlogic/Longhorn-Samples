//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace AccelDecel_csharp
{
    /// <summary>
    /// Creates and animates several line using different combinations of acceleration
    /// and deceleration.
    /// </summary>

    public partial class Window1 : Window
    {
        
        private void WindowLoaded(object sender, EventArgs e) {
			createAccelerationAndDecelerationExample(accelerationAndDecelerationExampleCanvas);
			createAccelerationExample(accelerationExampleCanvas);
			createDecelerationExample(decelerationExampleCanvas);
		}

		// Creates an animation and specifies acceleration and deceleration.
		private void createAccelerationAndDecelerationExample(System.Windows.Controls.Panel myPanel)
		{

			// Create a line to animate.
			System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();
			myLine.X1 = new System.Windows.Length(20);
			myLine.Y1 = new System.Windows.Length(50);
			myLine.X2 = new System.Windows.Length(30);
			myLine.Y2 = new System.Windows.Length(50);
			myLine.Stroke = System.Windows.Media.Brushes.Blue;
			myLine.StrokeThickness = new System.Windows.Length(10);
			
			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = 
				new System.Windows.Media.Animation.LengthAnimation(
						new System.Windows.Length(10),
						new System.Windows.Length(400),
						new System.Windows.Media.Animation.Time(10000));

			myLengthAnimation.AutoReverse = true;
			myLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			myLengthAnimation.Acceleration = 0.4;
			myLengthAnimation.Deceleration = 0.6;

			myLine.AddAnimation(
				System.Windows.Shapes.Line.X2Property, 
				myLengthAnimation);

			myPanel.Children.Add(myLine);


			// Create another line and animation 
			// without acceleration or deceleration for comparison.
			// Create a line to animate.
			System.Windows.Shapes.Line comparisonLine = new System.Windows.Shapes.Line();

			comparisonLine.X1 = new System.Windows.Length(20);
			comparisonLine.Y1 = new System.Windows.Length(80);
			comparisonLine.X2 = new System.Windows.Length(30);
			comparisonLine.Y2 = new System.Windows.Length(80);
			comparisonLine.Stroke = System.Windows.Media.Brushes.Red;
			comparisonLine.StrokeThickness = new System.Windows.Length(10);

			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation comparisonLengthAnimation = new System.Windows.Media.Animation.LengthAnimation(new System.Windows.Length(10), new System.Windows.Length(400), new System.Windows.Media.Animation.Time(10000));

			comparisonLengthAnimation.AutoReverse = true;
			comparisonLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			comparisonLine.AddAnimation(System.Windows.Shapes.Line.X2Property, comparisonLengthAnimation);
			myPanel.Children.Add(comparisonLine);
		}

		// Creates an animation and specifies acceleration.
		private void createAccelerationExample(System.Windows.Controls.Panel myPanel)
		{
			// Create a line to animate.
			System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();

			myLine.X1 = new System.Windows.Length(20);
			myLine.Y1 = new System.Windows.Length(50);
			myLine.X2 = new System.Windows.Length(30);
			myLine.Y2 = new System.Windows.Length(50);
			myLine.Stroke = System.Windows.Media.Brushes.Blue;
			myLine.StrokeThickness = new System.Windows.Length(10);

			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = new System.Windows.Media.Animation.LengthAnimation(new System.Windows.Length(10), new System.Windows.Length(400), new System.Windows.Media.Animation.Time(10000));

			myLengthAnimation.AutoReverse = true;
			myLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			myLengthAnimation.Acceleration = 0.4;
			myLine.AddAnimation(System.Windows.Shapes.Line.X2Property, myLengthAnimation);
			myPanel.Children.Add(myLine);

			// Create another line and animation 
			// without acceleration or deceleration for comparison.
			// Create a line to animate.
			System.Windows.Shapes.Line comparisonLine = new System.Windows.Shapes.Line();

			comparisonLine.X1 = new System.Windows.Length(20);
			comparisonLine.Y1 = new System.Windows.Length(80);
			comparisonLine.X2 = new System.Windows.Length(30);
			comparisonLine.Y2 = new System.Windows.Length(80);
			comparisonLine.Stroke = System.Windows.Media.Brushes.Red;
			comparisonLine.StrokeThickness = new System.Windows.Length(10);

			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation comparisonLengthAnimation = new System.Windows.Media.Animation.LengthAnimation(new System.Windows.Length(10), new System.Windows.Length(400), new System.Windows.Media.Animation.Time(10000));

			comparisonLengthAnimation.AutoReverse = true;
			comparisonLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			comparisonLine.AddAnimation(System.Windows.Shapes.Line.X2Property, comparisonLengthAnimation);
			myPanel.Children.Add(comparisonLine);
		}

		// Creates an animation and specifies deceleration.
		private void createDecelerationExample(System.Windows.Controls.Panel myPanel)
		{
			// Create a line to animate.
			System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();

			myLine.X1 = new System.Windows.Length(20);
			myLine.Y1 = new System.Windows.Length(50);
			myLine.X2 = new System.Windows.Length(30);
			myLine.Y2 = new System.Windows.Length(50);
			myLine.Stroke = System.Windows.Media.Brushes.Blue;
			myLine.StrokeThickness = new System.Windows.Length(10);

			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation myLengthAnimation = new System.Windows.Media.Animation.LengthAnimation(new System.Windows.Length(10), new System.Windows.Length(400), new System.Windows.Media.Animation.Time(10000));

			myLengthAnimation.AutoReverse = true;
			myLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			myLengthAnimation.Deceleration = 0.6;
			myLine.AddAnimation(System.Windows.Shapes.Line.X2Property, myLengthAnimation);
			myPanel.Children.Add(myLine);

			// Create another line and animation 
			// without acceleration or deceleration for comparison.
			// Create a line to animate.
			System.Windows.Shapes.Line comparisonLine = new System.Windows.Shapes.Line();

			comparisonLine.X1 = new System.Windows.Length(20);
			comparisonLine.Y1 = new System.Windows.Length(80);
			comparisonLine.X2 = new System.Windows.Length(30);
			comparisonLine.Y2 = new System.Windows.Length(80);
			comparisonLine.Stroke = System.Windows.Media.Brushes.Red;
			comparisonLine.StrokeThickness = new System.Windows.Length(10);

			// Create the animation.
			System.Windows.Media.Animation.LengthAnimation comparisonLengthAnimation = new System.Windows.Media.Animation.LengthAnimation(new System.Windows.Length(10), new System.Windows.Length(400), new System.Windows.Media.Animation.Time(10000));

			comparisonLengthAnimation.AutoReverse = true;
			comparisonLengthAnimation.RepeatDuration = System.Windows.Media.Animation.Time.Indefinite;
			comparisonLine.AddAnimation(System.Windows.Shapes.Line.X2Property, comparisonLengthAnimation);
			myPanel.Children.Add(comparisonLine);
		}
	}
}