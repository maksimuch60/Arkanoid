using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Local data

    private enum GameState
    {
        Starting,
        Playing,
        Ending
    }

    #endregion


    #region Variables

    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private GameScreenManager _gameScreenManager;
    
    
    [SerializeField] private Ball _ball;
    [SerializeField] private int _lives;

    private GameState _currentState = GameState.Starting;
    
    #endregion


    #region Unity lifecycle

    private void Start()
    {   
        _gameScreen.SetLivesLabelText(_lives);
        _gameScreenManager.ChangeScreen(Screens.GameScreen);
        LevelManager.Instance.OnAllBlocksDestroyed += PerformWin;
        _ball.OnBallFell += DecrementLives;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnAllBlocksDestroyed -= PerformWin;
        _ball.OnBallFell -= DecrementLives;
    }

    private void Update()
    {
        if (_currentState != GameState.Starting)
        {
            return;
        }

        _ball.MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    #endregion


    #region Private methods

    private void DecrementLives()
    {
        _lives--;
        _currentState = _lives > 0 ? GameState.Starting : GameState.Ending;
        if (_currentState == GameState.Ending)
        {
            SetLastPause();
            _gameScreenManager.ChangeScreen(Screens.EndGameScreen);
        }
        _gameScreen.SetLivesLabelText(_lives);
    }

    private void PerformWin()
    {
        SetLastPause();
        _gameScreenManager.ChangeScreen(Screens.EndGameScreen);
    }

    private static void SetLastPause()
    {
        PauseManager.Instance.TogglePause();
        PauseManager.Instance.enabled = false;
    }

    private void StartBall()
    {
        _currentState = GameState.Playing;
        _ball.StartMove();
    }

    #endregion
}