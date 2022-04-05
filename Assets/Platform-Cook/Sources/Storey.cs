using System;
using UnityEngine;

public class Storey : MonoBehaviour, IStorey
{
    [SerializeField] private Transform _platformDock;
    [SerializeField] private StoreyWaveSettings _waveSettings;

    private IHumansFactory _humansFactory;

    public Vector3 PlatformDockPosition => _platformDock.position;

    public event Action HumansDied;

    public void Init(IHumansFactory humansFactory)
    {
        _humansFactory = humansFactory;
    }

    public void StartWaves(ITable table)
    {
        for (int i = 0; i < _waveSettings.StartHumansCount; i++)
        {
            var human = _humansFactory.CreateHungryHuman(_waveSettings.HumansPositions[i].position);
            human.Init(table);
        }
    }
}

public interface ITable
{
    
}
