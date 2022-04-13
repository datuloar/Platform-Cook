using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected bool CanMovement = true;

    public abstract float Velocity { get; }
    public bool IsMoving { get; protected set; }

    public abstract void ChangeSpeed(int speed);

    public abstract void Move(Vector3 direction);

    public abstract void Stop();

    public void Freeze() => CanMovement = false;

    public void Unfreeze() => CanMovement = true;
}