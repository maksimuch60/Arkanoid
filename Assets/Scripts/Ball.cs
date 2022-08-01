using System;
using System.Collections;
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

    [Header("Ball min/max size")]
    [SerializeField] private Vector3 _maxSize;
    [SerializeField] private Vector3 _minSize;

    private LastBallChecker _lastBallChecker;
    private Vector3 _originalSize = Vector3.one;
    private bool _isStarted;
    private bool _isMagnetActive;

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_isMagnetActive && col.gameObject.CompareTag(Tags.Pad))
        {
            StopBall();
            //TODO: make stop at touch point
        }
    }

    #endregion


    #region Public methods

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

    public void MagnetToPad(float time)
    {
        _isMagnetActive = true;
        StartCoroutine(WaitForEndMagnet(time));
    }

    #endregion


    #region Private methods

    private void StartMove()
    {
        OnBallCreated?.Invoke();
        _rigidbody2D.velocity = GetRandomDirection();
    }

    private void ResetBall()
    {
        _isStarted = false;
        ResetSize();
        MoveWithPad();
    }

    private IEnumerator WaitForEndMagnet(float time)
    {
        yield return new WaitForSeconds(time);
        _isMagnetActive = false;
    }
    private void StopBall()
    {
        _isStarted = false;
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
        transform.position = currentPosition;
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }

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