using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour, ICamera
{
    [SerializeField] private Vector3 _offset;

    private ICameraTarget _target;

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void SetTarget(ICameraTarget target)
    {
        _target = target;
    }

    private void FollowTarget()
    {
        transform.position = _target.transform.position + _offset;
    }
}

public interface ICamera
{
    void SetTarget(ICameraTarget target);
}

public interface ICameraTarget
{
    Transform transform { get; }
}