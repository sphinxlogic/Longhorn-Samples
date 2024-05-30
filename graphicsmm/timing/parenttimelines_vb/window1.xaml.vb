'
'		Window1.xaml And Window1.xaml.vb:		
'			Used to layout and initialize the timing
'			and animation samples.
'
'		AnimationAndTimlineLinkingExample.vb
'			Demonstrates how to use a single
'			Timeline to control other timelines
'			and animations.
'
'		BeginTimesExample.vb
'			Demonstrates the behavior of the Begin property.
'
'		EndTimesExample.vb
'			Demonstrates the use of implicit end times on a timeline.
'
'		SynchronizeTimelinesExample.vb
'			Demonstrates how to synchronize timeline objects.
'
'		MyClock.cs
'			Used to visually represent a timeline.
'



Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data

Namespace parentTimelines_vb

    '@ <summary>
    '@ Interaction logic for Window1_xaml.xaml
    '@ </summary>
    Expands Class Window1

        Private mainPanel As GridPanel

        Private Sub WindowLoaded(ByVal sender As Object, ByVal e As System.EventArgs)
            mainPanel = New GridPanel()
            mainPanel.CellSpacing = New Length(20)

            mainPanel.Columns = 1
            Try

                initializeFirstSample()
                initializeSecondSample()
                initializeThirdSample()
                initializeFourthSample()
                initializeFifthSample()
                ' initializeSixthSample()


            Catch ex As Exception

                MessageBox.Show(ex.ToString())
            End Try


            Dim sViewer As New ScrollViewer()
            sViewer.Content = mainPanel
            mainWindow.Content = sViewer



        End Sub

        Private Sub initializeFirstSample()

            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New System.Windows.Documents.TextPanel()
            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(12, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "Animation and Timeline Linking Example"
            samplePanel.Children.Add(myTextPanel)

            Dim firstSample As New TimingExamples.AnimationAndTimelineLinkingExample()
            samplePanel.Children.Add(firstSample)
            mainPanel.Children.Add(samplePanel)
        End Sub

        Private Sub initializeSecondSample()
            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New System.Windows.Documents.TextPanel()

            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(12, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "Begin Time Example"
            samplePanel.Children.Add(myTextPanel)

            Dim secondSample As New TimingExamples.BeginTimeExample()
            samplePanel.Children.Add(secondSample)
            mainPanel.Children.Add(samplePanel)
        End Sub



        Private Sub initializeThirdSample()

            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New TextPanel()
            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(14, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "Timeline Sync Example"
            samplePanel.Children.Add(myTextPanel)

            Dim thirdSample = New TimingExamples.SynchronizeTimelinesExample()

            samplePanel.Children.Add(thirdSample)
            mainPanel.Children.Add(samplePanel)
        End Sub


        Private Sub initializeFourthSample()

            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New TextPanel()

            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(12, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "End Times Example (AllChildren)"
            samplePanel.Children.Add(myTextPanel)

            Dim fourthSample As New TimingExamples.TimeEndSyncAllChildrenExample()

            samplePanel.Children.Add(fourthSample)
            mainPanel.Children.Add(samplePanel)
        End Sub

        Private Sub initializeFifthSample()

            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New TextPanel()

            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(12, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "End Times Example (LastChild)"
            samplePanel.Children.Add(myTextPanel)

            Dim fifthSample As New TimingExamples.TimeEndSyncLastChildExample()

            samplePanel.Children.Add(fifthSample)
            mainPanel.Children.Add(samplePanel)
        End Sub


        Private Sub initializeSixthSample()

            Dim samplePanel As New GridPanel()
            Dim myTextPanel As New TextPanel()

            myTextPanel.Width = New Length(500)
            myTextPanel.Height = New Length(20)
            myTextPanel.FontSize = New FontSize(12, FontSizeType.Point)
            myTextPanel.Background = System.Windows.Media.Brushes.LightGray
            myTextPanel.Foreground = System.Windows.Media.Brushes.Black
            myTextPanel.TextContent = "Unresolved End Time Example (AllChildren)"
            samplePanel.Children.Add(myTextPanel)

            Dim sixthSample As New TimingExamples.AnotherTimeEndSyncAllChildrenExample()

            samplePanel.Children.Add(sixthSample)
            mainPanel.Children.Add(samplePanel)
        End Sub




    End Class

End Namespace