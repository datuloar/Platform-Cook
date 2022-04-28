using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private ParticleSystem _walkVFX;

    private bool _isPlaying;

    private void Update()
    {
        if (_movement.IsMoving)
        {
            if (!_isPlaying)
            {
                _walkVFX.Play();
                _isPlaying = true;
            }
        }
        else
        {
            if (_isPlaying)
            {
                _walkVFX.Stop();
                _isPlaying = false;
            }
        }
    }
}
