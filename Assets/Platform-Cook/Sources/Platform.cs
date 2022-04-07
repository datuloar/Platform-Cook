using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveDuration;
    [SerializeField] private PlatformZoneTrigger _zone;
    [SerializeField] private List<Food> _food;

    private Coroutine _movingToStoreyDock;
    private bool _isCookInsideZone;
    private bool _isMovingToNextStorey;

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

    public IFood GetFood()
    {
        var selectedFood = _food.FirstOrDefault();
        selectedFood.Eat();

        _food.Remove(selectedFood);

        if (!HasFood)
            FoodEnded?.Invoke();

        FoodCountChanged?.Invoke();

        return selectedFood;
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

        _isMovingToNextStorey = true;

        _rigidbody.DOMove(dockPosition, _moveDuration)
            .OnComplete(() =>
            {
                moved?.Invoke();
                _isMovingToNextStorey = false;
            });      
    }

    private void OnZoneExit(ICook _) => _isCookInsideZone = false;

    private void OnZoneStay(ICook cook)
    {
        if (_isMovingToNextStorey)
            cook.FreezeMovement();
        else
            cook.UnfreezeMovement();

        _isCookInsideZone = true;
    }
}
