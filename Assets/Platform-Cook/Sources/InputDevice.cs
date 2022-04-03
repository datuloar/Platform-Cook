using UnityEngine;

public abstract class InputDevice : MonoBehaviour, IInputDevice
{
    public abstract Vector2 Axis { get; }
    public abstract void Disable();
    public abstract void Enable();
}
