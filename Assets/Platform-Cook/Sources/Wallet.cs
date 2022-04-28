using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _dollarsCount;

    public int MaxDollars { get; private set; } = 9999;
    public int DollarsCount => _dollarsCount;

    public event Action BalanceChanged;

    private void Start()
    {
        _dollarsCount = PlayerPrefs.GetInt(SaveKey.Currency.ToString());

        Add(_dollarsCount);
    }

    public void Add(int amount)
    {
        if (amount + 1 > MaxDollars)
            _dollarsCount = MaxDollars;

        _dollarsCount += amount;

        BalanceChanged?.Invoke();
    }

    public void Spend(int amount)
    {
        if (amount > DollarsCount)
            throw new InvalidOperationException();

        _dollarsCount -= amount;

        BalanceChanged?.Invoke();
    }
}
