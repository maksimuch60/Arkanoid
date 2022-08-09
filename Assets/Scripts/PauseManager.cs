using System;
using UnityEngine;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Variables

    //private ScreenManager _screenManager;

    private bool _ultimatePause;

    #endregion


    #region Events

    public event Action<string> OnScreenChanged;
    public event Action OnPrevScreenChanged;

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


    #region Public methods

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


    #region Private methods

    private void TogglePause()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            StopGame();
            OnScreenChanged?.Invoke(GameUIScreens.PauseScreen);
        }
        else
        {
            ResumeGame();
            OnPrevScreenChanged?.Invoke();
        }

        _ultimatePause = false;
    }

    #endregion
}