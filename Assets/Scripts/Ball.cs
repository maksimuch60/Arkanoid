using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Pad _pad;

    [SerializeField] private float speed;

    private Vector2 _startDirection;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        SetupDirectionAndSpeed();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rigidbody2D.velocity);
    }

    #endregion


    #region Public methods

    public void StartMove()
    {
        _rigidbody2D.velocity = _startDirection;
    }

    public void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
        transform.position = currentPosition;
    }

    #endregion


    #region Private methods

    private void SetupDirectionAndSpeed()
    {
        _startDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.25f, 1f));
        
        _startDirection.Normalize();
        
        _startDirection *= speed;
    }

    #endregion
}