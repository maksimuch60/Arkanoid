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

    [Header("Random range")]
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _xMin;
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float _xMax;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _yMin;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _yMax;

    private LastBallChecker _lastBallChecker;

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
        MoveWithPad();
    }

    private void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
        currentPosition.y = padPosition.y + _offset;
        transform.position = currentPosition;
    }

    public void OnBallFall()
    {
        OnBallFell?.Invoke();
    }

    #endregion


    #region Private methods

    private Vector2 GetRandomDirection()
    {
        Vector2 startDirection = new Vector2(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax));
        startDirection.Normalize();
        startDirection *= _speed;

        return startDirection;
    }

    #endregion
}