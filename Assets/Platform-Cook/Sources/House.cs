using System;
using System.Collections.Generic;
using UnityEngine;

public class House : IHouse
{
    private readonly IReadOnlyList<IStorey> _storeys;
    private readonly IPlatform _platform;
    private readonly IHumansFactory _humansFactory;

    private int _currentStoreyIndex;

    public House(IReadOnlyList<IStorey> storeys, IPlatform platform, IHumansFactory humansFactory)
    {
        _storeys = storeys;
        _platform = platform;
        _humansFactory = humansFactory;

        foreach (var storey in storeys)
            storey.Init(humansFactory);
    }

    public bool HasNextStorey => _currentStoreyIndex + 1 <= _storeys.Count;

    public void MoveNextStorey(Action moved = null)
    {
        _currentStoreyIndex++;
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
