using System;
using UnityEngine;

public interface IHuman
{
    Transform transform { get; }

    event Action Dead;

    void Damage(float value);
}
