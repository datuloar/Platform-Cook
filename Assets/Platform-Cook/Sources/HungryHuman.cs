using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HungryHuman : MonoBehaviour, IHungryHuman
{
    [SerializeField] private HumanHealth _health;
    [SerializeField] private HumanAnimation _animation;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private HumanSkin _skin;
    [SerializeField] private HumanBelly _belly;

    private float _delayBetweenMeals;
    private int _amountFoodEatenPerDelay;
    private IPlatform _platform;
    private Coroutine _eating;

    public event Action Dead;

    private void OnEnable()
    {
        _health.HealthPointsEnded += OnHealthPointsEnded;
    }

    private void OnDisable()
    {
        _health.HealthPointsEnded -= OnHealthPointsEnded;
    }

    public void Init(IPlatform platform, HungryHumanConfig config)
    {
        _platform = platform;
        _health.Init(config.HealthPoints, config.HealthPoints);

        foreach (var food in config.Foods)
            _belly.AddFood(food);

        _amountFoodEatenPerDelay = config.AmountFoodEatenPerDelay;
        _delayBetweenMeals = config.DelayBetweenMeals;
        _navMeshAgent.speed = config.Speed;
    }

    public void StartMove()
    {
        _skin.Appearance();

        if (_eating != null)
            StopCoroutine(_eating);

        _navMeshAgent.SetDestination(_platform.transform.position);
        _animation.PlayMovement(true);
    }

    public void Damage(float value)
    {
        _health.Damage(value);
    }

    private void StartEating()
    {
        if (_eating != null)
            StopCoroutine(_eating);

        _eating = StartCoroutine(Eating());

        _animation.PlayMovement(false);
        _animation.PlayEating(true);       
    }

    private IEnumerator Eating()
    {
        yield return Yielder.WaitForSeconds(_delayBetweenMeals);

        while (_platform.HasFood)
        {
            for (int i = 0; i < _amountFoodEatenPerDelay; i++)
            {
                IFood food = _platform.GetFood();
                _belly.AddFood(food);
            }

            yield return Yielder.WaitForSeconds(_delayBetweenMeals);
        }
    }

    private void Die()
    {
        Dead?.Invoke();
        Destroy(gameObject);
        _skin.Destroy();
    }

    private void OnHealthPointsEnded()
    {
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformZoneTrigger platformZoneTrigger))
        {
            StartEating();
        }
    }
}
