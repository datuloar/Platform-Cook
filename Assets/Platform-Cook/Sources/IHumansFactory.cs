using UnityEngine;

public interface IHumansFactory
{
    ICook CreateCook(Vector3 position, Vector3 rotationEuler);
    IHungryHuman CreateHungryHuman(Vector3 position, Vector3 rotationEuler);
}
