using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private TextMeshProUGUI _scoreLabel;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(NextLevelButtonClicked);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(NextLevelButtonClicked);
    }

    private void Start()
    {
        SetScoreText();
    }

    #endregion


    #region Private methods

    private void NextLevelButtonClicked()
    {
        SceneLoader.Instance.LoadScene(NextSceneName());
    }

    private string NextSceneName()
    {
        int currentSceneIndex = -1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"{currentSceneName}");

        for (int i = 0; i < SceneNames.LevelScene.Count; i++)
        {
            if (currentSceneName.Equals(SceneNames.LevelScene[i]))
            {
                currentSceneIndex = i;
            }
        }

        Debug.Log($"{SceneNames.LevelScene[currentSceneIndex]}");
        if (currentSceneIndex == SceneNames.LevelScene.Count - 1)
        {
            return SceneNames.StartScene;
        }

        return SceneNames.LevelScene[currentSceneIndex];
    }

    private void SetScoreText()
    {
        _scoreLabel.text = ScoreManager.Instance.Score.ToString();
    }

    #endregion
}