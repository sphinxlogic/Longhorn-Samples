'------------------------------------------------------------------------------
' XAML code behind file (VB.NET)
'------------------------------------------------------------------------------
Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Data
Imports System.Windows.Media

Namespace SDKSample

'/ <summary>
'/ NumberListItem. This is the class that encapsulates each item
'/ in the numberlist.  The NLValue property is the property
'/ that is bound to by UI elements in the XAML markup.
'/ </summary>

Public Class NumberListItem
  Implements IPropertyChange

  Private _NLValue As Integer

  Sub New()

  End Sub 'New

  ' The following event and method provide the support for
  ' handling property changed events.
  Public Event PropertyChanged As PropertyChangedEventHandler Implements IPropertyChange.PropertyChanged

  Private Sub NotifyPropertyChanged(ByVal info As String)
    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
  End Sub 'NotifyPropertyChanged

  Public Property NLValue() As Integer
    Get
      Return _NLValue
    End Get

    Set
      If _NLValue <> value Then
        _NLValue = value
        NotifyPropertyChanged("NLValue")
      End If
    End Set
  End Property
End Class 'NumberListItem

'/ <summary>
'/ NumberList. In addition to serving as the datasource for the
'/ binding in this program, this class also exposes the methods
'/ through which a change to the list content can be initiated.
'/ </summary>

Public Class NumberList
  Inherits ArrayListDataCollection
  Private nli1 As New NumberListItem()
  Private nli2 As New NumberListItem()
  Private nli3 As New NumberListItem()

  Public Sub New()
    Add(nli1)
    Add(nli2)
    Add(nli3)
  End Sub 'New

  Public Sub SetOdd()
    ' Each of these assignments causes a NotifyPropertyChanged event.
    nli1.NLValue = 1
    nli2.NLValue = 3
    nli3.NLValue = 5
  End Sub 'SetOdd

  Public Sub SetEven()
    ' Each of these assignments causes a NotifyPropertyChanged event.
    nli1.NLValue = 2
    nli2.NLValue = 4
    nli3.NLValue = 6
  End Sub 'SetEven

End Class 'NumberList

End Namespace 'SDKSample
