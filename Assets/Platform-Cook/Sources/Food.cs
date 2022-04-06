using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public void Eat()
    {
        gameObject.SetActive(false);
    }
}