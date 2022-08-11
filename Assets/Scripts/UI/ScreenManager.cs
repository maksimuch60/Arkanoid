using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<GameObject> _screens; 
    private Stack<GameObject> _screenHierarchy = new Stack<GameObject>();

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _screenHierarchy.Push(_screens.First());
        SetActive(true);
    }

    private void Start()
    {
        GameManager.Instance.OnScreenChanged += NextScreen;
        PauseManager.Instance.OnScreenChanged += NextScreen;
        PauseManager.Instance.OnPrevScreenChanged += PrevScreen;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnScreenChanged -= NextScreen;
        PauseManager.Instance.OnScreenChanged -= NextScreen;
        PauseManager.Instance.OnPrevScreenChanged -= PrevScreen;
    }

    #endregion

    #region Public methods

    public void NextScreen(string nextScreenName)
    {
        SetActive(false);   
        SetCurrentScreen(nextScreenName);
    }

    public void PrevScreen()
    {
        if (_screenHierarchy.Count == 0)
        {
            return;
        }

        SetActive(false);
        _screenHierarchy.Pop();
        SetActive(true);
    }

    #endregion


    #region Private methods

    private void SetCurrentScreen(string nextScreenName)
    {
        foreach (GameObject screen in _screens)
        {
            if (screen.name.Equals(nextScreenName))
            {
                _screenHierarchy.Push(screen);
                break;
            }
        }

        SetActive(true);
    }

    private void SetActive(bool isActive)
    {
        if (_screenHierarchy == null || _screenHierarchy.Count == 0)
        {
            return;
        }
        
        _screenHierarchy.Peek().SetActive(isActive);
    }

    #endregion
}