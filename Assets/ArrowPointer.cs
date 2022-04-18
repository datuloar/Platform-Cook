using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] private float _hideDuration;
    [SerializeField] private float _showDuration;

    private void Awake()
    {
        Hide(false);
    }

    public void Hide(bool animate = true)
    {
        if (animate)
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, _hideDuration).SetEase(Ease.OutBack)
            .OnComplete(() => gameObject.SetActive(false));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Show(bool animate = true)
    {
        gameObject.SetActive(true);

        if (animate)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _showDuration).SetEase(Ease.OutBack);
        }
    }
}
