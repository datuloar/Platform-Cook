using System;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    [SerializeField] private Coundown _countdown;
    [SerializeField] private List<Food> _food;
    [SerializeField] private int _secondsToCollectFood;

    public event Action TimeOut;

    private void Awake()
    {
        foreach (var food in _food)
            food.Hide(false);
    }

    private void OnEnable()
    {
        _countdown.Completed += OnCountdownCompleted;
    }

    private void OnDisable()
    {
        _countdown.Completed -= OnCountdownCompleted;
    }

    public void StartCountdown()
    {
        foreach (var food in _food)
            food.Show();

        _countdown.StartCountdown(_secondsToCollectFood);
    }

    private void OnCountdownCompleted()
    {
        foreach (var food in _food)
            food.Hide();

        TimeOut?.Invoke();
    }
}