using UnityEngine;

public class HumanSkin : MonoBehaviour
{
    private const string GrowFatKey = "Key1";

    [SerializeField] private HumanDestroyEffect _destroyEffect;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private float _growValueAdd = 15f;

    public void GrowFat()
    {

    }

    public void Destroy()
    {
        _meshRenderer.enabled = false;
        _destroyEffect.Play();
    }
}