using System.Numerics;

namespace BowlingScoreInterface.Models;

public class HomeModel
{
    public List<string> Players { get; set; } = new List<string>();
    public int NumberOfRounds { get; set; } = 1;

}
