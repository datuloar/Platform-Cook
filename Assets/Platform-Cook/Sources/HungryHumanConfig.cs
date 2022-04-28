using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hungry Human Config", menuName = "HungryHuman / Config", order = 51)]
public class HungryHumanConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private int _healthPoints;
    [SerializeField] private float _delayBetweenMeals;
    [SerializeField] private int _amountFoodEatenPerDelay;
    [SerializeField] private int _foodDropCount;
    [SerializeField] private List<Food> _foods;

    public float Speed => _speed;
    public int HealthPoints => _healthPoints;
    public float DelayBetweenMeals => _delayBetweenMeals;
    public int AmountFoodEatenPerDelay => _amountFoodEatenPerDelay;
    public int FoodDropCount => _foodDropCount;
    public IReadOnlyList<Food> Foods => _foods;
}
