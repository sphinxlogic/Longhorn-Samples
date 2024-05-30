Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Data
Imports System.Windows.Commands

Namespace EnableAndDisableTextFormattingNew

    '@ <summary>
    '@ Interaction logic for Window1_xaml.xaml
    '@ </summary>
    Partial Class Window1

        Private Sub WindowLoaded(ByVal sender As Object, ByVal args As EventArgs)
            '			// Richtext is on by default. Only enable button once plain text has been toggled on.
            btnrichtext.IsEnabled = False
        End Sub

        Private Sub doCommand(ByVal sender As Object, ByVal args As ClickEventArgs)

            Dim currentID As String
            Dim button As Button
            button = CType(sender, Button)
            currentID = button.ID

            Try
                Select Case currentID

                    Case "btnbold"

                        tb1.RaiseCommand(StandardCommands.BoldCommand)

                    Case "btnitalic"


                        tb1.RaiseCommand(StandardCommands.ItalicCommand)


                    Case "btnunderline"


                        tb1.RaiseCommand(StandardCommands.UnderlineCommand)


                End Select

            Catch ex As System.Exception

                MessageBox.Show(ex.Message)

            End Try

        End Sub

        Private Sub RichText(ByVal sender As Object, ByVal args As ClickEventArgs)
            '		
            '			For Each CommandLink link in tb1.CommandLinks
            '			
            '				Command command = link.Command

            '				If command == StandardCommands.BoldCommand || command == StandardCommands.ItalicCommand || command == StandardCommands.UnderlineCommand
            '					link.Enabled = true
            '			

            '			Set the state of the toolbar buttons
            '			btnbold.IsEnabled = true
            '			btnitalic.IsEnabled = true
            '			btnunderline.IsEnabled = true
            '			btnplaintext.IsEnabled = true
            '			btnrichtext.IsEnabled = false
            '
            '           Next 

        End Sub

        Private Sub PlainText(ByVal sender As Object, ByVal args As ClickEventArgs)
            '		
            '			For Each CommandLink link in tb1.CommandLinks
            '			
            '				Command command = link.Command

            '				If command == StandardCommands.BoldCommand || command == StandardCommands.ItalicCommand || command == StandardCommands.UnderlineCommand
            '					link.Enabled = false
            '			
            '		    Set the state of the toolbar buttons

            '			btnbold.IsEnabled = false
            '			btnitalic.IsEnabled = false
            '			btnunderline.IsEnabled = false
            '			btnplaintext.IsEnabled = false
            '			btnrichtext.IsEnabled = true

            '			Replace the formatted text with plain text. The Text property is a string so it effectively removes formatting by not storing the formatting information

            '			string s = tb1.Text
            '			tb1.Clear()
            '			tb1.Text = s

            '		Next
        End Sub

    End Class

End Namespace