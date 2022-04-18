using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourcesStack : MonoBehaviour
{
    [SerializeField] private Transform _stackContainer;
    [SerializeField] private float _animationDuration;
    [SerializeField] private FloatSetting _scalePunch = new FloatSetting(true, 1.1f);
    [SerializeField] private FloatSetting _jumpPower = new FloatSetting(false, 0f);

    private List<Transform> _transforms = new List<Transform>();

    public abstract Vector3 CalculateEndRotation(Transform container, IResource resource);

    public abstract Vector3 CalculateAddEndPosition(Transform container, IResource resource);

    public abstract void OnRemove(Transform removeTransform);

    public abstract void Sort(List<Transform> unsortedTransforms);

    public void Add(IResource resource)
    {
        Vector3 endPosition = CalculateAddEndPosition(_stackContainer, resource);
        Vector3 endRotation = CalculateEndRotation(_stackContainer, resource);
        Vector3 defaultScale = resource.transform.localScale;

        resource.transform.DOComplete(true);
        resource.transform.parent = _stackContainer;

        resource.transform.DOLocalMove(endPosition, _animationDuration);
        resource.transform.DOLocalRotate(endRotation, _animationDuration);

        if (_scalePunch.Enabled)
            resource.transform.DOPunchScale(defaultScale * _scalePunch.Value, _animationDuration);
        if (_jumpPower.Enabled)
            resource.transform.DOLocalJump(endPosition, _jumpPower.Value, 1, _animationDuration);

        _transforms.Add(resource.transform);
    }

    public void Remove(IResource resource)
    {
        resource.transform.DOComplete(true);
        resource.transform.parent = null;

        int removedIndex = _transforms.IndexOf(resource.transform);
        _transforms.RemoveAt(removedIndex);
        OnRemove(resource.transform);

        Sort(_transforms);
    }

    public float FindTopPositionY()
    {
        float topPositionY = 0f;

        foreach (var item in _transforms)
            if (item.position.y > topPositionY)
                topPositionY = item.position.y;

        return topPositionY;
    }
}
