using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Pad _pad;

    [SerializeField] private float _speed;
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
    
    private LastBallChecker _lastBallChecker;
    private bool _isStarted;

    #endregion


    #region Events

    public event Action OnBallFell;
    public event Action OnBallCreated;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        _lastBallChecker = FindObjectOfType<LastBallChecker>();
        OnBallCreated += _lastBallChecker.BallCreate;
        OnBallFell += _lastBallChecker.BallDestroy;
    }

    private void OnDestroy()
    {
        OnBallCreated -= _lastBallChecker.BallCreate;
        OnBallFell -= _lastBallChecker.BallDestroy;
    }

    private void Start()
    {
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
        
        ResetBall();

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

    public void StartMove()
    {
        OnBallCreated?.Invoke();
        _rigidbody2D.velocity = GetRandomDirection();
    }

    public void ResetBall()
    {
        _isStarted = false;
        MoveWithPad();
    }

    public void OnBallFall()
    {
        OnBallFell?.Invoke();
        if (_lastBallChecker.BallCount == 0)
        {
            _isStarted = false;
        }
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        Vector2 velocity = _rigidbody2D.velocity;
        float velocityMagnitude = velocity.magnitude;
        velocityMagnitude *= speedMultiplier;

        if (velocityMagnitude < _minSpeed)
        {
            velocityMagnitude = _minSpeed;
        }

        if (velocityMagnitude > _maxSpeed)
        {
            velocityMagnitude = _maxSpeed;
        }

        _rigidbody2D.velocity = velocity.normalized * velocityMagnitude;

    }

    #endregion


    #region Private methods

    private void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
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