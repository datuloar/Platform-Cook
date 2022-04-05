using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private float _speed;
    [SerializeField] private PlatformZoneTrigger _zone;

    private Coroutine _movingToStoreyDock;

    private bool _isCookInsideZone;

    private void OnEnable()
    {
        _zone.Stay += OnZoneStay;
        _zone.Exit += OnZoneExit;
    }

    private void OnDisable()
    {
        _zone.Stay -= OnZoneStay;
        _zone.Exit -= OnZoneExit;
    }

    public void MoveToStoreyDock(Vector3 dockPosition, Action moved = null)
    {
        if (_movingToStoreyDock != null)
            throw new Exception("Platform did't arrive and you demand from it to come to another position");

        _movingToStoreyDock = StartCoroutine(MovingToStoreyDock(dockPosition, moved));
    }

    private IEnumerator MovingToStoreyDock(Vector3 dockPosition, Action moved = null)
    {
        yield return new WaitUntil(() => _isCookInsideZone);

        while (transform.position != dockPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, dockPosition, _speed * Time.deltaTime);

            yield return null;
        }

        _movingToStoreyDock = null;
        moved?.Invoke();
    }

    private void OnZoneExit(ICook _) => _isCookInsideZone = false;

    private void OnZoneStay(ICook _) => _isCookInsideZone = true;
}
