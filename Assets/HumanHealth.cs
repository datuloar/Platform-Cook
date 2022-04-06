using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHealth : MonoBehaviour
{
    [SerializeField] private float _currentHealthPoints;
    [SerializeField] private float _maxHealthPoints;

    public float CurrentHealthPoints => _currentHealthPoints;

    public event Action HealthPointsEnded;

    private void OnValidate()
    {
        if (_currentHealthPoints > _maxHealthPoints)
            _currentHealthPoints = _maxHealthPoints;
    }

    public void Damage(float value)
    {
        if (value < 0)
            throw new ArgumentException("Value can't be less zero");

        _currentHealthPoints -= value;

        if (IsHealthPointsEnded(_currentHealthPoints))
            HealthPointsEnded?.Invoke();
    }

    public void ResetHealthPoints() => _currentHealthPoints = _maxHealthPoints;

    private bool IsHealthPointsEnded(float currentHealthPoints) => currentHealthPoints <= 0;
}
