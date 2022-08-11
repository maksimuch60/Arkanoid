using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GameOverScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _menuButton;
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    
    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _menuButton.onClick.AddListener(MenuButtonClicked);
    }

    private void Start()
    {
        SetScoreText();
    }

    #endregion


    #region Private methods

    private void MenuButtonClicked()
    {
        SceneLoader.Instance.LoadScene(SceneNames.StartScene);
    }

    private void SetScoreText()
    {
        _scoreLabel.text = ScoreManager.Instance.Score.ToString();
    }

    #endregion
}