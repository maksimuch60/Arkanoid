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
        DecrementHP();
        CheckDestruction();
        ChangeSprite();
    }

    #endregion


    #region Private methods

    private void ChangeSprite()
    {
        int index = _stateSprites.Length - _blockHP;
        if (_blockHP > 0)
        {
            _spriteRenderer.sprite = _stateSprites[index];
        }
    }

    private void DecrementHP()
    {
        _blockHP--;
    }

    private void CheckDestruction()
    {
        if (_blockHP < 1)
        {
            ScoreManager.Instance.IncrementScore(_blockScore);
            Destroy(gameObject);
        }
    }

    #endregion
}