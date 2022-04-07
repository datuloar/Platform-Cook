using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : MonoBehaviour, IPlayWindow
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _restartGameButton;

    public event Action RestartGameButtonClicked;

    private void OnEnable()
    {
        _restartGameButton.onClick.AddListener(OnRestartGameButtonClicked);
    }

    private void OnDisable()
    {
        _restartGameButton.onClick.AddListener(OnRestartGameButtonClicked);
    }

    public void Close()
    {
        _canvasGroup.Close();
    }

    public void Open()
    {
        _canvasGroup.Open();
    }

    private void OnRestartGameButtonClicked()
    {
        RestartGameButtonClicked?.Invoke();
    }
}
