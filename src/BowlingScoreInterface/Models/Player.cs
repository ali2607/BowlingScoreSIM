namespace BowlingScoreInterface.Models;

class Player
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
    int score_1, score_2;
    public int TotalScore;
    private bool displayScore;
    private int waitingToDisplay = 2; //Roll remaining to display score
    public Player(string name, Home home)
    {
        Name = name;
        Home = home;
        CurrentRound = -1;
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
            Tab2DScores[CurrentRound] = (score_1, null, SpecialRoll.Strike);
            CalculateRoundScore();
        }
        else 
            throw new Exception("Problem! Incorrect score!");
    }
    public void Roll2()
    {
        
        // asks the user to enter the result
        if (score_1 + score_2 == Home.NumberOfPins)
            Tab2DScores[CurrentRound] = (score_1, score_2, SpecialRoll.Spare);
        else if(score_1 + score_2 < Home.NumberOfPins)
        {
            Tab2DScores[CurrentRound] = (score_1, score_2, SpecialRoll.Default);
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
            if (Tab2DScores[CurrentRound - 1].SpecialRoll == SpecialRoll.Strike)
            {
                // Previous Roll was a strike
                if (Tab2DScores[CurrentRound - 2].SpecialRoll == SpecialRoll.Strike)
                {
                    TotalScore += score_1;
                }

                TotalScore += score_1 + score_2;
            }
            else if (Tab2DScores[CurrentRound - 1].SpecialRoll == SpecialRoll.Spare)
            {
                // Previous Roll was a spare
                TotalScore += score_1;
            }
        }

        throw new NotImplementedException();
    }
}
