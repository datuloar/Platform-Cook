using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : MonoBehaviour
{
    public float CurrentHealthPoints { get; private set; }
    public float MaxHealthPoints { get; private set; }

    public event Action HealthPointsChanged;
    public event Action HealthPointsEnded;

    public void Init(float currentHealthPoints, float maxHealthPoints)
    {
        CurrentHealthPoints = currentHealthPoints;
        MaxHealthPoints = maxHealthPoints;

        if (CurrentHealthPoints > MaxHealthPoints)
            CurrentHealthPoints = MaxHealthPoints;
    }

    public void Damage(float value)
    {
        if (value < 0)
            throw new ArgumentException("Value can't be less zero");

        CurrentHealthPoints -= value;

        HealthPointsChanged?.Invoke();

        if (IsHealthPointsEnded(CurrentHealthPoints))
            HealthPointsEnded?.Invoke();
    }

    public void ResetHealthPoints() => CurrentHealthPoints = MaxHealthPoints;

    private bool IsHealthPointsEnded(float currentHealthPoints) => currentHealthPoints <= 0;
}
