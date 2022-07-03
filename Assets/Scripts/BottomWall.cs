using UnityEngine;

public class BottomWall : MonoBehaviour
{
    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        ScoreManager.Instance.ResetScore();
        SceneLoader.Instance.ReloadScene();
    }

    #endregion
}