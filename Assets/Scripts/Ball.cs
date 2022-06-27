using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Pad _pad;
    [SerializeField] private BoxCollider2D _bottomWall;

    [SerializeField] private Vector2 _startDirection;
    

    #endregion


    #region Unity lifecycle

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rigidbody2D.velocity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider == _bottomWall)
        {
            SceneLoader.Instance.ReloadScene();
        }
    }

    #endregion


    #region Public methods

    public void StartMove()
    {
        _rigidbody2D.velocity = _startDirection;
    }

    public void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
        transform.position = currentPosition;
    }

    #endregion
}