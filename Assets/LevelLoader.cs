using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeOutDuration = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "_boot")
            return;

        if (Persistence.IsOldUser(SaveKey.Scene))
        {
            var scene = PlayerPrefs.GetString(SaveKey.Scene.ToString());
            LoadSceneCorutine(scene);
        }
        else
        {
            Persistence.SetDefaultValues();
            LoadSceneCorutine("Level1");
        }
    }

    public void LoadSceneCorutine(string sceneName)
    {
        Persistence.Save(SaveKey.Scene, sceneName);
        StartCoroutine(LoadScene(sceneName));      
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation loadNextSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        _canvasGroup.Open();

        while (!loadNextSceneAsync.isDone)
            yield return null;

        _canvasGroup.DOFade(0, _fadeOutDuration).OnComplete(()=> _canvasGroup.Close());
    }
}

public enum SaveKey
{
    Scene,
    Currency,
}