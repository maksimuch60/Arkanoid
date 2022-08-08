using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Variables

    private ScreenManager _screenManager;

    private bool _ultimatePause;

    #endregion


    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        _screenManager = FindObjectOfType<ScreenManager>();
    }

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
            _screenManager.NextScreen(GameUIScreens.PauseScreen);
        }
        else
        {
            ResumeGame();
            _screenManager.PrevScreen();
        }

        _ultimatePause = false;
    }

    #endregion
}