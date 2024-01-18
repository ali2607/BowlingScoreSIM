using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using BowlingScoreInterface.Models;


namespace BowlingScoreTest;

[TestClass]
public class PlayerTest
{
    [TestMethod]
    public void canTest()
    {
        Assert.AreEqual(1, 1);
    }
    [TestMethod]
    public void TestInitialScore()
    {
        Home home = new Home();
        Player player = new Player("name", home);

        Assert.AreEqual(0, player.TotalScore, "Le score initial d'un nouveau joueur devrait être 0.");
    }
    [TestMethod]
    public void TestNameAssignment()
    {
        Home home = new Home();
        string expectedName = "name";
        Player player = new Player(expectedName, home);

        Assert.AreEqual(expectedName, player.Name, "Le nom du joueur n'est pas correctement assigné.");
    }
    [TestMethod]
    public void TestSingleRollScoreCalculation()
    {
        Home home = new Home();
        Player player = new Player("name", home);
        player.score_1 = 5;
        player.score_2 = 0;

        player.Roll1();
        Assert.AreEqual(5, player.TotalScore, "Le score après un seul lancer n'est pas correct.");
    }
    [TestMethod]
    public void TestSpareScoreCalculation()
    {
        Home home = new Home();
        Player player = new Player("name", home);
        player.Tab2DScores.Add((5, 5, Player.SpecialRoll.Spare));

        player.CalculateRoundScore();
        Assert.AreEqual(10, player.TotalScore, "Le score après un spare n'est pas correct.");
    }
    [TestMethod]
    public void TestStrikeScoreCalculation()
    {
        Home home = new Home();
        Player player = new Player("name", home);
        player.Tab2DScores.Add((10, null, Player.SpecialRoll.Strike));

        player.CalculateRoundScore();
        Assert.AreEqual(10, player.TotalScore, "Le score après un strike n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            player.Tab2DScores.Add((4, 5, Player.SpecialRoll.Default));
            player.CalculateRoundScore();
        }

        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
}