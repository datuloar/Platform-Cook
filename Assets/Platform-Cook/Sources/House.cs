using System;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IHouse
{
    [SerializeField] private List<Storey> _storeys;
    [SerializeField] private Platform _platform;
    [SerializeField] private Cook _cookTemplate;

    private int _currentStoreyIndex;

    public bool HasNextStorey => _currentStoreyIndex + 1 < _storeys.Count;

    private void Awake()
    {
        foreach (var storey in _storeys)
            storey.Init(_platform);
    }

    public void Tick(float time)
    {
        foreach (var storey in _storeys)
            storey.Tick(time);
    }

    public void NextStorey()
    {
        _currentStoreyIndex++;
    }

    public void MoveToStorey(Action moved = null)
    {
        _platform.MoveToStoreyDock(_storeys[_currentStoreyIndex].PlatformDockPosition, moved);
    }

    public void StartStoreyEvent(Action ended = null)
    {
        _storeys[_currentStoreyIndex].StartEvent();
        _storeys[_currentStoreyIndex].Completed += (() => ended?.Invoke());
    }

    public ICook CreateCook() =>
        Instantiate(_cookTemplate, _platform.transform.position, Quaternion.Euler(0, -180,0));

    public IPlatform GetPlatform() => _platform;
}
