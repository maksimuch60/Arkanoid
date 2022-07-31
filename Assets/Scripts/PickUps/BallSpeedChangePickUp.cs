using UnityEngine;

public class BallSpeedChangePickUp : PickUpBase
{
    [Header(nameof(BallSpeedChangePickUp))]
    [SerializeField] private float _speedMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.ChangeSpeed(_speedMultiplier);
        }
    }
}