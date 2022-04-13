using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UnityGameEngine _gameEngine;
    [SerializeField] private Viewport _viewport;
    [SerializeField] private Platform _platform;
    [SerializeField] private List<Storey> _storeys;
    [SerializeField] private MainCamera _camera;
    [SerializeField] private MiniGame _miniGame;
    [SerializeField] private string _nextLevelName;

    private IGame _game;

    private void Awake()
    {
        var assetsProvider = new AssetsProvider();
        var level = new Level(_nextLevelName);
        var humansFactory = new HumansFactory(assetsProvider);
        var house = new House(_storeys, _platform, humansFactory);

        _game = new Game(_viewport, house, _camera, level, _miniGame, _gameEngine);
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
