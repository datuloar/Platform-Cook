using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : InputDevice
{
    [SerializeField] private Joystick _joystick;

    public override Vector2 Axis => SimpleInputAxis();

    public override void Disable()
    {
        _joystick.gameObject.SetActive(false);
        enabled = false;
    }

    public override void Enable()
    {
        _joystick.gameObject.SetActive(true);
        enabled = true;
    }

    private Vector2 SimpleInputAxis() =>
        new Vector2(_joystick.xAxis.value, _joystick.yAxis.value);
}
