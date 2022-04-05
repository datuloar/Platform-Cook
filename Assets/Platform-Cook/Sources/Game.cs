using System.Collections.Generic;

public class Game : IGame
{
    private readonly IViewport _viewport;
    private readonly IGameEngine _gameEngine;
    private readonly IHouse _house;
    private readonly ICamera _camera;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IHouse house, ICamera camera, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _gameEngine = gameEngine;
        _house = house;
        _camera = camera;
    }

    public void Start()
    {
        _isPlaying = true;

        InitializationComponents();

        _gameEngine.GetInputDevice().Enable();
        _viewport.GetPlayWindow().Open();

        _house.CurrentStorey.HumansDied += OnStoreyCleared;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
        }
    }

    private void InitializationComponents()
    {
        var cook = _house.CreateCook();
        _camera.SetTarget(cook);

        _player = new Player(cook, _gameEngine.GetInputDevice());       
    }

    private void OnStoreyCleared()
    {
        _house.MoveNextStorey(OnNextStoreyMoved);
    }

    private void OnNextStoreyMoved()
    {
        
    }

    private void End()
    {
        _isPlaying = false;
        _gameEngine.GetInputDevice().Disable();
    }
}
