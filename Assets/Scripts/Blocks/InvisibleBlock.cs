using UnityEngine;

public class InvisibleBlock : Block
{
    #region Variables


    private bool _isVisible;

    #endregion
    
    #region Unity lifecycle

    private void Awake()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ApplyDamage();
    }

    protected internal override void ApplyDamage()
    {
        if (!_isVisible)
        {
            _spriteRenderer.enabled = true;
            _isVisible = true;
        }
        else
        {
            base.ApplyDamage();
        }
    }

    #endregion
}