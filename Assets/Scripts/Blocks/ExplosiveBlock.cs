using System;
using UnityEngine;

class ExplosiveBlock : Block
{
    #region Variables

    [Header(nameof(ExplosiveBlock))]
    [SerializeField] private float _explodeRadius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioClip _explodeSound;
    

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
        
        AudioPlayer.Instance.PlaySound(_explodeSound);
        
        Explode();
    }

    #endregion


    #region Private methods

    private void Explode()
    {
        ExplosionAffect.PerformExplode(transform.position, _explodeRadius, _layerMask);
    }

    #endregion
}