using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDestroyEffect : MonoBehaviour
{
    [SerializeField] private HumanPiece[] _pieces;

    public void Play()
    {
        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].Show();
            _pieces[i].Scatter();
        }
    }
}
