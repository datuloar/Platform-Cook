using System;
using UnityEngine;

public interface IPlatform
{
    Transform transform { get; }
    ITable Table { get; }

    void MoveToStoreyDock(Vector3 dockPosition, Action moved = null);
}
