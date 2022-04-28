using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBelly : MonoBehaviour, IHumanBelly
{
    private const string GrowKey = "Fatness";

    [SerializeField] private HumanSkin _skin;
    [SerializeField] private int _maxBellySize = 100;

    private int _currentWeight;
    private Stack<Food> _food = new Stack<Food>();

    public int FoodCount => _food.Count;

    public int Weight
    {
        get
        {
            int weight = 0;

            foreach (var food in _food)
                weight += food.Weight;

            return weight;
        }
    }

    public void AddFood(Food food)
    {
        food.Hide();
        _food.Push(food);
        Grow(food.Weight);
    }

    public void Grow(int foodWeight)
    {
        _currentWeight += foodWeight;
        _currentWeight = Mathf.Clamp(_currentWeight, 0, 100);

        if (_currentWeight <= _maxBellySize)
        {
            ChangeBellySize(_currentWeight);
        }
    }

    public Food RemoveFood()
    {
        var food = _food.Pop();
        food.Show();
        food.transform.parent = null;
        _currentWeight -= food.Weight;
        ChangeBellySize(_currentWeight);

        return food;
    }

    private void ChangeBellySize(int size)
    {
        var blendShapeIndex = _skin.MeshRenderer.sharedMesh.GetBlendShapeIndex(GrowKey);
        _skin.MeshRenderer.SetBlendShapeWeight(blendShapeIndex, size);
    }
}
