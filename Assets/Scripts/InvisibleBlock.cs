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

    protected sealed override void OnCollisionEnter2D(Collision2D col)
    {
        if (!_isVisible)
        {
            _spriteRenderer.enabled = true;
            _isVisible = true;
        }
        else
        {
            base.OnCollisionEnter2D(col);
        }
    }

    #endregion
}