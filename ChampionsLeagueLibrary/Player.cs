namespace ChampionsLeagueLibrary;

#region Třída Player
// TODO: Vytvořte třídu Player

// TODO: Vytvořte vlastnosti
// - string Name
// - FootballClub Club
// - int GoalCount

// TODO: Vytvořte parametrický konstruktor (name, club, goalCount)
public class Player
{
    public string Name { get; set; }
    public FootballClub Club { get; set; }
    public int GoalCount { get; set; }

    public Player(string name, FootballClub club, int goalCount)
    {
        Name = name;
        Club = club;
        GoalCount = goalCount;
    }
}
#endregion
