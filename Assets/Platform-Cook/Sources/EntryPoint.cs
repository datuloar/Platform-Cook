using System;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Viewport _viewport;
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private Platform _platform;
    [SerializeField] private UnityGameEngine _gameEngine;

    private IGame _game;

    private void Awake()
    {
        _game = new Game(_viewport, _rooms, _platform, _gameEngine);
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

public interface IRoom
{
    event Action Cleared;
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