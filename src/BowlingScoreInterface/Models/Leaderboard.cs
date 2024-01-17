namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the Leaderboard page.
/// </summary>
public class Leaderboard
{
    public List<string> Players { get; set; }
    public int NumberOfRounds { get; set; }
    public int NumberOfPins { get; set; }
    public int actualplayer { get; set; }

    public Leaderboard(Leaderboard sartingParameter)
    {
        Players = sartingParameter.Players;
        NumberOfRounds = sartingParameter.NumberOfRounds;
    }

    public Leaderboard() : this(new Leaderboard())
    {
        Players = new List<string>() { "Player 1" };
        NumberOfRounds = 1;
    }
}