using UnityEngine;

public class BallSizeChangePickUp : PickUpBase
{
    [Header(nameof(BallSizeChangePickUp))]
    [SerializeField] private float _sizeMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        foreach (Ball ball in LastBallChecker.Instance.AllBalls)
        {
            ball.ChangeSize(_sizeMultiplier);
        }
    }
}