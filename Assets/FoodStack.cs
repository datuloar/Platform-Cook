using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStack : ResourcesStack
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3Int _countStack;

    private Vector3Int _currentCountStack = new Vector3Int(1, 1, 1);
    private int _topStack = 0;

    public override Vector3 CalculateAddEndPosition(Transform container, IResource resource)
    {
        Vector3 foodScale = container.InverseTransformVector(resource.transform.lossyScale);

        var endPosition = foodScale / 2;
        endPosition.x += (foodScale.x + _offset.x) * (_countStack.x / 2 - _currentCountStack.x);
        endPosition.y += (resource.Height) * (_currentCountStack.y - 1);
        endPosition.z += (foodScale.z + _offset.z) * (_countStack.z / 2 - _currentCountStack.z);

        _currentCountStack.x += 1;

        if (_currentCountStack.x > _countStack.x)
        {
            _currentCountStack.x = 1;
            _currentCountStack.z += 1;
        }

        if (_currentCountStack.z > _countStack.z)
        {
            _currentCountStack.z = 1;

            if (_currentCountStack.y < _countStack.y)
                _currentCountStack.y += 1;
            else
                _topStack += 1;
        }

        return endPosition;
    }

    public override void OnRemove(Transform removeTransform)
    {
        RecalculatePosition();
    }

    private void RecalculatePosition()
    {
        _currentCountStack.x -= 1;

        if (_currentCountStack.x < 1)
        {
            _currentCountStack.x = _countStack.x;
            _currentCountStack.z -= 1;
        }

        if (_currentCountStack.z < 1)
        {
            _currentCountStack.z = _countStack.z;

            if (_topStack == 0)
            {
                if (_currentCountStack.y > 1)
                    _currentCountStack.y -= 1;
            }
            else
            {
                _topStack -= 1;
            }
        }
    }

    public override Vector3 CalculateEndRotation(Transform container, IResource resource) =>
        Vector3.up * Random.Range(-20, 20);

    public override void Sort(List<Transform> unsortedTransforms) { }
}
