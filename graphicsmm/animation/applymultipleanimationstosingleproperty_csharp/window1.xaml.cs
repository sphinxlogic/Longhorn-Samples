//This is a list of commonly used namespaces for a window.
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ApplyMultipleAnimationsToSingleProperty_csharp
{
    /// <summary>
    /// Demonstrates the application of multiple animations to a single property.
    /// </summary>

    public partial class Window1 : Window
    {
		private System.Windows.Media.Animation.Timeline freezeAnimationsTimeline;
		private System.Windows.Media.Animation.Timeline nonfreezeAnimationsTimeline;
		
		private void WindowLoaded(object sender, EventArgs e) {
			applyMultipleAnimationsToFirstRectangle();
			applyMultipleAnimationsToSecondRectangle();
		}

		private void applyMultipleAnimationsToFirstRectangle()
		{
			// Create a Timeline to contain the animations.
			freezeAnimationsTimeline 
					= new System.Windows.Media.Animation.Timeline();
			freezeAnimationsTimeline.StatusOfNextUse = System.Windows.UseStatus.ChangeableReference;

			// Create the first animation. This animation's Fill property is set to Freeze so that
			// the second animation animates from its ending value.
			System.Windows.Media.Animation.LengthAnimation firstLengthAnimation =
										new System.Windows.Media.Animation.LengthAnimation();

			firstLengthAnimation.To = new System.Windows.Length(200);
			firstLengthAnimation.Duration = new System.Windows.Media.Animation.Time(2000);
			firstLengthAnimation.Fill = System.Windows.Media.Animation.TimeFill.Freeze;

			// Create the second animation.
			System.Windows.Media.Animation.LengthAnimation secondLengthAnimation = new System.Windows.Media.Animation.LengthAnimation();

			secondLengthAnimation.To = new System.Windows.Length(400);
			secondLengthAnimation.Duration = new System.Windows.Media.Animation.Time(5000);
			secondLengthAnimation.Begin = 
				new System.Windows.Media.Animation.TimeSyncValue(
					new System.Windows.Media.Animation.Time(5000));

			// Add both animations as a child of the previously created timeline.
			freezeAnimationsTimeline.Children.Add(firstLengthAnimation);
			freezeAnimationsTimeline.Children.Add(secondLengthAnimation);

			// Associate the timeline with the rectangle's RectangleWidth property.
			rectangleWithFreezeAnimation.AddAnimation(
				System.Windows.Shapes.Rectangle.RectangleWidthProperty, freezeAnimationsTimeline);

		}

		private void applyMultipleAnimationsToSecondRectangle()
		{
			// Create a Timeline to contain the animations.
			nonfreezeAnimationsTimeline = new System.Windows.Media.Animation.Timeline();
			nonfreezeAnimationsTimeline.StatusOfNextUse = System.Windows.UseStatus.ChangeableReference;

			// Create the first animation. 
			System.Windows.Media.Animation.LengthAnimation firstLengthAnimation = new System.Windows.Media.Animation.LengthAnimation();

			firstLengthAnimation.To = new System.Windows.Length(200);
			firstLengthAnimation.Duration = new System.Windows.Media.Animation.Time(2000);

			// Create the second animation.
			System.Windows.Media.Animation.LengthAnimation secondLengthAnimation = new System.Windows.Media.Animation.LengthAnimation();

			secondLengthAnimation.To = new System.Windows.Length(400);
			secondLengthAnimation.Duration = new System.Windows.Media.Animation.Time(5000);
			secondLengthAnimation.Begin = new System.Windows.Media.Animation.TimeSyncValue(new System.Windows.Media.Animation.Time(5000));

			// Add both animations as a child of the previously created timeline.
			nonfreezeAnimationsTimeline.Children.Add(firstLengthAnimation);
			nonfreezeAnimationsTimeline.Children.Add(secondLengthAnimation);

			// Associate the timeline with the rectangle's RectangleWidth property.
			rectangleWithoutFreezeAnimation.AddAnimation(System.Windows.Shapes.Rectangle.RectangleWidthProperty, nonfreezeAnimationsTimeline);
		}

		private void restartFreezeAnimationExamples(object sender, ClickEventArgs args)
		{
			freezeAnimationsTimeline.BeginIn(0);
			nonfreezeAnimationsTimeline.BeginIn(0);
		}

    }
}