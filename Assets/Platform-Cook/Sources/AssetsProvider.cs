using UnityEngine;

public class AssetsProvider : IAssetsProvider
{
    public GameObject Instantiate(string path, Vector3 position, Vector3 rotationEuler)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, position, Quaternion.Euler(rotationEuler));
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }
}
