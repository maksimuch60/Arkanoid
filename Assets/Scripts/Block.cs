using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    [Header("Values")]
    [SerializeField] private int _blockHP;
    [SerializeField] private int _blockScore;

    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _stateSprites;

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        DecrementHp();
        CheckDestruction();
        ChangeSprite();
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
        
        ScoreManager.Instance.IncrementScore(_blockScore);
        Destroy(gameObject);
    }

    #endregion
}