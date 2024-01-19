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
    public bool isRoll1 { get; set; }

    public Game(Home startingParameter)
    {
        Players = new List<Player>(startingParameter.Players.Count);
        for (int i = 0; i < startingParameter.Players.Count; i++)
        {
            Players.Add(new Player(startingParameter.Players[i], startingParameter));
        }
        NumberOfRounds = startingParameter.NumberOfRounds;
        NumberOfPins = startingParameter.NumberOfPins;
        isRoll1 = true;
        actualplayer = 0;
    }

    public Game() : this(new Home())
    {
        Players = new(1);
        NumberOfRounds = 10;
    }

    /// <summary>
    /// Method to update the game.
    /// </summary>
    /// <param name="pinsScore">the number of pins that fell in this roll</param>
    /// <returns>the updated Game</returns>
    public Game Update(int pinsScore)
    {
        if (isRoll1)
        {
            if (pinsScore == NumberOfPins)
            {
                actualplayer = (actualplayer + 1) % Players.Count();
                Players[actualplayer].Roll1();
            }
            else
            {
                isRoll1 = false;
            }
            Players[actualplayer].score_1 = pinsScore;
        }
        else
        {
            Players[actualplayer].score_2 = pinsScore;
            isRoll1 = true;
            actualplayer =  (actualplayer + 1) % Players.Count();
            Players[actualplayer].Roll1();
        }
        return this;
    }
}