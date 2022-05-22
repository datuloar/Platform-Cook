using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(_axis * _speed * Time.deltaTime);
    }

    public void Rotate()
    {
        enabled = true;
    }

    public void StopRotate()
    {     
        transform.rotation = Quaternion.Euler(Vector3.zero);
        enabled = false;
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
