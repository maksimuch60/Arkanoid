using System;
using UnityEngine;

public class GameScreenManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseScreen;

    private GameObject[] _windows;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _windows = new[] {_gameScreen, _gameOverScreen, _pauseScreen};
    }

    #endregion


    #region Public methods

    public void ChangeWindow()
    {
        
    }

    #endregion
}