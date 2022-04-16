using System;
using UnityEngine;

[Serializable]
public class Setting<T>
{
    [SerializeField] private bool _enabled;
    [SerializeField] private T _value;

    public Setting(bool enabled, T value)
    {
        _enabled = enabled;
        _value = value;
    }

    public bool Enabled => _enabled;
    public T Value => _value;
}
