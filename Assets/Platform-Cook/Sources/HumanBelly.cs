using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBelly : MonoBehaviour, IHumanBelly
{
    private const string GrowKey = "Fatness";

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private int _maxBellySize = 100;

    private int _currentWeight;
    private List<IFood> _food = new List<IFood>();

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

    public void AddFood(IFood food)
    {
        food.Hide();
        _food.Add(food);
        Grow(food.Weight);
    }

    public void Grow(int foodWeight)
    {
        _currentWeight += foodWeight;
        _currentWeight = Mathf.Clamp(_currentWeight, 0, 100);

        if (_currentWeight <= _maxBellySize)
        {
            var blendShapeIndex = _skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(GrowKey);
            _skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, _currentWeight);
        }
    }
}
