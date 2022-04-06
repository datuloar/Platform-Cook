using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainCamera : MonoBehaviour, ICamera
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _startRotationEuler;
    [SerializeField] private float _moveToStartPointDuration;

    private ICameraTarget _target;
    private bool _canFollowing;

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void MoveToStartPoint(Action moved = null)
    {
        if (_target == null)
            throw new NullReferenceException("Set Camera Target before use it");

        transform.DORotate(_startRotationEuler, _moveToStartPointDuration);
        transform.DOMove(GetCameraTargetPosition(_target), _moveToStartPointDuration)
            .OnComplete(() =>
            {
                _canFollowing = true;
                moved?.Invoke();
            });
    }

    public void SetTarget(ICameraTarget target)
    {
        _target = target;
    }

    private void FollowTarget()
    {
        if (_canFollowing)
            transform.position = GetCameraTargetPosition(_target);
    }

    private Vector3 GetCameraTargetPosition(ICameraTarget target) =>
        target.transform.position + _offset;
}
