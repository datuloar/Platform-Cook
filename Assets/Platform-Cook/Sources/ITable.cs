using System;
using UnityEngine;

public interface ITable
{
    Transform transform { get; }
    bool HasFood { get; }
    int FoodCount { get; }
    int MaxCapacity { get; }
    event Action FoodCountChanged;
    event Action FoodEnded;

    void AddFood(Food food, bool animate = true);
    Food GetFood();
}