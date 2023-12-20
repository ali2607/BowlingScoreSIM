using BowlingScore;


namespace BowlingScoreTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual(4, Class1.Carre(2));
    }
}