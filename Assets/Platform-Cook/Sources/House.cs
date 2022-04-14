using System;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IHouse
{
    [SerializeField] private Basement _basement;
    [SerializeField] private List<Storey> _storeys;
    [SerializeField] private Platform _platform;

    private IHumansFactory _humansFactory;

    private int _currentStoreyIndex;

    public bool HasNextStorey => _currentStoreyIndex + 1 < _storeys.Count;

    public void Init(IHumansFactory humansFactory)
    {
        _humansFactory = humansFactory;

        foreach (var storey in _storeys)
            storey.Init(humansFactory);
    }

    public void Tick(float time)
    {
        foreach (var storey in _storeys)
            storey.Tick(time);
    }

    public void StartBasement(Action completed = null)
    {
        _basement.StartCountdown();
        _basement.TimeOut += (() => completed?.Invoke());
    }

    public void NextStorey()
    {
        _currentStoreyIndex++;
    }

    public void MoveToStorey(Action moved = null)
    {
        _platform.MoveToStoreyDock(_storeys[_currentStoreyIndex].PlatformDockPosition, moved);
    }

    public void StartWaves(Action ended = null)
    {
        _storeys[_currentStoreyIndex].StartWaves(_platform);
        _storeys[_currentStoreyIndex].HumansDied += (() => ended?.Invoke());
    }

    public ICook CreateCook() => _humansFactory.CreateCook(Vector3.zero, new Vector3(0, 180));

    public IPlatform GetPlatform() => _platform;
}
