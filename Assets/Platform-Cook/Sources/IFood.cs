using UnityEngine;

public interface IFood
{
    Transform transform { get; }

    void Hide(bool animate = true);
    void Show(bool animate = true);
}
