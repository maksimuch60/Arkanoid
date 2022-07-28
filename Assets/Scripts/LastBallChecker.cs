using System;
using UnityEngine;

public class LastBallChecker : MonoBehaviour
{
    #region Variables

    private int _ballCount;

    #endregion


    #region Eventa

    public event Action OnAllBallsDestroyed;

    #endregion


    #region Public methods

    public void BallCreate()
    {
        _ballCount++;
    }

    public void BallDestroy()
    {
        _ballCount--;
        if (_ballCount == 0)
        {
            OnAllBallsDestroyed?.Invoke();
        }
    }
    #endregion
}