using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, ICook
{
    [SerializeField] private Attack _attack;
    [SerializeField] private AttackZoneTrigger _attackZoneTrigger;
    [SerializeField] private Movement _movement;
    [SerializeField] private HumanAnimation _animation;
    [SerializeField] private HumanHealth _health;
    [SerializeField] private HumanBelly _belly;
    [SerializeField] private ParticleSystem _fartTrailVfx;

    private ITable _table;

    public event Action Dead;

    public IHumanBelly Belly => _belly;
    public HumanAnimation Animation => _animation;

    private void OnEnable()
    {
        _attackZoneTrigger.Stay += OnAttackZoneStay;
    }

    private void OnDisable()
    {
        _attackZoneTrigger.Entered -= OnAttackZoneStay;
    }

    public void Init(ITable table)
    {
        _table = table;
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

    public void StartFarting()
    {
        _fartTrailVfx.Play();
    }

    public void FreezeMovement() => _movement.Freeze();

    public void UnfreezeMovement() => _movement.Unfreeze();

    public void Eat(IFood food)
    {
        _belly.AddFood(food);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IFood food))
        {
            if (food.CanTake)
            {
                food.Take();
                _table.AddFood(food);
            }
        }
    }
}
