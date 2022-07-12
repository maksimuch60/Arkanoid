using UnityEngine;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Variables

    [SerializeField] private GameScreenManager _gameScreenManager;
    [SerializeField] private GameObject _pauseScreen;

    #endregion


    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    #endregion


    #region Private methods

    public void TogglePause()
    {
        _gameScreenManager.ChangeScreen(!IsPaused ? _pauseScreen : _gameScreenManager.GetPreviousScreen());
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
    }

    #endregion
}