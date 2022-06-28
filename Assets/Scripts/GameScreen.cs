using System;
using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLabel;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        ScoreManager.Instance.SetListener(SetScoreLabelText);
    }

    #endregion

    #region Public methods

    public void SetScoreLabelText(int score)
    {
        _scoreLabel.text = score.ToString();
    }

    #endregion
}