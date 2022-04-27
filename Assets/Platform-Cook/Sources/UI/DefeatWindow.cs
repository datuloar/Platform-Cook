using System;
using UnityEngine;
using UnityEngine.UI;

public class DefeatWindow : MonoBehaviour, IDefeatWindow
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private UIContent _content;

    public event Action RestartButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    public void Close()
    {
        _canvasGroup.Close();
    }

    public void Open()
    {
        _canvasGroup.Open();
        _content.Show();
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }
}
