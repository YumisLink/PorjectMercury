using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalWarrior : Item
{
    public override void GetItem()
    {
        role.Properties.Attack += role.BaseProperties.Attack * Data[0] * 0.01f;
    }
    public override void DiscardItem()
    {
        role.Properties.Attack -= role.BaseProperties.Attack * Data[0] * 0.01f;
    }
}
