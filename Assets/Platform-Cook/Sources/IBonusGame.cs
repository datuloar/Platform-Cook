using System;

public interface IBonusGame
{
    event Action<GameResult> GameOver;

    void Init(ICook cook, IPlatform platform, ICamera camera);
    void StartGame();
}