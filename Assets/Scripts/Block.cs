using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    #endregion
}