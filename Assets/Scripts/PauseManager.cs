using UnityEngine;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Variables

    [SerializeField] private GameScreenManager _gameScreenManager;

    #endregion


    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseScreen();
            TogglePause();
        }
    }

    #endregion


    #region Private methods

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
    }

    private void TogglePauseScreen()
    {
        _gameScreenManager.ChangeScreen(!IsPaused ? Screens.PauseScreen : _gameScreenManager.GetPreviousScreenName());
    }

    #endregion
}