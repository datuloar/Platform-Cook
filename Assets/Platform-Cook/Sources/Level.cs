using UnityEngine.SceneManagement;

public class Level : ILevel
{
    private readonly string _nextLevelName;
    private readonly LevelLoader _loader;

    public Level(string nextLevelName, LevelLoader loader)
    {
        _nextLevelName = nextLevelName;
        _loader = loader;
    }

    public void GoNextLevel()
    {
        _loader.LoadSceneCorutine(_nextLevelName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}