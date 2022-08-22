using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    #region Variables

    [Header("Block")]
    [Header("Values")]
    [SerializeField] private int _blockHP;
    [SerializeField] private int _blockScore;

    [Header("Components")]
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _stateSprites;

    [Header("PickUp")]
    [SerializeField] private List<PickUpInfo> _pickUps;
    [Range(0f, 1f)]
    [SerializeField] private float _pickUpSpawnChance;

    [Header("Audio")]
    [SerializeField] private AudioClip _touchSound;

    [Header("Particles")]
    [SerializeField] private GameObject _vfxPrefab;

    #endregion


    #region Events

    public static event Action OnDestroyed;
    public static event Action OnCreated;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        OnCreated?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ApplyDamage();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }

    private void OnValidate()
    {
        if (_pickUps == null || _pickUps.Count == 0)
        {
            return;
        }

        foreach (PickUpInfo pickUp in _pickUps)
        {
            pickUp.UpdateName();
        }
    }

    #endregion


    #region Private methods

    protected internal virtual void ApplyDamage()
    {
        PlaySound();

        DecrementHp();
        CheckDestruction();
        ChangeSprite();
    }

    private void PlaySound()
    {
        AudioPlayer.Instance.PlaySound(_touchSound);
    }

    private void ChangeSprite()
    {
        if (_blockHP <= 0)
            return;

        int index = _stateSprites.Length - _blockHP;
        _spriteRenderer.sprite = _stateSprites[index];
    }

    private void DecrementHp()
    {
        _blockHP--;
    }

    private void CheckDestruction()
    {
        if (_blockHP >= 1)
            return;

        ScoreManager.Instance.ChangeScore(_blockScore);
        SpawnPickUp();
        SpawnVfx();
        DestroyBlock();
    }

    private void SpawnVfx()
    {
        if (_vfxPrefab == null)
        {
            return;
        }

        Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
    }

    protected virtual void DestroyBlock()
    {
        Destroy(gameObject);
    }

    private void SpawnPickUp()
    {
        if (!IsSpawnPickUp())
        {
            return;
        }

        SpawnRandomPickUp();
    }

    private bool IsSpawnPickUp()
    {
        float random = Random.Range(0f, 1f);
        return random <= _pickUpSpawnChance;
    }

    private void SpawnRandomPickUp()
    {
        float sum = 0;
        float currentValue = 0;
        foreach (PickUpInfo pickUp in _pickUps)
        {
            sum += pickUp.Chance;
        }

        float pickUpChance = Random.Range(0f, sum);
        foreach (PickUpInfo pickup in _pickUps)
        {
            currentValue += pickup.Chance;
            if (currentValue >= pickUpChance)
            {
                Instantiate(pickup.PickUp, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    #endregion
}