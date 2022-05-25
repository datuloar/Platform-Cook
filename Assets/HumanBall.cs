using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBall : MonoBehaviour, IHumanBall
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private ParticleSystem _trailVFX;

    public void TransformateToBall()
    {
        _trail.enabled = true;
        _collider.enabled = true;
        _trailVFX.gameObject.SetActive(true);

        _rotator.StartRotate();
    }

    public void TransformateToHuman()
    {
        _trail.enabled = false;
        _collider.enabled = false;
        _trailVFX.gameObject.SetActive(false);

        _rotator.StopRotate();
    }
}

public interface IHumanBall
{
    void TransformateToBall();
    void TransformateToHuman();
}