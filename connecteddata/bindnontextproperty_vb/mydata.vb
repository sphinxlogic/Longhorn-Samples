Imports System.Windows.Media
Imports System

Namespace WCPSample
  Public Class myData
    Public _boundcolor As String = "Red"

    Public ReadOnly Property BoundColor() As String
      Get
        Return _boundcolor
      End Get
    End Property

  End Class
End Namespace
