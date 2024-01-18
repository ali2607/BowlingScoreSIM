using BowlingScoreInterface.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoreInterface.Tests
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaultValues()
        {
            var home = new Home();

            Assert.IsNotNull(home.Players);
            Assert.AreEqual(0, home.Players.Count);
            Assert.AreEqual(1, home.NumberOfRounds);
            Assert.AreEqual(10, home.NumberOfPins);
            Assert.AreEqual(10, home.MaxPlayers);
        }

        [TestMethod]
        public void SettingProperties_UpdatesValuesCorrectly()
        {
            var home = new Home();

            home.Players = new List<string> { "Alice", "Bob" };
            home.NumberOfRounds = 5;
            home.NumberOfPins = 8;

            Assert.AreEqual(2, home.Players.Count);
            Assert.IsTrue(home.Players.Contains("Alice"));
            Assert.IsTrue(home.Players.Contains("Bob"));
            Assert.AreEqual(5, home.NumberOfRounds);
            Assert.AreEqual(8, home.NumberOfPins);
        }
    }
}
