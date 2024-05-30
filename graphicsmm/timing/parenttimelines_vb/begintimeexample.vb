Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Documents

Namespace TimingExamples
    ' <summary>
    ' This example demonstrates the behavior of the
    ' Begin attribute.
    ' </summary>
    Public Class BeginTimeExample
        Inherits GridPanel

        Private WithEvents beginExampleTimeline As System.Windows.Media.Animation.Timeline
        ' lengthAnimationTracker is used to display the status of the LengthAnimation.
        Private lengthAnimationTracker As System.Windows.Controls.Text
        Private WithEvents myLengthAnimation As LengthAnimation

        Public Sub New()
            ' Initialize layout and provide a description.
            Me.Columns = 2
            Me.Width = New Length(300)
            Dim explanationText As New TextPanel()
            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates the behavior of the Begin property." + _
             " A LengthAnimation's Begin property is set to 10 seconds, and " + _
             " its speed property is set to 2. The animation begins after " + _
             " 10 seconds (not 5), because the Begin property is measured " + _
             " in the animation's parent's time. In this case, the animation's " + _
             " parent has a Speed of 1."
            Me.Children.Add(explanationText)

            ' The interesting code begins here.
            initalizeBeginTimeExample()
        End Sub


        Private Sub initalizeBeginTimeExample()
            ' Set a LengthAnimation to begin after 10 seconds.
            ' Even though its speed is set to 2, the animation
            ' starts after 10 seconds, not 5, because the Begin
            ' time is relative to the animation's parent timeline
            ' (in this case, the document timeline).
            myLengthAnimation = New LengthAnimation()
            myLengthAnimation.Speed = 2
            myLengthAnimation.Begin = New TimeSyncValue(New Time(10000))
            myLengthAnimation.Duration = New Time(5000)
            myLengthAnimation.To = New Length(150)
            myLengthAnimation.Fill = TimeFill.Freeze
		

            ' Create a line to demonstrate the animation.
            Dim myCanvas = New Canvas()
            myCanvas.Width = New Length(200)
            Dim myLine = New Line()
            myLine.Stroke = Brushes.MediumBlue
            myLine.StrokeThickness = New Length(10)
            myLine.X1 = New Length(10)
            myLine.X2 = New Length(15)
            myLine.Y1 = New Length(10)
            myLine.Y2 = New Length(10)

            ' Wrap the animation in a Setter because it's going to
            ' be used to animate a dependency property and connected
            ' to another timeline.
            ' If it weren't going to be connected to another timeline,
            ' the animation could be applied with the following code:
            ' myLine.AddAnimation(Line.X2Property, myLengthAnimation)
            Dim mySetter As New Setter(myLine, Line.X2Property)
            mySetter.Children.Add(myLengthAnimation)

            ' Initialize the timeline we'll use to control the animation.
            beginExampleTimeline = New Timeline()
            beginExampleTimeline.Duration = New Time(17000)
            beginExampleTimeline.RepeatDuration = Time.Indefinite

            ' Connect the animation to the timeline.
            beginExampleTimeline.Children.Add(mySetter)

            myCanvas.Children.Add(myLine)

            Dim myGridPanel As New GridPanel()
            myGridPanel.CellSpacing = New Length(10)
            setupBeginExampleVisuals(myGridPanel)
            myGridPanel.Children.Add(myCanvas)
            Me.Children.Add(myGridPanel)

            ' Connect the timeline to the timing tree.
            Timeline.Root.Children.Add(beginExampleTimeline)
        End Sub



        Private Sub setupBeginExampleVisuals(ByVal parentPanel As GridPanel)

            ' Create a line to show the progress of the timeline.
            ' Create a line to demonstrate the animation.
            Dim myTimelineProgressCanvas As New Canvas()

            myTimelineProgressCanvas.Width = New Length(200)

            Dim myTimelineProgressLine As New Line()

            myTimelineProgressLine.Stroke = Brushes.Black
            myTimelineProgressLine.StrokeThickness = New Length(10)
            myTimelineProgressLine.X1 = New Length(10)
            myTimelineProgressLine.X2 = New Length(110)
            myTimelineProgressLine.Y1 = New Length(10)
            myTimelineProgressLine.Y2 = New Length(10)
            myTimelineProgressCanvas.Children.Add(myTimelineProgressLine)
            myTimelineProgressLine = New Line()
            myTimelineProgressLine.Stroke = Brushes.Orange
            myTimelineProgressLine.StrokeThickness = New Length(10)
            myTimelineProgressLine.X1 = New Length(10)
            myTimelineProgressLine.X2 = New Length(10)
            myTimelineProgressLine.Y1 = New Length(10)
            myTimelineProgressLine.Y2 = New Length(10)
            Dim progressAnimation As New LengthAnimation(New Length(110), beginExampleTimeline.Duration)
            Dim myProgressAnimationSetter As New Setter(myTimelineProgressLine, Line.X2Property)

            myProgressAnimationSetter.Children.Add(progressAnimation)
            beginExampleTimeline.Children.Add(myProgressAnimationSetter)
            myTimelineProgressCanvas.Children.Add(myTimelineProgressLine)
            myTimelineProgressCanvas.Height = New Length(30)

            Dim timelineLabel As New System.Windows.Controls.Text()

            timelineLabel.TextContent = "Parent Timeline Progress: "

            Dim myLabel As New System.Windows.Controls.Text()

            myLabel.TextContent = "Animation Status: "
            lengthAnimationTracker = New System.Windows.Controls.Text()
            lengthAnimationTracker.TextContent = " Not Started"

            Dim animationLabels As New FlowPanel()

            animationLabels.Children.Add(myLabel)
            animationLabels.Children.Add(lengthAnimationTracker)

            parentPanel.Children.Add(timelineLabel)
            parentPanel.Children.Add(myTimelineProgressCanvas)
            parentPanel.Children.Add(animationLabels)
        End Sub

        Private Sub beginExampleTimeline_Repeated(ByVal sender As Object, ByVal args As EventArgs) _
            Handles beginExampleTimeline.Repeated

            lengthAnimationTracker.TextContent = " Not Started"
        End Sub

        Private Sub lengthAnimation_Begun(ByVal sender As Object, ByVal args As EventArgs) _
            Handles myLengthAnimation.Begun

            lengthAnimationTracker.TextContent = " Begun"
        End Sub

        Private Sub lengthAnimation_Ended(ByVal sender As Object, ByVal args As EventArgs) _
            Handles myLengthAnimation.Ended

            lengthAnimationTracker.TextContent = " Ended"
        End Sub



    End Class
End Namespace
