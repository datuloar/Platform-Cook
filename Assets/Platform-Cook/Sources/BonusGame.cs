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
    [SerializeField] private Wall _wall;

    private ICook _cook;
    private ICamera _camera;
    private IPlatform _platform;
    private int _destroyedBlocksCount;

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

        _cook.transform.DOJump(_startPoint.position, 2f, 1, 3.5f)
            .OnComplete(() => OnJumped());
    }

    private void OnJumped()
    {
        _cook.Rotator.StartRotate();

        var distance = Mathf.Clamp(_cook.Weight / 12, 4, 35);

        Vector3 movePosition = new Vector3(_cook.transform.position.x, _cook.transform.position.y, _cook.transform.position.z + distance);

        _cook.transform.DOMove(movePosition, 7)
       .OnComplete(
            () =>
            {
                _cook.Rotator.StopRotate();
                _camera.RotateAroundTarget();
                _cook.Animation.PlayJump(false);
                _cook.Animation.PlayFly(true);

                GameOver?.Invoke(new GameResult(_completeGameCurrency, _destroyedBlocksCount));
            });
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
}
