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
            _humansCount++;

            var randomConfig = _hungryHumanConfigs[UnityEngine.Random.Range(0, _hungryHumanConfigs.Length)]; 
            var human = CreateHungryHuman(randomConfig);

            human.Dead += OnHumanDead;

            yield return Yielder.WaitForSeconds(_waveSettings.DelayBetweenSpawn);
        }
    }

    private IHungryHuman CreateHungryHuman(HungryHumanConfig config)
    {
        var randomIndexPosition = UnityEngine.Random.Range(0, _waveSettings.HumansPositions.Count);

        IHungryHuman human =  Instantiate(_hungryHumanTemplate, _waveSettings.HumansPositions[randomIndexPosition].position,
            Quaternion.Euler(_waveSettings.HumansPositions[randomIndexPosition].rotation.eulerAngles));

        human.Init(_platform, config);

        return human;
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
