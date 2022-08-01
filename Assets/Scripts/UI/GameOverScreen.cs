using System;
using TMPro;
using UnityEngine;

class GameOverScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLabel;
    
    #endregion


    #region Unity lifecycle

    private void Start()
    {
        SetScoreText();
    }

    #endregion


    #region Private methods

    private void SetScoreText()
    {
        _scoreLabel.text = ScoreManager.Instance.Score.ToString();
    }

    #endregion
}