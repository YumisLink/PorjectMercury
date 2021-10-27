using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalKnight : Item
{

    public override void GetItem()
    {
        role.Properties.MaxHealth += role.BaseProperties.MaxHealth * Data[0] * 0.01f;
        role.RecoverHealth(Data[0]*0.01f* role.BaseProperties.MaxHealth);
    }
    public override void DiscardItem()
    {
        role.Properties.MaxHealth -= role.BaseProperties.MaxHealth * Data[0] * 0.01f;
    }
}
