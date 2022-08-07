using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    #region Variables

    [Header(nameof(PickUpBase))]
    [SerializeField] private int _pickUpScore;

    #endregion


    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(Tags.Pad))
        {
            return;
        }

        ApplyEffect(col);
        AddScore();
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