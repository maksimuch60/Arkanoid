using UnityEngine;

public static class ExplosionAffect
{
    public static void PerformExplode(Vector3 position, float explodeRadius, int layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, explodeRadius, layerMask);
        foreach (Collider2D collider1 in colliders)
        {
            Block block = collider1.GetComponent<Block>();
            block.ApplyDamage();
        }
    }
}