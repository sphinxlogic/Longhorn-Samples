Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Documents

Namespace TimingExamples
    ' <summary>
    ' This example demonstrates how to use a timeline to control
    ' other timelines and animations.
    ' </summary>
    Public Class AnimationAndTimelineLinkingExample
        Inherits GridPanel

        Dim WithEvents myTimeline As System.Windows.Media.Animation.Timeline
        Dim WithEvents restartButton As Button
        Dim WithEvents pauseButton As Button
        Dim WithEvents stopButton As Button


        Public Sub New()

            Me.Columns = 2
            Me.Width = New Length(300)

            Dim explanationText As New TextPanel()
            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates how to " + _
             "link animations and timelines together, using" + _
             " a Timeline to control another timeline and two animations. " + _
             "The restart, pause, and stop buttons are connected to " + _
             "a timeline that is the parent of the two animations."
            Me.Children.Add(explanationText)

            linkAnimationsAndTimelines()
        End Sub

        ' <summary>
        '  This sample creates two animations and links
        '	 them to the same timeline (anotherTimeline)
        ' This timeline is then used to control
        '  the two animations.
        ' </summary>
        Private Sub linkAnimationsAndTimelines()


            ' Initialize the main Timeline.
            myTimeline = New Timeline()

            ' Set the StatusOfNextUse of the Timelines so that a handle
            ' to the original object is retained even after it's used.
            ' This is important if you wish to dynamically restart or
            ' end the Timeline at a later point.
            myTimeline.StatusOfNextUse = UseStatus.ChangeableReference

            ' Create another timeline and link it to myTimeline.
            Dim anotherTimeline As New Timeline()
            anotherTimeline.StatusOfNextUse = UseStatus.ChangeableReference
            myTimeline.Children.Add(anotherTimeline)

            ' Create an animation and connect it to the second timeline.
            Dim myColorAnimation As New ColorAnimation(Colors.Red, New Time(10000))
            myColorAnimation.StatusOfNextUse = UseStatus.ChangeableReference
            myColorAnimation.RepeatDuration = Time.Indefinite
            anotherTimeline.Children.Add(myColorAnimation)

            ' Apply the animation to a brush.
            Dim myBrush As New SolidColorBrush(Colors.Blue)
            myBrush.ColorAnimations.Add(myColorAnimation)

            ' Use the animated brush to fill a rectangle.
            Dim myRectangle As New Rectangle()
            myRectangle.RectangleHeight = New Length(100)
            myRectangle.RectangleWidth = New Length(100)
            myRectangle.Fill = myBrush

            ' Create a button an animate its opacity.
            Dim aButton As New Button()
            aButton.Content = "aButton"
            aButton.Width = New Length(100)
            aButton.Opacity = 1

            Dim myDoubleAnimation As New DoubleAnimation(0, New Time(10000))
            myDoubleAnimation.RepeatDuration = Time.Indefinite

            ' If the animation is going to be used to set a dependency property,
            ' you must wrap it in a Setter.
            Dim mySetter As New Setter(aButton, UIElement.OpacityProperty)
            mySetter.Children.Add(myDoubleAnimation)

            ' Link the animation to the timeline.
            anotherTimeline.Children.Add(mySetter)

            ' Connect the parent timeline to the timing tree.
            Timeline.Root.Children.Add(myTimeline)

            ' Add some buttons to control the Timeline.
            restartButton = New Button()
            pauseButton = New Button()
            stopButton = New Button()
            restartButton.Content = "Restart"
            pauseButton.Content = "Pause"
            stopButton.Content = "Stop"

            Dim myGridPanel As New GridPanel()
            myGridPanel.Columns = 2
            myGridPanel.Width = New Length(250)
            myGridPanel.CellSpacing = New Length(10)
            myGridPanel.Children.Add(myRectangle)
            myGridPanel.Children.Add(aButton)

            Dim f As New FlowPanel()
            f.Children.Add(restartButton)
            f.Children.Add(pauseButton)
            f.Children.Add(stopButton)
            GridPanel.SetColumnSpan(f, 2)
            myGridPanel.Children.Add(f)

            Me.Children.Add(myGridPanel)
        End Sub

        '/ <summary>
        '/  Restarts the timelines.
        '/ </summary>
        '/ <param name="sender"></param>
        '/ <param name="args"></param>
        Private Sub restartButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles restartButton.Click

            myTimeline.BeginIn(0)

            ' Restarting a timeline also unpauses it.
            pauseButton.Content = "Pause"
        End Sub

        '/ <summary>
        '/  Pauses or resumes the timelines.
        '/ </summary>
        '/ <param name="sender"></param>
        '/ <param name="args"></param>
        Private Sub pauseButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles pauseButton.Click

            ' Test to see if the timeline is already paused.
            If (myTimeline.IsPaused) Then

                myTimeline.Resume()
                CType(sender, Button).Content = "Pause"

            Else

                myTimeline.Pause()
                CType(sender, Button).Content = "Resume"
            End If

        End Sub

        '/ <summary>
        '/ Stops the timelines.
        '/ </summary>
        '/ <param name="sender"></param>
        '/ <param name="args"></param>
        Private Sub stopButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles stopButton.Click

            myTimeline.EndIn(0)
        End Sub


    End Class
End Namespace
