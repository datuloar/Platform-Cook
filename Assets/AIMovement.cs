using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private PhysicsMovement _physicsMovement;

    private Coroutine _moveToPosition;

    public override float Velocity => _physicsMovement.Velocity;

    public override void ChangeSpeed(float speed)
    {
        _physicsMovement.ChangeSpeed(speed);
    }

    public override void Move(Vector3 direction)
    {
        if (_moveToPosition != null)
            StopCoroutine(_moveToPosition);

        _moveToPosition = StartCoroutine(MoveToPosition(direction));
    }

    public override void Stop()
    {
        if (_moveToPosition != null)
            StopCoroutine(_moveToPosition);
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > _stoppingDistance)
        {
            Vector3 directionToPosition = (position - transform.position).normalized;

            _physicsMovement.Move(directionToPosition);

            yield return Yielder.FixedUpdate;
        }
    }
}
