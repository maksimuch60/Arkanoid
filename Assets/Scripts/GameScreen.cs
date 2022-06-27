using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLabel;

    private int _score;

    #endregion


    #region Public methods

    public void IncrementScore(int score)
    {
        _score += score;
        SetScoreLabelText();
    }

    #endregion


    #region Private methods

    private void SetScoreLabelText()
    {
        _scoreLabel.text = _score.ToString();
    }

    #endregion
}