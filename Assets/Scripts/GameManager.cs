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
    [SerializeField] private LastBallChecker _lastBallChecker;
    
    
    
    [SerializeField] private Ball _ball;
    [SerializeField] private int _lives;

    private GameState _currentState = GameState.Starting;
    
    #endregion


    #region Unity lifecycle

    private void Start()
    {   
        _gameScreen.SetLivesLabelText(_lives);
        _gameScreenManager.ChangeScreen(Screens.GameScreen);
        LevelManager.Instance.OnAllBlocksDestroyed += PerformEndGame;
        _lastBallChecker.OnAllBallsDestroyed += DecrementLives;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnAllBlocksDestroyed -= PerformEndGame;
        _lastBallChecker.OnAllBallsDestroyed -= DecrementLives;
    }

    private void Update()
    {
        if (_currentState != GameState.Starting)
        {
            return;
        }

        _ball.ResetBall();

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
            PerformEndGame();
        }
        _gameScreen.SetLivesLabelText(_lives);
    }

    private void PerformEndGame()
    {
        StopGame();
        _gameScreenManager.ChangeScreen(Screens.EndGameScreen);
    }

    private void StopGame()
    {
        PauseManager.Instance.StopGame();
    }

    private void StartBall()
    {
        _currentState = GameState.Playing;
        _ball.StartMove();
    }

    #endregion
}