using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Health _health;
    [SerializeField] private Image _barFront;
    [SerializeField] private Image _barBack;
    [SerializeField] private float _secondsToHide;
    [SerializeField] private float _hideDuration;
    [SerializeField] private float _showDuration;
    [SerializeField] private float _barBackFillDuration;

    private bool _isHidden;
    private Timer _timer = new Timer();

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _isHidden = true;
    }

    private void OnEnable()
    {
        _health.HealthPointsChanged += OnHealthPointsChanged;
        _timer.Completed += Hide;
    }

    private void OnDisable()
    {
        _health.HealthPointsChanged -= OnHealthPointsChanged;
        _timer.Completed -= Hide;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void RenderHealthPoints(float currentHealthPoints, float maxHealthPoints)
    {
        float difference = currentHealthPoints / maxHealthPoints;

        _barFront.fillAmount = difference;
        _barBack.DOFillAmount(difference, _barBackFillDuration);
    }

    private void OnHealthPointsChanged()
    {
        if (_isHidden)
            Show();

        RenderHealthPoints(_health.CurrentHealthPoints, _health.MaxHealthPoints);
        _timer.Start(_secondsToHide);
    }

    private void Hide()
    {
        _canvasGroup.DOFade(0, _hideDuration);
        _isHidden = true;
    }

    private void Show()
    {
        _canvasGroup.DOFade(1, _showDuration);
        _isHidden = false;
    }
}