using System;
using UnityEngine;

public class LastBallChecker : MonoBehaviour
{
    #region Properties

    public int BallCount { get; private set; }

    #endregion


    #region Events

    public event Action OnAllBallsDestroyed;

    #endregion


    #region Public methods

    public void BallCreate()
    {
        BallCount++;
    }

    public void BallDestroy()
    {
        BallCount--;
        if (BallCount == 0)
        {
            OnAllBallsDestroyed?.Invoke();
        }
    }
    #endregion
}