using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputDevice
{
    public Vector2 Axis { get; }

    void Disable();
    void Enable();
}
