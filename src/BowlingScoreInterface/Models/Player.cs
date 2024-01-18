using System.Diagnostics;

namespace BowlingScoreInterface.Models;

public class Player
{
    public string Name { get; private set; }
    public enum SpecialRoll
    {
        Default = 0,
        Spare=1,
        Strike=2
    }
    public List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> Tab2DScores { get; private set; }

    public Home Home { get; private set; }
    public int CurrentRound {  get; private set; } 
    public List<Round> Rounds { get; private set; } // for display (Ali)

    public int score_1, score_2;
    public int TotalScore;
    private bool displayScore;
    private int waitingToDisplay = 2; //Roll remaining to display score
    
    public Player(string name, Home home)
    {
        Name = name;
        Home = home;
        Rounds = new List<Round>(Home.NumberOfRounds);
        for (int i = 0; i < Home.NumberOfRounds; i++)
        {
            Rounds.Add(new Round());
        }

        TotalScore = 0;
        CurrentRound = -1;
        Rounds = new List<Round>(home.NumberOfRounds);
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>(Home.NumberOfRounds);
    }

    public void Roll1()
    {
        CurrentRound++;
        waitingToDisplay --;
        // asks the user to enter the result
        if (score_1 < Home.NumberOfPins)
            Roll2();
        else if (score_1 == Home.NumberOfPins)
        {
            Tab2DScores.Add((score_1, null, SpecialRoll.Strike));
            Rounds[CurrentRound].FirstRound = "X";
            Rounds[CurrentRound].SecondRound = " ";
            Rounds[CurrentRound].RoundScore = " ";
            CalculateRoundScore();
        }
        else 
            throw new Exception("Problem! Incorrect score!");
    }
    public void Roll2()
    {
        // asks the user to enter the result
        if (score_1 + score_2 == Home.NumberOfPins)
        {
            Tab2DScores.Add((score_1, score_2, SpecialRoll.Spare));
            Rounds[CurrentRound].FirstRound = score_1.ToString();
            Rounds[CurrentRound].SecondRound = "/";
            Rounds[CurrentRound].RoundScore = " ";
        }
            
        else if(score_1 + score_2 < Home.NumberOfPins)
        {
            Tab2DScores.Add((score_1, score_2, SpecialRoll.Default));

            Rounds[CurrentRound].FirstRound = score_1.ToString();
            Rounds[CurrentRound].SecondRound = score_2.ToString();
            Rounds[CurrentRound].RoundScore = (score_1+score_2+TotalScore).ToString();

            waitingToDisplay--;
        }
        else 
            throw new Exception("Problem! Incorrect score!");

        CalculateRoundScore();
    }
    public int CalculateRoundScore()
    {
        TotalScore += score_1 + score_2;
        if (CurrentRound > 1)
        {
            if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Strike)
            {
                // Previous Roll was a strike
                if (Tab2DScores[CurrentRound - 2].specialRoll == SpecialRoll.Strike)
                {
                    TotalScore += score_1;
                    Tab2DScores[CurrentRound - 1] = (score_1+ Home.NumberOfPins, null, SpecialRoll.Strike);

                    Rounds[CurrentRound -1].RoundScore = (score_1 + Home.NumberOfPins + TotalScore).ToString();
                }

                TotalScore += score_1 + score_2;
                Tab2DScores[CurrentRound - 2] = (score_1 + score_2+2*(Home.NumberOfPins), null, SpecialRoll.Strike);
                Rounds[CurrentRound - 2].RoundScore = (score_1 + score_2 + 2 * (Home.NumberOfPins) + TotalScore).ToString();
            }
            else if (Tab2DScores[CurrentRound - 1].specialRoll == SpecialRoll.Spare)
            {
                // Previous Roll was a spare
                TotalScore += score_1;
                Tab2DScores[CurrentRound - 1] = (score_1 + Home.NumberOfPins, null, SpecialRoll.Spare);
                Rounds[CurrentRound - 1].RoundScore = (score_1 + Home.NumberOfPins + TotalScore).ToString();
            }
        }
        throw new NotImplementedException();
    }
}
