using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainCamera : MonoBehaviour, ICamera
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _zoomOffset;
    [SerializeField] private float _moveToStartPointDuration;
    [SerializeField] private float _moveToZoomPointDuration;

    private ICameraTarget _target;
    private bool _canFollowing;

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void ZoomToTarget(Action moved = null)
    {
        if (_target == null)
            throw new NullReferenceException("Set Camera Target before use it");

        _canFollowing = false;

        transform.DOMove(GetCameraTargetPosition(_target, _zoomOffset), _moveToZoomPointDuration)
            .OnComplete(() =>
            {
                moved?.Invoke();
            });
    }    

    public void MoveToStartPoint(Action moved = null)
    {
        if (_target == null)
            throw new NullReferenceException("Set Camera Target before use it");

        transform.DOMove(GetCameraTargetPosition(_target, _offset), _moveToStartPointDuration)
            .OnComplete(() =>
            {
                _canFollowing = true;
                moved?.Invoke();
            });
    }

    public void SetTarget(ICameraTarget target) => _target = target;

    public void StopFollowing() => _canFollowing = false;

    private void FollowTarget()
    {
        if (_canFollowing)
            transform.position = GetCameraTargetPosition(_target, _offset);
    }

    private Vector3 GetCameraTargetPosition(ICameraTarget target, Vector3 offset) =>
        target.transform.position + offset;
}
