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
    [SerializeField] private List<Rigidbody> _rigidbodies;
    [SerializeField] private List<MeshRenderer> _meshRenderers;

    public event Action Destroyed;

    private void OnValidate()
    {
        _multiplierLabel.text = $"x{_multiplier}";

        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.SetProperty("_Color", _color);
    }

    private void Destroy()
    {
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;

        Destroyed?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICook cook))
        {
            Destroy();
        }
    }
}
