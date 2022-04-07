using System;

public interface IHouse : IUpdateLoop
{
    bool HasNextStorey { get; }

    IPlatform GetPlatform();
    ICook CreateCook();
    void MoveNextStorey(Action moved = null);
    void StartWaves(Action ended = null);
}
