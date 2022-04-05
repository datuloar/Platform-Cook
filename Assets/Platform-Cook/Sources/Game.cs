using System.Collections.Generic;
using UnityEngine;

public class Game : IGame
{
    private readonly IViewport _viewport;
    private readonly IPlatform _platform;
    private readonly IGameEngine _gameEngine;
    private readonly IReadOnlyList<IRoom> _rooms;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IReadOnlyList<IRoom> rooms, IPlatform platform, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _rooms = rooms;
        _gameEngine = gameEngine;
        _platform = platform;
    }

    public void Start()
    {
        _viewport.GetPlayWindow().Open();

        _isPlaying = true;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
        }
    }



    private void End()
    {
        _isPlaying = false;
    }
}

public class HumansFactory : IHumansFactory
{
    private const string CookPath = "Humans/Cook";
    private const string HumanPath = "Humans/Human";

    private readonly IAssetsProvider _assetsProvider;

    public HumansFactory(IAssetsProvider assetsProvider)
    {
        _assetsProvider = assetsProvider;
    }

    public ICook CreateCook(Vector3 position)
    {
        var instantiatedObject = _assetsProvider.Instantiate(CookPath, position);

        if (instantiatedObject.TryGetComponent(out ICook cook))
            return cook;

        throw new System.ArgumentException($"missing script {nameof(ICook)}");
    }

    public IHuman CreateHuman(Vector3 position)
    {
        var instantiatedObject = _assetsProvider.Instantiate(HumanPath, position);

        if (instantiatedObject.TryGetComponent(out IHuman cook))
            return cook;

        throw new System.ArgumentException($"missing script {nameof(IHuman)}");
    }
}

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

public interface IAssetsProvider
{
    GameObject Instantiate(string path, Vector3 position, Vector3 rotationEuler);
    GameObject Instantiate(string path, Vector3 position);
    GameObject Instantiate(string path);
}

public interface IHumansFactory
{
    ICook CreateCook(Vector3 position);
    IHuman CreateHuman(Vector3 position);
}
