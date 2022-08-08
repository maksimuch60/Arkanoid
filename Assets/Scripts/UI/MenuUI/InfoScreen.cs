using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _descriptionLabel;
    [SerializeField] private ScreenManager _screenManager;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        SetDescriptionText();
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(BackButtonClicked);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(BackButtonClicked);
    }

    #endregion


    #region Private methods

    private void BackButtonClicked()
    {
        _screenManager.PrevScreen();
    }
    
    private void SetDescriptionText()
    {
        _descriptionLabel.text += "<sprite=\"FireBall\" index=0> - pick up. Make ball explosive in radius of 1 block.\n" +
            "<sprite=\"life\" index=0> - pick up. Add one life.\n" +
            "<sprite=\"loseLife\" index=0> - pick up. Remove one life.\n" +
            "<sprite=\"losePoints\" index=0> - pick up. Give you -5 points.\n" +
            "<sprite=\"coinGold\" index=0> - pick up. Give you 5 points.\n" +
            "<sprite=\"decreaseSpeed\" index=0> - pick up. Decrease ball speed.\n" +
            "<sprite=\"starGold\" index=0> - pick up. Increase ball speed.\n" +
            "<sprite=\"DecreasePadSize\" index=0> - pick up. Decrease pad size.\n" +
            "<sprite=\"IncreasePadSize\" index=0> - pick up. Increase pad size.\n" +
            "<sprite=\"DecreaseBallSize\" index=0> - pick up. Decrease ball size.\n" +
            "<sprite=\"IncreaseBallSize\" index=0> - pick up. Increase ball size.\n" +
            "<sprite=\"magnet\" index=0> - make pad magnetic.\n" +
            "<sprite=\"multiBall\" index=0> - add extra balls.";
    }


    #endregion
}