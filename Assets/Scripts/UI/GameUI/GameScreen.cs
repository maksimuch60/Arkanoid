using System;
using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLabel;
    [SerializeField] private TextMeshProUGUI _livesLabel;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        ScoreManager.Instance.OnScoreChanged += SetScoreLabelText;
        GameManager.Instance.OnLivesChanged += SetLivesLabelText;
    }

    private void Start()
    {
        SetScoreLabelText(ScoreManager.Instance.Score);
        SetLivesLabelText(GameManager.Instance.Lives);
    }

    #endregion

    #region Private methods

    private void SetScoreLabelText(int score)
    {
        _scoreLabel.text = score.ToString();
    }

    private void SetLivesLabelText(int lives)
    {
        string text = String.Empty;
        for (int i = 0; i < lives; i++)
        {
            text += "<sprite=\"life\" index=0>";
        }
        _livesLabel.text = text;
    }

    #endregion
}