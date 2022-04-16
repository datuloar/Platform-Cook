using System;
using UnityEngine;

public interface IStorey : IUpdateLoop
{
    Vector3 PlatformDockPosition { get; }

    void StartEvent();
}
