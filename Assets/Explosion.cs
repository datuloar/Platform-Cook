using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Transform _parts;
    [SerializeField] private float _hideDuration = 1f;
    [SerializeField] private float _waitHide = 4f;
    [SerializeField] private Vector3 _force;

    private Timer _timer = new Timer();

    private void OnEnable()
    {
        _timer.Completed += OnTimerCompleted;
    }

    private void OnDisable()
    {
        _timer.Completed -= OnTimerCompleted;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void Explode()
    {
        transform.parent = null;
        _parts.gameObject.SetActive(true);

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(_force, ForceMode.VelocityChange);
        }

        _timer.Start(_waitHide);
    }

    private void HideParts()
    {
        _parts.DOScale(0, _hideDuration)
            .OnComplete(() => Destroy(gameObject));
    }

    private void OnTimerCompleted()
    {
        HideParts();
    }
}
