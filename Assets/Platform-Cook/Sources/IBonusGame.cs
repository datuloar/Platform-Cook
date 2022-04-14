using System;

public interface IBonusGame
{
    event Action GameOver;

    void Init(ICook cook, IPlatform platform, ICamera camera);
    void StartGame();
}