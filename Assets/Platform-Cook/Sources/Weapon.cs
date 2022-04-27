using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract event Action Hit;

    public abstract void Enable();

    public abstract void Disable();
}