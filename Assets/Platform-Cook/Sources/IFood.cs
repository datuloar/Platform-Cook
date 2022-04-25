using System;
using UnityEngine;

public interface IFood : IResource
{
    bool CanTake { get; }
    int Weight { get; }

    event Action<IFood> Taken; 

    void Hide(bool animate = true);
    void Show(bool animate = true);
    void Take();
}
