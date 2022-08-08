using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    #region Local data

    private enum GameState
    {
        Playing,
        Ending
    }

    #endregion


    #region Variables

    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private bool _needAutoPlay;

    [SerializeField] private int _originalLives;
    
    private int _lives;
    private ScreenManager _screenManager;
    private GameState _currentState = GameState.Playing;

    #endregion


    #region Properties

    public bool NeedAutoPlay => _needAutoPlay;
    public int Lives => _lives;

    #endregion


    #region Unity lifecycle

    protected override void Awake()
    {
        base.Awake();

        _lives = _originalLives;
    }

    private void Start()
    {
        _screenManager = FindObjectOfType<ScreenManager>();
        LevelManager.Instance.OnAllBlocksDestroyed += PerformGameWin;
        BallsHandler.Instance.OnAllBallsDestroyed += DecrementLives;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnAllBlocksDestroyed -= PerformGameWin;
        BallsHandler.Instance.OnAllBallsDestroyed -= DecrementLives;
    }

    #endregion


    #region Public methods

    public void ResetGame()
    {
        ResetLives();
        ResetScore();
        BallsHandler.Instance.ResetBallHandler();
    }

    public void ChangeLives(int life)
    {
        _lives += life;
        CheckLose();
        _gameScreen.SetLivesLabelText(_lives);
    }

    #endregion


    #region Private methods

    private void DecrementLives()
    {
        _lives--;
        CheckLose();
        _gameScreen.SetLivesLabelText(_lives);
    }

    private void CheckLose()
    {
        _currentState = _lives > 0 ? GameState.Playing : GameState.Ending;
        if (_currentState == GameState.Ending)
        {
            PerformEndGame();
        }
    }

    private void PerformEndGame()
    {
        StopGame();
        ResetGame();
        _screenManager.NextScreen(GameUIScreens.GameOverScreen);
    }

    private void PerformGameWin()
    {
        StopGame();
        _screenManager.NextScreen(GameUIScreens.GameWinScreen);
    }

    private void StopGame()
    {
        PauseManager.Instance.StopGame();
    }

    private void ResetLives()
    {
        _lives = _originalLives;
    }

    private void ResetScore()
    {
        ScoreManager.Instance.ResetScore();
    }

    #endregion
}