using UnityEngine;

public class BottomWall : MonoBehaviour
{
    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            ball.OnBallFall();
        }
    }

    #endregion
}