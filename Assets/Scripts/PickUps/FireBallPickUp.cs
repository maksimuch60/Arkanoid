using UnityEngine;

public class FireBallPickUp : PickUpBase
{
    protected override void ApplyEffect(Collision2D col)
    {
        foreach (Ball ball in BallsHandler.Instance.AllBalls)
        {
            ball.EnableFireEffect();
        }
    }
}