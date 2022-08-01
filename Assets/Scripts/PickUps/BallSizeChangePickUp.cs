using UnityEngine;

public class BallSizeChangePickUp : PickUpBase
{
    [Header(nameof(BallSizeChangePickUp))]
    [SerializeField] private float _sizeMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.ChangeSize(_sizeMultiplier);
        }
    }
}