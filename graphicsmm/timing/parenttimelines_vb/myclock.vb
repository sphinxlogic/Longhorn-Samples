Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes

Namespace TimingExamples


    ' <summary>
    ' Summary description for MyClock.
    ' </summary>
    Public Class MyClock
        Inherits FlowPanel

        Private WithEvents ownerTimeline As Timeline
      

        Private speedInputBox As TextBox
        Private clockCanvas As Canvas

        Public Sub New(ByVal t As Timeline)

            ownerTimeline = t
            ownerTimeline.StatusOfNextUse = UseStatus.ChangeableReference

            clockCanvas = New Canvas()

            createClockFace(clockCanvas)
            Me.Children.Add(clockCanvas)
        End Sub

        Private Sub createClockFace(ByVal clockCanvas As Canvas)

            clockCanvas.Width = New Length(100)
            clockCanvas.Height = New Length(100)

            Dim clockBody As New Ellipse()

            clockBody.RadiusX = New Length(40)
            clockBody.RadiusY = New Length(40)
            clockBody.CenterX = New Length(50)
            clockBody.CenterY = New Length(50)
            clockBody.StrokeThickness = New Length(6)

            Dim clockBodyStrokeBrush As New RadialGradientBrush()

            clockBodyStrokeBrush.AddStop(Colors.Yellow, 0.5)
            clockBodyStrokeBrush.AddStop(Colors.Gold, 1)
            clockBody.Stroke = clockBodyStrokeBrush

            Dim clockBodyBrush As New SolidColorBrush(Colors.Gray)
            Dim clockBodyColorAnimation As New ColorAnimation(Colors.White, New Time(500))
            clockBodyColorAnimation.StatusOfNextUse = UseStatus.ChangeableReference
            clockBodyColorAnimation.Fill = TimeFill.Freeze
            clockBodyColorAnimation.AutoReverse = True
            ownerTimeline.Children.Add(clockBodyColorAnimation)
            clockBodyBrush.ColorAnimations.Add(clockBodyColorAnimation)
            clockBody.Fill = clockBodyBrush

            clockCanvas.Children.Add(clockBody)

            Dim minuteHand As New Line()

            minuteHand.Stroke = Brushes.Black
            minuteHand.StrokeThickness = New Length(5)
            minuteHand.StrokeEndLineCap = PenLineCap.Triangle
            minuteHand.X1 = New Length(50)
            minuteHand.Y1 = New Length(50)
            minuteHand.X2 = New Length(50)
            minuteHand.Y2 = New Length(25)

            Dim minuteHandTransformDecorator As New TransformDecorator()
            Dim minuteHandRotateTransform As New RotateTransform(0, New System.Windows.Point(50, 50))
            Dim minuteHandAnimation As New DoubleAnimation(0, 360, New Time(60000 * 60))
            minuteHandAnimation.Fill = TimeFill.Freeze
            minuteHandAnimation.StatusOfNextUse = UseStatus.ChangeableReference
            minuteHandAnimation.RepeatDuration = Time.Indefinite

            ' Connect the animation to the timeline.
            ownerTimeline.Children.Add(minuteHandAnimation)
            minuteHandRotateTransform.AngleAnimations.Add(minuteHandAnimation)
            minuteHandTransformDecorator.Child = minuteHand
            clockCanvas.Children.Add(minuteHandTransformDecorator)

            Dim secondHand As New Line()

            secondHand.Stroke = Brushes.Black
            secondHand.StrokeThickness = New Length(2)
            secondHand.X1 = New Length(50)
            secondHand.Y1 = New Length(50)
            secondHand.X2 = New Length(50)
            secondHand.Y2 = New Length(13)

            Dim secondHandTransformDecorator As New TransformDecorator()
            Dim secondHandRotateTransform As New RotateTransform(0, New System.Windows.Point(50, 50))
            Dim secondHandAnimation As New DoubleAnimation(0, 360, New Time(60000))
            secondHandAnimation.RepeatDuration = Time.Indefinite
            secondHandAnimation.Fill = TimeFill.Freeze
            secondHandAnimation.StatusOfNextUse = UseStatus.ChangeableReference

            ' Connect the animation to the timeline.
            ownerTimeline.Children.Add(secondHandAnimation)
            secondHandRotateTransform.AngleAnimations.Add(secondHandAnimation)
            secondHandTransformDecorator.Transform = secondHandRotateTransform
            secondHandTransformDecorator.Child = secondHand
            clockCanvas.Children.Add(secondHandTransformDecorator)

            Dim clockCenter As New Ellipse()

            clockCenter.CenterX = New Length(50)
            clockCenter.CenterY = New Length(50)
            clockCenter.RadiusX = New Length(4)
            clockCenter.RadiusY = New Length(4)
            clockCenter.Fill = New RadialGradientBrush(Colors.DarkGray, Colors.Black)
            clockCanvas.Children.Add(clockCenter)

            Dim Reflection As New Ellipse()

            Reflection.CenterX = New Length(50)
            Reflection.CenterY = New Length(50)
            Reflection.RadiusX = New Length(35)
            Reflection.RadiusY = New Length(35)
            Reflection.Fill = New RadialGradientBrush(Colors.Blue, Colors.White)

            Dim reflectionOpacityMask As New LinearGradientBrush()

            reflectionOpacityMask.StartPoint = New System.Windows.Point(0, 1)
            reflectionOpacityMask.EndPoint = New System.Windows.Point(1, 0)
            reflectionOpacityMask.AddStop(Colors.Transparent, 0.7)
            reflectionOpacityMask.AddStop(Colors.Black, 1)
            Reflection.OpacityMask = reflectionOpacityMask
            clockCanvas.Children.Add(Reflection)
            clockCanvas.Opacity = 0.75
        End Sub

        Private Sub ownerTimeline_Begun(ByVal sender As Object, ByVal args As EventArgs) _
            Handles ownerTimeline.Begun

            clockCanvas.Opacity = 1
        End Sub

        Private Sub ownerTimeline_Ended(ByVal sender As Object, ByVal args As EventArgs) _
            Handles ownerTimeline.Ended

            clockCanvas.Opacity = 0.75
        End Sub

    End Class
End Namespace
