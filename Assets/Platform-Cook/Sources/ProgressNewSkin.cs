using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressNewSkin : MonoBehaviour
{
    private const float MaxProgress = 100f;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _targetProgress;
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _progressLabel;
    [SerializeField] private float _filingDuration;

    public void Show()
    {
        _canvasGroup.Open();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        _fill.DOFillAmount(_targetProgress / MaxProgress, _filingDuration);

        if (_targetProgress >= MaxProgress) 
            return;

        _progressLabel.text = $"{_targetProgress}%";
    }
}
