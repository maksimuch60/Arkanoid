using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    private Ball _ball;

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