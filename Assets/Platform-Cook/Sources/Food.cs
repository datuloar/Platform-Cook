using DG.Tweening;
using System;
using UnityEngine;

public class Food : MonoBehaviour, IFood
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private float _weight;

    public float Weight => _weight;
    public bool CanTake { get; private set; } = true;
    public ResourceType Type => _type;

    public event Action<IFood> Taken;

    public void Eat()
    {
        Hide();
    }

    public void Take()
    {
        CanTake = false;
        transform.parent = null;

        Taken?.Invoke(this);
    }

    public void Hide(bool animate = true)
    {
        if (animate)
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.OutBack)
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
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
    }
}