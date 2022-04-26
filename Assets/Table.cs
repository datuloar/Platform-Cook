using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, ITable
{
    [SerializeField] private FoodStack _stack;
    [SerializeField] private int _maxCapacity;

    private Stack<IFood> _food = new Stack<IFood>();

    public bool HasFood
    {
        get
        {
            if (_food.Count > 0)
            {
                return true;
            }

            FoodEnded?.Invoke();
            return false;
        }
    }

    public int FoodCount => _food.Count;
    public int MaxCapacity => _maxCapacity;

    public event Action FoodCountChanged;
    public event Action FoodEnded;

    public void AddFood(IFood food, bool animate = true)
    {
        _stack.Add(food, animate);
        _food.Push(food);
        _maxCapacity++;

        FoodCountChanged?.Invoke();
    }

    public IFood GetFood()
    {
        var food = _food.Pop();
        _stack.Remove(food);

        FoodCountChanged?.Invoke();

        if (!HasFood)
            FoodEnded?.Invoke();

        return food;
    }
}
