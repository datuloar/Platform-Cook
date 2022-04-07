using UnityEngine.SceneManagement;

public class Level : ILevel
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}