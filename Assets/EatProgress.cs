using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatProgress : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Platform _platform;

    private void OnEnable()
    {
        _platform.FoodCountChanged += OnFoodCountChanged;
    }

    private void OnDisable()
    {
        _platform.FoodCountChanged -= OnFoodCountChanged;
    }

    private void OnFoodCountChanged()
    {
        _fill.fillAmount = _platform.FoodCount / (float)_platform.MaxFoodCount;
    }
}
