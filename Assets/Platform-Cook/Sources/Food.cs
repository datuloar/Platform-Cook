using System;
using UnityEngine;

public class Food : MonoBehaviour, IFood
{
    [SerializeField] private float _weight;

    public float Weight => _weight;

    public void Eat()
    {
        gameObject.SetActive(false);
    }
}