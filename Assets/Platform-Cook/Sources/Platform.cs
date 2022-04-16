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
    [SerializeField] private ArrowPointer _pointer;
    [SerializeField] private UnityGameEngine _gameEngine;
    [SerializeField] private PlatformZoneTrigger _zone;
    [SerializeField] private List<Food> _food;

    private Coroutine _movingToStoreyDock;
    private bool _isCookInsideZone;

    public int MaxFoodCount { get; private set; }
    public bool IsMovingToNextStorey { get; private set; }
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
        if (!_isCookInsideZone)
            _pointer.Show();

        yield return new WaitUntil(() => _isCookInsideZone);

        _pointer.Hide();

        _gameEngine.GetInputDevice().Disable();

        IsMovingToNextStorey = true;

        _rigidbody.DOMove(dockPosition, _moveDuration)
            .OnComplete(() =>
            {
                moved?.Invoke();

                _gameEngine.GetInputDevice().Enable();
                IsMovingToNextStorey = false;
                _movingToStoreyDock = null;
            });      
    }

    private void OnZoneExit(ICook _) => _isCookInsideZone = false;

    private void OnZoneStay(ICook cook)
    {
        _isCookInsideZone = true;
    }
}
