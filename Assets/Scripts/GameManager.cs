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

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _gameOverScreen;
    
    [SerializeField] private GameScreenManager _gameScreenManager;
    
    
    [SerializeField] private Ball _ball;
    [SerializeField] private int _lives;

    private GameState _currentState = GameState.Starting;
    private GameScreen _gameScreenComponent;
    #endregion


    #region Unity lifecycle

    private void Start()
    {   
        _gameScreenComponent = _gameScreen.GetComponentInChildren<GameScreen>();
        _gameScreenComponent.SetLivesLabelText(_lives);
        _gameScreenManager.ChangeScreen(_gameScreen);
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
            SetupEndGame();
            _gameScreenManager.ChangeScreen(_gameOverScreen);
        }
        _gameScreenComponent.SetLivesLabelText(_lives);
    }

    private void PerformWin()
    {
        SetupEndGame();
        _gameScreenManager.ChangeScreen(_gameOverScreen);
    }

    private static void SetupEndGame()
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