using System;
using System.Collections;
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

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private float _speed;

    private Coroutine _movingToTargetPosition;

    public void Move(Vector3 targetPosition, Action arrived = null)
    {
        if (_movingToTargetPosition != null)
            throw new Exception("Platform did not arrive and you demand from it to come to another position");

        _movingToTargetPosition = StartCoroutine(MovingToTarget(targetPosition, arrived));
    }

    private IEnumerator MovingToTarget(Vector3 targetPosition, Action arrived = null)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            yield return null;
        }

        _movingToTargetPosition = null;
        arrived?.Invoke();
    }
}

public class Player : IUpdateLoop
{
    private readonly ICook _controlledCharacter;
    private readonly IInputDevice _inputDevice;

    private Vector3 _moveDirection;

    public Player(ICook controlledCharacter, IInputDevice inputDevice)
    {
        _controlledCharacter = controlledCharacter;
        _inputDevice = inputDevice;

        _moveDirection = new Vector3();
    }

    public void Tick(float time)
    {
        if (_inputDevice.Axis.magnitude > Constants.Math.Epsilon)
        {
            _moveDirection.x = _inputDevice.Axis.x;
            _moveDirection.z = _inputDevice.Axis.y;
            _moveDirection.Normalize();

            _controlledCharacter.Move(_moveDirection);
        }
        else
        {
            _controlledCharacter.StopMove();
        }
    }
}

public interface IRoom
{
    event Action Cleared;
}

public interface ICook : ICharacter
{
    void Attack();
}

public interface ICharacter : IMovement
{
    event Action Dead; 
}

public interface IMovement
{
    void Move(Vector3 direction);
    void StopMove();
}