using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the game page.
/// </summary>
public class Game
{
    public int CurrentRound {  get; set; }
    public List<Player> Players { get; set; }
    public int NumberOfRounds { get; set; }
    public int NumberOfPins { get; set; }
    public int actualplayer { get; set; }
    public bool isRoll1 { get; set; }

    public Game(Home startingParameter)
    {
        NumberOfRounds = startingParameter.NumberOfRounds;
        Players = new List<Player>(startingParameter.Players.Count);
        for (int i = 0; i < startingParameter.Players.Count; i++)
        {
            Players.Add(new Player(startingParameter.Players[i], NumberOfRounds));
        }
        NumberOfPins = startingParameter.NumberOfPins;
        CurrentRound = 0;
        isRoll1 = true;
        actualplayer = 0;
    }
    public Game() {}

    public Game(int currentRound, List<Player> players, int numberOfRounds, int numberOfPins, int actualplayer, bool isRoll1)
    {
        CurrentRound = currentRound;
        Players = players;
        NumberOfRounds = numberOfRounds;
        NumberOfPins = numberOfPins;
        this.actualplayer = actualplayer;
        this.isRoll1 = isRoll1;
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
            Players[actualplayer].Score_1 = pinsScore;
            Players[actualplayer].UpdateRounds(NumberOfPins, CurrentRound);
            if (pinsScore == NumberOfPins)
            {
                Players[actualplayer].Roll1(NumberOfPins, CurrentRound);
                actualplayer = (actualplayer + 1) % Players.Count();
            }
            else
            {
                isRoll1 = false;
            }
        }
        else
        {
            Players[actualplayer].Score_2 = pinsScore;
            Players[actualplayer].Roll1(NumberOfPins, CurrentRound);
            isRoll1 = true;
            actualplayer =  (actualplayer + 1) % Players.Count();
        }
      /*  if (CurrentRound == NumberOfRounds && (Players[actualplayer - 1].Rounds[CurrentRound].FirstRound == "X" || Players[actualplayer -1].Rounds[CurrentRound].SecondRound == "/"))
        {
            Gérer le lancé en plus 
        }*/
        if (actualplayer == 0 && isRoll1) 
        {
            CurrentRound++;
        }

       
        return this;
    }
}