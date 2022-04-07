public interface ICook : IHuman, IControlledHuman, IUpdateLoop
{
    HumanAnimation Animation { get; }

    void FreezeMovement();
    void StartFarting();
    void UnfreezeMovement();
    void Eat(IFood food);
}
