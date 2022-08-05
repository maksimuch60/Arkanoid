using UnityEngine;

public class BallSpeedChangePickUp : PickUpBase
{
    [Header(nameof(BallSpeedChangePickUp))]
    [SerializeField] private float _speedMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        foreach (Ball ball in BallsHandler.Instance.AllBalls)
        {
            ball.ChangeSpeed(_speedMultiplier);
        }
    }
}