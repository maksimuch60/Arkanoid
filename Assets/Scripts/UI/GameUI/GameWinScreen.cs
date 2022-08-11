using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWinScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private TextMeshProUGUI _scoreLabel;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(NextLevelButtonClicked);
    }

    private void Start()
    {
        SetScoreText();
    }

    #endregion


    #region Private methods

    private void NextLevelButtonClicked()
    {
        PauseManager.Instance.ResumeGame();
        BallsHandler.Instance.ResetBallHandler();
        SceneLoader.Instance.LoadNextScene();
    }
    private void SetScoreText()
    {
        _scoreLabel.text = ScoreManager.Instance.Score.ToString();
    }

    #endregion
}