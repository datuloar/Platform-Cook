using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGame : MonoBehaviour, IBonusGame
{
    [SerializeField] private List<ScoreBlock> _scoreBlocks;

    private ICook _cook;
    private ICamera _camera;
    private IPlatform _platform;
    private int _destroyedBlocksCount;

    public event Action GameOver;

    private void OnEnable()
    {
        foreach (var scoreBlock in _scoreBlocks)
            scoreBlock.Destroyed += OnScoreBlockDestroyed;
    }

    private void OnDisable()
    {
        foreach (var scoreBlock in _scoreBlocks)
            scoreBlock.Destroyed -= OnScoreBlockDestroyed;
    }

    public void Init(ICook cook, IPlatform platform, ICamera camera)
    {
        _platform = platform;
        _cook = cook;
        _camera = camera;
    }

    public void StartGame()
    {
        _cook.transform.position = _platform.transform.position;
        _cook.transform.rotation = Quaternion.Euler(new Vector3(0, 180));
        _cook.FreezeMovement();

        _camera.ZoomToTarget(OnCookZoomed);
    }

    private void OnCookZoomed()
    {
        StartCoroutine(CookEatingFood());
    }

    private void OnScoreBlockDestroyed()
    {
        _destroyedBlocksCount++;
    }

    private void OnCameraMovedToStartPoint()
    {
        StartCoroutine(CookFlying());
    }

    private IEnumerator CookFlying()
    {
        var targetPosition = new Vector3(_cook.transform.position.x, _cook.transform.position.y + _cook.Weight / 10, _cook.transform.position.z);
        _cook.Animation.PlayFly(true);
        _cook.StartFarting();

        while (_cook.transform.position != targetPosition)
        {
            _cook.transform.position = Vector3.MoveTowards(_cook.transform.position, targetPosition, 4f * Time.deltaTime);

            yield return null;
        }

        _camera.StopFollowing();
        GameOver?.Invoke();
    }

    private IEnumerator CookEatingFood()
    {
        while (_platform.Table.HasFood)
        {
            _cook.Animation.PlayEating(true);
            _cook.Eat(_platform.Table.GetFood());

            yield return Yielder.WaitForSeconds(0.3f);
        }

        _cook.Animation.PlayEating(false);
        _camera.MoveToStartPoint(OnCameraMovedToStartPoint);
    }
}
