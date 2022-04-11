using DG.Tweening;
using UnityEngine;

public class HumanSkin : MonoBehaviour
{
    [SerializeField] private HumanDestroyEffect _destroyEffect;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _appearanceVfx;

    public void Appearance()
    {
        _appearanceVfx.transform.parent = null;
        _appearanceVfx.Play();

        transform.localScale = Vector3.zero;
        transform.DOComplete(true);
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.InOutBack);
    }

    public void Shake()
    {
        transform.DOComplete(true);
        transform.DOShakeScale(1);
    }

    public void Destroy()
    {
        _meshRenderer.enabled = false;
        _destroyEffect.Play();
    }
}