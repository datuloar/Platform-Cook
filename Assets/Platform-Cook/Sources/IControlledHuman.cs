using UnityEngine;

public interface IControlledHuman
{
    void Move(Vector3 direction);
    void StopMove();
}