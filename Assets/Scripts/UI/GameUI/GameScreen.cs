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
        SetScoreLabelText(ScoreManager.Instance.Score);
        SetLivesLabelText(GameManager.Instance.Lives);
    }

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += SetScoreLabelText;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= SetScoreLabelText;
    }

    #endregion

    #region Private methods

    private void SetScoreLabelText(int score)
    {
        _scoreLabel.text = score.ToString();
    }

    public void SetLivesLabelText(int lives)
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