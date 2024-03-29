﻿using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Pad _pad;

    [SerializeField] private float _originalSpeed;
    [SerializeField] private Sprite _originalSprite;
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

    [Header("Fire ball")]
    [SerializeField] private Sprite _fireBallSprite;
    [SerializeField] private float _explodeRadius;
    [SerializeField] private LayerMask _layerMask;
    
    [Header("Music")]
    [SerializeField] private AudioClip _originalSound;
    [SerializeField] private AudioClip _fireBallSound;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    

    private readonly Vector3 _originalSize = Vector3.one;

    private Vector2 _contactPoint;
    private float _speed;
    private bool _isStarted;
    private bool _isNewBall;
    private bool _isFireBallActive;
    private AudioClip _currentSound;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        AudioPlayer.Instance.PlaySound(_currentSound, _volume);
        
        if (_isFireBallActive && col.gameObject.CompareTag(Tags.Block))
        {
            ExplosionAffect.PerformExplode(transform.position, _explodeRadius, _layerMask);
        }
    }

    #endregion


    #region Public methods

    public void OnBallFall()
    {
        OnBallFell?.Invoke(this);
        if (BallsHandler.Instance.BallCount == 0)
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

    public void EnableFireEffect()
    {
        _isFireBallActive = true;
        _spriteRenderer.sprite = _fireBallSprite;
        _currentSound = _fireBallSound;
        _trailRenderer.enabled = true;
    }

    public void Clone(Ball ball)
    {
        _isNewBall = true;
        _isFireBallActive = ball._isFireBallActive;
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
        _isFireBallActive = false;
        _contactPoint = Vector2.zero;
        _currentSound = _originalSound;
        _trailRenderer.enabled = false;

        ResetSize();
        ResetSpeed();
        ResetSprite();
        MoveWithPad();
    }

    private void ResetSprite()
    {
        _spriteRenderer.sprite = _originalSprite;
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