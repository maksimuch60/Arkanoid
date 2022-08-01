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

    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private GameScreenManager _gameScreenManager;
    [SerializeField] private LastBallChecker _lastBallChecker;
    [SerializeField] private bool _needAutoPlay;
    
    
    
    [SerializeField] private int _lives;

    private GameState _currentState = GameState.Playing;
    
    #endregion


    #region Properties

    public bool NeedAutoPlay => _needAutoPlay;

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

    #endregion


    #region Public methods

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
        _gameScreenManager.ChangeScreen(Screens.EndGameScreen);
    }

    private void StopGame()
    {
        PauseManager.Instance.StopGame();
    }
    

    #endregion
}