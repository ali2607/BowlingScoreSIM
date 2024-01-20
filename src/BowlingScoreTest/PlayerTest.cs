using System.Diagnostics;
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

        player.UpdateRounds();
        player.Roll1();
        Assert.AreEqual(5, player.TotalScore, "Le score après un seul lancer n'est pas correct.");
    }
    [TestMethod]
    public void TestSpareScoreCalculation()
    {
        Home home = new Home();
        Player player = new Player("name", home);
        player.score_1 = 5;
        player.score_2 = 5;

        player.UpdateRounds();
        player.Roll1();
        Assert.AreEqual(10, player.TotalScore, "Le score après un spare n'est pas correct.");
    }
    [TestMethod]
    public void TestStrikeScoreCalculation()
    {
        Home home = new Home();
        Player player = new Player("name", home);
        player.score_1 = 10;

        player.UpdateRounds();
        player.Roll1();
        Assert.AreEqual(10, player.TotalScore, "Le score après un strike n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            player.score_1 = 4;
            player.score_2 = 5;
            player.UpdateRounds();
            player.Roll1();
        }

        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare1()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 0)
            {
                player.score_1 = 5;
                player.score_2 = 5;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 1)
            {
                player.score_1 = 5;
                player.score_2 = 5;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 9)
            {
                player.score_1 = 5;
                player.score_2 = 5;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 5;
                player.score_2 = 4;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 103;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare2()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 0)
            {
                player.score_1 = 7;
                player.score_2 = 3;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 5;
                player.score_2 = 4;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 96;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare3()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 0)
            {
                player.score_1 = 0;
                player.score_2 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 5;
                player.score_2 = 4;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 96;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike1()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 0)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 5;
                player.score_2 = 4;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 100;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike2()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 0)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 1)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 5;
                player.score_2 = 4;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 116;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike3()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 5)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 6)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 7;
                player.score_2 = 2;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 118;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike4()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 5)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 6)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 7)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 7;
                player.score_2 = 2;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 139;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrikeAndSpare()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            if (i == 5)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 6)
            {
                player.score_1 = 5;
                player.score_2 = 5;
                player.UpdateRounds();
                player.Roll1();
            }
            else if (i == 7)
            {
                player.score_1 = 10;
                player.UpdateRounds();
                player.Roll1();
            }
            else
            {
                player.score_1 = 7;
                player.score_2 = 2;
                player.UpdateRounds();
                player.Roll1();
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 122;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithFullStrike()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        for (int i = 0; i < home.NumberOfRounds; i++)
        {
            player.score_1 = 10;
            player.UpdateRounds();
            player.Roll1();
        }

        int expectedTotalScore = 270;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame2()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 7;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 5;
        player.score_2 = 4;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 1;
        player.score_2 = 9;
        player.UpdateRounds();
        player.Roll1();


        int expectedTotalScore = 34;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame3()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 4;
        player.score_2 = 5;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 6;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 2;
        player.score_2 = 7;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 6;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 5;
        player.score_2 = 4;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 9;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 1;
        player.score_2 = 8;
        player.UpdateRounds();
        player.Roll1();


        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame4()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 6;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 5;
        player.score_2 = 4;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 9;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 6;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 4;
        player.score_2 = 5;
        player.UpdateRounds();
        player.Roll1();

        int expectedTotalScore = 72;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame5()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 4;
        player.score_2 = 5;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 6;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 8;
        player.UpdateRounds();
        player.Roll1();


        int expectedTotalScore = 44;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame6()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 2;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 1;
        player.score_2 = 4;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 5;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 2;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 4;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 1;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();


        int expectedTotalScore = 31;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète de 7 tours n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame7()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 1;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 2;
        player.score_2 = 3;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 4;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 4;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 2;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 5;
        player.UpdateRounds();
        player.Roll1();

        int expectedTotalScore = 33;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète de 7 tours n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame8()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 1;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 7;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 9;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame9()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 4;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 0;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 8;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 4;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        int expectedTotalScore = 20;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame10()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 8;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 1;
        player.score_2 = 6;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 5;
        player.score_2 = 2;
        player.UpdateRounds();
        player.Roll1();

        player.score_1 = 3;
        player.score_2 = 0;
        player.UpdateRounds();
        player.Roll1();

        int expectedTotalScore = 25;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame11()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 8;
        player.score_2 = 1;

        player.UpdateRounds();
        player.Roll1();

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("1", player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("9", player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame12()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 8;
        player.score_2 = 2;

        player.UpdateRounds();
        player.Roll1();

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("/", player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(" ", player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame13()
    {
        Home home = new Home() { NumberOfRounds = 10 };
        Player player = new Player("name", home);

        player.score_1 = 10;
        player.UpdateRounds();
        player.Roll1();

        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(" ", player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(" ", player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }



}