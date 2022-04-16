using System;

public interface IHouse : IUpdateLoop
{
    bool HasNextStorey { get; }

    IPlatform GetPlatform();
    ICook CreateCook();
    void MoveToStorey(Action moved = null);
    void StartStoreyEvent(Action completed = null);
    void NextStorey();
}
