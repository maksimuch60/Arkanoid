using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Local data

    private enum GameState
    {
        Starting,
        Playing
    }

    #endregion


    #region Variables

    [SerializeField] private Ball _ball;

    private GameState _currentState = GameState.Starting;

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (_currentState == GameState.Playing)
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

    private void StartBall()
    {
        _currentState = GameState.Playing;
        _ball.StartMove();
    }

    #endregion
}