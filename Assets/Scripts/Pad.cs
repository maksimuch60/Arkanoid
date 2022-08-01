using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables
    
    [Header("Pad min/max size")]
    [SerializeField] private Vector3 _minSize;
    [SerializeField] private Vector3 _maxSize;
    

    private Ball _ball;
    private Vector3 _originalSize;

    #endregion
    
    #region Unity lifecycle

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
        {
            return;
        }

        if (GameManager.Instance.NeedAutoPlay)
        {
            MoveWithBall();
        }
        else
        {
            MoveWithMouse();
        }
    }

    #endregion


    #region Public methods

    public void ChangeSize(float sizeMultiplier)
    {
        Vector3 scale = transform.localScale;
        scale.x *= sizeMultiplier;
        
        if (scale.x > _maxSize.x)
        {
            scale = _maxSize;
        }
        
        if (scale.x < _minSize.x)
        {
            scale = _minSize;
        }

        transform.localScale = scale;
    }

    #endregion


    #region Private methods

    private void MoveWithBall()
    {
        Vector3 ballPositionInUnits = _ball.transform.position;

        Vector3 currentPosition = transform.position;
        currentPosition.x = ballPositionInUnits.x;
        transform.position = currentPosition;
    }

    private void MoveWithMouse()
    {
        Vector3 mousePositionInPixels = Input.mousePosition;
        Vector3 mousePositionInUnits = Camera.main.ScreenToWorldPoint(mousePositionInPixels);

        Vector3 currentPosition = transform.position;
        currentPosition.x = mousePositionInUnits.x;
        transform.position = currentPosition;
    }

    #endregion
}