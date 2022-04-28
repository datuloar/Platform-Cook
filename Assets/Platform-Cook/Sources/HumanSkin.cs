using DG.Tweening;
using UnityEngine;

public class HumanSkin : MonoBehaviour
{
    [SerializeField] private HumanDestroyEffect _destroyEffect;
    [SerializeField] private ParticleSystem _appearanceVfx;
    [SerializeField] private GameObject _currentSkin;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    public SkinnedMeshRenderer MeshRenderer => _meshRenderer;

    public GameObject ChangeSuit(Suit suit)
    {
        Destroy(_currentSkin.gameObject);
        _currentSkin = Instantiate(suit.Skin, transform);
        _meshRenderer = _currentSkin.GetComponentInChildren<SkinnedMeshRenderer>();

        return _currentSkin;
    }

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
