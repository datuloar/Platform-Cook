using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, ICook
{
    [SerializeField] private Attack _attack;
    [SerializeField] private AttackZoneTrigger _attackZoneTrigger;
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HumanAnimation _animation;
    [SerializeField] private HumanHealth _health;

    public event Action Dead;

    private void OnEnable()
    {
        _attackZoneTrigger.Stay += OnAttackZoneStay;
    }

    private void OnDisable()
    {
        _attackZoneTrigger.Entered -= OnAttackZoneStay;
    }

    public void Tick(float time)
    {
        _attack.Tick(time);
    }

    public void Damage(float value)
    {
        _health.Damage(value);
    }

    public void Move(Vector3 direction)
    {
        _movement.Move(direction);
        _animation.PlayMovement(true);
    }

    public void StopMove()
    {
        _movement.Stop();
        _animation.PlayMovement(false);
    }

    private void OnAttackZoneStay(IHuman human)
    {
        if (_attack.CanAttack)
        {
            _attack.StartAttack();
            _animation.PlayAttack();
        }
    }
}
