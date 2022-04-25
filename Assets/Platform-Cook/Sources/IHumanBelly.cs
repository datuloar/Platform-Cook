public interface IHumanBelly
{
    int Weight { get; }

    void AddFood(IFood food);
}
