using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FoodProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Table _table;
    [SerializeField] private float _fillDuration = 0.4f;

    private void OnEnable()
    {
        _table.FoodCountChanged += OnFoodCountChanged;
    }

    private void OnDisable()
    {
        _table.FoodCountChanged -= OnFoodCountChanged;
    }

    private void OnFoodCountChanged()
    {
        _fill.DOFillAmount(_table.FoodCount / (float)_table.MaxCapacity, _fillDuration);
    }
}
