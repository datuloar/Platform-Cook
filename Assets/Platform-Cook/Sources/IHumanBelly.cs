public interface IHumanBelly
{
    int Weight { get; }
    int FoodCount { get; }

    void AddFood(Food food);
    Food RemoveFood();
}
