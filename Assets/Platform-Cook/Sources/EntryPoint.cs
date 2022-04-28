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
    [SerializeField] private LevelLoader _levelLoaderTemplate;

    private IGame _game;

    private void Awake()
    {
        var levelLoader = FindObjectOfType<LevelLoader>();
        
        if (levelLoader == null)
            levelLoader = Instantiate(_levelLoaderTemplate, transform.position, Quaternion.identity);

        var level = new Level(_nextLevelName, levelLoader);

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

    private void FixedUpdate()
    {
        _game.FixedTick(Time.deltaTime);
    }
}
