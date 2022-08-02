using System;
using UnityEngine;

[Serializable]
public class PickUps
{
    public PickUpBase PickUp;
    [Range(0f, 1f)]
    public float Chance;

}