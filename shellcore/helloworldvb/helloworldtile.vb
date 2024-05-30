Imports System
Imports System.Windows.Media     'SolidColorBrush
Imports System.Windows.Desktop     'BaseTile

Namespace MySidebarTiles

    Public Class HelloWorldTile
        Inherits BaseTile

        Public Overrides Sub Initialize()
            Dim displayText As New System.Windows.Controls.Text
            displayText.Foreground = New SolidColorBrush(Colors.AntiqueWhite)
            displayText.TextContent = "Hello World"
            Me.Foreground = displayText
        End Sub
    End Class
End Namespace
