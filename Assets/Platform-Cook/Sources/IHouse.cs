using System;

public interface IHouse
{
    IPlatform GetPlatform();
    ICook CreateCook();
    void MoveNextStorey(Action moved = null);
    void StartWaves(Action ended = null);
}
