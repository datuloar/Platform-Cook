using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : IGame
{
    private readonly IViewport _viewport;
    private readonly IGameEngine _gameEngine;
    private readonly IHouse _house;
    private readonly ICamera _camera;
    private readonly ILevel _level;
    private readonly IMiniGame _miniGame;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IHouse house, ICamera camera, ILevel level, IMiniGame miniGame, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _gameEngine = gameEngine;
        _house = house;
        _camera = camera;
        _level = level;
        _miniGame = miniGame;
    }

    public void Start()
    {
        _isPlaying = true;

        InitializationComponents();

        _gameEngine.GetInputDevice().Disable();

        _viewport.GetStartWindow().StartGameButtonClicked += OnStartGameButtonClicked;
        _house.GetPlatform().FoodEnded += OnPlatformFoodEnded;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
            _house.Tick(time);
        }
    }

    private void InitializationComponents()
    {
        var cook = _house.CreateCook();
        _camera.SetTarget(cook);
        _miniGame.Init(cook, _house.GetPlatform(), _camera);

        _player = new Player(cook, _gameEngine.GetInputDevice());       
    }

    private void Lose()
    {
        End();
        _viewport.GetDefeatWindow().Open();
    }

    private void Restart()
    {
        _level.Restart();
    }

    private void End()
    {
        _isPlaying = false;
        _viewport.GetPlayWindow().Close();
        _gameEngine.GetInputDevice().Disable();
        _house.GetPlatform().FoodEnded -= OnPlatformFoodEnded;
    }

    private void OnCameraMovedOnStartPoint()
    {
        _viewport.GetPlayWindow().Open();
        _gameEngine.GetInputDevice().Enable();
        _house.StartWaves(OnWavesEnded);
    }

    private void OnPlatformFoodEnded()
    {
        Lose();
    }

    private void OnStartGameButtonClicked()
    {
        _camera.MoveToStartPoint(OnCameraMovedOnStartPoint);
        _viewport.GetStartWindow().Close();
    }

    private void OnNextStoreyMoved()
    {
        _house.StartWaves(OnWavesEnded);
    }

    private void OnWavesEnded()
    {
        if (_house.HasNextStorey)
            _house.MoveNextStorey(OnNextStoreyMoved);
        else
            StartMiniGame();
    }

    private void StartMiniGame()
    {
        _viewport.GetPlayWindow().Close();
        _gameEngine.GetInputDevice().Disable();
        _house.GetPlatform().FoodEnded -= OnPlatformFoodEnded;

        _miniGame.StartGame();
    }
}
