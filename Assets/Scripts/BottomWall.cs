using UnityEngine;

public class BottomWall : MonoBehaviour
{
    #region Unity lifecycle

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Tags.Ball))
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.OnBallFall();
        }
        else
        {
            Destroy(col.gameObject);
        }
    }

    #endregion
}