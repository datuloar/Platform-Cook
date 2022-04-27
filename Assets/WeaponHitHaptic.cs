using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitHaptic : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _weapon.Hit += OnHit;
    }

    private void OnDisable()
    {
        _weapon.Hit -= OnHit;
    }

    private void OnHit()
    {
        Taptic.Medium();
    }
}
