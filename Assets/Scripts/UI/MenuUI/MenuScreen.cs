using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private ScreenManager _screenManager;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayButtonClicked);
        _infoButton.onClick.AddListener(InfoButtonClicked);
        _exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayButtonClicked);
        _infoButton.onClick.RemoveListener(InfoButtonClicked);
        _exitButton.onClick.RemoveListener(ExitButtonClicked);
    }

    #endregion


    #region Private region

    private void PlayButtonClicked()
    {
        _screenManager.NextScreen(MenuUIScreens.SelectLevelScreen);
    }

    private void InfoButtonClicked()
    {
        _screenManager.NextScreen(MenuUIScreens.InfoScreen);
    }

    private void ExitButtonClicked()
    {
        ExitHelper.Exit();
    }

    #endregion
}