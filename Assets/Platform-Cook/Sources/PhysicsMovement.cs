using System;
using UnityEngine;

public class PhysicsMovement : Movement
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    public override float Velocity => _rigidbody.velocity.magnitude;

    public override void ChangeSpeed(float speed)
    {
        if (speed < 0)
            throw new ArgumentException();
    }

    public override void Move(Vector3 direction)
    {
        if (CanMovement)
        {
            _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
            MoveRotation(direction);

            IsMoving = true;
        }
    }

    public override void Stop()
    {
        _rigidbody.velocity = Vector3.zero;

        IsMoving = false;
    }

    private void MoveRotation(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        _rigidbody.MoveRotation(GetSmoothRotation(lookRotation));
    }

    private Quaternion GetSmoothRotation(Quaternion targetRotation) =>
        Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

}
