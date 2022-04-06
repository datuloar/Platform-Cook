using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float _damageValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHungryHuman human))
        {
            human.Damage(_damageValue);
        }
    }
}
