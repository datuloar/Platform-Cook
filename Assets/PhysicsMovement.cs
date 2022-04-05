using System;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    public float Velocity => _rigidbody.velocity.magnitude;
    public bool IsMoving { get; private set; }

    public void ChangeSpeed(int speed)
    {
        if (speed < 0)
            throw new ArgumentException();
    }

    public void Move(Vector3 direction)
    {
        MoveRotation(direction);

        _rigidbody.velocity = direction * _speed;

        IsMoving = true;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;

        IsMoving = false;
    }
    private void MoveRotation(Vector3 direction)
    {
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        _rigidbody.MoveRotation(GetSmoothRotation(rotation));
    }

    private Quaternion GetSmoothRotation(Quaternion targetRotation) =>
        Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
}

