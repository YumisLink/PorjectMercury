using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStrongWonton : Item
{
    public override void GetItem()
    {
        role.Properties.Attack += role.BaseProperties.Attack * Data[0];
    }
    public override void DiscardItem()
    {
        role.Properties.Attack -= role.BaseProperties.Attack * Data[0];
    }
}
