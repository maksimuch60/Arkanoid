using System;
using UnityEngine;

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

    [SerializeField] private bool _needAutoPlay;

    [SerializeField] private int _originalLives;
    
    private int _lives;
    private GameState _currentState = GameState.Playing;

    #endregion


    #region Properties

    public bool NeedAutoPlay => _needAutoPlay;
    public int Lives => _lives;

    #endregion


    #region Events

    public event Action<int> OnLivesChanged;
    public event Action<string> OnScreenChanged;

    #endregion


    #region Unity lifecycle

    protected override void Awake()
    {
        base.Awake();

        _lives = _originalLives;
    }

    private void Start()
    {
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
        ResetPause();
        LevelManager.Instance.ResetLevelManager();
        BallsHandler.Instance.ResetBallHandler();
    }

    public void ChangeLives(int life)
    {
        _lives += life;
        CheckLose();
        OnLivesChanged?.Invoke(_lives);
    }

    #endregion


    #region Private methods

    private void DecrementLives()
    {
        _lives--;
        CheckLose();
        OnLivesChanged?.Invoke(_lives);
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
        OnScreenChanged?.Invoke(GameUIScreens.GameOverScreen);
    }

    private void PerformGameWin()
    {
        StopGame();
        OnScreenChanged?.Invoke(GameUIScreens.GameWinScreen);
    }

    private void StopGame()
    {
        PauseManager.Instance.StopGame();
    }

    private void ResetLives()
    {
        _lives = _originalLives;
    }

    private void ResetPause()
    {
        PauseManager.Instance.ResumeGame();
    }

    private void ResetScore()
    {
        ScoreManager.Instance.ResetScore();
    }

    #endregion
}