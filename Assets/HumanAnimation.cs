using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimation : MonoBehaviour
{
    private const string Eating = nameof(Eating);
    private const string Movement = nameof(Movement);
    private const string KnifeAttack = nameof(KnifeAttack);

    [SerializeField] private Animator _animator;

    public void PlayMovement(bool isMove)
    {
        _animator.SetBool(Movement, isMove);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(KnifeAttack);
    }

    public void PlayEating(bool isEating)
    {
        _animator.SetBool(Eating, isEating);
    }
}
