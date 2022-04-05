using System;
using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private float _speed;

    private Coroutine _movingToTargetPosition;

    public void Move(Vector3 targetPosition, Action arrived = null)
    {
        if (_movingToTargetPosition != null)
            throw new Exception("Platform did not arrive and you demand from it to come to another position");

        _movingToTargetPosition = StartCoroutine(MovingToTarget(targetPosition, arrived));
    }

    private IEnumerator MovingToTarget(Vector3 targetPosition, Action arrived = null)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            yield return null;
        }

        _movingToTargetPosition = null;
        arrived?.Invoke();
    }
}
