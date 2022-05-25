using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;

    private Coroutine _rotating;

    public void StartRotate()
    {
        if (_rotating != null)
            StopCoroutine(_rotating);

        _rotating =  StartCoroutine(Rotating());
    }

    public void StopRotate()
    {
        if (_rotating != null)
            StopCoroutine(_rotating);

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private IEnumerator Rotating()
    {
        while (true)
        {
            transform.Rotate(_axis * _speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator StopingRotate()
    {
        while (_speed > 0)
        {
            _speed -= 0.1f;

            yield return null;
        }

        enabled = false;
    }
}
