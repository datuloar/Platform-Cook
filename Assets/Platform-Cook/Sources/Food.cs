using DG.Tweening;
using System;
using UnityEngine;

public class Food : MonoBehaviour, IResource
{
    [SerializeField] private float _height = 0.10f;
    [SerializeField] private ResourceType _type;
    [SerializeField] private int _weight;
    [SerializeField] private bool _canTake = true;
    [SerializeField] private TrailRenderer _trailRenderer;

    public int Weight => _weight;
    public bool CanTake => _canTake;
    public ResourceType Type => _type;

    public float Height => _height;

    public event Action<Food> Taken;

    public void Eat()
    {
        Hide();
    }

    public void Take()
    {
        _canTake = false;
        transform.parent = null;

        Taken?.Invoke(this);
    }

    public void Drop()
    {
        _canTake = true;
        transform.parent = null;
    }

    public void Hide(bool animate = true)
    {
        _trailRenderer.enabled = false;

        if (animate)
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, 0.4f)
            .OnComplete(() => gameObject.SetActive(false));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Show(bool animate = true)
    {
        _trailRenderer.enabled = true;
        gameObject.SetActive(true);

        if (animate)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
    }
}