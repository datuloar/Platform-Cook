using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _dollarsCountLabel;
    [SerializeField] private float _scaleDuration;

    private void OnEnable()
    {
        _wallet.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _wallet.BalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged()
    {
        _dollarsCountLabel.text = _wallet.DollarsCount.ToString();
        transform.DOScale(1.1f, _scaleDuration)
            .OnComplete(() => transform.DOScale(1.0f, _scaleDuration));
    }
}
