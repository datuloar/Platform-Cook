using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour, IStartWindow
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _levelLabel;

    public event Action StartGameButtonClicked;

    private void Awake()
    {
        _levelLabel.text = $"LEVEL {SceneManager.GetActiveScene().buildIndex}";
    }

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
    }

    public void Close() => _canvasGroup.Close();

    public void Open() => _canvasGroup.Open();

    private void OnStartGameButtonClicked() => StartGameButtonClicked?.Invoke();
}
