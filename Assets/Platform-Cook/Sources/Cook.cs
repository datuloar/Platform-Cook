using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, ICook
{
    [SerializeField] private Attack _attack;
    [SerializeField] private HumanSkin _skin;
    [SerializeField] private AttackZoneTrigger _attackZoneTrigger;
    [SerializeField] private Movement _movement;
    [SerializeField] private HumanAnimation _animation;
    [SerializeField] private Health _health;
    [SerializeField] private HumanBelly _belly;
    [SerializeField] private HumanBall _humanBall;

    private ITable _table;

    public HumanAnimation Animation => _animation;
    public float Weight => _belly.Weight;

    public event Action Dead;

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

    public void ChangeSuit(Suit suit)
    {
        _skin.ChangeSuit(suit);
    }

    public void Eat(Food food)
    {
        _belly.AddFood(food);
    }

    private void OnAttackZoneStay(IHuman human)
    {
        if (_attack.CanAttack)
        {
            _attack.StartAttack();
            _animation.PlayAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            if (food.CanTake)
            {
                food.Take();
                _table.AddFood(food);
                Taptic.Selection();
            }
        }
    }

    public void TransormateToBall()
    {
        _humanBall.TransformateToBall();
    }

    public void TransformateToCook()
    {
        _humanBall.TransformateToHuman();
        _animation.PlayJump(false);
    }
}
