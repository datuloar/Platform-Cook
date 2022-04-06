using System;
using UnityEngine;

public interface IStorey
{
    Vector3 PlatformDockPosition { get; }

    event Action HumansDied;

    void Init(IHumansFactory humansFactory);
    void StartWaves(IPlatform platform);
}
