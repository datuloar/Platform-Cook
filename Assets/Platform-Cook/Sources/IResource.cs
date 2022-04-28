using UnityEngine;

public interface IResource
{
    float Height { get; }
    ResourceType Type { get; }
    Transform transform { get; }
}
