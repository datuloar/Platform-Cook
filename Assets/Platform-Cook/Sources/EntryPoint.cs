using System;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UnityGameEngine _gameEngine;
    [SerializeField] private Viewport _viewport;
    [SerializeField] private Platform _platform;
    [SerializeField] private List<Storey> _storeys;

    private IGame _game;

    private void Awake()
    {
        var assetsProvider = new AssetsProvider();
        var humansFactory = new HumansFactory(assetsProvider);
        var house = new House(_storeys, _platform, humansFactory);

        _game = new Game(_viewport, house, _gameEngine);
    }

    private void Start()
    {
        _game.Start();
    }

    private void Update()
    {
        _game.Tick(Time.deltaTime);
    }
}

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
    public IStorey CurrentStorey => _storeys[_currentStoreyIndex];

    public void MoveNextStorey(Action moved = null)
    {
        _currentStoreyIndex++;
        _platform.MoveToStoreyDock(CurrentStorey.PlatformDockPosition, moved);
    }

    public ICook CreateCook() => _humansFactory.CreateCook(Vector3.zero);
}

public interface IHouse
{
    IStorey CurrentStorey { get; }

    ICook CreateCook();
    void MoveNextStorey(Action moved = null);
}

public interface IStorey
{
    Vector3 PlatformDockPosition { get; }

    event Action HumansDied;

    void Init(IHumansFactory humansFactory);
    void StartWaves(ITable table);
}

public interface ICook : IHuman
{
    void Attack();
}

public interface IHuman : IMovement
{
    event Action Dead; 
}

public interface IMovement
{
    void Move(Vector3 direction);
    void StopMove();
}