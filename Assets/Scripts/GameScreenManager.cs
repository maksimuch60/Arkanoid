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

    public void ChangeScreen(GameObject nextScreen)
    {
        _currentScreen.SetActive(false);
        _previousScreen = _currentScreen;
        _currentScreen = nextScreen;
        _currentScreen.SetActive(true);
    }

    public GameObject GetPreviousScreen()
    {
        return _previousScreen;
    }

    #endregion
}