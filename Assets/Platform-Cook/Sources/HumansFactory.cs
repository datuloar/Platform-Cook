using UnityEngine;

public class HumansFactory : IHumansFactory
{
    private const string CookPath = "Humans/Cook";
    private const string HumanPath = "Humans/HungryHuman";

    private readonly IAssetsProvider _assetsProvider;

    public HumansFactory(IAssetsProvider assetsProvider)
    {
        _assetsProvider = assetsProvider;
    }

    public ICook CreateCook(Vector3 position, Vector3 rotationEuler)
    {
        var instantiatedObject = _assetsProvider.Instantiate(CookPath, position, rotationEuler);

        if (instantiatedObject.TryGetComponent(out ICook cook))
            return cook;

        throw new System.ArgumentException($"Missing script {nameof(ICook)}");
    }

    public IHungryHuman CreateHungryHuman(Vector3 position, Vector3 rotationEuler)
    {
        var instantiatedObject = _assetsProvider.Instantiate(HumanPath, position, rotationEuler);

        if (instantiatedObject.TryGetComponent(out IHungryHuman hungryHuman))
            return hungryHuman;

        throw new System.ArgumentException($"Missing script {nameof(IHungryHuman)}");
    }
}
