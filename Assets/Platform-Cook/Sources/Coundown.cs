using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Coundown : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _countdownLabel;

    private Coroutine _countingdown;

    public event Action Completed;

    public void StartCountdown(int time)
    {
        Show();

        if (_countingdown != null)
            StopCoroutine(_countingdown);

        _countingdown = StartCoroutine(CountingDown(time));
    }

    public void Stop()
    {
        _canvasGroup.Close();

        if (_countingdown != null)
            StopCoroutine(_countingdown);

        _countdownLabel.text = "0:00";
    }

    private IEnumerator CountingDown(int time)
    {
        while (time > 0)
        {
            time--;
            Render(time);

            yield return Yielder.WaitForSeconds(1);
        }

        Hide();

        Completed?.Invoke();
    }

    private void Render(int time)
    {
        var timeSpan = TimeSpan.FromSeconds(time);

        _countdownLabel.text = timeSpan.ToString(@"mm\:ss");
    }

    public void Hide(bool animate = true)
    {
        if (animate)
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InOutBack)
            .OnComplete(() => _canvasGroup.Close());
        }
        else
        {
            _canvasGroup.Close();
        }
    }

    public void Show(bool animate = true)
    {
        _canvasGroup.Open();

        if (animate)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
        }
    }
}