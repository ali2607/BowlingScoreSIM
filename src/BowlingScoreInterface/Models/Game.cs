using System.Runtime.CompilerServices;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the game page.
/// </summary>
public class Game
{
    public List<Player> Players { get; set; }
    public int NumberOfRounds { get; set; }
    public int NumberOfPins { get; set; }
    public int actualplayer { get; set; }

    public Game(Home startingParameter)
    {
        Players = new List<Player>(startingParameter.Players.Count);
        for (int i = 0; i < startingParameter.Players.Count; i++)
        {
            Players.Add(new Player(startingParameter.Players[i], startingParameter));
        }
        NumberOfRounds = startingParameter.NumberOfRounds;
        NumberOfPins = startingParameter.NumberOfPins;
    }

    public Game() : this(new Home())
    {
        Players = new(1);
        NumberOfRounds = 1;
    }

    /// <summary>
    /// Method to update the game.
    /// </summary>
    /// <param name="pinsScore">the number of pins that fell in this roll</param>
    /// <returns>the updated Game</returns>
    public Game Update(int pinsScore)
    {

        throw new NotImplementedException();
    }
}