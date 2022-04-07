using System;
using UnityEngine;

public interface IHuman : ICameraTarget
{
    event Action Dead;

    void Damage(float value);
}
