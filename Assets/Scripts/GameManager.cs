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
    
    
    [SerializeField] private Ball _ball;
    [SerializeField] private int _lives;

    private GameState _currentState = GameState.Starting;

    #endregion


    #region Unity lifecycle

    private void Start()
    {   
        _gameScreen.SetActive(true);
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
            _gameScreen.SetActive(false);
            _gameOverScreen.SetActive(true);
            
        }
    }

    private void PerformWin()
    {
        Debug.Log("Win");
    }

    private void StartBall()
    {
        _currentState = GameState.Playing;
        _ball.StartMove();
    }

    #endregion
}