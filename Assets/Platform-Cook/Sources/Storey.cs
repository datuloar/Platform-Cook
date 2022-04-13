using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storey : MonoBehaviour, IStorey
{
    [SerializeField] private Transform _platformDock;
    [SerializeField] private StoreyWaveSettings _waveSettings;
    [SerializeField] private HungryHumanConfig[] _hungryHumanConfigs;

    private IHumansFactory _humansFactory;
    private Coroutine _spawningHungryHumans;
    private Timer _timer;
    private bool _isWavesEnded;
    private int _humansCount;

    public Vector3 PlatformDockPosition => _platformDock.position;

    public event Action HumansDied;

    private void OnDisable()
    {
        _timer.Completed -= OnWavesEnded;
    }

    public void Init(IHumansFactory humansFactory)
    {
        _humansFactory = humansFactory;

        _timer = new Timer();
        _timer.Completed += OnWavesEnded;
    }

    public void Tick(float time)
    {
        _timer.Tick(time);
    }

    public void StartWaves(IPlatform platform)
    {
        if (_spawningHungryHumans != null)
            StopCoroutine(_spawningHungryHumans);

        _spawningHungryHumans = StartCoroutine(SpawningHungryHumans(platform));

        _timer.Start(_waveSettings.DurationSpawn);
    }

    private IEnumerator SpawningHungryHumans(IPlatform target)
    {
        while (true)
        {
            _humansCount++;

            var randomConfig = _hungryHumanConfigs[UnityEngine.Random.Range(0, _hungryHumanConfigs.Length)]; 
            var human = CreateHungryHuman(target, randomConfig);

            human.Dead += OnHumanDead;

            yield return Yielder.WaitForSeconds(_waveSettings.DelayBetweenSpawn);
        }
    }

    private IHungryHuman CreateHungryHuman(IPlatform target, HungryHumanConfig config)
    {
        var randomIndexPosition = UnityEngine.Random.Range(0, _waveSettings.HumansPositions.Count);

        var human = _humansFactory.CreateHungryHuman(_waveSettings.HumansPositions[randomIndexPosition].position,
            _waveSettings.HumansPositions[randomIndexPosition].rotation.eulerAngles);

        human.Init(target, config);
        human.StartMove();

        return human;
    }

    private void OnHumanDead()
    {
        _humansCount--;

        if (_humansCount <= 0 && _isWavesEnded)
            HumansDied?.Invoke();
    }

    private void OnWavesEnded()
    {
        if (_spawningHungryHumans != null)
            StopCoroutine(_spawningHungryHumans);

        _isWavesEnded = true;

        if (_humansCount <= 0)
            HumansDied?.Invoke();
    }
}