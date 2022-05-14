using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryHumansStorey : Storey
{
    [SerializeField] private StoreyWaveSettings _waveSettings;
    [SerializeField] private HungryHumanConfig[] _hungryHumanConfigs;
    [SerializeField] private HungryHuman _hungryHumanTemplate;

    private IPlatform _platform;
    private Coroutine _spawningHungryHumans;
    private Timer _timer = new Timer();
    private bool _isWavesEnded;
    private int _humansCount;

    public override event Action Completed;

    private void OnEnable()
    {
        _timer.Completed += OnWavesEnded;
    }

    private void OnDisable()
    {
        _timer.Completed -= OnWavesEnded;
    }

    public override void Init(IPlatform platform)
    {
        _platform = platform;
    }

    public override void Tick(float time)
    {
        _timer.Tick(time);
    }

    public override void StartEvent()
    {
        if (_spawningHungryHumans != null)
            StopCoroutine(_spawningHungryHumans);

        _spawningHungryHumans = StartCoroutine(SpawningHungryHumans());

        _timer.Start(_waveSettings.DurationSpawn);
    }

    private IEnumerator SpawningHungryHumans()
    {
        while (true)
        {
            CreateHungryHumans();

            yield return Yielder.WaitForSeconds(_waveSettings.DelayBetweenSpawn);
        }
    }

    private void CreateHungryHumans()
    {
        SpawnPattern spawnPattern = _waveSettings.SpawnPatterns[UnityEngine.Random.Range(0, _waveSettings.SpawnPatterns.Length)];

        for (int i = 0; i < spawnPattern.SpawnPoints.Length; i++)
        {
            HungryHumanConfig randomConfig = _hungryHumanConfigs[UnityEngine.Random.Range(0, _hungryHumanConfigs.Length)];

            IHungryHuman human = Instantiate(_hungryHumanTemplate, spawnPattern.SpawnPoints[i].position,
                  Quaternion.Euler(spawnPattern.SpawnPoints[i].rotation.eulerAngles));
            human.Init(_platform, randomConfig);
            human.Dead += OnHumanDead;

            _humansCount++;
        }
    }

    private void OnHumanDead()
    {
        _humansCount--;

        if (_humansCount <= 0 && _isWavesEnded)
            Completed?.Invoke();
    }

    private void OnWavesEnded()
    {
        if (_spawningHungryHumans != null)
            StopCoroutine(_spawningHungryHumans);

        _isWavesEnded = true;

        if (_humansCount <= 0)
            Completed?.Invoke();
    }
}
