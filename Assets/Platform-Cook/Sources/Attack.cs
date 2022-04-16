﻿using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour, IUpdateLoop
{
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private Weapon _weapon;

    private Timer _timer = new Timer();

    public bool CanAttack { get; private set; } = true;

    private void OnEnable()
    {
        _timer.Completed += OnCooldownEnded;
    }

    private void OnDisable()
    {
        _timer.Completed -= OnCooldownEnded;
    }

    public void Tick(float time)
    {
        _timer.Tick(time);
    }

    public void StartAttack()
    {
        CanAttack = false;
        _weapon.Enable();
        _timer.Start(_cooldown);
    }

    private void OnCooldownEnded()
    {
        CanAttack = true;
        _weapon.Disable();
    }
}
