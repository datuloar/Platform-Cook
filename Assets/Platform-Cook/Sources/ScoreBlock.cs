using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBlock : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private int _multiplier;
    [SerializeField] private TMP_Text _multiplierLabel;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem[] _confetti;

    public event Action Destroyed;

    private void OnValidate()
    {
        FillSettings();
    }

    private void Awake()
    {
        FillSettings();
    }

    private void FillSettings()
    {
        _multiplierLabel.text = $"x{_multiplier}    x{_multiplier}";
        _meshRenderer.SetProperty("_Color", _color);
    }

    private IEnumerator ChangingColor()
    {
        var time = 0f;
        var speed = 1f;

        while (time < 1)
        {
            time += speed * Time.deltaTime;
            _meshRenderer.SetProperty("_Color", Color.Lerp(_color, Color.white, time));

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICook cook))
        {
            StartCoroutine(ChangingColor());

            foreach (var confetti in _confetti)
                confetti.Play();
        }
    }
}
