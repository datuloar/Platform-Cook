using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreyWaveSettings
{
    [SerializeField] private float _durationSpawn;
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private List<Transform> _humansPositions;

    public float DurationSpawn => _durationSpawn;
    public float DelayBetweenSpawn => _delayBetweenSpawn;
    public int StartHumansCount => _humansPositions.Count;
    public IReadOnlyList<Transform> HumansPositions => _humansPositions;
}