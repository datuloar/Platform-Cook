using System;
using UnityEngine;

public interface IFood : IResource
{
    bool CanTake { get; }
    event Action<IFood> Taken; 

    void Hide(bool animate = true);
    void Show(bool animate = true);
    void Take();
}
