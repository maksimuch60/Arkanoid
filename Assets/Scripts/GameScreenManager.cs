using System.Linq;
using UnityEngine;

public class GameScreenManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject[] _screens;

    private GameObject _currentScreen;
    private GameObject _previousScreen;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _previousScreen = null;
        _currentScreen = _screens.First();
        _currentScreen.SetActive(true);
    }

    #endregion


    #region Public methods

    public void ChangeScreen(string nextScreenName)
    {
        if (_currentScreen == null)
        {
            return;
        }

        _currentScreen.SetActive(false);
        _previousScreen = _currentScreen;
        SetCurrentScreen(nextScreenName);
    }

    public string GetPreviousScreenName()
    {
        return _previousScreen.name;
    }

    #endregion


    #region Private methods

    private void SetCurrentScreen(string nextScreenName)
    {
        foreach (GameObject screen in _screens)
        {
            if (screen.name.Equals(nextScreenName))
            {
                _currentScreen = screen;
                break;
            }
        }

        _currentScreen.SetActive(true);
    }

    #endregion
}