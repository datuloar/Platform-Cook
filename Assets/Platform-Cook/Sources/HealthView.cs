using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private HumanHealth _health;
    [SerializeField] private Image _fill;

    private void OnEnable()
    {
        _health.HealthPointsChanged += OnHealthPointsChanged;
    }

    private void OnDisable()
    {
        _health.HealthPointsChanged -= OnHealthPointsChanged;
    }

    private void OnHealthPointsChanged()
    {
        Show();
        _fill.fillAmount = _health.CurrentHealthPoints / _health.MaxHealthPoints;
    }

    private void Show()
    {
        _canvasGroup.DOFade(1, 0.2f);
    }

    private void Hide()
    {
        _canvasGroup.DOFade(0, 0.5f);
    }
}
