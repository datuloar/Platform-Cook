using System;
using UnityEngine;

public class Room : MonoBehaviour, IRoom
{
    [SerializeField] private Transform _platformDock;

    public Vector3 PlatformDockPoisition => _platformDock.position;

    public event Action Cleared;
}