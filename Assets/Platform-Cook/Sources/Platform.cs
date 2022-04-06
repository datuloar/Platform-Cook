using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private float _speed;
    [SerializeField] private PlatformZoneTrigger _zone;
    [SerializeField] private List<Food> _food;

    private Coroutine _movingToStoreyDock;
    private bool _isCookInsideZone;

    public int MaxFoodCount { get; private set; }
    public int FoodCount => _food.Count;
    public bool HasFood => FoodCount > 0;

    public event Action FoodCountChanged;
    public event Action FoodEnded;

    private void Awake()
    {
        MaxFoodCount = _food.Count;
    }

    private void OnEnable()
    {
        _zone.Stay += OnZoneStay;
        _zone.Exit += OnZoneExit;
    }

    private void OnDisable()
    {
        _zone.Stay -= OnZoneStay;
        _zone.Exit -= OnZoneExit;
    }

    public void EatFood(float foodPiecePerDelay)
    {
        if (foodPiecePerDelay < 0)
            throw new ArgumentException("Piece can't be lower zero");

        var selectedFood = _food.FirstOrDefault();
        selectedFood.Eat();

        _food.Remove(selectedFood);

        if (!HasFood)
            FoodEnded?.Invoke();

        FoodCountChanged?.Invoke();
    }

    public void MoveToStoreyDock(Vector3 dockPosition, Action moved = null)
    {
        if (_movingToStoreyDock != null)
            throw new Exception("Platform did't arrive and you demand from it to come to another position");

        _movingToStoreyDock = StartCoroutine(MovingToStoreyDock(dockPosition, moved));
    }

    private IEnumerator MovingToStoreyDock(Vector3 dockPosition, Action moved = null)
    {
        yield return new WaitUntil(() => _isCookInsideZone);

        while (transform.position != dockPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, dockPosition, _speed * Time.deltaTime);

            yield return null;
        }

        _movingToStoreyDock = null;
        moved?.Invoke();
    }

    private void OnZoneExit(ICook _) => _isCookInsideZone = false;

    private void OnZoneStay(ICook _) => _isCookInsideZone = true;

}
