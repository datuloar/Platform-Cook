public interface IMiniGame
{
    void Init(ICook cook, IPlatform platform, ICamera camera);
    void StartGame();
}