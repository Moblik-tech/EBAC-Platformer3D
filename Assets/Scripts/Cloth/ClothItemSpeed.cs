using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moblik.Cloth;

public class ClothItemSpeed : ClothItemBase
{
    public float speedMultiplier = 2f;

    public override void Collect()
    {
        base.Collect();
        PlayerController.Instance.ChangeSpeed(speedMultiplier, duration);
    }
}