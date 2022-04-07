using System;

public interface IMiniGame
{
    event Action GameOver;

    void Init(ICook cook, IPlatform platform, ICamera camera);
    void StartGame();
}