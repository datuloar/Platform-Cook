using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGame : MonoBehaviour, IBonusGame
{
    [SerializeField] private float _cookFlyingSpeed = 4f;
    [SerializeField] private int _completeGameCurrency = 150;
    [SerializeField] private List<ScoreBlock> _scoreBlocks;

    private ICook _cook;
    private ICamera _camera;
    private IPlatform _platform;
    private int _destroyedBlocksCount;

    public event Action<GameResult> GameOver;

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
        _cook.transform.position = _platform.transform.position + new Vector3(0, 0, -0.6f);
        _cook.transform.rotation = Quaternion.Euler(new Vector3(0, 0));

        _camera.ZoomToTarget(OnCookZoomed);
    }

    private void OnCookZoomed()
    {
        StartCoroutine(CookEatingFood());
    }

    private void OnScoreBlockDestroyed()
    {
        _destroyedBlocksCount++;
        Taptic.Medium();
    }

    private void OnCameraMovedToStartPoint()
    {
        StartCoroutine(CookFlying());
    }

    private IEnumerator CookFlying()
    {
        const float HeightLimit = 25f;

        var bonusHeight = Mathf.Clamp(_cook.Weight / HeightLimit, 2, HeightLimit);
        var targetPosition = new Vector3(_cook.transform.position.x, _cook.transform.position.y + bonusHeight, _cook.transform.position.z);

        _cook.Animation.PlayFly(true);
        _cook.StartFarting();

        while (_cook.transform.position != targetPosition)
        {
            _cook.transform.position = Vector3.MoveTowards(_cook.transform.position, targetPosition, _cookFlyingSpeed * Time.deltaTime);

            yield return null;
        }

        _camera.StopFollowing();
        GameOver?.Invoke(new GameResult(_completeGameCurrency, _destroyedBlocksCount));
    }

    private IEnumerator CookEatingFood()
    {
        float eatingDelay = 0.20f;

        while (_platform.Table.HasFood)
        {
            eatingDelay -= 0.005f;
            _cook.Animation.PlayEating(true);
            _cook.Eat(_platform.Table.GetFood());

            yield return Yielder.WaitForSeconds(eatingDelay);
        }

        _cook.Animation.PlayEating(false);
        _cook.Animation.PlayMovement(true);
        _camera.MoveToStartPoint(OnCameraMovedToStartPoint);
    }
}
