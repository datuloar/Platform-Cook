﻿using UnityEngine;

public class Viewport : MonoBehaviour, IViewport
{
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private PlayWindow _playWindow;
    [SerializeField] private DefeatWindow _defeatWindow;
    [SerializeField] private VictoryWindow _victoryWindow;

    public IStartWindow GetStartWindow() => _startWindow;
    public IPlayWindow GetPlayWindow() => _playWindow;
    public IDefeatWindow GetDefeatWindow() => _defeatWindow;
    public IVictoryWindow GetVictoryWindow() => _victoryWindow;
}
