using System;

public interface IStartWindow : IWindow
{
    event Action StartGameButtonClicked;
}
