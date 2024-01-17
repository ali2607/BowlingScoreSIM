namespace BowlingScoreInterface.Models;

public class Round
{
    public Player Player { get; set; }
    public string FirstRound { get; set; }
    public string SecondRound { get; set; }
    public string RoundScore { get; set; } 

    public Round (Player player, string firstRound, string secondRound, string roundScore)
    {
        Player = player;
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
