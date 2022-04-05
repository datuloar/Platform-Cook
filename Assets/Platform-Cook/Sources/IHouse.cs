using System;

public interface IHouse
{
    IStorey CurrentStorey { get; }

    ICook CreateCook();
    void MoveNextStorey(Action moved = null);
}
