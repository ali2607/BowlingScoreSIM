using BowlingScore;

namespace BowlingScoreTest;

[TestClass]
public class Class1Test
{
    [TestMethod]
    public void TestCarre()
    {
        Assert.AreEqual(4, Class1.Carre(2));
    }
}