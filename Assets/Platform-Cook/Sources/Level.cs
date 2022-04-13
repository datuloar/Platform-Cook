using UnityEngine.SceneManagement;

public class Level : ILevel
{
    private readonly string _nextLevelName;

    public Level(string nextLevelName)
    {
        _nextLevelName = nextLevelName;
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}