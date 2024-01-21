using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BowlingScoreInterface.Models;

public class Player
{
    public string Name { get; private set; }
    public List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> Tab2DScores { get; private set; }
    public List<Round> Rounds { get; private set; }

    public int Score_1 { get; set;}
    public int Score_2 { get; set; }
    public int TotalScore { get; set; }

 
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
    public Player(string name, List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> tab2DScores, List<Round> rounds, int Score_1, int Score_2, int totalScore)
    {
        Name = name;
        Tab2DScores = tab2DScores;
        Rounds = rounds;
        this.Score_1 = Score_1;
        this.Score_2 = Score_2;
        TotalScore = totalScore;
    }
    [JsonConstructor]
    public Player(string name, List<Round> rounds)
    {
        Name = name;
        Rounds = rounds;

        // Initialisation des autres propriétés
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        Score_1 = 0;  // Assurez-vous que ces champs existent dans la classe
        Score_2 = 0;  // et qu'ils sont appropriés pour votre logique d'application
        TotalScore = 0;
    }

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
    public void Roll1(int NumberOfPins, int CurrentRound)
    {
        if (Score_1 < NumberOfPins)
        {
            Roll2(NumberOfPins, CurrentRound);
        }
        else if (Score_1 == NumberOfPins)
        {
            Tab2DScores.Add((Score_1, null, SpecialRoll.Strike));
            Score_2 = 0;
            CalculateRoundScore(NumberOfPins, CurrentRound);
        }
        else
            throw new Exception("Problem! Incorrect score!");
    }
    public void Roll2(int NumberOfPins, int CurrentRound)
    {
        if (Score_1 + Score_2 == NumberOfPins)
        {
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Spare));
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = "/", RoundScore = String.Empty };
        }

        else if (Score_1 + Score_2 < NumberOfPins)
        {
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Default));
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = Score_2.ToString(), RoundScore = (Score_1 + Score_2 + TotalScore).ToString() };
        }
        else
            throw new Exception("Problem! Incorrect score!");

        CalculateRoundScore(NumberOfPins, CurrentRound);
    }
    public void CalculateRoundScore(int NumberOfPins, int CurrentRound)
    {
        TotalScore += Score_1 + Score_2;

        if (CurrentRound > 0)
        {
            if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Strike)
            {

                if (CurrentRound > 1 && Tab2DScores[CurrentRound - 2].specialRoll == SpecialRoll.Strike)
                {
                    // Two consecutive strikes
                    TotalScore += Score_1;
                    Tab2DScores[CurrentRound - 1] = (Score_1 + NumberOfPins, null, SpecialRoll.Strike);
                    Rounds[CurrentRound - 1].RoundScore = (Score_1 + NumberOfPins + TotalScore).ToString();
                }
                Tab2DScores[CurrentRound - 1] = (Score_1 + Score_2 + (NumberOfPins), null, SpecialRoll.Strike);
                Rounds[CurrentRound - 1].RoundScore = TotalScore.ToString();
                TotalScore += Score_1 + Score_2;
            }
            else if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Spare)
            {
                // Previous roll was a spare
                TotalScore += Score_1;
                Tab2DScores[CurrentRound - 1] = (Score_1 + NumberOfPins, null, SpecialRoll.Spare);
                Rounds[CurrentRound - 1].RoundScore = (Score_1 + NumberOfPins + TotalScore).ToString();
            }
        }
    }
}

