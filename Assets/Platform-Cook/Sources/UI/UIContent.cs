﻿using DG.Tweening;
using System;
using UnityEngine;

public class UIContent : MonoBehaviour
{
    [SerializeField] private float _showDuration;
    [SerializeField] private float _hideDuration;
    [SerializeField] private Ease _showEase = Ease.OutBack;
    [SerializeField] private Ease _hideEase;

    public void Show(Action completed = null)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, _showDuration).SetEase(_showEase)
            .OnComplete(() => completed?.Invoke());
    }

    public void Hide()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(1, _hideDuration).SetEase(_hideEase);
    }
}