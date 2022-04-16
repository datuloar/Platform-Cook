using UnityEngine;

public interface IResource
{
    ResourceType Type { get; }
    Transform transform { get; }
}
