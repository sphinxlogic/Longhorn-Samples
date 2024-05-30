
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SDKSample
{
  public class LeagueList : ArrayListDataCollection
  {
    public LeagueList() : base(2)
    {
      League l;
      Division d;

      l = new League("American League");
      Add(l);
      d = new Division("East");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Baltimore"));
      d.Teams.Add(new Team("Boston"));
      d.Teams.Add(new Team("New York"));
      d.Teams.Add(new Team("Tampa Bay"));
      d.Teams.Add(new Team("Toronto"));
      d = new Division("Central");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Chicago"));
      d.Teams.Add(new Team("Cleveland"));
      d.Teams.Add(new Team("Detroit"));
      d.Teams.Add(new Team("Kansas City"));
      d.Teams.Add(new Team("Minnesota"));
      d = new Division("West");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Anaheim"));
      d.Teams.Add(new Team("Oakland"));
      d.Teams.Add(new Team("Seattle"));
      d.Teams.Add(new Team("Texas"));
      l = new League("National League");
      Add(l);
      d = new Division("East");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Atlanta"));
      d.Teams.Add(new Team("Florida"));
      d.Teams.Add(new Team("Montreal"));
      d.Teams.Add(new Team("New York"));
      d.Teams.Add(new Team("Philadelphia"));
      d = new Division("Central");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Chicago"));
      d.Teams.Add(new Team("Cincinnati"));
      d.Teams.Add(new Team("Houston"));
      d.Teams.Add(new Team("Milwaukee"));
      d.Teams.Add(new Team("Pittsburgh"));
      d.Teams.Add(new Team("St. Louis"));
      d = new Division("West");
      l.Divisions.Add(d);
      d.Teams.Add(new Team("Arizona"));
      d.Teams.Add(new Team("Colorado"));
      d.Teams.Add(new Team("Los Angeles"));
      d.Teams.Add(new Team("San Diego"));
      d.Teams.Add(new Team("San Francisco"));
    }
  }

  public class League
  {
    string _name;
    DivisionList _divisions;

    public League(string name)
    {
    	_name = name;
    	_divisions = new DivisionList();
    }

    public string Name { get { return _name; } }

    public DivisionList Divisions { get { return _divisions; } }
  }

  public class DivisionList : ArrayListDataCollection
  {
    public DivisionList() : base()
    {
    }
  }

  public class Division
  {
    string _name;
    TeamList _teams;

    public Division(string name)
    {
    	_name = name;
    	_teams = new TeamList();
    }

    public string Name { get { return _name; } }

    public TeamList Teams { get { return _teams; } }
  }

  public class TeamList : ArrayListDataCollection
  {
    public TeamList() : base()
    {
    }
  }

  public class Team
  {
    string _name;

    public Team(string name)
    {
      _name = name;
    }

		public string Name { get { return _name; } }
  }
}
