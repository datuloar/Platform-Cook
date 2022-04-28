using System;
using UnityEngine;

public class Claws : Weapon
{
    [SerializeField] private float _damageValue;
    [SerializeField] private Collider _collider;

    public override event Action Hit;

    public override void Disable()
    {
        _collider.enabled = false;
    }

    public override void Enable()
    {
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHungryHuman human))
        {
            human.Damage(_damageValue);
            Hit?.Invoke();
        }
    }
}