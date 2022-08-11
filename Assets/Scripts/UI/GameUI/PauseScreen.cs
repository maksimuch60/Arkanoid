using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _goOnButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private ScreenManager _screenManager;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _goOnButton.onClick.AddListener(GoOnButtonClicked);
        _menuButton.onClick.AddListener(MenuButtonClicked);
    }

    #endregion


    #region Private methods

    private void MenuButtonClicked()
    {
        LevelManager.Instance.SetHandExit();
        SceneLoader.Instance.LoadScene(SceneNames.StartScene);
    }

    private void GoOnButtonClicked()
    {
        _screenManager.PrevScreen();
        PauseManager.Instance.ResumeGame();
    }

    #endregion
}