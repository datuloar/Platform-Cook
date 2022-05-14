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
        _wall.transform.DOLocalRotate(new Vector3(-90, 0, 0), 2f);
        _cook.Animation.PlayJump(true);

        _cook.transform.DOJump(_startPoint.position, 1.5f, 1, 3f)
            .OnComplete(() => _cook.transform.DOMove(new Vector3(_cook.transform.position.x, _cook.transform.position.y, _cook.transform.position.z + 20), 7).OnComplete(() => GameOver?.Invoke(new GameResult(_completeGameCurrency, _destroyedBlocksCount))));
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
