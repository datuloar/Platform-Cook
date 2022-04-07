using System;
using UnityEngine;

public interface IPlatform
{
    Transform transform { get; }
    bool HasFood { get; }
    int FoodCount { get; }

    event Action FoodEnded;
    event Action FoodCountChanged;

    void MoveToStoreyDock(Vector3 dockPosition, Action moved = null);
    IFood GetFood();
}
