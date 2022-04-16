using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatProgress : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Table _table;

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
        _fill.fillAmount = _table.FoodCount / (float)_table.MaxCapacity;
    }
}
