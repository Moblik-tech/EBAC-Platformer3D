using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moblik.Cloth;

public class ClothItemPower : ClothItemBase
{
    public float damageMultiplier = 0.5f;

    public override void Collect()
    {
        base.Collect();
        PlayerController.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, duration);
    }
}