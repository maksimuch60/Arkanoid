using System;
using System.Collections;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [Header("Pad min/max size")]
    [SerializeField] private Vector3 _minSize;
    [SerializeField] private Vector3 _maxSize;

    private Ball _ball;
    private readonly Vector3 _originalSize = Vector3.one;
    private bool _isMagnetActive;
    private Vector2 _contactPoint;

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_isMagnetActive && col.gameObject.CompareTag(Tags.Ball))
        {
            _contactPoint = (Vector2) transform.position - col.GetContact(0).point;
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.SetContactPoint(_contactPoint);
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

    public void ResetPad()
    {
        ResetSize();
    }

    public void MagnetEffect(float time)
    {
        _isMagnetActive = true;
        StartCoroutine(WaitForEndMagnet(time));
    }

    #endregion


    #region Private methods

    private IEnumerator WaitForEndMagnet(float time)
    {
        yield return new WaitForSeconds(time);
        _isMagnetActive = false;
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }

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