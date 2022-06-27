using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}