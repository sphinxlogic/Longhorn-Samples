Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Documents

Namespace TimingExamples


    ' <summary>
    ' This example demonstrates how to synchronize timelines.
    ' Animated drawings of clocks are used to show the 
    ' timelines' behavior.
    ' </summary>
    Public Class SynchronizeTimelinesExample
        Inherits GridPanel

        Private WithEvents t1 As Timeline
        Private WithEvents restartButton As Button

        Public Sub New()

            ' Initialize layout and provide a description.
            Me.Columns = 2
            Me.Width = New Length(300)

            Dim explanationText As New TextPanel()

            explanationText.Margin = New Thickness(New Length(10))
            explanationText.Width = New Length(150)
            explanationText.TextContent = _
             "This example demonstrates how to synchronize timelines. " + _
             "Timeline t2 and t3 are connected to Timeline t1. " + _
             "Timeline t3 is set to begin when t2 ends. " + _
             "Animated drawings of clocks are used to show the " + _
             " timelines' behavior."
            Me.Children.Add(explanationText)

            SyncTimelines()

            restartButton = New Button()
            restartButton.Content = "Restart Sample"
            Me.Children.Add(restartButton)
        End Sub

        ' Create and sync three timelines.
        Private Sub SyncTimelines()
            t1 = New Timeline()
            Dim t2 As New Timeline()
            Dim t3 As New Timeline()

            t1.StatusOfNextUse = UseStatus.ChangeableReference
            t2.StatusOfNextUse = UseStatus.ChangeableReference
            t3.StatusOfNextUse = UseStatus.ChangeableReference

            t1.EndSync = TimeEndSync.AllChildren
            t1.RepeatDuration = Time.Indefinite
            t2.Duration = New Time(10000)
            t3.Duration = New Time(5000)
            t3.Begin = New TimeSyncValue(t2, TimeSyncBase.End, 0)
            t1.Children.Add(t2)
            t1.Children.Add(t3)

            ' Create visualizations for the timelines.
            Dim masterTimelineClock As New TimingExamples.MyClock(t1)
            Dim firstChildClock As New TimingExamples.MyClock(t2)
            Dim secondChildClock As New TimingExamples.MyClock(t3)


            ' Layout the clocks and label them.
            Dim myGridPanel As New GridPanel()
            myGridPanel.Columns = 4
            Dim label As New System.Windows.Controls.Text()
            Label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            Label.TextContent = "t1"
            Label.FontWeight = FontWeight.Bold
            GridPanel.SetColumnSpan(Label, 2)
            myGridPanel.Children.Add(Label)
            GridPanel.SetColumnSpan(masterTimelineClock, 2)
            myGridPanel.Children.Add(masterTimelineClock)

            label = New System.Windows.Controls.Text()
            Label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            Label.TextContent = "t2"
            Label.FontWeight = FontWeight.Bold
            myGridPanel.Children.Add(Label)
            myGridPanel.Children.Add(firstChildClock)

            label = New System.Windows.Controls.Text()
            Label.HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            Label.FontWeight = FontWeight.Bold
            Label.TextContent = "t3"
            myGridPanel.Children.Add(Label)
            myGridPanel.Children.Add(secondChildClock)

            Me.Children.Add(myGridPanel)

            ' Connect the main timeline to the timing tree.
            Timeline.Root.Children.Add(t1)
        End Sub

        Private Sub restartButton_Click(ByVal sender As Object, ByVal args As ClickEventArgs) _
            Handles restartButton.Click

            t1.BeginIn(0)
        End Sub
    End Class
End Namespace
