using DG.Tweening;
using UnityEngine;

public class HumanPiece : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _scatterDuration;
    [SerializeField] private float _hideDuration;
    [SerializeField] private float _jumpHeight;

    public void Scatter()
    {
        RandomRotation();
        MoveToTarget().OnComplete(() => Destroy());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private Tween RandomRotation() =>
        transform.DORotate(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)),
            _scatterDuration);

    private Tween MoveToTarget() =>
        transform.DOLocalJump(_targetPoint.position, _jumpHeight, 1, _scatterDuration);

    private void Destroy()
    {
        transform.DOScale(Vector3.zero, _hideDuration)
            .OnComplete(() => Destroy(gameObject));
    }
}