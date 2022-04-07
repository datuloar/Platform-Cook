using System;

public interface IPlayWindow : IWindow
{
    event Action RestartGameButtonClicked;
}
