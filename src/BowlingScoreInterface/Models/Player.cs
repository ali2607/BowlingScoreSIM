using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Represents a player in the bowling game.
/// </summary>
public class Player
{
    /// <summary>
    /// The name of the player.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// A list that stores tuples of each roll's scores and special roll indicators.
    /// </summary>
    public List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> Tab2DScores { get; internal set; }

    /// <summary>
    /// The list of rounds with their respective scores and special roll indicators.
    /// </summary>
    public List<Round> Rounds { get; private set; }

    /// <summary>
    /// Score for the first roll in a frame.
    /// </summary>
    public int Score_1 { get; set; }

    /// <summary>
    /// Score for the second roll in a frame, if applicable.
    /// </summary>
    public int Score_2 { get; set; }

    /// <summary>
    /// Total score accumulated by the player.
    /// </summary>
    public int TotalScore { get; set; }

    /// <summary>
    /// The type of special roll (strike, spare, etc.) achieved by the player.
    /// </summary>
    public SpecialRoll BonusRoll { get; set; }

    public bool Problem { get; set; } = true;

    /// <summary>
    /// Constructor for initializing a player with a name and the number of rounds.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="NumberOfRounds">The number of rounds in the game.</param>
    public Player(string name, int NumberOfRounds)
    {
        Name = name;
        Score_1 = 0;
        Score_2 = 0;
        TotalScore = 0;
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        Rounds = new List<Round> (NumberOfRounds);
        for (int i = 0; i < NumberOfRounds; i++)
        {
            Rounds.Add(new());
        }
    }
    /// <summary>
    /// Constructor for initializing a player with detailed scores and rolls.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="tab2DScores">A list of tuples representing the player's scores and special rolls.</param>
    /// <param name="rounds">A list of rounds for the player.</param>
    /// <param name="Score_1">Score of the first roll.</param>
    /// <param name="Score_2">Score of the second roll.</param>
    /// <param name="totalScore">Total score of the player.</param>
    public Player(string name, List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> tab2DScores, List<Round> rounds, int Score_1, int Score_2, int totalScore)
    {
        Name = name;
        Tab2DScores = tab2DScores;
        Rounds = rounds;
        this.Score_1 = Score_1;
        this.Score_2 = Score_2;
        TotalScore = totalScore;
    }

    /// <summary>
    /// Constructor used for deserialization from JSON.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="rounds">The rounds associated with the player.</param>
    [JsonConstructor]
    public Player(string name, List<Round> rounds)
    {
        Name = name;
        Rounds = rounds;

        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        Score_1 = 0;
        Score_2 = 0; 
        TotalScore = 0;
    }

    /// <summary>
    /// Updates the round's information for the player based on the current roll scores.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void UpdateRounds(int NumberOfPins, int CurrentRound)
    {
        if (Score_1 < NumberOfPins)
        {
            Rounds[CurrentRound] = new() {FirstRound = Score_1.ToString(), SecondRound = String.Empty, RoundScore = String.Empty};
        }
        else if (Score_1 == NumberOfPins)
        {
            Rounds[CurrentRound] = new() { FirstRound = "X", SecondRound = String.Empty, RoundScore = String.Empty };
        }
        else
        {
            throw new Exception("Problem! Incorrect score!");
        }
    }
    /// <summary>
    /// Handles the logic for the first roll in a round, including scoring and special roll assessment.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void Roll1(int NumberOfPins, int CurrentRound)
    {
        if (Score_1 < NumberOfPins)
        {
            Roll2(NumberOfPins, CurrentRound);
        }
        else if (Score_1 == NumberOfPins)
        {
            Tab2DScores.Add((Score_1, null, SpecialRoll.Strike));
            if (CurrentRound != Rounds.Count() - 1)
            {
                Score_2 = 0;
            }
            else if (Rounds[CurrentRound].RoundScore != String.Empty)
            {
                Score_1 = 0;
            }
            TotalScore += Score_1 + Score_2;
            CalculateRoundScore(NumberOfPins, CurrentRound);
        }
        else
            throw new Exception("Problem! Incorrect score!");
    }

    /// <summary>
    /// Handles the logic for the second roll in a round, including scoring and special roll assessment.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike or spare.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void Roll2(int NumberOfPins, int CurrentRound)
    {
        TotalScore += Score_1 + Score_2;
        if (Score_1 + Score_2 == NumberOfPins)
        {
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Spare));
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = "/", RoundScore = String.Empty };
    
        }


        else if (Score_1 + Score_2 < NumberOfPins)
        {
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Default));
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = Score_2.ToString(), RoundScore = TotalScore.ToString() };
        
        }
        else
            throw new Exception("Problem! Incorrect score!");

        CalculateRoundScore(NumberOfPins, CurrentRound);
    }

    /// <summary>
    /// Calculates the round score for the player, taking into account the special rolls like strikes and spares from previous rounds.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void CalculateRoundScore(int NumberOfPins, int CurrentRound)
    {

        if (CurrentRound > 0)
        {

            if (Rounds[CurrentRound - 1].FirstRound == "X")
            {

                if (CurrentRound > 1 && Rounds[CurrentRound - 2].FirstRound == "X")
                {
                    // Two consecutive strikes
                    if (Problem)
                    {
                        if (CurrentRound == Rounds.Count() - 1)
                        {
                            Problem = false;
                        }
                        Rounds[CurrentRound - 2].RoundScore = (int.Parse(Rounds[CurrentRound - 2].RoundScore) + Score_1).ToString();  //((TotalScore - Score_2)).ToString();
                        TotalScore += Score_1;
                    }
                    //    Tab2DScores[CurrentRound - 1] = (Score_1 + NumberOfPins, null, SpecialRoll.Strike);
                }
                // Tab2DScores[CurrentRound - 1] = (Score_1 + Score_2 + (NumberOfPins), null, SpecialRoll.Strike);
                Rounds[CurrentRound - 1].RoundScore = TotalScore.ToString();

                Rounds[CurrentRound].RoundScore = (TotalScore + Score_1 + Score_2).ToString();
                TotalScore += Score_1 + Score_2;
                

            }
            else if (Rounds[CurrentRound - 1].SecondRound == "/")
            {
                // Previous roll was a spare
                //Tab2DScores[CurrentRound - 1] = (Score_1 + NumberOfPins, null, SpecialRoll.Spare);
                Rounds[CurrentRound - 1].RoundScore = (TotalScore - Score_2).ToString();
                TotalScore += Score_1;
                Rounds[CurrentRound].RoundScore = (TotalScore).ToString();
            }

        }
        if (CurrentRound == Rounds.Count() - 1)
        {
            TotalScore -= (Score_1 + Score_2);
            Rounds[CurrentRound].RoundScore = (int.Parse(Rounds[CurrentRound].RoundScore) - (Score_1 + Score_2)).ToString();
        }
    }
}