using UnityEngine;

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
