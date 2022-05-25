using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class BonusGame : MonoBehaviour, IBonusGame
{
    [SerializeField] private float _cookFlyingSpeed = 4f;
    [SerializeField] private int _completeGameCurrency = 150;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform[] _jumpPoints;
    [SerializeField] private Wall _wall;

    private ICook _cook;
    private ICamera _camera;
    private IPlatform _platform;
    private int _currentJumpsCount;

    public event Action<GameResult> GameOver;

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

        _camera.ZoomToTarget(() => StartCoroutine(CookEatingFood()));
    }

    private void OnCameraMovedToStartPoint()
    {
        _wall.AllowExplosion();
        _cook.Animation.PlayJump(true);

        _cook.transform.DOJump(_startPoint.position, 2.5f, 1, 2f).SetEase(Ease.Linear)
            .OnComplete(() => OnJumpedOnFinishLine());
    }

    private void OnJumpedOnFinishLine()
    {
        var jumpsCount = Mathf.Clamp((int)_cook.Weight / 37, 4, 15);

        _cook.TransormateToBall();

        StartCoroutine(JumpingOnBlocks(jumpsCount));
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
        _cook.Animation.PlayMovement(false);
        _camera.MoveToStartPoint(OnCameraMovedToStartPoint);
    }

    private IEnumerator JumpingOnBlocks(int blocksCount)
    {
        var jumpPower = 2.5f;
        var jumpDuration = 1.2f;

        for (int i = 0; i < blocksCount; i++)
        {
            if (jumpPower > 0.4f)
                jumpPower -= 0.3f;

            if (jumpDuration > 0.1f)
                jumpDuration -= 0.02f;

            _cook.transform.DOJump(_jumpPoints[i].position, jumpPower, 1, jumpDuration).SetEase(Ease.Linear);

            yield return Yielder.WaitForSeconds(jumpDuration);
        }

        _cook.TransformateToCook();
        _cook.Animation.PlayJump(false);
        _cook.Animation.PlayFly(true);
        _camera.RotateAroundTarget();

        GameOver?.Invoke(new GameResult(_completeGameCurrency, _currentJumpsCount));
    }
}
