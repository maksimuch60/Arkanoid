using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    #region Variables

    [Header(nameof(PickUpBase))]
    [SerializeField] private int _pickUpScore;
    
    [Header("Music")]
    [SerializeField] private AudioClip _pickUpSound;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    
    

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(Tags.Pad))
        {
            return;
        }

        AudioPlayer.Instance.PlaySound(_pickUpSound, _volume);
        AddScore();
        ApplyEffect(col);
        Destroy(gameObject);
    }

    #endregion


    #region Private methods

    private void AddScore()
    {
        ScoreManager.Instance.ChangeScore(_pickUpScore);
    }
    protected abstract void ApplyEffect(Collision2D col);

    #endregion
}