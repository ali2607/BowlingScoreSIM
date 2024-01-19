namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the Leaderboard page.
/// </summary>
public class Leaderboard
{
    public List<Player> Players { get; set; }

    public Leaderboard(Leaderboard sartingParameter)
    {
        Players = sartingParameter.Players;
    }

    public Leaderboard() : this(new Leaderboard())
    {
        Players = new(1);
    }
}