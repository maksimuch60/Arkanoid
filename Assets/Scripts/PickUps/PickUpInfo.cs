using System;
using UnityEngine;

[Serializable]
public class PickUpInfo
{
    [HideInInspector]
    public string name;
    public PickUpBase PickUp;
    public float Chance;

    public void UpdateName()
    {
        name = PickUp == null ? String.Empty : PickUp.name;
    }

}