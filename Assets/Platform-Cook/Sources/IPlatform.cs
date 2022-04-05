using System;
using UnityEngine;

public interface IPlatform
{
    void Move(Vector3 targetPosition, Action arrived = null);
}
