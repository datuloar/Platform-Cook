using UnityEngine;

public interface IAssetsProvider
{
    GameObject Instantiate(string path, Vector3 position, Vector3 rotationEuler);
    GameObject Instantiate(string path, Vector3 position);
    GameObject Instantiate(string path);
}
