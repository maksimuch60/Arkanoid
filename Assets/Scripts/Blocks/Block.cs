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
    [SerializeField] private List<PickUps> _pickUps;
    [Range(0f, 1f)]
    [SerializeField] private float _pickUpSpawnChance;

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

    protected virtual void ApplyDamage()
    {
        DecrementHp();
        CheckDestruction();
        ChangeSprite();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }

    #endregion


    #region Private methods

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
        float pickUpChance = Random.Range(0f, 1f);
        foreach (PickUps pickup in _pickUps)
        {
            if (pickup.Chance >= pickUpChance)
            {
                Instantiate(pickup.PickUp, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    #endregion
}