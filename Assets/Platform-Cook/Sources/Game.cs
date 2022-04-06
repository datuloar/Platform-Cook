using System;
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

        _gameEngine.GetInputDevice().Disable();

        _viewport.GetStartWindow().StartGameButtonClicked += OnStartGameButtonClicked;
        _house.GetPlatform().FoodEnded += OnPlatformFoodEnded;
    }

    private void OnPlatformFoodEnded()
    {
        Lose();
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

    private void OnWavesEnded()
    {
        _house.MoveNextStorey(OnNextStoreyMoved);
    }

    private void OnNextStoreyMoved()
    {
        
    }

    private void OnStartGameButtonClicked()
    {
        _camera.MoveToStartPoint(OnCameraMovedOnStartPoint);
        _viewport.GetStartWindow().Close();
    }

    private void OnCameraMovedOnStartPoint()
    {
        _viewport.GetPlayWindow().Open();
        _gameEngine.GetInputDevice().Enable();
        _house.StartWaves(OnWavesEnded);
    }

    private void Lose()
    {
        End();
        _viewport.GetDefeatWindow().Open();
    }

    private void End()
    {
        _isPlaying = false;
        _viewport.GetPlayWindow().Close();
        _gameEngine.GetInputDevice().Disable();
        _house.GetPlatform().FoodEnded -= OnPlatformFoodEnded;
    }
}
