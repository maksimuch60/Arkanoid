using System;
using System.Collections.Generic;
using UnityEngine;

public class BallsHandler : SingletonMonoBehavior<BallsHandler>
{
    #region Properties

    public List<Ball> AllBalls { get; private set; } = new List<Ball>();
    public int BallCount => AllBalls.Count;

    #endregion


    #region Events

    public event Action OnAllBallsDestroyed;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Ball.OnBallCreated += BallCreate;
        Ball.OnBallFell += BallDestroy;
    }

    #endregion


    #region Private methods

    private void BallCreate(Ball ball)
    {
        AllBalls.Add(ball);
        Debug.Log($"Balls {BallCount}");
    }

    private void BallDestroy(Ball ball)
    {
        AllBalls.Remove(ball);
        Debug.Log($"Balls {BallCount}");
        if (BallCount == 0)
        {
            OnAllBallsDestroyed?.Invoke();
        }
    }
    #endregion
}