using TMPro;
using UnityEngine;

public class PlayWindow : MonoBehaviour, IPlayWindow
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Close()
    {
        _canvasGroup.Close();
    }

    public void Open()
    {
        _canvasGroup.Open();
    }
}
