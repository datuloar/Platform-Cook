using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreyWaveSettings
{
    [SerializeField] private float _durationSpawn;
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private SpawnPattern[] _spawnPatterns;

    public float DurationSpawn => _durationSpawn;
    public float DelayBetweenSpawn => _delayBetweenSpawn;
    public SpawnPattern[] SpawnPatterns => _spawnPatterns;
}