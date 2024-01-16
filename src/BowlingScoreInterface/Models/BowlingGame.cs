namespace BowlingScoreInterface.Models;
class BowlingGame
{
    private int pins_1, pins_2;
    
    public void Roll_1(int pins_1)
    {
        this.pins_1 = pins_1;
    }

    public void Roll_2(int pins_2)
    {
        this.pins_2 = pins_2;
    }


}