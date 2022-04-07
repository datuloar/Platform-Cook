using System;
using UnityEngine;

public class Food : MonoBehaviour, IFood
{
    public void Eat()
    {
        gameObject.SetActive(false);
    }
}