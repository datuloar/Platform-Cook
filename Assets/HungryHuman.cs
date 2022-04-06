using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HungryHuman : MonoBehaviour, IHungryHuman
{
    [SerializeField] private float _delayBetweenMeals = 1f;
    [SerializeField] private int _amountFoodEatenPerDelay = 1;
    [SerializeField] private HumanHealth _health;
    [SerializeField] private HumanAnimation _animation;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private HumanSkin _skin;

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

    public void Init(IPlatform platform)
    {
        _platform = platform;
    }

    public void StartMove()
    {
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

    private void OnHealthPointsEnded()
    {
        Die();
    }

    private void Die()
    {
        Dead?.Invoke();
        _skin.Destroy();
        enabled = false;
    }

    private IEnumerator Eating()
    {
        yield return Yielder.WaitForSeconds(_delayBetweenMeals);

        while (_platform.HasFood)
        {
            _platform.EatFood(_amountFoodEatenPerDelay);

            yield return Yielder.WaitForSeconds(_delayBetweenMeals);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformZoneTrigger platformZoneTrigger))
        {
            StartEating();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlatformZoneTrigger platformZoneTrigger))
        {
            StartMove();
        }
    }
}
