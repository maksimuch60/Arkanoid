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

    #endregion


    #region Events

    public event Action OnBallFell;

    #endregion

    #region Unity lifecycle

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rigidbody2D.velocity);
    }

    #endregion


    #region Public methods

    public void StartMove()
    {
        _rigidbody2D.velocity = GetRandomDirection();
    }

    public void MoveWithPad()
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