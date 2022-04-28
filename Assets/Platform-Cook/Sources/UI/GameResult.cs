public class GameResult
{
    public int Currency { get; private set; }
    public int Multiplier { get; private set; }

    public GameResult(int currency, int multiplier)
    {
        Currency = currency;
        Multiplier = multiplier;
    }
}