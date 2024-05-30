Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Documents

Namespace TimingExamples
    '/ <summary>
    '/ This example demonstrates the use of the
    '/ TimeEndSync.AllChildren setting.
    '/ </summary>
    Public Class TimeEndSyncAllChildrenExample
        Inherits GridPanel

        Private mainTimelineStatus As System.Windows.Controls.Text
        Private WithEvents t1 As Timeline
        Private WithEvents restartButton As Button

        Public Sub New()

            Me.Columns = 2
            Me.Width = New Length(300)

            Dim explanationText As New TextPanel()

            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates the use of implicit end times." + _
             " Two Timelines, t2 and t3, are connected to a third Timeline, t1. " + _
             " t1's TimeEndSync property is set to TimeEndSync.AllChildren, " + _
             " so t1 ends after t2 and t3 end. "
            Me.Children.Add(explanationText)

            implicitEndTimes()

            Dim restartButton As New Button()
            restartButton.Content = "Restart Sample"
            Me.Children.Add(restartButton)
        End Sub

        ' Demonstrates some implicit end time settings.
        Private Sub implicitEndTimes()

            ' Set t1 to end only after all its child timelines have ended.	
            t1 = New Timeline()
            t1.StatusOfNextUse = UseStatus.ChangeableReference
            t1.EndSync = TimeEndSync.AllChildren

            Dim t2 As New Timeline()
            t2.StatusOfNextUse = UseStatus.ChangeableReference

            t2.Duration = New Time(5000)
            t1.Children.Add(t2)

            Dim t3 As New Timeline()
            t3.StatusOfNextUse = UseStatus.ChangeableReference
            t3.Duration = New Time(15000)
            t1.Children.Add(t3)

            ' Create a visualization for t1.
            mainTimelineStatus = New System.Windows.Controls.Text()
            mainTimelineStatus.TextContent = " Running"
            Dim label As New System.Windows.Controls.Text()
            Label.TextContent = "t1 Status: "
            Dim labelPanel As New FlowPanel()
            labelPanel.Children.Add(Label)
            labelPanel.Children.Add(mainTimelineStatus)

            ' Create visualizations for the child timelines.
            Dim t2Clock As New TimingExamples.MyClock(t2)
            Dim t3Clock As New TimingExamples.MyClock(t3)

            Dim samplesPanel As New GridPanel()
            samplesPanel.Columns = 4
            samplesPanel.CellSpacing = New Length(10)
            GridPanel.SetColumnSpan(labelPanel, 4)
            samplesPanel.Children.Add(labelPanel)

            label = New System.Windows.Controls.Text()
            Label.TextContent = "t2"
            samplesPanel.Children.Add(Label)
            samplesPanel.Children.Add(t2Clock)

            label = New System.Windows.Controls.Text()
            Label.TextContent = "t3"
            samplesPanel.Children.Add(Label)
            samplesPanel.Children.Add(t3Clock)

            Me.Children.Add(samplesPanel)

            ' Connect the timelines to the timing tree.
            Timeline.Root.Children.Add(t1)

        End Sub

        Private Sub t1_Ended(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Ended

            mainTimelineStatus.TextContent = " Ended"
        End Sub

        Private Sub t1_Begun(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Begun

            mainTimelineStatus.TextContent = " Running"
        End Sub

        Private Sub restartButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles restartButton.Click
            t1.BeginIn(0)
            mainTimelineStatus.TextContent = " Running"
        End Sub
    End Class

    ' <summary>
    ' This example demonstrates the use of the
    ' TimeEndSync.LastChild setting.
    ' </summary>
    Public Class TimeEndSyncLastChildExample
        Inherits GridPanel



        Private WithEvents t1 As Timeline
        Private t1Tracker As System.Windows.Controls.Text
        Private WithEvents restartButton As Button

        Public Sub New()

            Me.Columns = 2
            Me.Width = New Length(300)

            Dim explanationText As New TextPanel()

            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates the use of implicit end times." + _
             " Two Timelines, t2 and t3, are connected to a third Timeline, t1. " + _
             " t1's TimeEndSync property is set to TimeEndSync.LastChild, " + _
             " so t1 ends after t2 ends the duration of t3 is ignored because it has an unresolved begin time. "
            Me.Children.Add(explanationText)

            beginExample()

            restartButton = New Button()
            restartButton.Content = "Restart Sample"
            Me.Children.Add(restartButton)
        End Sub

        Private Sub beginExample()


            ' In this example, t1 ends after t2 ends the duration of 
            ' t3 is ignored because it has an unresolved begin time.

            t1 = New Timeline()
            t1.EndSync = TimeEndSync.LastChild
            t1.StatusOfNextUse = UseStatus.ChangeableReference

            Dim t2 As New Timeline()
            t2.StatusOfNextUse = UseStatus.ChangeableReference
            t2.Begin = New TimeSyncValue(New Time(1000))
            t2.Duration = New Time(10000)
            t1.Children.Add(t2)

            Dim t3 As New Timeline()
            t3.StatusOfNextUse = UseStatus.ChangeableReference
            t3.Begin = New TimeSyncValue(Time.Indefinite)
            t1.Children.Add(t3)


            ' Add visualizations.
            Dim gPanel As New GridPanel()
            gPanel.CellSpacing = New Length(10)
            gPanel.Columns = 4
            Dim label As New System.Windows.Controls.Text()
            Label.TextContent = "t1 Status: "


            t1Tracker = New System.Windows.Controls.Text()
            t1Tracker.TextContent = "Running"
            Dim labels As New FlowPanel()
            labels.Children.Add(Label)
            labels.Children.Add(t1Tracker)
            GridPanel.SetColumnSpan(labels, 4)
            gPanel.Children.Add(labels)

            label = New System.Windows.Controls.Text()
            Label.TextContent = "t2"
            Label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            gPanel.Children.Add(Label)
            Dim t2Clock As New MyClock(t2)
            gPanel.Children.Add(t2Clock)

            label = New System.Windows.Controls.Text()
            Label.TextContent = "t3"
            Label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            gPanel.Children.Add(Label)
            Dim t3Clock As New MyClock(t3)
            gPanel.Children.Add(t3Clock)

            Me.Children.Add(gPanel)


            Timeline.Root.Children.Add(t1)

        End Sub

        Private Sub t1_Begun(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Begun

            t1Tracker.TextContent = " Running"

        End Sub

        Private Sub t1_Ended(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Ended

            t1Tracker.TextContent = " Ended"
        End Sub

        Private Sub restartButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles restartButton.Click

            t1.BeginIn(0)
            t1Tracker.TextContent = " Running"

        End Sub

    End Class

    ' <summary>
    ' This example demonstrates the use of the
    ' TimeEndSync.LastChild setting.
    ' </summary>
    Public Class AnotherTimeEndSyncAllChildrenExample
        Inherits GridPanel

        Private WithEvents t1 As Timeline

        Private t1Tracker As System.Windows.Controls.Text
        Private WithEvents restartButton As Button

        Public Sub New()

            Me.Columns = 2
            Me.Width = New Length(300)

            Dim explanationText As New TextPanel()

            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates the use of implicit end times." + _
             " Two Timelines, t2 and t3, are connected to a third Timeline, t1. " + _
             " t1's TimeEndSync property is set to TimeEndSync.AllChildren, " + _
             " so t1's end time is never resolved because t3 has an unresolved begin time. "
            Me.Children.Add(explanationText)
            beginExample()

            restartButton = New Button()

            restartButton.Content = "Restart Sample"
            Me.Children.Add(restartButton)
        End Sub

        Private Sub beginExample()

            ' In this example, t1's end time is unresolved, because
            ' t3 has an unresolved begin time.
            t1 = New Timeline()
            t1.EndSync = TimeEndSync.AllChildren
            t1.StatusOfNextUse = UseStatus.ChangeableReference

            Dim t2 As New Timeline()

            t2.StatusOfNextUse = UseStatus.ChangeableReference
            t2.Begin = New TimeSyncValue(New Time(1000))
            t2.Duration = New Time(10000)
            t1.Children.Add(t2)

            Dim t3 As New Timeline()

            t3.StatusOfNextUse = UseStatus.ChangeableReference
            t3.Begin = New TimeSyncValue(Time.Indefinite)
            t1.Children.Add(t3)

            ' Add visualizations.
            Dim gPanel As New GridPanel()

            gPanel.CellSpacing = New Length(10)
            gPanel.Columns = 4

            Dim label As New System.Windows.Controls.Text()

            label.TextContent = "t1 Status: "
            t1Tracker = New System.Windows.Controls.Text()
            t1Tracker.TextContent = "Running"

            Dim labels As New FlowPanel()

            labels.Children.Add(label)
            labels.Children.Add(t1Tracker)
            GridPanel.SetColumnSpan(labels, 4)
            gPanel.Children.Add(labels)
            label = New System.Windows.Controls.Text()
            label.TextContent = "t2"
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            gPanel.Children.Add(label)

            Dim t2Clock As New MyClock(t2)

            gPanel.Children.Add(t2Clock)
            label = New System.Windows.Controls.Text()
            label.TextContent = "t3"
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            gPanel.Children.Add(label)

            Dim t3Clock As New MyClock(t3)

            gPanel.Children.Add(t3Clock)
            Me.Children.Add(gPanel)
            Timeline.Root.Children.Add(t1)
        End Sub

        Private Sub t1_Begun(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Begun

            t1Tracker.TextContent = " Running"
        End Sub

        Private Sub t1_Ended(ByVal sender As Object, ByVal args As EventArgs) _
            Handles t1.Ended

            t1Tracker.TextContent = " Ended"
        End Sub

        Private Sub restartButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles restartButton.Click
            t1.BeginIn(0)
            t1Tracker.TextContent = " Running"
        End Sub
    End Class
End Namespace
