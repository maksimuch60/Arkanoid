using UnityEngine;

public class PadSizeChangePickUp : PickUpBase
{
    [Header(nameof(PadSizeChangePickUp))]
    [SerializeField] private float _sizeMultiplier;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Pad pad = FindObjectOfType<Pad>();
        pad.ChangeSize(_sizeMultiplier);
    }
}