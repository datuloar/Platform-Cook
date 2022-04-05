using UnityEngine;

public interface IHumansFactory
{
    ICook CreateCook(Vector3 position);
    IHungryHuman CreateHungryHuman(Vector3 position);
}
