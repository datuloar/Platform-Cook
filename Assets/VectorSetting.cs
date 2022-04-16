using System;
using UnityEngine;

[Serializable]
public class VectorSetting : Setting<Vector3>
{
    public VectorSetting(bool enabled, Vector3 value)
    : base(enabled, value) { }
}