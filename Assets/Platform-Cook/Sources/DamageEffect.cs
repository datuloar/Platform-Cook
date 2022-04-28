using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private ParticleSystem _damageVFX;
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private FloatSetting _jumpPower = new FloatSetting(true, 0.5f);
    [SerializeField] private float _duration = 0.7f;

    private void OnEnable()
    {
        _health.HealthPointsChanged += OnDamaged;
    }

    private void OnDisable()
    {
        _health.HealthPointsChanged -= OnDamaged;
    }

    private void OnDamaged()
    {
        _damageVFX.Play();
        _rigidbody.DOJump(_hitPoint.position, _jumpPower.Value, 1, _duration);
    }
}
