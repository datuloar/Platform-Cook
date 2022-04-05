using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, ICook
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HumanAnimation _animation;

    public event Action Dead;

    public void Move(Vector3 direction)
    {
        _movement.Move(direction);
        _animation.PlayMovement(_movement.Velocity);
    }

    public void StopMove()
    {
        _movement.Stop();
        _animation.PlayMovement(_movement.Velocity);
    }

    private void Attack()
    {

    }
}
