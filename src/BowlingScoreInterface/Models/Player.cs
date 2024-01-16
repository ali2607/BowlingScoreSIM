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

    public Player(string name, Home home)
    {
        Name = name;
        Home = home;
        CurrentRound = -1;
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRolll)>(Home.NumberOfRounds);
        
    }

    public void Roll1()
    {
        CurrentRound++;
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
            Tab2DScores[CurrentRound] = (score_1, score_2, SpecialRoll.Default);
        else 
            throw new Exception("Problem! Incorrect score!");

        CalculateRoundScore();
    }
    public int CalculateRoundScore()
    {
        // va regarder 1 ou 2 coups avant pour combler les potentiels trous avec les resultats récents

        throw new NotImplementedException();
    }
}
