using UnityEngine;

public class MagnetPickUp : PickUpBase
{
    [Header(nameof(MagnetPickUp))]
    [SerializeField] private float _time;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.MagnetToPad(_time);
        }
    }
}