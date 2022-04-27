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
        _rigidbody.AddForce(-transform.forward * _knockbackForce, ForceMode.Impulse);
    }
}
