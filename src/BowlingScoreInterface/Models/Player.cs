using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BowlingScoreInterface.Models;

public class Player
{
    public string Name { get; private set; }
    public List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> Tab2DScores { get; private set; }
    public List<Round> Rounds { get; private set; }

    public int score_1, score_2;
    public int TotalScore;

 
    public Player(string name, int NumberOfRounds)
    {
        Name = name;
        score_1 = 0;
        score_2 = 0;
        TotalScore = 0;
        Rounds = new List<Round> (NumberOfRounds);
        for (int i = 0; i < NumberOfRounds; i++)
        {
            Rounds.Add(new());
        }
    }
    public Player(string name, List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> tab2DScores, List<Round> rounds, int score_1, int score_2, int totalScore)
    {
        Name = name;
        Tab2DScores = tab2DScores;
        Rounds = rounds;
        this.score_1 = score_1;
        this.score_2 = score_2;
        TotalScore = totalScore;
    }
    [JsonConstructor]
    public Player(string name, List<Round> rounds)
    {
        Name = name;
        Rounds = rounds;

        // Initialisation des autres propriétés
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        score_1 = 0;  // Assurez-vous que ces champs existent dans la classe
        score_2 = 0;  // et qu'ils sont appropriés pour votre logique d'application
        TotalScore = 0;
    }

    public void UpdateRounds(int NumberOfPins, int CurrentRound)
    {
        if (score_1 < NumberOfPins)
        {
            Rounds[CurrentRound] = new() {FirstRound = score_1.ToString(), SecondRound = String.Empty, RoundScore = String.Empty};
        }
        else if (score_1 == NumberOfPins)
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
        if (score_1 < NumberOfPins)
        {
            Roll2(NumberOfPins, CurrentRound);
        }
        else if (score_1 == NumberOfPins)
        {
            Tab2DScores.Add((score_1, null, SpecialRoll.Strike));
            score_2 = 0;
            CalculateRoundScore(NumberOfPins, CurrentRound);
        }
        else
            throw new Exception("Problem! Incorrect score!");
    }
    public void Roll2(int NumberOfPins, int CurrentRound)
    {
        if (score_1 + score_2 == NumberOfPins)
        {
            Tab2DScores.Add((score_1, score_2, SpecialRoll.Spare));
            Rounds[CurrentRound] = new() { FirstRound = score_1.ToString(), SecondRound = "/", RoundScore = String.Empty };


        }

        else if (score_1 + score_2 < NumberOfPins)
        {
            Tab2DScores.Add((score_1, score_2, SpecialRoll.Default));
            Rounds[CurrentRound] = new() { FirstRound = score_1.ToString(), SecondRound = score_2.ToString(), RoundScore = (score_1 + score_2 + TotalScore).ToString() };

        }
        else
            throw new Exception("Problem! Incorrect score!");

        CalculateRoundScore(NumberOfPins, CurrentRound);
    }
    public void CalculateRoundScore(int NumberOfPins, int CurrentRound)
    {
        TotalScore += score_1 + score_2;

        if (CurrentRound > 1)
        {
            if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Strike)
            {

                if (CurrentRound > 2 && Tab2DScores[CurrentRound - 2].specialRoll == SpecialRoll.Strike)
                {
                    // Two consecutive strikes
                    TotalScore += score_1;
                    Tab2DScores[CurrentRound - 1] = (score_1 + NumberOfPins, null, SpecialRoll.Strike);
                    Rounds[CurrentRound - 1].RoundScore = (score_1 + NumberOfPins + TotalScore).ToString();
                }
                TotalScore += score_1 + score_2;
                Tab2DScores[CurrentRound - 1] = (score_1 + score_2 + (NumberOfPins), null, SpecialRoll.Strike);
                Rounds[CurrentRound - 1].RoundScore = (score_1 + score_2 + (NumberOfPins) + TotalScore).ToString();
            }
            else if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Spare)
            {
                // Previous roll was a spare
                TotalScore += score_1;
                Tab2DScores[CurrentRound - 1] = (score_1 + NumberOfPins, null, SpecialRoll.Spare);
                Rounds[CurrentRound - 1].RoundScore = (score_1 + NumberOfPins + TotalScore).ToString();
            }
        }
    }
}

