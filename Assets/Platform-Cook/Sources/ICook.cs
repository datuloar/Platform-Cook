public interface ICook : IHuman, IControlledHuman, IUpdateLoop
{
    HumanAnimation Animation { get; }
    float Weight { get; }

    void Init(ITable table);
    void FreezeMovement();
    void StartFarting();
    void UnfreezeMovement();
    void Eat(Food food);
}
