using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorey : Storey
{
    [SerializeField] private List<FoodPlace> _foodPlaces;
    [SerializeField] private List<Food> _templates;
    [SerializeField] private Coundown _countdown;
    [SerializeField] private int _secondsToCollectFood;

    private IPlatform _platform;
    private List<IFood> _food = new List<IFood>();

    public override event Action Completed;

    private void Awake()
    {
        SpawnRandomFood();
    }

    private void OnEnable()
    {
        _countdown.Completed += OnCountdownCompleted;

        foreach (var food in _food)
            food.Taken += OnFoodTaken;
    }

    private void OnDisable()
    {
        _countdown.Completed -= OnCountdownCompleted;

        foreach (var food in _food)
            food.Taken -= OnFoodTaken;
    }

    public override void Init(IPlatform platform, IHumansFactory humansFactory)
    {
        _platform = platform;
    }

    public override void Tick(float time) { }

    public override void StartEvent()
    {
        foreach (var food in _food)
            food.Show();

        _countdown.StartCountdown(_secondsToCollectFood);
    }

    private void SpawnRandomFood()
    {
        for (int i = 0; i < _foodPlaces.Count; i++)
        {
            Food food = _templates[UnityEngine.Random.Range(0, _templates.Count)];

            SpawnFood(food, _foodPlaces[i]);
        }
    }

    private void SpawnFood(Food template, FoodPlace place)
    {
        Food food = Instantiate(template, place.transform);
        food.Hide(false);

        _food.Add(food);
    }

    private void OnFoodTaken(IFood food)
    {
        _platform.Table.AddFood(food);
        _food.Remove(food);
    }

    private void OnCountdownCompleted()
    {
        foreach (var food in _food)
            food.Hide();

        Completed?.Invoke();
    }
}