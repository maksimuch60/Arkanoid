using System;
using UnityEngine;

class ExplosiveBlock : Block
{
    #region Variables

    [Header(nameof(ExplosiveBlock))]
    [SerializeField] private float _explodeRadius;
    [SerializeField] private LayerMask _layerMask;
    

    #endregion


    #region Unity lifecycle

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }
    

    #endregion


    #region Protected methods

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
        
        Explode();
    }

    #endregion


    #region Private methods

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explodeRadius, _layerMask);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log($"{collider.name}");
            Block blockToExplode = collider.gameObject.GetComponent<Block>();
            blockToExplode.ApplyDamage();
        }
        
    }

    #endregion
}