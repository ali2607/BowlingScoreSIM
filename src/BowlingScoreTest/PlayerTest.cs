using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using BowlingScoreInterface.Models;


namespace BowlingScoreTest;
//score_1, score2, 10 remplace par nombre de tours, update rounds (parametres), pareil pr roll1
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
        Player player = new Player("name", 10);

        Assert.AreEqual(0, player.TotalScore, "Le score initial d'un nouveau joueur devrait �tre 0.");
    }
    [TestMethod]
    public void TestNameAssignment()
    {
        string expectedName = "name";
        Player player = new Player(expectedName, 10);

        Assert.AreEqual(expectedName, player.Name, "Le nom du joueur n'est pas correctement assign�.");
    }
    [TestMethod]
    public void TestSingleRollScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 5;
        player.Score_2 = 0;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(5, player.TotalScore, "Le score apr�s un seul lancer n'est pas correct.");
    }
    [TestMethod]
    public void TestSpareScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 5;
        player.Score_2 = 5;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(10, player.TotalScore, "Le score apr�s un spare n'est pas correct.");
    }
    [TestMethod]
    public void TestStrikeScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 10;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(10, player.TotalScore, "Le score apr�s un strike n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = 4;
            player.Score_2 = 5;
            player.UpdateRounds(10, i);
            player.Roll1(10, i);
        }

        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare1()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 5;
                player.Score_2 = 5;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 1)
            {
                player.Score_1 = 5;
                player.Score_2 = 5;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 9)
            {
                player.Score_1 = 5;
                player.Score_2 = 5;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 103;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare2()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 7;
                player.Score_2 = 3;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 96;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare3()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 0;
                player.Score_2 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 96;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike1()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 100;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike2()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 1)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 116;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike3()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 118;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike4()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 7)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 139;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrikeAndSpare()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 5;
                player.Score_2 = 5;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 7)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            Debug.WriteLine(player.TotalScore);
        }

        int expectedTotalScore = 122;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithFullStrike()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = 10;
            player.UpdateRounds(10, i);
            player.Roll1(10, i);
        }

        int expectedTotalScore = 270;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame2()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 7;
        player.Score_2 = 3;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 1;
        player.Score_2 = 9;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);


        int expectedTotalScore = 34;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame3()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 7;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 6;
        player.Score_2 = 3;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 0;
        player.Score_2 = 9;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 8);
        player.Roll1(10, 8);

        player.Score_1 = 1;
        player.Score_2 = 8;
        player.UpdateRounds(10, 9);
        player.Roll1(10, 9);


        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame4()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 6;
        player.Score_2 = 3;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 0;
        player.Score_2 = 9;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        int expectedTotalScore = 72;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame5()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 0;
        player.Score_2 = 8;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);


        int expectedTotalScore = 44;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie incompl�te n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame6()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 2;
        player.Score_2 = 3;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 1;
        player.Score_2 = 4;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 0;
        player.Score_2 = 5;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 3;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 1;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 4;
        player.Score_2 = 0;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 1;
        player.Score_2 = 3;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);


        int expectedTotalScore = 31;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie incompl�te de 7 tours n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame7()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 1;
        player.Score_2 = 2;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 2;
        player.Score_2 = 3;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 4;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 4;
        player.Score_2 = 1;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 2;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 3;
        player.Score_2 = 1;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 0;
        player.Score_2 = 5;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        int expectedTotalScore = 33;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie incompl�te de 7 tours n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame8()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 8);
        player.Roll1(10, 8);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 9);
        player.Roll1(10, 9);

        int expectedTotalScore = 90;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame9()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 4;
        player.Score_2 = 2;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 0;
        player.Score_2 = 0;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 8;
        player.Score_2 = 0;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 4;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        int expectedTotalScore = 20;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame10()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 1;
        player.Score_2 = 6;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 5;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 3;
        player.Score_2 = 0;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        int expectedTotalScore = 25;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame11()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 1;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual("1", player.Rounds[0].SecondRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual("9", player.Rounds[0].RoundScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame12()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 2;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual("/", player.Rounds[0].SecondRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame13()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 10;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].SecondRound, "Le score total apr�s une partie compl�te n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le score total apr�s une partie compl�te n'est pas correct.");
    }



}