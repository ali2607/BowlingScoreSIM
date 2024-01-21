namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the Leaderboard page.
/// </summary>
public class Leaderboard
{
    public (string Name,int Score)[] PlayerLeaderboard { get; set; }

    public Leaderboard((string,int)[] playerLeaderboard)
    {
        PlayerLeaderboard = playerLeaderboard;
    }


}