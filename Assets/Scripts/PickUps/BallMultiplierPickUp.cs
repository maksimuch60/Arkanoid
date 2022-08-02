using UnityEngine;

public class BallMultiplierPickUp : PickUpBase
{
    [Header(nameof(BallMultiplierPickUp))]
    [SerializeField] private int _ballMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            for (int i = 0; i < _ballMultiplier; i++)
            {
                Instantiate(ball, ball.transform.position, Quaternion.identity);
                
            }
            //ball.MultipleBall(_ballMultiplier);
        }
    }
}