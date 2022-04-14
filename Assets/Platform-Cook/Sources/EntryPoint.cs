using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UnityGameEngine _gameEngine;
    [SerializeField] private Viewport _viewport;
    [SerializeField] private House _house;
    [SerializeField] private MainCamera _camera;
    [SerializeField] private BonusGame _bonusGame;
    [SerializeField] private string _nextLevelName;

    private IGame _game;

    private void Awake()
    {
        var level = new Level(_nextLevelName);
        var assetsProvider = new AssetsProvider();
        var humansFactory = new HumansFactory(assetsProvider);

        _house.Init(humansFactory);

        _game = new Game(_viewport, _house, _camera, level, _bonusGame, _gameEngine);
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
