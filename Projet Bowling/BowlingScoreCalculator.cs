using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        GameManager gm1 = new GameManager(2,1);
        Console.WriteLine($"New game started with {gm1.Players.Count} players and {gm1.NumberOfRounds} rounds.");
        GameManager gm2 = new GameManager(2,9);
        Console.WriteLine($"New game started with {gm2.Players.Count} players and {gm2.NumberOfRounds} rounds.");
        foreach (Player player in gm1.Players)
        {
            Console.WriteLine(player.Name);
        }
        gm1.StartGame();
        
    }
}
class GameManager
{
    private List<Player> players;
    private int numberOfRounds;
    public List<Player> Players
    {
        get { return players; }
    }
    //Getter - Setter for number of round
    public int NumberOfRounds
    {
        get { return numberOfRounds; }
        set
        {
            // You can add validation logic if needed
            if (value > 0)
            {
                numberOfRounds = value;
            }
            else
            {
                Console.WriteLine("Number of rounds must be greater than 0.");
            }
        }
    }

    //Constructor
    public GameManager(int numberOfPlayers, int rounds = 10)
    {
        numberOfRounds = rounds;
        InitializePlayers(numberOfPlayers);
        Console.WriteLine($"New game started with {numberOfPlayers} players and {numberOfRounds} rounds.");
    }

    private void InitializePlayers(int numberOfPlayers)
    {
        players = new List<Player>();
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            players.Add(new Player($"Player {i}"));
        }
    }


    public void StartGame()
    {
        Console.WriteLine("Game started!");
        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"\nRound {round}:");

            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name}'s turn:");
                player.Roll(); // You should implement the Roll method in the Player class
            }

            DisplayScores();
        }

        Console.WriteLine("\nGame Over!");
        DisplayFinalScores();
    }

    private void DisplayScores()
    {
        Console.WriteLine("Current Scores:");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name}: {player.CurrentGame.CalculateScore()}");
        }
        Console.WriteLine();
    }

    private void DisplayFinalScores()
    {
        Console.WriteLine("Final Scores:");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name}: {player.CurrentGame.CalculateScore()}");
        }
    }
}

class Player
{
    public string Name { get; private set; }
    public BowlingGame CurrentGame { get; private set; }

    public Player(string name)
    {
        Name = name;
        CurrentGame = new BowlingGame();
    }

    public void Roll()
    {
        int score;
        while (!int.TryParse(Console.ReadLine(), out score) || score < 0 || score > 10)
        {
            Console.WriteLine("Invalid input. Please enter a score between 0 and 10.");
            Console.Write("Enter the score for this roll: ");
        }

        CurrentGame.Roll(score);
    }
}
    class BowlingGame
{
    // Add properties and methods to handle game-specific logic

    public void Roll(int pins)
    {
        
    }

    public int CalculateScore()
    {
        // Implement the logic to calculate the current score
        return 0; // Placeholder, replace with actual implementation
    }
}
