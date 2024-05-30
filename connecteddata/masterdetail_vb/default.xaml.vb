
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace SDKSample

Public Class LeagueList
  Inherits ArrayListDataCollection

  Public Sub New()
    MyBase.New(2)
    Dim l As League
    Dim d As Division

    l = New League("American League")
    Add(l)
    d = New Division("East")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Baltimore"))
    d.Teams.Add(New Team("Boston"))
    d.Teams.Add(New Team("New York"))
    d.Teams.Add(New Team("Tampa Bay"))
    d.Teams.Add(New Team("Toronto"))
    d = New Division("Central")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Chicago"))
    d.Teams.Add(New Team("Cleveland"))
    d.Teams.Add(New Team("Detroit"))
    d.Teams.Add(New Team("Kansas City"))
    d.Teams.Add(New Team("Minnesota"))
    d = New Division("West")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Anaheim"))
    d.Teams.Add(New Team("Oakland"))
    d.Teams.Add(New Team("Seattle"))
    d.Teams.Add(New Team("Texas"))
    l = New League("National League")
    Add(l)
    d = New Division("East")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Atlanta"))
    d.Teams.Add(New Team("Florida"))
    d.Teams.Add(New Team("Montreal"))
    d.Teams.Add(New Team("New York"))
    d.Teams.Add(New Team("Philadelphia"))
    d = New Division("Central")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Chicago"))
    d.Teams.Add(New Team("Cincinnati"))
    d.Teams.Add(New Team("Houston"))
    d.Teams.Add(New Team("Milwaukee"))
    d.Teams.Add(New Team("Pittsburgh"))
    d.Teams.Add(New Team("St. Louis"))
    d = New Division("West")
    l.Divisions.Add(d)
    d.Teams.Add(New Team("Arizona"))
    d.Teams.Add(New Team("Colorado"))
    d.Teams.Add(New Team("Los Angeles"))
    d.Teams.Add(New Team("San Diego"))
    d.Teams.Add(New Team("San Francisco"))

  End Sub 'New
End Class 'LeagueList


Public Class League
  Private _name As String
  Private _divisions As DivisionList

  Public Sub New(ByVal name As String)
    _name = name
    _divisions = New DivisionList()

  End Sub 'New

  Public ReadOnly Property Name() As String
    Get
      Return _name
    End Get
  End Property

  Public ReadOnly Property Divisions() As DivisionList
    Get
      Return _divisions
    End Get
  End Property
End Class 'League

Public Class DivisionList
  Inherits ArrayListDataCollection

  Public Sub New()

  End Sub 'New
End Class 'DivisionList


Public Class Division
  Private _name As String
  Private _teams As TeamList

  Public Sub New(ByVal name As String)
    _name = name
    _teams = New TeamList()

  End Sub 'New

  Public ReadOnly Property Name() As String
    Get
      Return _name
    End Get
  End Property

  Public ReadOnly Property Teams() As TeamList
      Get
        Return _teams
      End Get
  End Property
End Class 'Division

Public Class TeamList
  Inherits ArrayListDataCollection

  Public Sub New()

  End Sub 'New
End Class 'TeamList


Public Class Team
  Private _name As String

  Public Sub New(ByVal name As String)
    _name = name
  End Sub 'New

  Public ReadOnly Property Name() As String
    Get
      Return _name
    End Get
  End Property
End Class 'Team

End Namespace 'SDKSample
