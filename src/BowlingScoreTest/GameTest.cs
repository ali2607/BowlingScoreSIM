using BowlingScoreInterface.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoreInterface.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Constructor_WithHomeParameter_SetsPropertiesCorrectly()
        {
            var home = new Home { Players = new List<string> { "Alice", "Bob" }, NumberOfRounds = 10 };

            var game = new Game(home);

            Assert.AreEqual(2, game.Players.Count);
            Assert.IsTrue(game.Players.Contains("Alice"));
            Assert.IsTrue(game.Players.Contains("Bob"));
            Assert.AreEqual(10, game.NumberOfRounds);
        }

        [TestMethod]
        public void DefaultConstructor_SetsDefaultValues()
        {

            var game = new Game();

            Assert.AreEqual(1, game.Players.Count);
            Assert.AreEqual("Player 1", game.Players.First());
            Assert.AreEqual(1, game.NumberOfRounds);
        }
    }
}
