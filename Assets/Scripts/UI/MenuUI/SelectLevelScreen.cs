﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelScreen : MonoBehaviour
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _levelNumberLabel;
    [SerializeField] private Image _levelSprite;
    
    [Header("Configs")]
    [SerializeField] private Levels _levels;
    
    [Header("Components")]
    [SerializeField] private ScreenManager _screenManager;

    private int _currentLevelIndex;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        _prevButton.interactable = false;
        SetSprite(_currentLevelIndex);
    }

    private void OnEnable()
    {
        _prevButton.onClick.AddListener(PrevButtonClicked);
        _playButton.onClick.AddListener(PlayButtonClicked);
        _nextButton.onClick.AddListener(NextButtonClicked);
        _backButton.onClick.AddListener(BackButtonClicked);
    }

    private void OnDisable()
    {
        _prevButton.onClick.RemoveListener(PrevButtonClicked);
        _playButton.onClick.RemoveListener(PlayButtonClicked);
        _nextButton.onClick.RemoveListener(NextButtonClicked);
        _backButton.onClick.RemoveListener(BackButtonClicked);
    }

    #endregion


    #region Private methods

    private void SetSprite(int index)
    {
        _levelSprite.sprite = _levels.LevelSprites[index];
        SetLevelNumberText(_currentLevelIndex);
    }

    private void SetLevelNumberText(int currentLevelIndex)
    {
        _levelNumberLabel.text = "Level " + (currentLevelIndex + 1);
    }

    private void PrevButtonClicked()
    {
        SetSprite(PrevSpriteIndex());
    }

    private int PrevSpriteIndex()
    {
        _nextButton.interactable = true;
        
        if (_currentLevelIndex > 0)
        {
            _currentLevelIndex--;
        }

        if (_currentLevelIndex == 0)
        {
            _prevButton.interactable = false;
        }

        return _currentLevelIndex;
    }

    private void PlayButtonClicked()
    {
        SceneLoader.Instance.LoadScene(SceneNames.LevelScene[_currentLevelIndex]);
    }

    private void NextButtonClicked()
    {
        SetSprite(NextLevelIndex());
    }

    private int NextLevelIndex()
    {
        _prevButton.interactable = true;
        
        if (_currentLevelIndex < _levels.LevelSprites.Length)
        {
            _currentLevelIndex++;
        }

        if (_currentLevelIndex == _levels.LevelSprites.Length - 1)
        {
            _nextButton.interactable = false;
        }

        return _currentLevelIndex;
    }

    private void BackButtonClicked()
    {
        _screenManager.PrevScreen();
    }

    #endregion
}