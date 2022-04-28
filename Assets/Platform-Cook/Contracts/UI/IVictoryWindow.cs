using System;

public interface IVictoryWindow
{
    event Action NextButtonClicked;

    void Open(GameResult result);
}
