using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalRanger : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<Dash>(out var a))
        {
            a.CoolDown -= a.CoolDown * 0.01f * Data[0];
        }
    }
}
