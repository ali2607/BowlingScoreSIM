using System.Threading.Channels;
using BowlingScoreInterface;
using BowlingScoreInterface.Controllers;


namespace BowlingScoreTest;

[TestClass]
public class BowlingGameTest
{
    [TestMethod]
    public void CanTest()
    {
        Assert.AreEqual(1, 1);
    }
    //[TestMethod]
    //public void CanCalculateRoundScore()
    //{
    //    for (int i = 0; i < 11; i++)
    //    {
    //        for (int j = 0; j < (11 - i); i++)
    //        {
    //            BowlingGame bowlingGame = new BowlingGame(i, j);
    //            Assert.AreEqual(bowlingGame.CalculateRoundScore(), i+j);
    //        }
    //    }
    //}
}

/*
class BowlingGame {
private:
    int pins_1, pins_2;

public:
    void Roll_1(int pins_1);
    void Roll_2(int pins_2);
    int CalculateRoundScore();
};

class Player {
private:
    std::string Name;
    BowlingGame CurrentGame;
    int score_1, score_2;

public:
    Player(const std::string& name);

    void Roll_1();
    void Roll_2();
};

class GameManager {
private:
    std::vector<Player> players;
    int numberOfRounds;
    std::vector<std::vector<int>> scores;

public:
    std::vector<Player>& GetPlayers();
    int GetNumberOfRounds() const;
    void SetNumberOfRounds(int rounds);
    
    GameManager(int numberOfPlayers, int rounds = 10);

    void InitializePlayers(int numberOfPlayers);
    void InitializeScores(int numberOfPlayers);

    void StartGame();
    void DisplayScores();
    void DisplayFinalScores();
};


*/