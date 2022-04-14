using System;

public interface IHouse : IUpdateLoop
{
    bool HasNextStorey { get; }

    IPlatform GetPlatform();
    ICook CreateCook();
    void MoveToStorey(Action moved = null);
    void StartWaves(Action completed = null);
    void StartBasement(Action completed = null);
    void NextStorey();
}
