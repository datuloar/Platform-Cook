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
    private readonly IBonusGame _bonusGame;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IHouse house, ICamera camera, ILevel level, IBonusGame bonusGame, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _gameEngine = gameEngine;
        _house = house;
        _camera = camera;
        _level = level;
        _bonusGame = bonusGame;

        _viewport.GetStartWindow().StartGameButtonClicked += OnStartGameButtonClicked;
        _viewport.GetVictoryWindow().NextButtonClicked += Restart;
        _viewport.GetDefeatWindow().RestartButtonClicked += Restart;
        _viewport.GetPlayWindow().RestartGameButtonClicked += Restart;
    }

    ~Game()
    {
        _viewport.GetStartWindow().StartGameButtonClicked -= OnStartGameButtonClicked;
        _viewport.GetVictoryWindow().NextButtonClicked -= Restart;
        _viewport.GetDefeatWindow().RestartButtonClicked -= Restart;
        _viewport.GetPlayWindow().RestartGameButtonClicked -= Restart;
    }

    public void Start()
    {
        _isPlaying = true;

        InitializationComponents();

        _viewport.GetStartWindow().Open();
        _gameEngine.GetInputDevice().Disable();

        _house.GetPlatform().Table.FoodEnded += OnPlatformFoodEnded;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _house.Tick(time);
        }
    }

    public void FixedTick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
        }
    }

    private void InitializationComponents()
    {
        IPlatform platform = _house.GetPlatform();
        ICook cook = _house.CreateCook();

        cook.Init(platform.Table);
        _camera.SetTarget(cook);
        _bonusGame.Init(cook, platform, _camera);

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

        _house.GetPlatform().Table.FoodEnded -= OnPlatformFoodEnded;
    }


    private void OnPlatformFoodEnded() => Lose();

    private void OnStartGameButtonClicked()
    {
        _camera.MoveToStartPoint(OnCameraMovedOnStartPoint);
        _viewport.GetStartWindow().Close();
    }

    private void OnCameraMovedOnStartPoint()
    {
        _viewport.GetPlayWindow().Open();
        _gameEngine.GetInputDevice().Enable();
        _house.StartStoreyEvent(OnStoreyCompleted);
    }

    private void OnStoreyMoved() =>
        _house.StartStoreyEvent(OnStoreyCompleted);

    private void OnStoreyCompleted()
    {
        if (_house.HasNextStorey)
        {
            _house.NextStorey();
            _house.MoveToStorey(OnStoreyMoved);
        }
        else
        {
            StartBonusGame();
        }
    }

    private void StartBonusGame()
    {
        _viewport.GetPlayWindow().Close();
        _gameEngine.GetInputDevice().Disable();
        _bonusGame.StartGame();

        _bonusGame.GameOver += OnGameOver;
        _house.GetPlatform().Table.FoodEnded -= OnPlatformFoodEnded;
    }

    private void OnGameOver()
    {
        _isPlaying = false;

        _viewport.GetVictoryWindow().Open();
    }

}
