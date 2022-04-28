using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour, IVictoryWindow
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _nextButton;
    [SerializeField] private CurrencyAnimation _currencyAnimation;
    [SerializeField] private UIContent _content;
    [SerializeField] private ProgressNewSkin _progressNewSkin;

    public event Action NextButtonClicked;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
    }

    public void Open(GameResult gameResult)
    {
        _canvasGroup.Open();
        _content.Show(
            () => 
            { 
                _currencyAnimation.AddToWallet(gameResult.Currency, 
                    () => _progressNewSkin.Show());
            });
    }

    private void OnNextButtonClicked()
    {
        NextButtonClicked?.Invoke();
    }
}
