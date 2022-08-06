using System;
using UnityEngine;

public class UnbreakableBlock : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioClip _audioClip;

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        AudioPlayer.Instance.PlaySound(_audioClip);
    }

    #endregion
}