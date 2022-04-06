using System;

public interface ICamera
{
    void MoveToStartPoint(Action moved = null);
    void SetTarget(ICameraTarget target);
}
