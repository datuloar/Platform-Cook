using System;
using UnityEngine;

public abstract class Storey : MonoBehaviour, IStorey, IUpdateLoop
{
    [SerializeField] private Transform _platformDock;

    public Vector3 PlatformDockPosition => _platformDock.position;

    public abstract event Action Completed;

    public abstract void Init(IPlatform platform);
    public abstract void StartEvent();
    public abstract void Tick(float time);
}