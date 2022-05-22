public interface ICook : IHuman, IControlledHuman, IUpdateLoop
{
    HumanAnimation Animation { get; }
    float Weight { get; }
    Rotator Rotator { get; }

    void Init(ITable table);
    void StartFarting();
    void Eat(Food food);
}
