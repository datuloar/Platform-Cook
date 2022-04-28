using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _damageValue = 200f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IHungryHuman hungryHuman))
        {
            hungryHuman.Damage(_damageValue);
        }
    }
}
