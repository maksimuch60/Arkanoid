using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Pad _pad;

    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _offset;

    [Header("Random direction range")]
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _xMin;
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _xMax;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _yMin;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _yMax;

    [Header("Ball min/max speed")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    [Header("Ball min/max size")]
    [SerializeField] private Vector3 _maxSize;
    [SerializeField] private Vector3 _minSize;

    private readonly Vector3 _originalSize = Vector3.one;
    private Vector2 _contactPoint;
    private bool _isStarted;
    private float _speed;
    private bool _isNewBall;

    #endregion


    #region Events

    public static event Action<Ball> OnBallFell;
    public static event Action<Ball> OnBallCreated;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        if (_isNewBall)
        {
            OnBallCreated?.Invoke(this);
            return;
        }

        _speed = _originalSpeed;
        ResetBall();
        if (GameManager.Instance.NeedAutoPlay)
        {
            StartBall();
        }
    }

    private void Update()
    {
        if (_isStarted)
        {
            return;
        }

        MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rigidbody2D.velocity);
    }

    #endregion


    #region Public methods

    public void OnBallFall()
    {
        OnBallFell?.Invoke(this);
        if (LastBallChecker.Instance.BallCount == 0)
        {
            ResetBall();
            _pad.ResetPad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        Vector2 velocity = _rigidbody2D.velocity;
        _speed *= speedMultiplier;

        if (_speed < _minSpeed)
        {
            _speed = _minSpeed;
        }

        if (_speed > _maxSpeed)
        {
            _speed = _maxSpeed;
        }

        _rigidbody2D.velocity = velocity.normalized * _speed;
    }

    public void ChangeSize(float sizeMultiplier)
    {
        Vector3 scale = transform.localScale;
        scale *= sizeMultiplier;

        if (scale.magnitude > _maxSize.magnitude)
        {
            scale = _maxSize;
        }

        if (scale.magnitude < _minSize.magnitude)
        {
            scale = _minSize;
        }

        transform.localScale = scale;
    }

    public void SetContactPoint(Vector2 contactPoint)
    {
        _isStarted = false;
        _contactPoint = contactPoint;
    }

    public void Clone(Ball ball)
    {
        _isNewBall = true;
        _speed = ball._speed;
        StartBall();
    }

    #endregion


    #region Private methods

    private void StartMove()
    {
        _rigidbody2D.velocity = GetRandomDirection();
    }

    private void ResetBall()
    {
        OnBallCreated?.Invoke(this);
        _isStarted = false;
        _contactPoint = Vector2.zero;

        ResetSize();
        ResetSpeed();
        MoveWithPad();
    }

    private void ResetSpeed()
    {
        _speed = _originalSpeed;
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }

    private void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x - _contactPoint.x;
        currentPosition.y = padPosition.y + _offset;
        transform.position = currentPosition;
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 startDirection = new Vector2(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax));
        startDirection.Normalize();
        startDirection *= _speed;

        return startDirection;
    }

    private void StartBall()
    {
        _isStarted = true;
        StartMove();
    }

    #endregion
}