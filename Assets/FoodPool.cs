using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPool : MonoBehaviour
{
    [SerializeField] private List<FoodPlace> _foodPlaces;
    [SerializeField] private List<Food> _templates;

    private List<Food> _food = new List<Food>();

    public IReadOnlyList<IFood> Food => _food;

    private void Awake()
    {
        SpawnRandomFood();
    }

    private void SpawnRandomFood()
    {
        for (int i = 0; i < _foodPlaces.Count; i++)
        {
            Food food = _templates[Random.Range(0, _templates.Count)];

            SpawnFood(food, _foodPlaces[i]);
        }
    }

    private void SpawnFood(Food template, FoodPlace place, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnFood(template, place);
        }
    }

    private void SpawnFood(Food template, FoodPlace place)
    {
        Food food = Instantiate(template, place.transform);
        food.Hide(false);

        _food.Add(food);
    }
}
