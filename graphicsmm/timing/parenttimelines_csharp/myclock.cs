using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TimingExamples
{
	/// <summary>
	/// Summary description for MyClock.
	/// </summary>
	public class MyClock : FlowPanel
	{
		private Timeline ownerTimeline;
		private Button restartButton, pauseButton, stopButton;
		private TextBox speedInputBox;
		private Canvas clockCanvas;

		public MyClock(Timeline t)
		{
			ownerTimeline = t;
			ownerTimeline.StatusOfNextUse = UseStatus.ChangeableReference;

			clockCanvas = new Canvas();

			createClockFace(clockCanvas);
			this.Children.Add(clockCanvas);
			ownerTimeline.Begun += new EventHandler(ownerTimeline_Begun);
			ownerTimeline.Ended += new EventHandler(ownerTimeline_Ended);
		}

		private void createClockFace(Canvas clockCanvas)
		{
			clockCanvas.Width = new Length(100);
			clockCanvas.Height = new Length(100);

			Ellipse clockBody = new Ellipse();

			clockBody.RadiusX = new Length(40);
			clockBody.RadiusY = new Length(40);
			clockBody.CenterX = new Length(50);
			clockBody.CenterY = new Length(50);
			clockBody.StrokeThickness = new Length(6);

			RadialGradientBrush clockBodyStrokeBrush = new RadialGradientBrush();

			clockBodyStrokeBrush.AddStop(Colors.Yellow, 0.5);
			clockBodyStrokeBrush.AddStop(Colors.Gold, 1);
			clockBody.Stroke = clockBodyStrokeBrush;

			SolidColorBrush clockBodyBrush = new SolidColorBrush(Colors.Gray);
			ColorAnimation clockBodyColorAnimation = new ColorAnimation(Colors.White, new Time(500));
			clockBodyColorAnimation.StatusOfNextUse = UseStatus.ChangeableReference;
			clockBodyColorAnimation.Fill = TimeFill.Freeze;
			clockBodyColorAnimation.AutoReverse = true;
			ownerTimeline.Children.Add(clockBodyColorAnimation);
			clockBodyBrush.ColorAnimations.Add(clockBodyColorAnimation);
			clockBody.Fill = clockBodyBrush;

			clockCanvas.Children.Add(clockBody);

			Line minuteHand = new Line();

			minuteHand.Stroke = Brushes.Black;
			minuteHand.StrokeThickness = new Length(5);
			minuteHand.StrokeEndLineCap = PenLineCap.Triangle;
			minuteHand.X1 = new Length(50);
			minuteHand.Y1 = new Length(50);
			minuteHand.X2 = new Length(50);
			minuteHand.Y2 = new Length(25);

			TransformDecorator minuteHandTransformDecorator = new TransformDecorator();
			RotateTransform minuteHandRotateTransform = new RotateTransform(0, new Point(50, 50));
			DoubleAnimation minuteHandAnimation = new DoubleAnimation(0, 360, new Time(60000 * 60));
			minuteHandAnimation.Fill = TimeFill.Freeze;
			minuteHandAnimation.StatusOfNextUse = UseStatus.ChangeableReference;
			minuteHandAnimation.RepeatDuration = Time.Indefinite;
			
			// Connect the animation to the timeline.
			ownerTimeline.Children.Add(minuteHandAnimation);
			minuteHandRotateTransform.AngleAnimations.Add(minuteHandAnimation);
			minuteHandTransformDecorator.Child = minuteHand;
			clockCanvas.Children.Add(minuteHandTransformDecorator);

			Line secondHand = new Line();

			secondHand.Stroke = Brushes.Black;
			secondHand.StrokeThickness = new Length(2);
			secondHand.X1 = new Length(50);
			secondHand.Y1 = new Length(50);
			secondHand.X2 = new Length(50);
			secondHand.Y2 = new Length(13);

			TransformDecorator secondHandTransformDecorator = new TransformDecorator();
			RotateTransform secondHandRotateTransform = new RotateTransform(0, new Point(50, 50));
			DoubleAnimation secondHandAnimation = new DoubleAnimation(0, 360, new Time(60000));
			secondHandAnimation.RepeatDuration = Time.Indefinite;
			secondHandAnimation.Fill = TimeFill.Freeze;
			secondHandAnimation.StatusOfNextUse = UseStatus.ChangeableReference;

			// Connect the animation to the timeline.
			ownerTimeline.Children.Add(secondHandAnimation);
			secondHandRotateTransform.AngleAnimations.Add(secondHandAnimation);
			secondHandTransformDecorator.Transform = secondHandRotateTransform;
			secondHandTransformDecorator.Child = secondHand;
			clockCanvas.Children.Add(secondHandTransformDecorator);

			Ellipse clockCenter = new Ellipse();

			clockCenter.CenterX = new Length(50);
			clockCenter.CenterY = new Length(50);
			clockCenter.RadiusX = new Length(4);
			clockCenter.RadiusY = new Length(4);
			clockCenter.Fill = new RadialGradientBrush(Colors.DarkGray, Colors.Black);
			clockCanvas.Children.Add(clockCenter);

			Ellipse reflection = new Ellipse();

			reflection.CenterX = new Length(50);
			reflection.CenterY = new Length(50);
			reflection.RadiusX = new Length(35);
			reflection.RadiusY = new Length(35);
			reflection.Fill = new RadialGradientBrush(Colors.Blue, Colors.White);

			LinearGradientBrush reflectionOpacityMask = new LinearGradientBrush();

			reflectionOpacityMask.StartPoint = new Point(0, 1);
			reflectionOpacityMask.EndPoint = new Point(1, 0);
			reflectionOpacityMask.AddStop(Colors.Transparent, 0.7);
			reflectionOpacityMask.AddStop(Colors.Black, 1);
			reflection.OpacityMask = reflectionOpacityMask;
			clockCanvas.Children.Add(reflection);
			clockCanvas.Opacity = 0.75;
		}

		private void ownerTimeline_Begun(object sender, EventArgs args)
		{
			clockCanvas.Opacity = 1;
		}

		private void ownerTimeline_Ended(object sender, EventArgs args)
		{
			clockCanvas.Opacity = 0.75;
		}
	
	}
}
