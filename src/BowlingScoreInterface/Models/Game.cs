namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the game page.
/// </summary>
public class Game
{
    public List<string> Players { get; set; }
    public int NumberOfRounds { get; set; } 

    public Game(Home sartingParameter)
    {
        Players = sartingParameter.Players;
        NumberOfRounds = sartingParameter.NumberOfRounds;
    }

    public Game() : this(new Home())
    {
        Players = new List<string>() {"Player 1"};
        NumberOfRounds = 1;
    }
}
