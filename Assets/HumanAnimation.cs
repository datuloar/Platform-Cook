using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimation : MonoBehaviour
{
    private const string Speed = nameof(Speed);

    [SerializeField] private Animator _animator;

    public void PlayMovement(float velocity)
    {
        _animator.SetFloat(Speed, velocity);
    }
}
