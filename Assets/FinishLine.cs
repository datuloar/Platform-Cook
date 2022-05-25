using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICook cook))
        {
            var effect = Instantiate(_jumpVFX, cook.transform.position, Quaternion.identity);
            effect.Play();
        }
    }
}