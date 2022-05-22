using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _damageValue = 200f;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private MeshRenderer _meshRenderer;

    private bool _canExplode;

    public void AllowExplosion()
    {
        _canExplode = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICook cook))
        {
            if (_canExplode)
            {
                _explosion.Explode();
                _meshRenderer.enabled = false;
            }
        }

        if (collision.gameObject.TryGetComponent(out IHungryHuman hungryHuman))
        {
            hungryHuman.Damage(_damageValue);
        }
    }
}
