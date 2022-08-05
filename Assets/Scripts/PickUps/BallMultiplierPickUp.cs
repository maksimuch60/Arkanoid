using UnityEngine;

public class BallMultiplierPickUp : PickUpBase
{
    [Header(nameof(BallMultiplierPickUp))]
    [SerializeField] private int _ballMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        foreach (Ball ball in BallsHandler.Instance.AllBalls)
        {
            for (int i = 0; i < _ballMultiplier; i++)
            {
                Ball newBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
                newBall.Clone(ball);
            }
        }
    }
}