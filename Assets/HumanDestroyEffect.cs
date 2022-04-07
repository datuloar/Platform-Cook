using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDestroyEffect : MonoBehaviour
{
    [SerializeField] private HumanPiece[] _pieces;
    [SerializeField] private ParticleSystem _vfx;

    public void Play()
    {
        _vfx.transform.parent = null;
        _vfx.Play();

        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].Show();
            _pieces[i].Scatter();
        }
    }
}
