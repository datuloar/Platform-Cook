using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStack : ResourcesStack
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int _height;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private Color _debugColor;

    private Stack[,] _stackMatrix;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = _debugColor;

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                var position = transform.TransformPoint(new Vector3(x * _xOffset, 0, y * _yOffset));
                Gizmos.DrawSphere(position, 0.05f);
            }
        }
    }
#endif

    private void Awake()
    {
        _stackMatrix = new Stack[_size.x, _size.y];

        for (int y = 0; y < _size.y; y++)
            for (int x = 0; x < _size.x; x++)
                _stackMatrix[x, y] = new Stack();
    }

    public override Vector3 CalculateEndRotation(Transform container, Transform stackable) =>
        Vector3.up * Random.Range(-90, 90);

    public override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var x = GetRandomIndex(_size.x);
        var y = GetRandomIndex(_size.y);

        _stackMatrix[x, y].Add(stackable);

        var count = _stackMatrix[x, y].Count < _height ? _stackMatrix[x, y].Count : _height;

        var horizontalOffset = new Vector3(x * _xOffset, 0, y * _yOffset) + new Vector3(Random.Range(0, 0.1f), 0, Random.Range(0, 0.1f));

        return Vector3.up * count * 0.15f + horizontalOffset;
    }

    public override void OnRemove(Transform removeTransform)
    {
        foreach (var stack in _stackMatrix)
            if (stack.Remove(removeTransform))
                break;
    }

    public override void Sort(List<Transform> unsortedTransforms) { }

    private int GetRandomIndex(int maxIndex)
    {
        var half = Mathf.Ceil((float)maxIndex / 2f);

        int sum = 0;

        for (int i = 1; i <= half; i++)
            sum += i;

        var rate = 1f / sum;
        var random = Random.Range(0f, 1f);

        var sumRate = rate;
        int index;

        for (index = 0; index < half; index++)
        {
            if (sumRate >= random)
                break;

            sumRate += rate * (index + 2);
        }

        if (Random.Range(0f, 1f) > 0.5f)
            return maxIndex - index - 1;
        else
            return index;
    }

    private class Stack
    {
        private List<Transform> _stack;

        public Stack()
        {
            _stack = new List<Transform>();
        }

        public int Count => _stack.Count;

        public void Add(Transform stackable)
        {
            _stack.Add(stackable);
        }

        public bool Remove(Transform stackable)
        {
            return _stack.Remove(stackable);
        }
    }
}
