using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyLabel;
    [SerializeField] private GameObject _currencyUITemplate;
    [SerializeField] private Transform _target;
    [SerializeField] private Wallet _wallet;

    [Space]
    [SerializeField] private int _currencyToSpawn;

    [Space]
    [SerializeField] [Range(0.5f, 2f)] private float _firstAnimDuration;
    [SerializeField] private Ease _firstEaseType;

    [Space]
    [SerializeField] [Range(0.5f, 2f)] private float _secondAnimDuration;
    [SerializeField] private Ease _secondEaseType;

    [Space]
    [SerializeField] private float _spread;
    [SerializeField] private float _increasedScale;

    private Queue<GameObject> _currencyQueue = new Queue<GameObject>();
    private int _currency;

    private void Awake()
    {
        CreatePool();
    }

    public void AddToWallet(int currency, Action completed = null)
    {
        for (int i = 0; i < _currencyToSpawn; i++)
        {
            if (_currencyQueue.Count > 0)
            {
                GameObject template = _currencyQueue.Dequeue();
                template.SetActive(true);

                var randomXspread = UnityEngine.Random.Range(-_spread, _spread);
                var randomYspread = UnityEngine.Random.Range(-_spread, _spread);
                var randomOffsetSpread = new Vector3(randomXspread, randomYspread, 0);

                template.transform.DOMove(randomOffsetSpread, _firstAnimDuration).SetRelative(true).SetEase(_firstEaseType);
                template.transform.DOScale(_increasedScale, _firstAnimDuration).SetRelative(true).SetEase(_firstEaseType)
                    .OnComplete(() =>
                    {
                        MoveToTarget(template, (() =>
                        {
                            if (_currencyQueue.Count == _currencyToSpawn)
                            {
                                completed?.Invoke();
                                _wallet.Add(currency);
                            }
                        }));
                    });
            }
        }
    }

    private void CreatePool()
    {
        GameObject _currency;

        for (int i = 0; i < _currencyToSpawn; i++)
        {
            _currency = Instantiate(_currencyUITemplate, transform);
            _currency.transform.parent = transform;
            _currency.SetActive(false);
            _currencyQueue.Enqueue(_currency);
        }
    }

    private void MoveToTarget(GameObject template, Action moved = null)
    {
        template.transform.DOMove(_target.position, _secondAnimDuration).SetEase(_secondEaseType)
            .OnComplete(() =>
            {
                template.SetActive(false);
                _currencyQueue.Enqueue(template);

                moved?.Invoke();
            });
    }
}
