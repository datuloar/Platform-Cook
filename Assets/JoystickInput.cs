using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : InputDevice
{
    [SerializeField] private Joystick _joystick;

    private bool _isEnabled;

    public override Vector2 Axis
    {
        get
        {
            if (_isEnabled)
                return SimpleInputAxis();
            else
                return Vector3.zero;
        }
    }

    public override void Disable()
    {
        _joystick.gameObject.SetActive(false);
        _joystick.enabled = false;
        _isEnabled = false;
    }

    public override void Enable()
    {
        _joystick.gameObject.SetActive(true);
        _joystick.enabled = true;
        _isEnabled = true;
    }

    private Vector2 SimpleInputAxis() =>
        new Vector2(_joystick.xAxis.value, _joystick.yAxis.value);
}
