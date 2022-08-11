using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    #region Variables

    [SerializeField] private Levels _levels;

    #endregion

    #region Public mehtods

    public void LoadScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        if (_levels.CurrentLevelIndex == _levels.LevelArray.Length - 1)
        {
            Instance.LoadScene(SceneNames.StartScene);
        }
        else
        {
            _levels.CurrentLevelIndex++;
            Instance.LoadScene(_levels.LevelArray[_levels.CurrentLevelIndex].LevelName);
        }
    }

    #endregion
}