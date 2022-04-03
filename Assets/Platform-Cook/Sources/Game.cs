using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : IGame
{
    private readonly IViewport _viewport;
    private readonly IPlatform _platform;
    private readonly IGameEngine _gameEngine;
    private readonly IReadOnlyList<IRoom> _rooms;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IReadOnlyList<IRoom> rooms, IPlatform platform, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _rooms = rooms;
        _gameEngine = gameEngine;
        _platform = platform;
    }

    public void Start()
    {
        _viewport.GetPlayWindow().Open();

        _isPlaying = true;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
        }
    }

    private void End()
    {
        _isPlaying = false;
    }  
}


public interface IPlatform
{
    void Move(Vector3 targetPosition, Action arrived = null);
}

public class Room : MonoBehaviour, IRoom
{
    [SerializeField] private Transform _platformDock;

    public Vector3 PlatformDockPoisition => _platformDock.position;

    public event Action Cleared;
}