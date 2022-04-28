using System;

[Serializable]
public class FloatSetting : Setting<float>
{
    public FloatSetting(bool enabled, float value)
    : base(enabled, value) { }
}
