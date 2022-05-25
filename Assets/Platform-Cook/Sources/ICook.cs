public interface ICook : IHuman, IControlledHuman, IUpdateLoop
{
    HumanAnimation Animation { get; }
    float Weight { get; }

    void Init(ITable table);
    void Eat(Food food);
    void TransormateToBall();
    void TransformateToCook();
}
