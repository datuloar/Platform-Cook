public interface ICook : IHuman, IControlledHuman, IUpdateLoop
{
    HumanAnimation Animation { get; }

    void Init(ITable table);
    void FreezeMovement();
    void StartFarting();
    void UnfreezeMovement();
    void Eat(IFood food);
}
