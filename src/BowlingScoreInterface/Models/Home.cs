using System.Numerics;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the home page.
/// </summary>
public class Home
{
    /// <summary>
    /// List of players.
    /// </summary>
    public List<string> Players { get; set; } = new List<string>();

    /// <summary>
    /// amount of rounds.
    /// </summary>
    public int NumberOfRounds { get; set; } = 1;

    /// <summary>
    /// Limit of players.
    /// </summary>
    public int MaxPlayers { get; } = 10;


}
