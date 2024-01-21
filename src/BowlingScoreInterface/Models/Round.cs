namespace BowlingScoreInterface.Models;

public class Round
{
    public string FirstRound { get; set; }
    public string SecondRound { get; set; }
    public string RoundScore { get; set; } 

    public Round (string firstRound, string secondRound, string roundScore)
    {
        FirstRound = firstRound;
        SecondRound = secondRound;
        RoundScore = roundScore;
    }

    public Round()
    {
        FirstRound = " ";
        SecondRound = " ";
        RoundScore = " ";
    }
}
