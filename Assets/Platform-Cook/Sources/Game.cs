using System.Collections.Generic;
using UnityEngine;

public class Game : IGame
{
    private readonly IViewport _viewport;
    private readonly IGameEngine _gameEngine;
    private readonly IHouse _house;

    private bool _isPlaying;
    private Player _player;

    public Game(IViewport viewport, IHouse house, IGameEngine gameEngine)
    {
        _viewport = viewport;
        _gameEngine = gameEngine;
        _house = house;
    }

    public void Start()
    {
        _isPlaying = true;

        InitializationComponents();

        _gameEngine.GetInputDevice().Enable();
        _viewport.GetPlayWindow().Open();

        _house.CurrentStorey.HumansDied += OnStoreyCleared;
    }

    public void Tick(float time)
    {
        if (_isPlaying)
        {
            _player.Tick(time);
        }
    }

    private void InitializationComponents()
    {
        var cook = _house.CreateCook();

        _player = new Player(cook, _gameEngine.GetInputDevice());       
    }

    private void OnStoreyCleared()
    {
        _house.MoveNextStorey(OnNextStoreyMoved);
    }

    private void OnNextStoreyMoved()
    {
        
    }

    private void End()
    {
        _isPlaying = false;
        _gameEngine.GetInputDevice().Disable();
    }
}

public class HumansFactory : IHumansFactory
{
    private const string CookPath = "Humans/Cook";
    private const string HumanPath = "Humans/HungryHuman";

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

        throw new System.ArgumentException($"Missing script {nameof(ICook)}");
    }

    public IHungryHuman CreateHungryHuman(Vector3 position)
    {
        var instantiatedObject = _assetsProvider.Instantiate(HumanPath, position);

        if (instantiatedObject.TryGetComponent(out IHungryHuman hungryHuman))
            return hungryHuman;

        throw new System.ArgumentException($"Missing script {nameof(IHungryHuman)}");
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
    IHungryHuman CreateHungryHuman(Vector3 position);
}

public interface IHungryHuman : IHuman
{
    void Init(ITable table);
}
