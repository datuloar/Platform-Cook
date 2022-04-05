using System;
using UnityEngine;

public interface IPlatform
{
    void MoveToStoreyDock(Vector3 dockPosition, Action moved = null);
}
