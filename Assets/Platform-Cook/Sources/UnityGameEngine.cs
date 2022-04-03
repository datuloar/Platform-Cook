using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityGameEngine : MonoBehaviour, IGameEngine
{
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private InputDevice _inputDevice;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }

    public IInputDevice GetInputDevice() => _inputDevice;
}
