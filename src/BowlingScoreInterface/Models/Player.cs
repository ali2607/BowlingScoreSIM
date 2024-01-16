namespace BowlingScoreInterface.Models;
class Player
{
    public string Name { get; private set; }
    public List<(int Roll1, int Roll2)> Tab2DScores { get; private set; }
    public Home Home { get; private set; }
    public int CurrentRound {  get; private set; } 
    int score_1, score_2;

    public Player(string name, Home home)
    {
        Name = name;
        Home = home;
        CurrentRound = -1;
        Tab2DScores = [];    
    }

    public void Roll_1()
    {
        Console.WriteLine("Roll 1: ");
        while (!int.TryParse(Console.ReadLine(), out score_1) || score_1 < 0 || score_1 > Home.NumberOfPins)
        {
            Console.WriteLine("Invalid input. Please enter a score between 0 and 10.");
            Console.Write("Enter the score for the first roll: ");

        }

        
    }
    public void Roll_2()
    {
        if (score_1 < Home.NumberOfPins)
        {
            Console.WriteLine("Roll 2: ");
            while (!int.TryParse(Console.ReadLine(), out score_2) || score_2 < 0 || score_2 + score_1 > Home.NumberOfPins)
            {
                Console.WriteLine("Invalid input. Please enter a score between 0 and 10.");
                Console.Write("Enter the score for the second roll: ");
            }



            CurrentRound++;
        }
    }
    public int CalculateRoundScore()
    {

        throw new NotImplementedException();
    }
}
