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
        ScoreManager.Instance.OnScoreChanged += SetScoreLabelText;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= SetScoreLabelText;
    }

    #endregion

    #region Public methods

    public void SetScoreLabelText(int score)
    {
        _scoreLabel.text = score.ToString();
    }

    #endregion
}