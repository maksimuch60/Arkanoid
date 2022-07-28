using UnityEngine;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Variables

    [SerializeField] private GameScreenManager _gameScreenManager;

    private bool _ultimatePause;

    #endregion


    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_ultimatePause)
        {
            TogglePause();
        }
    }

    #endregion


    #region Private methods

    private void TogglePause()
    {
        _gameScreenManager.ChangeScreen(IsPaused ? _gameScreenManager.GetPreviousScreenName() : Screens.PauseScreen);
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            StopGame();
        }
        else
        {
            ResumeGame();
        }

        _ultimatePause = false;
    }

    public void StopGame()
    {
        IsPaused = true;
        Time.timeScale = 0;
        _ultimatePause = true;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
        _ultimatePause = false;
    }

    #endregion
}