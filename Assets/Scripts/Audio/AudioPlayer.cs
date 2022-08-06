using UnityEngine;

public class AudioPlayer : SingletonMonoBehavior<AudioPlayer>
{
    #region Variables

    [SerializeField] private AudioSource _audioSource;

    #endregion
    
    #region Public methods

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }
        
        _audioSource.PlayOneShot(audioClip);
    }

    #endregion
}