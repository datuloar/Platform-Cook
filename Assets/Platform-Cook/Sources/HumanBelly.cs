using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBelly : MonoBehaviour, IHumanBelly
{
    private const string GrowKey = "Key 1";

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private int _growAddition = 15;
    [SerializeField] private int _maxBellySize = 100;

    private int _currentBellySize;
    private List<IFood> _food = new List<IFood>();

    public void AddFood(IFood food)
    {
        food.Hide();
        _food.Add(food);
        Grow();
    }

    public void Grow()
    {
        _currentBellySize += _growAddition;

        if (_currentBellySize <= _maxBellySize)
        {
            var blendShapeIndex = _skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(GrowKey);
            _skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, 100);
        }
    }
}
