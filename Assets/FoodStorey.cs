using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorey : Storey
{
    [SerializeField] private Coundown _countdown;
    [SerializeField] private FoodPool _pool;
    [SerializeField] private int _secondsToCollectFood;

    public override event Action Completed;

    private void OnEnable()
    {
        _countdown.Completed += OnCountdownCompleted;
    }

    private void OnDisable()
    {
        _countdown.Completed -= OnCountdownCompleted;
    }

    public override void Init(IPlatform platform, IHumansFactory humansFactory) { }

    public override void Tick(float time) { }

    public override void StartEvent()
    {
        foreach (var food in _pool.Food)
            food.Show();

        _countdown.StartCountdown(_secondsToCollectFood);
    }

    private void OnCountdownCompleted()
    {
        foreach (var food in _pool.Food)
            food.Hide();

        Completed?.Invoke();
    }

}